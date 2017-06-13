using System;
using System.Linq;
using Xamarin.Forms;

namespace Naxam.Effects
{
    public class ViewStyleEffect : RoutingEffect
    {
        const string NAME = "Naxam.Effects.ViewStyleEffect";
        public ViewStyleEffect() : base(NAME)
        {

        }
    }

    public static class ViewEffect
    {
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.CreateAttached(
                    "BorderWidth",
                    typeof(double),
            typeof(ViewEffect),
                    default(double),
            BindingMode.TwoWay, propertyChanged: AttachEffect);

        public static double GetBorderWidth(BindableObject element)
        {

            return (double)element.GetValue(BorderWidthProperty);
        }

        public static void SetBorderWidth(BindableObject element, double value)
        {
            element.SetValue(BorderWidthProperty, value);
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.CreateAttached(
                    "BorderColor",
            typeof(Color),
            typeof(ViewEffect),
            Color.Transparent,
            BindingMode.TwoWay, propertyChanged: AttachEffect);

        public static Color GetBorderColor(BindableObject element)
        {

            return (Color)element.GetValue(BorderColorProperty);
        }

        public static void SetBorderColor(BindableObject element, Color value)
        {
            element.SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.CreateAttached(
                    "CornerRadius",
                    typeof(double),
            typeof(ViewEffect),
                    default(double),
            BindingMode.TwoWay, propertyChanged: AttachEffect);

        public static double GetCornerRadius(BindableObject element)
        {

            return (double)element.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(BindableObject element, double value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty ShadowRadiusProperty = BindableProperty.CreateAttached(
                    "ShadowRadius",
                    typeof(double),
            typeof(ViewEffect),
                    default(double),
            BindingMode.TwoWay, propertyChanged: AttachEffect);

        public static double GetShadowRadius(BindableObject element)
        {

            return (double)element.GetValue(ShadowRadiusProperty);
        }

        public static void SetShadowRadius(BindableObject element, double value)
        {
            element.SetValue(ShadowRadiusProperty, value);
        }

        public static readonly BindableProperty ShadowColorProperty = BindableProperty.CreateAttached(
                    "ShadowColor",
            typeof(Color),
            typeof(ViewEffect),
            Color.Black,
            BindingMode.TwoWay, propertyChanged: AttachEffect);

        public static Color GetShadowColor(BindableObject element)
        {

            return (Color)element.GetValue(ShadowColorProperty);
        }

        public static void SetShadowColor(BindableObject element, Color value)
        {
            element.SetValue(ShadowColorProperty, value);
        }

        public static readonly BindableProperty ShadowOpacityProperty = BindableProperty.CreateAttached(
                   "ShadowOpacity",
            typeof(float),
           typeof(ViewEffect),
            default(float),
           BindingMode.TwoWay, propertyChanged: AttachEffect);

        public static float GetShadowOpacity(BindableObject element)
        {

            return (float)element.GetValue(ShadowOpacityProperty);
        }

        public static void SetShadowOpacity(BindableObject element, float value)
        {
            element.SetValue(ShadowOpacityProperty, value);
        }

        public static readonly BindableProperty ShadowOffsetXProperty = BindableProperty.CreateAttached(
            "ShadowOffsetX",
            typeof(double),
            typeof(ViewEffect),
            default(double),
            BindingMode.TwoWay,
            propertyChanged: AttachEffect
        );

        public static double GetShadowOffsetX(BindableObject element)
        {
            return (double)element.GetValue(ShadowOffsetXProperty);
        }

        public static void SetShadowOffsetX(BindableObject element, double value)
        {
            element.SetValue(ShadowOffsetXProperty, value);
        }

        public static readonly BindableProperty ShadowOffsetYProperty = BindableProperty.CreateAttached(
            "ShadowOffsetY",
            typeof(double),
            typeof(ViewEffect),
            default(double),
            BindingMode.TwoWay,
            propertyChanged: AttachEffect
        );

        public static double GetShadowOffsetY(BindableObject element)
        {
            return (double)element.GetValue(ShadowOffsetYProperty);
        }

        public static void SetShadowOffsetY(BindableObject element, double value)
        {
            element.SetValue(ShadowOffsetYProperty, value);
        }

        static void AttachEffect(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;
            if (view == null) return;

            var effect = view.Effects.FirstOrDefault(x => x is ViewStyleEffect) as ViewStyleEffect;

            //if (effect != null)
            //{
            //    view.Effects.Remove(effect);
            //}

            //view.Effects.Add(new ViewStyleEffect());
            if (effect == null)
            {
                view.Effects.Add(new ViewStyleEffect());
            }
        }

    }
}
