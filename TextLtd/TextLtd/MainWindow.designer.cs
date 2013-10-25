// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoMac.Foundation;

namespace TextLtd
{
	[Register ("MainWindow")]
	partial class MainWindow
	{
		[Outlet]
		MonoMac.AppKit.NSButton FormatButton { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField SourceTextField { get; set; }

		[Outlet]
		MonoMac.AppKit.NSTextField ResultTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (FormatButton != null) {
				FormatButton.Dispose ();
				FormatButton = null;
			}

			if (SourceTextField != null) {
				SourceTextField.Dispose ();
				SourceTextField = null;
			}

			if (ResultTextField != null) {
				ResultTextField.Dispose ();
				ResultTextField = null;
			}
		}
	}

	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		
		void ReleaseDesignerOutlets ()
		{
		}
	}
}
