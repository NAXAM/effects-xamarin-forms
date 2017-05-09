using System;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Naxam.Effects.Platform.iOS
{
    public class ViewStyleEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                UpdateStyle();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Cannot set property on attached control. Error: " + ex.Message);
            }
        }

        bool _DefaultMaskToBounds { get; set; }
        CALayer _BorderLayer { get; set; } //Shadow & corner radius doesn't play nice with each other
        UIColor _DefaultBackgroundColor { get; set; }
        void UpdateStyle()
        {
            var view = Control ?? Container;
            _DefaultMaskToBounds = view.Layer.MasksToBounds;
            _DefaultBackgroundColor = view.BackgroundColor;

            if (_BorderLayer == null)
            {
                _BorderLayer = new CALayer();
                view.Layer.AddSublayer(_BorderLayer);

            }
            _BorderLayer.Frame = new CGRect(CGPoint.Empty, view.Layer.Frame.Size);
            _BorderLayer.BorderWidth = (nfloat)ViewEffect.GetBorderWidth(Element);
            _BorderLayer.BorderColor = ViewEffect.GetBorderColor(Element).ToUIColor().CGColor;
            _BorderLayer.CornerRadius = (nfloat)ViewEffect.GetCornerRadius(Element);
            _BorderLayer.MasksToBounds = true;
            _BorderLayer.BackgroundColor = view.BackgroundColor.CGColor;
            view.BackgroundColor = UIColor.Clear;
            view.Layer.ShadowColor = ViewEffect.GetShadowColor(Element).ToUIColor().CGColor;
            view.Layer.ShadowRadius = (nfloat) ViewEffect.GetShadowRadius(Element);
            view.Layer.ShadowOpacity = ViewEffect.GetShadowOpacity(Element);
            view.Layer.ShadowOffset = new CGSize((nfloat) ViewEffect.GetShadowOffsetX(Element),
                                                 (nfloat) ViewEffect.GetShadowOffsetY(Element));
            view.Layer.MasksToBounds = false;
            view.ClipsToBounds = false;
        }

        protected override void OnDetached()
        {
            var view = Control ?? Container;
            view.Layer.MasksToBounds = _DefaultMaskToBounds;
            view.BackgroundColor = _DefaultBackgroundColor;
            _BorderLayer?.RemoveFromSuperLayer();
            _BorderLayer = null;
        }

        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
            if (_BorderLayer == null) return;
            if (args.PropertyName == "Height")
            {
                var frame = _BorderLayer.Frame;
                var newHeight = (double)Element.GetValue(VisualElement.HeightProperty);
                _BorderLayer.Frame = new CGRect(0, 0, frame.Width, new nfloat(newHeight));
            }
            else if (args.PropertyName == "Width")
            {
                var frame = _BorderLayer.Frame;
                var newWidth = (double)Element.GetValue(VisualElement.WidthProperty);
                _BorderLayer.Frame = new CGRect(0, 0, new nfloat(newWidth), frame.Height);

            }
        }

        public static void Init()
        {

        }
    }
}
