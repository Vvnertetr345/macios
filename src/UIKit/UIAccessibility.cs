//
// This file describes the API that the generator will produce
//
// Authors:
//   Miguel de Icaza
//
// Copyrigh 2012-2014, Xamarin Inc.
//

using Foundation;
using ObjCRuntime;
using UIKit;
using CoreGraphics;

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

// Disable until we get around to enable + fix any issues.
#nullable disable

namespace UIKit {

	// helper enum - not part of Apple API
	public enum UIAccessibilityPostNotification {
		/// <summary>Inform the accessibility system that an announcement must be made to the user, use an NSString argument for this notification.</summary>
		Announcement,
		/// <summary>Inform the accessibility system that new UI elements have been added or removed from the screen, use an NSString argument with the information to convey the details.</summary>
		LayoutChanged,
		/// <summary>Inform the accessibility system that scrolling has completed, use an NSString argument to pass the information to be conveyed.</summary>
		PageScrolled,
		/// <summary>Inform the accessibility system that a major change to the user interface has taken place (essentially, a new screen is visible), use an NSString argument to convey the details.</summary>
		ScreenChanged,
	}

	// NSInteger -> UIAccessibilityZoom.h
	[Native]
	public enum UIAccessibilityZoomType : long {
		/// <summary>The system zoom type is the text insertion point.</summary>
		InsertionPoint,
	}

	public static partial class UIAccessibility {
		// UIAccessibility.h
		[DllImport (Constants.UIKitLibrary)]
		extern static /* BOOL */ byte UIAccessibilityIsVoiceOverRunning ();

		/// <summary>Determines whether voiceover is currently active.</summary>
		///         <value>eturns a Boolean indicating whether voiceover is enabled.
		///         </value>
		///         <remarks>
		///         </remarks>
		static public bool IsVoiceOverRunning {
			get {
				return UIAccessibilityIsVoiceOverRunning () != 0;
			}
		}

		// UIAccessibility.h
		[DllImport (Constants.UIKitLibrary)]
		extern static /* BOOL */ byte UIAccessibilityIsMonoAudioEnabled ();

		/// <summary>Determines whether the system is running with mono audio.</summary>
		///         <value>Returns a Boolean indicating whether mono audio is enabled.
		///         </value>
		///         <remarks>
		///         </remarks>
		static public bool IsMonoAudioEnabled {
			get {
				return UIAccessibilityIsMonoAudioEnabled () != 0;
			}
		}


		// UIAccessibility.h
#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		[DllImport (Constants.UIKitLibrary)]
		extern static /* NSObject */ IntPtr UIAccessibilityFocusedElement (IntPtr assistiveTechnologyIdentifier);

#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		public static NSObject FocusedElement (string assistiveTechnologyIdentifier)
		{
			using (var s = new NSString (assistiveTechnologyIdentifier))
				return Runtime.GetNSObject (UIAccessibilityFocusedElement (s.Handle));
		}

		// UIAccessibility.h
#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		[DllImport (Constants.UIKitLibrary)]
		extern static /* BOOL */ byte UIAccessibilityIsShakeToUndoEnabled ();

#if NET
		/// <summary>Whether the "shake to undo" gesture is enabled on the device.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		public static bool IsShakeToUndoEnabled {
			get {
				return UIAccessibilityIsShakeToUndoEnabled () != 0;
			}
		}

		// UIAccessibility.h
		[DllImport (Constants.UIKitLibrary)]
		extern static /* BOOL */ byte UIAccessibilityIsClosedCaptioningEnabled ();

		/// <summary>Determines whether close captioning is currently enabled.</summary>
		///         <value>Returns a Boolean indicating whether closed captioning is enabled.
		///         </value>
		///         <remarks>
		///         </remarks>
		static public bool IsClosedCaptioningEnabled {
			get {
				return UIAccessibilityIsClosedCaptioningEnabled () != 0;
			}
		}

		// UIAccessibility.h
		[DllImport (Constants.UIKitLibrary)]
		extern static /* BOOL */ byte UIAccessibilityIsInvertColorsEnabled ();

