#nullable enable

using System.IO.Compression;

namespace Xamarin.Tests {
	public class PackTest : TestBaseClass {


		[Test]
		[TestCase (ApplePlatform.iOS)]
		[TestCase (ApplePlatform.MacCatalyst)]
		[TestCase (ApplePlatform.TVOS)]
		[TestCase (ApplePlatform.MacOSX)]
		public void BindingOldStyle (ApplePlatform platform)
		{
			BindingOldStyleImpl (platform);
		}

		[Test]
		[TestCase (ApplePlatform.iOS)]
		[Category ("RemoteWindows")]
		public void BindingOldStyleOnRemoteWindows (ApplePlatform platform)
		{
			BindingOldStyleImpl (platform, AddRemoteProperties ());
		}

		void BindingOldStyleImpl (ApplePlatform platform, Dictionary<string, string>? properties = null)
		{
			var project = "BindingOldStyle";
			Configuration.IgnoreIfIgnoredPlatform (platform);

			var project_path = GetProjectPath (project, platform: platform);
			Clean (project_path);

			string tmpdir;
			if (Configuration.IsBuildingRemotely) {
				tmpdir = Path.Combine ("bin", "tmp-dir");
			} else {
				tmpdir = Cache.CreateTemporaryDirectory ();
			}
			var outputPath = Path.Combine (tmpdir, "OutputPath");
			var intermediateOutputPath = Path.Combine (tmpdir, "IntermediateOutputPath");
			properties = GetDefaultProperties (extraProperties: properties);
			properties ["OutputPath"] = outputPath + Path.DirectorySeparatorChar;
			properties ["IntermediateOutputPath"] = intermediateOutputPath + Path.DirectorySeparatorChar;

			var rv = DotNet.AssertPackFailure (project_path, properties, msbuildParallelism: false);
			var errors = BinLog.GetBuildLogErrors (rv.BinLogPath).ToArray ();
			Assert.AreEqual (1, errors.Length, "Error count");
			Assert.AreEqual ($"Creating a NuGet package is not supported for projects that have ObjcBindingNativeLibrary items. Migrate to use NativeReference items instead.", errors [0].Message, "Error message");
		}

