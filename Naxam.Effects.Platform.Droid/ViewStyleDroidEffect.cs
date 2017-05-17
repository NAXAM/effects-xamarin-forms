using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Android.Content.Res;

namespace Naxam.Effects.Platform.Droid
{
    public class ViewStyleDroidEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            UpdateStyle();
        }
        void UpdateStyle()
        {
            try
            {
                var view = Control ?? Container;

                PaintDrawable paint = new PaintDrawable(Color.Transparent.ToAndroid());
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
            catch { }
        }

        protected override void OnDetached()
        {
            // throw new NotImplementedException();
        }
        //protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        //{
        //    base.OnElementPropertyChanged(args);
        //    var view = Control ?? Container;
        //    if (args.PropertyName == "Height")
        //    {

        //        var newHeight = (int)Forms.Context.ToPixels((double)Element.GetValue(VisualElement.HeightProperty));
        //        view.Layout(0, 0, view.Width, newHeight);
        //        UpdateStyle();
        //    }
        //    else if (args.PropertyName == "Width")
        //    {
        //        var newWeight = (int)Forms.Context.ToPixels((double)Element.GetValue(VisualElement.WidthProperty));
        //        view.Layout(0, 0, newWeight, view.Height);
        //        UpdateStyle();
        //    }
        //}
    }
}