		/// <summary>Determines if the system is currently rendering with inverted colors for accessibility</summary>
		///         <value>Returns a Boolean indicating whether inverted colors are enabled.
		///         </value>
		///         <remarks>
		///         </remarks>
		static public bool IsInvertColorsEnabled {
			get {
				return UIAccessibilityIsInvertColorsEnabled () != 0;
			}
		}

		// UIAccessibility.h
		[DllImport (Constants.UIKitLibrary)]
		extern static /* BOOL */ byte UIAccessibilityIsGuidedAccessEnabled ();

		/// <summary>Determines whether guide access is currently enabled.</summary>
		///         <value>Returns a Boolean indicating whether guide access is enabled.
		///         </value>
		///         <remarks>
		///         </remarks>
		static public bool IsGuidedAccessEnabled {
			get {
				return UIAccessibilityIsGuidedAccessEnabled () != 0;
			}
		}

		// UIAccessibility.h
		[DllImport (Constants.UIKitLibrary)]
		extern static void UIAccessibilityPostNotification (/* UIAccessibilityNotifications */ int notification, /* id */ IntPtr argument);
		// typedef uint32_t UIAccessibilityNotifications

		public static void PostNotification (UIAccessibilityPostNotification notification, NSObject argument)
		{
			PostNotification (NotificationEnumToInt (notification), argument);
		}

		public static void PostNotification (int notification, NSObject argument)
		{
			UIAccessibilityPostNotification (notification, argument is null ? IntPtr.Zero : argument.Handle);
			GC.KeepAlive (argument);
		}

		static int NotificationEnumToInt (UIAccessibilityPostNotification notification)
		{
			switch (notification) {
			case UIKit.UIAccessibilityPostNotification.Announcement:
				return UIView.AnnouncementNotification;
			case UIKit.UIAccessibilityPostNotification.LayoutChanged:
				return UIView.LayoutChangedNotification;
			case UIKit.UIAccessibilityPostNotification.PageScrolled:
				return UIView.PageScrolledNotification;
			case UIKit.UIAccessibilityPostNotification.ScreenChanged:
				return UIView.ScreenChangedNotification;
			default:
				throw new ArgumentOutOfRangeException (string.Format ("Unknown UIAccessibilityPostNotification: {0}", notification.ToString ()));
			}
		}

		// UIAccessibilityZoom.h
		[DllImport (Constants.UIKitLibrary)]
		extern static void UIAccessibilityZoomFocusChanged (/* UIAccessibilityZoomType */ IntPtr type, CGRect frame, IntPtr view);

		public static void ZoomFocusChanged (UIAccessibilityZoomType type, CGRect frame, UIView view)
		{
			UIAccessibilityZoomFocusChanged ((IntPtr) type, frame, view is not null ? view.Handle : IntPtr.Zero);
			GC.KeepAlive (view);
		}

		// UIAccessibilityZoom.h
		[DllImport (Constants.UIKitLibrary, EntryPoint = "UIAccessibilityRegisterGestureConflictWithZoom")]
		extern public static void RegisterGestureConflictWithZoom ();

		// UIAccessibility.h
#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		[DllImport (Constants.UIKitLibrary)]
		extern static /* UIBezierPath* */ IntPtr UIAccessibilityConvertPathToScreenCoordinates (/* UIBezierPath* */ IntPtr path, /* UIView* */ IntPtr view);

#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		public static UIBezierPath ConvertPathToScreenCoordinates (UIBezierPath path, UIView view)
		{
			if (path is null)
				throw new ArgumentNullException ("path");
			if (view is null)
				throw new ArgumentNullException ("view");

			UIBezierPath result = new UIBezierPath (UIAccessibilityConvertPathToScreenCoordinates (path.Handle, view.Handle));
			GC.KeepAlive (path);
			GC.KeepAlive (view);
			return result;
		}

