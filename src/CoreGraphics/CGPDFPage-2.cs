// 
// CGPDFPage.cs: Implements the managed CGPDFPage
//
// Authors: Mono Team
//     
// Copyright 2009 Novell, Inc
// Copyright 2011, 2012 Xamarin Inc
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

#nullable enable

using System;
using System.Runtime.InteropServices;

using Foundation;
using ObjCRuntime;

namespace CoreGraphics {

	// untyped enum -> CGPDFPage.h
	public enum CGPDFBox {
		/// <summary>To be added.</summary>
		Media = 0,
		/// <summary>To be added.</summary>
		Crop = 1,
		/// <summary>To be added.</summary>
		Bleed = 2,
		/// <summary>To be added.</summary>
		Trim = 3,
		/// <summary>To be added.</summary>
		Art = 4,
	}

	// CGPDFPage.h
	public partial class CGPDFPage {
#if !COREBUILD
		[Preserve (Conditional = true)]
		internal CGPDFPage (NativeHandle handle, bool owns)
			: base (handle, owns)
		{
		}

		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static /* CGPDFDocumentRef */ IntPtr CGPDFPageGetDocument (/* CGPDFPageRef */ IntPtr page);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		public CGPDFDocument Document {
			get {
				return new CGPDFDocument (CGPDFPageGetDocument (Handle), false);
			}
		}

		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static /* size_t */ nint CGPDFPageGetPageNumber (/* CGPDFPageRef */ IntPtr page);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		public nint PageNumber {
			get {
				return CGPDFPageGetPageNumber (Handle);
			}
		}

		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static CGRect CGPDFPageGetBoxRect (/* CGPDFPageRef */ IntPtr page, CGPDFBox box);

		public CGRect GetBoxRect (CGPDFBox box)
		{
			return CGPDFPageGetBoxRect (Handle, box);
		}

		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static /* int */ int CGPDFPageGetRotationAngle (/* CGPDFPageRef */ IntPtr page);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		public int RotationAngle {
			get {
				return CGPDFPageGetRotationAngle (Handle);
			}
		}

		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static CGAffineTransform CGPDFPageGetDrawingTransform (/* CGPDFPageRef */ IntPtr page, CGPDFBox box, CGRect rect, int rotate, byte preserveAspectRatio);

		public CGAffineTransform GetDrawingTransform (CGPDFBox box, CGRect rect, int rotate, bool preserveAspectRatio)
		{
			return CGPDFPageGetDrawingTransform (Handle, box, rect, rotate, preserveAspectRatio.AsByte ());
		}

		[DllImport (Constants.CoreGraphicsLibrary)]
		extern static /* CGPDFDictionaryRef */ IntPtr CGPDFPageGetDictionary (/* CGPDFPageRef */ IntPtr page);

		/// <summary>To be added.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		public CGPDFDictionary Dictionary {
			get {
				return new CGPDFDictionary (CGPDFPageGetDictionary (Handle));
			}
		}
#endif // !COREBUILD
	}
}
