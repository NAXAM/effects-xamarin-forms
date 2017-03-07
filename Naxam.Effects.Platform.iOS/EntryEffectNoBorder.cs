using System;
using System.Linq;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace Naxam.Effects.Platform.iOS
{
	public class EntryEffectNoBorder : PlatformEffect
	{
		UITextBorderStyle defaultBorderStyle;

		protected override void OnAttached()
		{
			var control = Control as UITextField;

			if (control == null) return;

			defaultBorderStyle = control.BorderStyle;

			var effect = Element.Effects.FirstOrDefault(x => x is Naxam.Effects.EntryEffectNoBorder);

			if (effect == null) return;

			control.BorderStyle = UITextBorderStyle.None;
		}

		protected override void OnDetached()
		{
			var control = Control as UITextField;

			if (control == null) return;

			control.BorderStyle = defaultBorderStyle;
		}
	}
}
