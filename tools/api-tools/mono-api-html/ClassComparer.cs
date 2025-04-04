// 
// Authors
//    Sebastien Pouliot  <sebastien@xamarin.com>
//
// Copyright 2013 Xamarin Inc. http://www.xamarin.com
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace Mono.ApiTools {

	class ClassComparer : Comparer {

		InterfaceComparer icomparer;
		ConstructorComparer ccomparer;
		FieldComparer fcomparer;
		PropertyComparer pcomparer;
		EventComparer ecomparer;
		MethodComparer mcomparer;
		ClassComparer kcomparer;

		public ClassComparer (State state)
			: base (state)
		{
			icomparer = new InterfaceComparer (state);
			ccomparer = new ConstructorComparer (state);
			fcomparer = new FieldComparer (state);
			pcomparer = new PropertyComparer (state);
			ecomparer = new EventComparer (state);
			mcomparer = new MethodComparer (state);
		}

		public override string GroupName {
			get { return "classes"; }
		}

		public override string ElementName {
			get { return "class"; }
		}

		public override void SetContext (XElement current)
		{
			State.Type = current.GetAttribute ("name");
			State.BaseType = current.GetAttribute ("base");
		}

		public void Compare (XElement source, XElement target)
		{
			var s = source.Element ("classes");
			var t = target.Element ("classes");
			if (XNode.DeepEquals (s, t))
				return;
			Compare (s.Elements ("class"), t.Elements ("class"));
		}

		public override void Added (XElement target, bool wasParentAdded)
		{
			var addedDescription = $"{State.Namespace}.{State.Type}: Added type";
			State.LogDebugMessage ($"Possible -n value: {addedDescription}");
			if (State.IgnoreNew.Any (re => re.IsMatch (addedDescription)))
				return;

			Formatter.BeginTypeAddition ();
			AddedInner (target);
			Formatter.EndTypeAddition ();
		}

		public void AddedInner (XElement target)
		{
			SetContext (target);
			if (target.IsTrue ("serializable"))
				Indent ().WriteLine ("[Serializable]");

			var type = target.Attribute ("type").Value;

			WriteAttributes (target);

			Indent ().Write ("public");

			if (type != "enum") {
				bool seal = target.IsTrue ("sealed");
				bool abst = target.IsTrue ("abstract");
				if (seal && abst)
					Output.Write (" static");
				else if (seal && type != "struct")
					Output.Write (" sealed");
				else if (abst && type != "interface")
					Output.Write (" abstract");
			}

			Output.Write (' ');
			Output.Write (type);
			Output.Write (' ');
			Output.Write (target.GetAttribute ("name"));

			var baseclass = target.GetAttribute ("base");
			if ((type != "enum") && (type != "struct")) {
				if (baseclass is not null) {
					if (baseclass == "System.Object") {
						// while true we do not need to be reminded every time...
						baseclass = null;
					} else {
						Output.Write (" : ");
						Output.Write (baseclass);
					}
				}
			}

			// interfaces on enums are "standard" not user provided - so we do not want to show them
			if (type != "enum") {
				var i = target.Element ("interfaces");
				if (i is not null) {
					var interfaces = new List<string> ();
					foreach (var iface in i.Elements ("interface"))
						interfaces.Add (icomparer.GetDescription (iface));
					Output.Write ((baseclass is null) ? " : " : ", ");
					Output.Write (String.Join (", ", interfaces));
				}
			}

			Output.WriteLine (" {");

			Formatter.IncreaseIndentation ();
			var t = target.Element ("constructors");
			if (t is not null) {
				Indent ().WriteLine ("// constructors");
				foreach (var ctor in t.Elements ("constructor"))
					ccomparer.Added (ctor, true);
			}

			t = target.Element ("fields");
			if (t is not null) {
				if (type != "enum")
					Indent ().WriteLine ("// fields");
				else
					SetContext (target);
				foreach (var field in t.Elements ("field"))
					fcomparer.Added (field, true);
			}

			t = target.Element ("properties");
			if (t is not null) {
				Indent ().WriteLine ("// properties");
				foreach (var property in t.Elements ("property"))
					pcomparer.Added (property, true);
			}

			t = target.Element ("events");
			if (t is not null) {
				Indent ().WriteLine ("// events");
				foreach (var evnt in t.Elements ("event"))
					ecomparer.Added (evnt, true);
			}

			t = target.Element ("methods");
			if (t is not null) {
				Indent ().WriteLine ("// methods");
				foreach (var method in t.Elements ("method"))
					mcomparer.Added (method, true);
			}

			t = target.Element ("classes");
			if (t is not null) {
				Output.WriteLine ();
				Indent ().WriteLine ("// inner types");
				State.Parent = State.Type;
				kcomparer = new NestedClassComparer (State);
				foreach (var inner in t.Elements ("class"))
					kcomparer.AddedInner (inner);
				State.Type = State.Parent;
			}
			Formatter.DecreaseIndentation ();
			Indent ().WriteLine ("}");
		}

		bool IsBaseChangeCompatible (string source, string target)
		{
			if (State.TargetClassHierarchyMap is null)
				State.TargetClassHierarchyMap = CreateClassHierarchyMap (State.TargetFile);

			// Changing a type hierarchy like this:
			//
			// class UIPointerStyle : NSObject {}
			// class UIHoverStyle : NSObject {}
			//
			// to
			//
			// class UIPointerStyle : UIHoverStyle {}
			// class UIHoverStyle : NSObject {}
			//
			// is considered a compatible change.
			// There are a few caveats, but we're hoping they're corner cases we won't hit:
			//	* A type can be introduced into a hierarchy between two existing types if it doesn't introduce any new abstract members or change the semantics or behavior of existing types.
			//  * https://learn.microsoft.com/en-us/dotnet/core/compatibility/library-change-rules

			var nextTarget = target;
			while (State.TargetClassHierarchyMap.TryGetValue (nextTarget, out var nextBase)) {
				if (nextBase == source)
					return true;
				nextTarget = nextBase;
			}

			return false;
		}

		// Create a map of a type and its base type.
		static Dictionary<string, string> CreateClassHierarchyMap (string xml)
		{
			var rv = new Dictionary<string, string> ();
			// We need to load the XDocument again, because the one we're processing might have been modified (things are removed as comparison progresses).
			// Also it's rare to need this class hierarchy map (base class changes aren't very common), so creating the map when the XDocuments are
			// originally loaded would incur the cost of computing the map for every api comparison, not only when we need the map.
			var document = XDocument.Load (xml);
			foreach (var assemblies in document.Elements ("assemblies")) {
				foreach (var @assembly in assemblies.Elements ("assembly")) {
					foreach (var e in assembly.Elements ("namespaces")) {
						foreach (var nsElement in e.Elements ("namespace")) {
							var ns = nsElement.GetAttribute ("name");
							foreach (var classesElement in nsElement.Elements ("classes")) {
								MapClasses (rv, ns, string.Empty, classesElement);
							}
						}
					}
				}
			}
			return rv;

			static void MapClasses (Dictionary<string, string> dictionary, string @namespace, string declaringType, XElement classes)
			{
				foreach (var classElement in classes.Elements ("class")) {
					var className = classElement.GetAttribute ("name");
					var baseType = classElement.GetAttribute ("base");
					string fullname;
					if (string.IsNullOrEmpty (@namespace)) {
						// nested type
						fullname = declaringType + "/" + className;
					} else {
						fullname = @namespace + "." + className;
					}
					dictionary.Add (fullname, baseType);
					foreach (var nestedClassesElement in classElement.Elements ("classes")) {
						MapClasses (dictionary, string.Empty, fullname, nestedClassesElement);
					}
				}
			}
		}

		public override void Modified (XElement source, XElement target, ApiChanges diff)
		{
			// hack - there could be changes that we're not monitoring (e.g. attributes properties)
			Formatter.PushOutput ();

			var attributeDiff = new ApiChange ("", State);
			RenderAttributes (source, target, attributeDiff);
			if (attributeDiff.AnyChange) {
				Formatter.BeginAttributeModification ();
				Formatter.Diff (attributeDiff);
				Formatter.EndAttributeModification ();
			}

			var sb = source.GetAttribute ("base");
			var tb = target.GetAttribute ("base");
			var rm = $"{State.Namespace}.{State.Type}: Modified base type: '{sb}' to '{tb}'";
			State.LogDebugMessage ($"Possible -r value: {rm}");
			if (sb != tb && !State.IgnoreRemoved.Any (re => re.IsMatch (rm))) {
				var isCompatible = IsBaseChangeCompatible (sb, tb);
				if (!(State.IgnoreNonbreaking && isCompatible)) {
					Formatter.BeginMemberModification ("Modified base type");
					var apichange = new ApiChange ($"{State.Namespace}.{State.Type}", State).AppendModified (sb, tb, !isCompatible);
					Formatter.Diff (apichange);
					Formatter.EndMemberModification ();
				}
			}

			ccomparer.Compare (source, target);
			icomparer.Compare (source, target);
			fcomparer.Compare (source, target);
			pcomparer.Compare (source, target);
			ecomparer.Compare (source, target);
			mcomparer.Compare (source, target);

			var si = source.Element ("classes");
			if (si is not null) {
				var ti = target.Element ("classes");
				kcomparer = new NestedClassComparer (State);
				State.Parent = State.Type;
				kcomparer.Compare (si.Elements ("class"), ti is null ? null : ti.Elements ("class"));
				State.Type = State.Parent;
			}

			foreach (var formatter in State.Formatters) {
				var s = formatter.PopOutput ();
				if (s.Length > 0) {
					SetContext (target);
					formatter.BeginTypeModification ();
					formatter.WriteLine (s);
					formatter.EndTypeModification ();
				}
			}
		}

		public override void Removed (XElement source)
		{
			string name = State.Namespace + "." + State.Type;

			var memberDescription = $"{name}: Removed type";
			State.LogDebugMessage ($"Possible -r value: {memberDescription}");
			if (State.IgnoreRemoved.Any (re => re.IsMatch (name)))
				return;

			Formatter.BeginTypeRemoval (!source.IsExperimental ());
			Formatter.EndTypeRemoval ();
		}

		public virtual string GetTypeName (XElement type)
		{
			return type.GetAttribute ("name");
		}
	}

	class NestedClassComparer : ClassComparer {

		public NestedClassComparer (State state)
			: base (state)
		{
		}

		public override void SetContext (XElement current)
		{
			State.Type = State.Parent + "." + current.GetAttribute ("name");
			State.BaseType = current.GetAttribute ("base");
		}

		public override string GetTypeName (XElement type)
		{
			return State.Parent + "." + base.GetTypeName (type);
		}
	}
}
