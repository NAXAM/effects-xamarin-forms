using System.Linq;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Naxam.Effects.Platform.Droid
{
	public class ImageEffectColorized : PlatformEffect
	{
		protected override void OnAttached()
		{
			UpdateColor();
		}

		protected override void OnDetached()
		{
		}

		protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
		{
			base.OnElementPropertyChanged(args);

			if (Element is Image && args.PropertyName == Image.SourceProperty.PropertyName)
			{
				UpdateColor();

				return;
			}
		}

		private void UpdateColor()
		{
			var effect = Element.Effects.FirstOrDefault(x => x is Effects.ImageEffectColorized) as Effects.ImageEffectColorized;
			var color = effect?.TintColor.ToAndroid();

			var imageView = Control as ImageView;

			if (imageView != null && color.HasValue)
			{
				Android.Graphics.Color tint = color.Value;

				imageView.SetColorFilter(tint);
			}
		}
	}
}