		[Test]
		[Category ("Multiplatform")]
		[TestCase (ApplePlatform.MacCatalyst, true)]
		[TestCase (ApplePlatform.MacCatalyst, false)]
		[TestCase (ApplePlatform.MacOSX, true)]
		[TestCase (ApplePlatform.MacOSX, false)]
		// The bindings-framework-test project doesn't work on iOS or tvOS.
		public void BindingFrameworksProject (ApplePlatform platform, bool noBindingEmbedding)
		{
			var project = "bindings-framework-test";
			Configuration.IgnoreIfIgnoredPlatform (platform);

			var project_path = Path.Combine (Configuration.RootPath, "tests", project, "dotnet", platform.AsString (), $"{project}.csproj");
			Clean (project_path);

			var tmpdir = Cache.CreateTemporaryDirectory ();
			var outputPath = Path.Combine (tmpdir, "OutputPath");
			var intermediateOutputPath = Path.Combine (tmpdir, "IntermediateOutputPath");
			var properties = GetDefaultProperties ();
			properties ["OutputPath"] = outputPath + Path.DirectorySeparatorChar;
			properties ["IntermediateOutputPath"] = intermediateOutputPath + Path.DirectorySeparatorChar;
			properties ["NoBindingEmbedding"] = noBindingEmbedding ? "true" : "false";

			DotNet.AssertPack (project_path, properties, msbuildParallelism: false);

			var nupkg = Path.Combine (outputPath, project + ".1.0.0.nupkg");
			Assert.That (nupkg, Does.Exist, "nupkg existence");

			var archive = ZipFile.OpenRead (nupkg);
			var files = archive.Entries.Select (v => v.FullName).ToHashSet ();
			var tfm = platform.ToFrameworkWithPlatformVersion (isExecutable: false);
			var hasSymlinks = noBindingEmbedding && (platform == ApplePlatform.MacCatalyst || platform == ApplePlatform.MacOSX);
			if (noBindingEmbedding) {
				Assert.That (archive.Entries.Count, Is.EqualTo (hasSymlinks ? 6 : 10), $"nupkg file count - {nupkg}");
			} else {
				Assert.That (archive.Entries.Count, Is.EqualTo (5), $"nupkg file count - {nupkg}");
			}
			Assert.That (files, Does.Contain (project + ".nuspec"), "nuspec");
			Assert.That (files, Does.Contain ("_rels/.rels"), ".rels");
			Assert.That (files, Does.Contain ("[Content_Types].xml"), "[Content_Types].xml");
			Assert.That (files, Does.Contain ($"lib/{tfm}/{project}.dll"), $"{project}.dll");
			Assert.That (files, Has.Some.Matches<string> (v => v.StartsWith ("package/services/metadata/core-properties/", StringComparison.Ordinal) && v.EndsWith (".psmdcp", StringComparison.Ordinal)), "psmdcp");
			if (noBindingEmbedding) {
				if (hasSymlinks) {
					Assert.That (files, Does.Contain ($"lib/{tfm}/{project}.resources.zip"), $"{project}.resources.zip");
				} else {
					Assert.That (files, Does.Contain ($"lib/{tfm}/{project}.resources/XStaticArTest.framework/XStaticArTest"), $"XStaticArTest.framework/XStaticArTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{project}.resources/XStaticObjectTest.framework/XStaticObjectTest"), $"XStaticObjectTest.framework/XStaticObjectTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{project}.resources/XTest.framework/XTest"), $"XTest.framework/XTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{project}.resources/XTest.framework/Info.plist"), $"XTest.framework/Info.plist");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{project}.resources/manifest"), $"manifest");
				}
			}
		}

		[Test]
		[Category ("Multiplatform")]
		[TestCase (ApplePlatform.iOS, true)]
		[TestCase (ApplePlatform.iOS, false)]
		[TestCase (ApplePlatform.MacCatalyst, true)]
		[TestCase (ApplePlatform.MacCatalyst, false)]
		[TestCase (ApplePlatform.TVOS, true)]
		[TestCase (ApplePlatform.TVOS, false)]
		[TestCase (ApplePlatform.MacOSX, true)]
		[TestCase (ApplePlatform.MacOSX, false)]
		public void BindingXcFrameworksProject (ApplePlatform platform, bool noBindingEmbedding)
		{
			BindingXcFrameworksProjectImpl (platform, noBindingEmbedding);
		}

		[Category ("RemoteWindows")]
		[TestCase (ApplePlatform.iOS, true)]
		[TestCase (ApplePlatform.iOS, false)]
		public void BindingXcFrameworksProjectOnRemoteWindows (ApplePlatform platform, bool noBindingEmbedding)
		{
			BindingXcFrameworksProjectImpl (platform, noBindingEmbedding, AddRemoteProperties ());
		}

		void BindingXcFrameworksProjectImpl (ApplePlatform platform, bool noBindingEmbedding, Dictionary<string, string>? properties = null)
		{
			var project = "bindings-xcframework-test";
			var assemblyName = "bindings-framework-test";

			// This tests gets really complicated if not all platforms are included,
			// because the (number of) files included in the nupkg depends not only
			// on the current platform, but on the other included platforms as well.
			// For example: if either macOS or Mac Catalyst is included, then some
			// parts of the .xcframework will be zipped differently (due to symlinks
			// in the xcframework).
			Configuration.IgnoreIfAnyIgnoredPlatforms ();

			var project_path = Path.Combine (Configuration.RootPath, "tests", project, "dotnet", platform.AsString (), $"{project}.csproj");
			Clean (project_path);

			string tmpdir;
			if (Configuration.IsBuildingRemotely) {
				tmpdir = Path.Combine ("bin", "tmp-dir");
			} else {
				tmpdir = Cache.CreateTemporaryDirectory ();
			}
			var outputPath = Path.Combine (tmpdir, "OutputPath");
			var intermediateOutputPath = Path.Combine (tmpdir, "IntermediateOutputPath");
			properties = GetDefaultProperties (extraProperties: properties);
			properties ["OutputPath"] = outputPath + Path.DirectorySeparatorChar;
			properties ["IntermediateOutputPath"] = intermediateOutputPath + Path.DirectorySeparatorChar;
			properties ["NoBindingEmbedding"] = noBindingEmbedding ? "true" : "false";

			DotNet.AssertPack (project_path, properties, msbuildParallelism: false);

			string nupkg;
			if (Configuration.IsBuildingRemotely) {
				nupkg = Path.Combine (Path.GetDirectoryName (project_path)!, outputPath, assemblyName + ".1.0.0.nupkg");
			} else {
				nupkg = Path.Combine (outputPath, assemblyName + ".1.0.0.nupkg");
			}
			Assert.That (nupkg, Does.Exist, "nupkg existence");

			using var archive = ZipFile.OpenRead (nupkg);
			var files = archive.Entries.Select (v => v.FullName).ToHashSet ();
			var tfm = platform.ToFrameworkWithPlatformVersion (isExecutable: false);
			int archiveCount;
			if (noBindingEmbedding) {
				if (Configuration.IsBuildingRemotely) {
					// When building remotely, the xcframework is originally on Windows. This means any symlinks won't be symlinks anymore,
					// and the xcframework isn't compressed into a zip file before storing in the nupkg. This also means the nupkg won't
					// work on macOS/Mac Catalyst (because those are the frameworks with symlinks), but this doesn't seem like a big issue
					// at the moment (never heard of a customer running into this), so for now we can probably just ignore this scenario.
					// A possibility for the future, would be to detect this and show warning/error telling people to build from macOS
					// instead of remotely.
					archiveCount = 39;
				} else {
					archiveCount = 6;
				}
			} else {
				archiveCount = 5;
			}
			Assert.That (archive.Entries.Count, Is.EqualTo (archiveCount), $"nupkg file count - {nupkg}:\n\t{string.Join ("\n\t", archive.Entries.Select (v => v.FullName))}");
			Assert.That (files, Does.Contain (assemblyName + ".nuspec"), "nuspec");
			Assert.That (files, Does.Contain ("_rels/.rels"), ".rels");
			Assert.That (files, Does.Contain ("[Content_Types].xml"), "[Content_Types].xml");
			Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.dll"), $"{assemblyName}.dll");
			Assert.That (files, Has.Some.Matches<string> (v => v.StartsWith ("package/services/metadata/core-properties/", StringComparison.Ordinal) && v.EndsWith (".psmdcp", StringComparison.Ordinal)), "psmdcp");
			if (noBindingEmbedding) {
				if (Configuration.IsBuildingRemotely) {
					Assert.That (files, Does.Not.Contain ($"lib/{tfm}/{assemblyName}.resources.zip"), $"{assemblyName}.resources.zip");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/manifest"), $"{assemblyName}.resources/manifest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticArTest.xcframework/Info.plist"), $"{assemblyName}.resources/XStaticArTest.xcframework/Info.plist");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticArTest.xcframework/ios-arm64/XStaticArTest.framework/XStaticArTest"), $"{assemblyName}.resources/XStaticArTest.xcframework/ios-arm64/XStaticArTest.framework/XStaticArTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticArTest.xcframework/ios-arm64_x86_64-maccatalyst/XStaticArTest.framework/XStaticArTest"), $"{assemblyName}.resources/XStaticArTest.xcframework/ios-arm64_x86_64-maccatalyst/XStaticArTest.framework/XStaticArTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticArTest.xcframework/ios-arm64_x86_64-simulator/XStaticArTest.framework/XStaticArTest"), $"{assemblyName}.resources/XStaticArTest.xcframework/ios-arm64_x86_64-simulator/XStaticArTest.framework/XStaticArTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticArTest.xcframework/macos-arm64_x86_64/XStaticArTest.framework/XStaticArTest"), $"{assemblyName}.resources/XStaticArTest.xcframework/macos-arm64_x86_64/XStaticArTest.framework/XStaticArTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticArTest.xcframework/tvos-arm64/XStaticArTest.framework/XStaticArTest"), $"{assemblyName}.resources/XStaticArTest.xcframework/tvos-arm64/XStaticArTest.framework/XStaticArTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticArTest.xcframework/tvos-arm64_x86_64-simulator/XStaticArTest.framework/XStaticArTest"), $"{assemblyName}.resources/XStaticArTest.xcframework/tvos-arm64_x86_64-simulator/XStaticArTest.framework/XStaticArTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticObjectTest.xcframework/Info.plist"), $"{assemblyName}.resources/XStaticObjectTest.xcframework/Info.plist");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticObjectTest.xcframework/ios-arm64/XStaticObjectTest.framework/XStaticObjectTest"), $"{assemblyName}.resources/XStaticObjectTest.xcframework/ios-arm64/XStaticObjectTest.framework/XStaticObjectTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticObjectTest.xcframework/ios-arm64_x86_64-maccatalyst/XStaticObjectTest.framework/XStaticObjectTest"), $"{assemblyName}.resources/XStaticObjectTest.xcframework/ios-arm64_x86_64-maccatalyst/XStaticObjectTest.framework/XStaticObjectTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticObjectTest.xcframework/ios-arm64_x86_64-simulator/XStaticObjectTest.framework/XStaticObjectTest"), $"{assemblyName}.resources/XStaticObjectTest.xcframework/ios-arm64_x86_64-simulator/XStaticObjectTest.framework/XStaticObjectTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticObjectTest.xcframework/macos-arm64_x86_64/XStaticObjectTest.framework/XStaticObjectTest"), $"{assemblyName}.resources/XStaticObjectTest.xcframework/macos-arm64_x86_64/XStaticObjectTest.framework/XStaticObjectTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticObjectTest.xcframework/tvos-arm64/XStaticObjectTest.framework/XStaticObjectTest"), $"{assemblyName}.resources/XStaticObjectTest.xcframework/tvos-arm64/XStaticObjectTest.framework/XStaticObjectTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticObjectTest.xcframework/tvos-arm64_x86_64-simulator/XStaticObjectTest.framework/XStaticObjectTest"), $"{assemblyName}.resources/XStaticObjectTest.xcframework/tvos-arm64_x86_64-simulator/XStaticObjectTest.framework/XStaticObjectTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/Info.plist"), $"{assemblyName}.resources/XTest.xcframework/Info.plist");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/ios-arm64/XTest.framework/Info.plist"), $"{assemblyName}.resources/XTest.xcframework/ios-arm64/XTest.framework/Info.plist");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/ios-arm64/XTest.framework/XTest"), $"{assemblyName}.resources/XTest.xcframework/ios-arm64/XTest.framework/XTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/ios-arm64_x86_64-maccatalyst/XTest.framework/Resources"), $"{assemblyName}.resources/XTest.xcframework/ios-arm64_x86_64-maccatalyst/XTest.framework/Resources");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/ios-arm64_x86_64-maccatalyst/XTest.framework/XTest"), $"{assemblyName}.resources/XTest.xcframework/ios-arm64_x86_64-maccatalyst/XTest.framework/XTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/ios-arm64_x86_64-maccatalyst/XTest.framework/Versions/A/Resources/Info.plist"), $"{assemblyName}.resources/XTest.xcframework/ios-arm64_x86_64-maccatalyst/XTest.framework/Versions/A/Resources/Info.plist");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/ios-arm64_x86_64-maccatalyst/XTest.framework/Versions/A/XTest"), $"{assemblyName}.resources/XTest.xcframework/ios-arm64_x86_64-maccatalyst/XTest.framework/Versions/A/XTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/ios-arm64_x86_64-maccatalyst/XTest.framework/Versions/Current"), $"{assemblyName}.resources/XTest.xcframework/ios-arm64_x86_64-maccatalyst/XTest.framework/Versions/Current");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/ios-arm64_x86_64-simulator/XTest.framework/Info.plist"), $"{assemblyName}.resources/XTest.xcframework/ios-arm64_x86_64-simulator/XTest.framework/Info.plist");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/ios-arm64_x86_64-simulator/XTest.framework/XTest"), $"{assemblyName}.resources/XTest.xcframework/ios-arm64_x86_64-simulator/XTest.framework/XTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/macos-arm64_x86_64/XTest.framework/Resources"), $"{assemblyName}.resources/XTest.xcframework/macos-arm64_x86_64/XTest.framework/Resources");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/macos-arm64_x86_64/XTest.framework/XTest"), $"{assemblyName}.resources/XTest.xcframework/macos-arm64_x86_64/XTest.framework/XTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/macos-arm64_x86_64/XTest.framework/Versions/A/Resources/Info.plist"), $"{assemblyName}.resources/XTest.xcframework/macos-arm64_x86_64/XTest.framework/Versions/A/Resources/Info.plist");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/macos-arm64_x86_64/XTest.framework/Versions/A/XTest"), $"{assemblyName}.resources/XTest.xcframework/macos-arm64_x86_64/XTest.framework/Versions/A/XTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/macos-arm64_x86_64/XTest.framework/Versions/Current"), $"{assemblyName}.resources/XTest.xcframework/macos-arm64_x86_64/XTest.framework/Versions/Current");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/tvos-arm64/XTest.framework/Info.plist"), $"{assemblyName}.resources/XTest.xcframework/tvos-arm64/XTest.framework/Info.plist");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/tvos-arm64/XTest.framework/XTest"), $"{assemblyName}.resources/XTest.xcframework/tvos-arm64/XTest.framework/XTest");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/tvos-arm64_x86_64-simulator/XTest.framework/Info.plist"), $"{assemblyName}.resources/XTest.xcframework/tvos-arm64_x86_64-simulator/XTest.framework/Info.plist");
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework/tvos-arm64_x86_64-simulator/XTest.framework/XTest"), $"{assemblyName}.resources/XTest.xcframework/tvos-arm64_x86_64-simulator/XTest.framework/XTest");
				} else {
					Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources.zip"), $"{assemblyName}.resources.zip");
				}
			}
		}

		[Test]
		[TestCase (ApplePlatform.iOS, true)]
		[TestCase (ApplePlatform.iOS, false)]
		[TestCase (ApplePlatform.MacCatalyst, true)]
		[TestCase (ApplePlatform.MacCatalyst, false)]
		[TestCase (ApplePlatform.TVOS, true)]
		[TestCase (ApplePlatform.TVOS, false)]
		[TestCase (ApplePlatform.MacOSX, true)]
		[TestCase (ApplePlatform.MacOSX, false)]
		public void BindingCompressedXcFrameworksProject (ApplePlatform platform, bool compressed)
		{
			var project = "BindingWithCompressedXCFramework";
			var assemblyName = project;
			var configuration = "Release";
			Configuration.IgnoreIfIgnoredPlatform (platform);
			var project_path = GetProjectPath (project, runtimeIdentifiers: string.Empty, platform: platform, out var appPath, configuration: configuration);
			Clean (project_path);

			var tmpdir = Cache.CreateTemporaryDirectory ();
			var outputPath = Path.Combine (tmpdir, "OutputPath");
			var intermediateOutputPath = Path.Combine (tmpdir, "IntermediateOutputPath");
			var properties = GetDefaultProperties ();
			properties ["OutputPath"] = outputPath + Path.DirectorySeparatorChar;
			properties ["IntermediateOutputPath"] = intermediateOutputPath + Path.DirectorySeparatorChar;
			properties ["CompressBindingResourcePackage"] = compressed ? "true" : "false";

			DotNet.AssertPack (project_path, properties, msbuildParallelism: false);

			var nupkg = Path.Combine (outputPath, assemblyName + ".1.0.0.nupkg");
			Assert.That (nupkg, Does.Exist, "nupkg existence");

			var archive = ZipFile.OpenRead (nupkg);
			var files = archive.Entries.Select (v => v.FullName).ToHashSet ();
			var tfm = platform.ToFrameworkWithPlatformVersion (isExecutable: false);
			Assert.AreEqual (compressed ? 6 : 9, archive.Entries.Count, $"nupkg file count - {nupkg}");
			Assert.That (files, Does.Contain (assemblyName + ".nuspec"), "nuspec");
			Assert.That (files, Does.Contain ("_rels/.rels"), ".rels");
			Assert.That (files, Does.Contain ("[Content_Types].xml"), "[Content_Types].xml");
			Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.dll"), $"{assemblyName}.dll");
			Assert.That (files, Has.Some.Matches<string> (v => v.StartsWith ("package/services/metadata/core-properties/", StringComparison.Ordinal) && v.EndsWith (".psmdcp", StringComparison.Ordinal)), "psmdcp");
			string? manifest;
			if (compressed) {
				var resourcesZip = $"lib/{tfm}/{assemblyName}.resources.zip";
				Assert.That (files, Does.Contain (resourcesZip), $"{assemblyName}.resources.zip");
				var innerZip = ZipHelpers.ListInnerZip (nupkg, resourcesZip);
				var innerZipContents = new string [] {
					"manifest",
					"XTest.xcframework.zip",
					"XStaticArTest.xcframework.zip",
					"XStaticObjectTest.xcframework.zip",
				};
				CollectionAssert.AreEqual (innerZipContents.OrderBy (v => v), innerZip.OrderBy (v => v), "Inner zip");
				manifest = ZipHelpers.GetInnerString (nupkg, resourcesZip, "manifest");
			} else {
				Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/manifest"), $"manifest");
				Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XTest.xcframework.zip"), $"XTest.xcframework.zip");
				Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticArTest.xcframework.zip"), $"XStaticArTest.xcframework.zip");
				Assert.That (files, Does.Contain ($"lib/{tfm}/{assemblyName}.resources/XStaticObjectTest.xcframework.zip"), $"XStaticObjectTest.xcframework.zip");
				manifest = ZipHelpers.GetString (nupkg, $"lib/{tfm}/{assemblyName}.resources/manifest");
			}
			var expectedManifest = $"""
			<BindingAssembly>
				<NativeReference Name="XTest.xcframework.zip">
					<ForceLoad></ForceLoad>
					<Frameworks></Frameworks>
					<IdentityWithoutPathSeparatorSuffix>../../../test-libraries/.libs/XTest.xcframework.zip</IdentityWithoutPathSeparatorSuffix>
					<IsCxx></IsCxx>
					<Kind>Framework</Kind>
					<LinkerFlags></LinkerFlags>
					<LinkWithSwiftSystemLibraries></LinkWithSwiftSystemLibraries>
					<NeedsGccExceptionHandling></NeedsGccExceptionHandling>
					<SmartLink></SmartLink>
					<WeakFrameworks></WeakFrameworks>
				</NativeReference>
				<NativeReference Name="XStaticArTest.xcframework.zip">
					<ForceLoad></ForceLoad>
					<Frameworks></Frameworks>
					<IdentityWithoutPathSeparatorSuffix>../../../test-libraries/.libs/XStaticArTest.xcframework.zip</IdentityWithoutPathSeparatorSuffix>
					<IsCxx></IsCxx>
					<Kind>Static</Kind>
					<LinkerFlags></LinkerFlags>
					<LinkWithSwiftSystemLibraries></LinkWithSwiftSystemLibraries>
					<NeedsGccExceptionHandling></NeedsGccExceptionHandling>
					<SmartLink></SmartLink>
					<WeakFrameworks></WeakFrameworks>
				</NativeReference>
				<NativeReference Name="XStaticObjectTest.xcframework.zip">
					<ForceLoad></ForceLoad>
					<Frameworks></Frameworks>
					<IdentityWithoutPathSeparatorSuffix>../../../test-libraries/.libs/XStaticObjectTest.xcframework.zip</IdentityWithoutPathSeparatorSuffix>
					<IsCxx></IsCxx>
					<Kind>Static</Kind>
					<LinkerFlags></LinkerFlags>
					<LinkWithSwiftSystemLibraries></LinkWithSwiftSystemLibraries>
					<NeedsGccExceptionHandling></NeedsGccExceptionHandling>
					<SmartLink></SmartLink>
					<WeakFrameworks></WeakFrameworks>
				</NativeReference>
			</BindingAssembly>
			""";
			Assert.AreEqual (expectedManifest, manifest, "manifest contents");
		}

		[Test]
		[TestCase (ApplePlatform.iOS)]
		[TestCase (ApplePlatform.MacCatalyst)]
		[TestCase (ApplePlatform.TVOS)]
		[TestCase (ApplePlatform.MacOSX)]
		public void LibraryProject (ApplePlatform platform)
		{
			LibraryProjectImpl (platform);
		}

		[TestCase (ApplePlatform.iOS)]
		[Category ("RemoteWindows")]
		public void LibraryProjectOnRemoteWindows (ApplePlatform platform)
		{
			LibraryProjectImpl (platform, AddRemoteProperties ());
		}

		void LibraryProjectImpl (ApplePlatform platform, Dictionary<string, string>? properties = null)
		{
			var project = "MyClassLibrary";
			var configuration = "Release";
			Configuration.IgnoreIfIgnoredPlatform (platform);

			var project_path = GetProjectPath (project, platform: platform);
			Clean (project_path);
			properties = GetDefaultProperties (extraProperties: properties);

			DotNet.AssertPack (project_path, properties);

			var nupkg = Path.Combine (Path.GetDirectoryName (project_path)!, "bin", configuration, project + ".1.0.0.nupkg");
			Assert.That (nupkg, Does.Exist, "nupkg existence");

			var archive = ZipFile.OpenRead (nupkg);
			var files = archive.Entries.Select (v => v.FullName).ToHashSet ();
			Assert.That (archive.Entries.Count, Is.EqualTo (5), "nupkg file count");
			Assert.That (files, Does.Contain (project + ".nuspec"), "nuspec");
			Assert.That (files, Does.Contain ("_rels/.rels"), ".rels");
			Assert.That (files, Does.Contain ("[Content_Types].xml"), "[Content_Types].xml");
			Assert.That (files, Does.Contain ($"lib/{platform.ToFrameworkWithPlatformVersion (isExecutable: false)}/{project}.dll"), $"{project}.dll");
			Assert.That (files, Has.Some.Matches<string> (v => v.StartsWith ("package/services/metadata/core-properties/", StringComparison.Ordinal) && v.EndsWith (".psmdcp", StringComparison.Ordinal)), "psmdcp");
		}

		[Test]
		[TestCase (ApplePlatform.MacCatalyst)]
		[TestCase (ApplePlatform.iOS)]
		[TestCase (ApplePlatform.TVOS)]
		[TestCase (ApplePlatform.MacOSX)]
		public void MultiTargetLibraryProject (ApplePlatform platform)
		{
			Configuration.IgnoreIfIgnoredPlatform (platform);

			// Get all the supported API versions
			var supportedApiVersion = Configuration.GetVariableArray ($"SUPPORTED_API_VERSIONS_{platform.AsString ().ToUpperInvariant ()}");
			supportedApiVersion = DotNetProjectTest.RemovePostCurrentOnMacCatalyst (supportedApiVersion, platform);
			var targetFrameworks = string.Join (";", supportedApiVersion.Select (v => v.Replace ("-", "-" + platform.AsString ().ToLowerInvariant ())));

			var project = "MultiTargetingLibrary";
			var configuration = "Release";
			var project_path = GetProjectPath (project, platform: platform);
			Clean (project_path);

			var properties = GetDefaultProperties ();
			properties ["cmdline:AllTheTargetFrameworks"] = targetFrameworks;

			DotNet.AssertPack (project_path, properties);

			var nupkg = Path.Combine (Path.GetDirectoryName (project_path)!, "bin", configuration, project + ".1.0.0.nupkg");
			Assert.That (nupkg, Does.Exist, "nupkg existence");

			var archive = ZipFile.OpenRead (nupkg);
			var files = archive.Entries.Select (v => v.FullName).ToHashSet ();
			Assert.That (archive.Entries.Count, Is.EqualTo (4 + supportedApiVersion.Count), "nupkg file count");
			Assert.That (files, Does.Contain (project + ".nuspec"), "nuspec");
			Assert.That (files, Does.Contain ("_rels/.rels"), ".rels");
			Assert.That (files, Does.Contain ("[Content_Types].xml"), "[Content_Types].xml");
			foreach (var sav in supportedApiVersion) {
				Assert.That (files, Does.Contain ($"lib/{sav.Replace ("-", "-" + platform.AsString ().ToLowerInvariant ())}/{project}.dll"), $"{project}.dll");
			}
			Assert.That (files, Has.Some.Matches<string> (v => v.StartsWith ("package/services/metadata/core-properties/", StringComparison.Ordinal) && v.EndsWith (".psmdcp", StringComparison.Ordinal)), "psmdcp");
		}
	}
}
