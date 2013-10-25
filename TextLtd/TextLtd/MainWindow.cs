
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace TextLtd
{
	public partial class MainWindow : MonoMac.AppKit.NSWindow
	{
		#region Constructors
		
		// Called when created from unmanaged code
		public MainWindow (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindow (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize ()
		{
		}
		
		#endregion

		const string ValidCharSet = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/|:=-.";

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();
			FormatButton.Activated += (object sender, EventArgs e) => {
				var str = string.Empty;
				int i = 0;
				foreach(var c in SourceTextField.StringValue) {
					if(ValidCharSet.Contains(c)) {
						str += c;
						i++;
						if(i % 60 == 0) str += Environment.NewLine;
					}
				}
				ResultTextField.StringValue = str;
			};
		}
	}
}

