//
// Unit tests for [DllImport]
//
// Authors:
//	Sebastien Pouliot <sebastien@xamarin.com>
//
// Copyright 2014-2015 Xamarin Inc. All rights reserved.
//

using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using Foundation;
using ObjCRuntime;
#if !__MACOS__
using UIKit;
#endif
using NUnit.Framework;

namespace LinkSdk {

	[TestFixture]
	[Preserve (AllMembers = true)]
	public class DllImportTest {

		class NestedFirstLevel {

			public class NestedSecondLevel {

				[DllImport ("__Internal")]
				public extern static string xamarin_get_locale_country_code ();
			}
		}

		[Test]
		public void ScanForStrip_17327 ()
		{
			// note: must be tested on a release (strip'ed) build
			Assert.NotNull (NestedFirstLevel.NestedSecondLevel.xamarin_get_locale_country_code ());
		}

		[Test]
		public void Sqlite3 ()
		{
			var lib = Dlfcn.dlopen ("/usr/lib/libsqlite3.dylib", 0);
			Assert.That (lib, Is.Not.EqualTo (IntPtr.Zero), "/usr/lib/libsqlite3.dylib");
			try {
				Assert.That (Dlfcn.dlsym (lib, "sqlite3_bind_int"), Is.Not.EqualTo (IntPtr.Zero), "sqlite3_bind_int");
				// iOS does not have some symbols defined - if that change/fail in the future we'll need to update Mono.Data.Sqlite
				// note: Apple devices (at least iOS and AppleTV) running 10.x have a more recent version of libsqlite which includes _key and _rekey
				// note 2: simulators also got the new libsqlite version with Xcode 9, and since Xcode 9 ships an ever newer sqlite version,
				// we get even more API as well.
				var hasNewerSqlite = TestRuntime.CheckXcodeVersion (9, 0);
#if __MACOS__ || __MACCATALYST__
				var hasNewSqlite = hasNewerSqlite || TestRuntime.CheckXcodeVersion (8, 0);
#else
				var hasNewSqlite = hasNewerSqlite || TestRuntime.CheckXcodeVersion (8, 0) && Runtime.Arch == Arch.DEVICE;
#endif

				var new_symbols = new string [] {
					"sqlite3_key",
					"sqlite3_rekey",
				};
				var newer_symbols = new string [] {
					"sqlite3_column_database_name",
					"sqlite3_column_database_name16",
					"sqlite3_column_origin_name",
					"sqlite3_column_origin_name16",
					"sqlite3_column_table_name",
					"sqlite3_column_table_name16",
				};

				foreach (var symbol in new_symbols)
					Assert.That (Dlfcn.dlsym (lib, symbol), hasNewSqlite ? Is.Not.EqualTo (IntPtr.Zero) : Is.EqualTo (IntPtr.Zero), symbol);

				foreach (var symbol in newer_symbols)
					Assert.That (Dlfcn.dlsym (lib, symbol), hasNewerSqlite ? Is.Not.EqualTo (IntPtr.Zero) : Is.EqualTo (IntPtr.Zero), symbol);
			} finally {
				Dlfcn.dlclose (lib);
			}
		}

		[Test]
		public void PingSend ()
		{
			var p = new Ping ();
			// support was implemented with https://github.com/dotnet/runtime/pull/52240
			var reply = p.Send ("localhost");
			Assert.That (reply.Status, Is.EqualTo (IPStatus.Success), "Pong");
		}
	}
}