		// UIAccessibility.h
#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		[DllImport (Constants.UIKitLibrary)]
		extern static CGRect UIAccessibilityConvertFrameToScreenCoordinates (CGRect rect, /* UIView* */ IntPtr view);

#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		public static CGRect ConvertFrameToScreenCoordinates (CGRect rect, UIView view)
		{
			if (view is null)
				throw new ArgumentNullException ("view");

			var result = UIAccessibilityConvertFrameToScreenCoordinates (rect, view.Handle);
			GC.KeepAlive (view);
			return result;
		}

		// UIAccessibility.h
#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		[DllImport (Constants.UIKitLibrary)]
		extern unsafe static void UIAccessibilityRequestGuidedAccessSession (/* BOOL */ byte enable, /* void(^completionHandler)(BOOL didSucceed) */ BlockLiteral* completionHandler);

#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		[BindingImpl (BindingImplOptions.Optimizable)]
		public static void RequestGuidedAccessSession (bool enable, Action<bool> completionHandler)
		{
			unsafe {
#if NET
				delegate* unmanaged<IntPtr, byte, void> trampoline = &TrampolineRequestGuidedAccessSession;
				using var block = new BlockLiteral (trampoline, completionHandler, typeof (UIAccessibility), nameof (TrampolineRequestGuidedAccessSession));
#else
				using var block = new BlockLiteral ();
				block.SetupBlock (callback, completionHandler);
#endif
				UIAccessibilityRequestGuidedAccessSession (enable ? (byte) 1 : (byte) 0, &block);
			}
		}

#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		public static Task<bool> RequestGuidedAccessSessionAsync (bool enable)
		{
			var tcs = new TaskCompletionSource<bool> ();
			RequestGuidedAccessSession (enable, (result) => {
				tcs.SetResult (result);
			});
			return tcs.Task;
		}

#if !NET
		internal delegate void InnerRequestGuidedAccessSession (IntPtr block, byte enable);
		static readonly InnerRequestGuidedAccessSession callback = TrampolineRequestGuidedAccessSession;

