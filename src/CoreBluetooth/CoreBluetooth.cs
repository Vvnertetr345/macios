using System;

using Foundation;
using CoreFoundation;

#nullable enable

namespace CoreBluetooth {

	// The init ctor is invalid, but to present a nicer API (the delegate is optional/
	// hidden if events are desired) we fake it and provide a null delegate. This
	// is intentional and should not be obsoleted like the others below.
	public partial class CBCentralManager {

		public CBCentralManager () : this (new _CBCentralManagerDelegate (), null)
		{
		}

		public CBCentralManager (DispatchQueue dispatchQueue) : this (new _CBCentralManagerDelegate (), dispatchQueue)
		{
		}
	}
}
