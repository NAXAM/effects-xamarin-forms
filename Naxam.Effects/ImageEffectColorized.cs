using System;
using System.Linq;
using Xamarin.Forms;

namespace Naxam.Effects
{
	public class ImageEffectColorized : RoutingEffect
	{
		public const string NAME = "Naxam.Effects.ImageEffectColorized";

		public Color TintColor { get; private set; }

		public ImageEffectColorized(Color color) : base(NAME)
		{
			TintColor = color;
		}
	}

	public static class ImageEffect
	{
		public static BindableProperty TintColorProperty = 
			BindableProperty.CreateAttached("TintColor",
			                                typeof(Color),
											typeof(ImageEffect),
			                                Color.Default,
			                                propertyChanged: OnTintColorPropertyPropertyChanged);

		public static Color GetTintColor(BindableObject element)
		{
			return (Color)element.GetValue(TintColorProperty);
		}

		public static void SetTintColor(BindableObject element, Color value)
		{
			element.SetValue(TintColorProperty, value);
		}


		static void OnTintColorPropertyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			AttachEffect(bindable as Image, (Color) newValue);
		}

		static void AttachEffect(Image element, Color color)
		{
			var effect = element.Effects.FirstOrDefault(x => x is ImageEffectColorized) as ImageEffectColorized;

			if (effect != null) {
				element.Effects.Remove(effect);
			}

			element.Effects.Add(new ImageEffectColorized(color));
		}

}
}
