using System;
using System.Linq;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Naxam.Effects.Platform.Droid
{
	public class EntryEffectNoBorder : PlatformEffect
	{
		Drawable defaultBackground;
		protected override void OnAttached()
		{
			var control = Control as EntryEditText;

			if (control == null) return;

			defaultBackground = control.Background;

			var effect = Element.Effects.FirstOrDefault(x => x is Naxam.Effects.EntryEffectNoBorder);

			if (effect == null) return;

			control.SetBackgroundColor(Android.Graphics.Color.Transparent);
		}

		protected override void OnDetached()
		{
			var control = Control as EditorEditText;

			if (control == null) return;

			control.SetBackground(defaultBackground);
		}
	}
}