		[MonoPInvokeCallback (typeof (InnerRequestGuidedAccessSession))]
#else
		[UnmanagedCallersOnly]
#endif
		static unsafe void TrampolineRequestGuidedAccessSession (IntPtr block, byte enable)
		{
			var descriptor = (BlockLiteral*) block;
			var del = (Action<bool>) (descriptor->Target);
			if (del is not null)
				del (enable != 0);
		}

#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		[DllImport (Constants.UIKitLibrary)]
		static extern byte UIAccessibilityDarkerSystemColorsEnabled ();

#if NET
		/// <summary>Determines whether darker system colors are currently enabled</summary>
		///         <value>Returns a Boolean indicating whether colors are enabled.</value>
		///         <remarks>To be added.</remarks>
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		public static bool DarkerSystemColorsEnabled {
			get {
				return UIAccessibilityDarkerSystemColorsEnabled () != 0;
			}
		}

#if !NET
		[Obsolete ("Use 'DarkerSystemColorsEnabled' instead.")]
		public static bool DarkerSystemColosEnabled {
			get {
				return UIAccessibilityDarkerSystemColorsEnabled () != 0;
			}
		}
#endif

#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		[DllImport (Constants.UIKitLibrary)]
		static extern byte UIAccessibilityIsBoldTextEnabled ();

#if NET
		/// <summary>Determines whether bold text is currently enabled</summary>
		///         <value>Returns a Boolean indicating whether bold text is enabled.</value>
		///         <remarks>To be added.</remarks>
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		public static bool IsBoldTextEnabled {
			get {
				return UIAccessibilityIsBoldTextEnabled () != 0;
			}
		}

#if NET
		[SupportedOSPlatform ("tvos14.0")]
		[SupportedOSPlatform ("ios14.0")]
		[SupportedOSPlatform ("maccatalyst")]
#else
		[TV (14, 0)]
		[iOS (14, 0)]
		[MacCatalyst (14, 0)]
#endif
		[DllImport (Constants.UIKitLibrary)]
		static extern byte UIAccessibilityButtonShapesEnabled ();

#if NET
		[SupportedOSPlatform ("tvos14.0")]
		[SupportedOSPlatform ("ios14.0")]
		[SupportedOSPlatform ("maccatalyst")]
#else
		[TV (14, 0)]
		[iOS (14, 0)]
		[MacCatalyst (14, 0)]
#endif
		public static bool ButtonShapesEnabled => UIAccessibilityButtonShapesEnabled () != 0;

#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		[DllImport (Constants.UIKitLibrary)]
		static extern byte UIAccessibilityIsGrayscaleEnabled ();

#if NET
		/// <summary>Determines whether gray scale is currently enabled</summary>
		///         <value>Returns a Boolean indicating whether gray scale is enabled.</value>
		///         <remarks>To be added.</remarks>
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		static public bool IsGrayscaleEnabled {
			get {
				return UIAccessibilityIsGrayscaleEnabled () != 0;
			}
		}

#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		[DllImport (Constants.UIKitLibrary)]
		static extern byte UIAccessibilityIsReduceMotionEnabled ();

#if NET
		/// <summary>Determines whether the system is running with reduced motion.</summary>
		///         <value>Returns a Boolean indicating whether reduced motion is enabled.</value>
		///         <remarks>To be added.</remarks>
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		static public bool IsReduceMotionEnabled {
			get {
				return UIAccessibilityIsReduceMotionEnabled () != 0;
			}
		}

#if NET
		[SupportedOSPlatform ("tvos14.0")]
		[SupportedOSPlatform ("ios14.0")]
		[SupportedOSPlatform ("maccatalyst")]
#else
		[TV (14, 0)]
		[iOS (14, 0)]
		[MacCatalyst (14, 0)]
#endif
		[DllImport (Constants.UIKitLibrary)]
		static extern byte UIAccessibilityPrefersCrossFadeTransitions ();

#if NET
		[SupportedOSPlatform ("tvos14.0")]
		[SupportedOSPlatform ("ios14.0")]
		[SupportedOSPlatform ("maccatalyst")]
#else
		[TV (14, 0)]
		[iOS (14, 0)]
		[MacCatalyst (14, 0)]
#endif
		public static bool PrefersCrossFadeTransitions => UIAccessibilityPrefersCrossFadeTransitions () != 0;

#if NET
		[SupportedOSPlatform ("ios13.0")]
		[SupportedOSPlatform ("tvos13.0")]
		[SupportedOSPlatform ("maccatalyst")]
#else
		[iOS (13, 0)]
		[TV (13, 0)]
#endif
		[DllImport (Constants.UIKitLibrary)]
		static extern byte UIAccessibilityIsVideoAutoplayEnabled ();

#if NET
		[SupportedOSPlatform ("ios13.0")]
		[SupportedOSPlatform ("tvos13.0")]
		[SupportedOSPlatform ("maccatalyst")]
#else
		[iOS (13, 0)]
		[TV (13, 0)]
#endif
		static public bool IsVideoAutoplayEnabled => UIAccessibilityIsVideoAutoplayEnabled () != 0;

#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		[DllImport (Constants.UIKitLibrary)]
		static extern byte UIAccessibilityIsReduceTransparencyEnabled ();

#if NET
		/// <summary>Determines whether the system is running with reduced transparency.</summary>
		///         <value>Returns a Boolean indicating whether reduced transparency is enabled.</value>
		///         <remarks>To be added.</remarks>
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		static public bool IsReduceTransparencyEnabled {
			get {
				return UIAccessibilityIsReduceTransparencyEnabled () != 0;
			}
		}

#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		[DllImport (Constants.UIKitLibrary)]
		static extern byte UIAccessibilityIsSwitchControlRunning ();

#if NET
		/// <summary>Determines whether the system is running with switch control enabled.</summary>
		///         <value>Returns a Boolean indicating whether switch control is enabled.</value>
		///         <remarks>To be added.</remarks>
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		static public bool IsSwitchControlRunning {
			get {
				return UIAccessibilityIsSwitchControlRunning () != 0;
			}
		}

#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		[DllImport (Constants.UIKitLibrary)]
		static extern byte UIAccessibilityIsSpeakSelectionEnabled ();

#if NET
		/// <summary>Determines whether the system is running with speak selection enabled.</summary>
		///         <value>Returns a Boolean indicating whether speak selecton is enabled.</value>
		///         <remarks>To be added.</remarks>
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		static public bool IsSpeakSelectionEnabled {
			get {
				return UIAccessibilityIsSpeakSelectionEnabled () != 0;
			}
		}

#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		[DllImport (Constants.UIKitLibrary)]
		static extern byte UIAccessibilityIsSpeakScreenEnabled ();

#if NET
		/// <summary>Determines whether the system is running with the speak screen enabled.</summary>
		///         <value>Returns a Boolean indicating whether the speak screen is enabled.</value>
		///         <remarks>To be added.</remarks>
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		static public bool IsSpeakScreenEnabled {
			get {
				return UIAccessibilityIsSpeakScreenEnabled () != 0;
			}
		}

#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("tvos")]
		[SupportedOSPlatform ("maccatalyst")]
#endif
		[DllImport (Constants.UIKitLibrary)]
		static extern byte UIAccessibilityIsAssistiveTouchRunning ();

#if NET
		/// <summary>Whether assistive touch is active.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("tvos")]
		[SupportedOSPlatform ("maccatalyst")]
#endif
		public static bool IsAssistiveTouchRunning {
			get {
				return UIAccessibilityIsAssistiveTouchRunning () != 0;
			}
		}

#if NET
		[SupportedOSPlatform ("ios13.0")]
		[SupportedOSPlatform ("tvos13.0")]
		[SupportedOSPlatform ("maccatalyst")]
#else
		[iOS (13, 0)]
		[TV (13, 0)]
#endif
		[DllImport (Constants.UIKitLibrary)]
		static extern byte UIAccessibilityShouldDifferentiateWithoutColor ();

#if NET
		[SupportedOSPlatform ("ios13.0")]
		[SupportedOSPlatform ("tvos13.0")]
		[SupportedOSPlatform ("maccatalyst")]
#else
		[iOS (13, 0)]
		[TV (13, 0)]
#endif
		public static bool ShouldDifferentiateWithoutColor => UIAccessibilityShouldDifferentiateWithoutColor () != 0;

#if NET
		[SupportedOSPlatform ("ios13.0")]
		[SupportedOSPlatform ("tvos13.0")]
		[SupportedOSPlatform ("maccatalyst")]
#else
		[iOS (13, 0)]
		[TV (13, 0)]
#endif
		[DllImport (Constants.UIKitLibrary)]
		static extern byte UIAccessibilityIsOnOffSwitchLabelsEnabled ();

#if NET
		[SupportedOSPlatform ("ios13.0")]
		[SupportedOSPlatform ("tvos13.0")]
		[SupportedOSPlatform ("maccatalyst")]
#else
		[iOS (13, 0)]
		[TV (13, 0)]
#endif
		public static bool IsOnOffSwitchLabelsEnabled => UIAccessibilityIsOnOffSwitchLabelsEnabled () != 0;

#if !TVOS
#if NET
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		[DllImport (Constants.UIKitLibrary)]
		static extern nuint UIAccessibilityHearingDevicePairedEar ();

#if NET
		/// <summary>Retrieves the status of how a hearing device is paired to one, both, or no ears.</summary>
		///         <value>To be added.</value>
		///         <remarks>To be added.</remarks>
		[SupportedOSPlatform ("ios")]
		[SupportedOSPlatform ("maccatalyst")]
		[SupportedOSPlatform ("tvos")]
#endif
		public static UIAccessibilityHearingDeviceEar HearingDevicePairedEar {
			get {
				return (UIAccessibilityHearingDeviceEar) (ulong) UIAccessibilityHearingDevicePairedEar ();
			}
		}
#endif
	}


}
