using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;

namespace Naxam.Effects.Platform.Droid
{
    public class ViewStyleDroidEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            UpdateStyle();
        }

        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
            {
                UpdateStyle();
            }
        }

        void UpdateStyle()
        {
            var view = Control ?? Container;
            var bgColor = (Element as VisualElement)?.BackgroundColor ?? Color.Transparent;

            PaintDrawable paint = new PaintDrawable(bgColor.ToAndroid());
            GradientDrawable gradient = new GradientDrawable();
            paint.SetCornerRadius(Forms.Context.ToPixels((double)Element.GetValue(ViewEffect.CornerRadiusProperty)));
            gradient.SetCornerRadius(Forms.Context.ToPixels((double)Element.GetValue(ViewEffect.CornerRadiusProperty)));
            gradient.SetColor(Color.Transparent.ToAndroid());
            gradient.SetOrientation(GradientDrawable.Orientation.LeftRight);
            gradient.SetShape(ShapeType.Rectangle);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                view.Elevation = Forms.Context.ToPixels((double)Element.GetValue(ViewEffect.ShadowOffsetYProperty));
                view.TranslationZ = Forms.Context.ToPixels((double)Element.GetValue(ViewEffect.ShadowOffsetYProperty));
            }

            gradient.SetStroke((int)Forms.Context.ToPixels(ViewEffect.GetBorderWidth(Element)), ((Color)Element.GetValue(ViewEffect.BorderColorProperty)).ToAndroid());
            LayerDrawable layer = new LayerDrawable(
                new Drawable[]
                {
                        paint,gradient
                });
            view.SetBackground(layer);
        }

        protected override void OnDetached()
        {
        }
    }
}