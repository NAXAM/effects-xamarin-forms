using System;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Naxam.Effects.Platform.iOS
{
	public class ImageEffectColorized : PlatformEffect
	{
		protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
		{
			base.OnElementPropertyChanged(args);

			if (Element is Image && args.PropertyName == Image.SourceProperty.PropertyName)
			{
				UpdateColor();

				return;
			}
		}

		protected override void OnAttached()
		{
			UpdateColor();
		}

		protected override void OnDetached()
		{
            var imageView = Control as UIImageView;
            if (imageView != null && imageView.Image != null)
            {
                imageView.Image = imageView.Image.ImageWithRenderingMode(UIImageRenderingMode.Automatic);
            }
		}

		void UpdateColor()
		{
			var effect = Element.Effects.FirstOrDefault(x => x is Effects.ImageEffectColorized) as Effects.ImageEffectColorized;
			var color = effect?.TintColor.ToUIColor();
			var imageView = Control as UIImageView;

			if (imageView != null && color != null)
			{
				imageView.Image = imageView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
			}

			Control.TintColor = color;
		}
	}
}
