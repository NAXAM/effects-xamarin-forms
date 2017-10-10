using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FBKVOControllerNS;
using System.Collections.Generic;
using System.Linq;

namespace Naxam.Effects.Platform.iOS
{
    internal class KVOObserver : NSObject
    {

    }

    public class ViewStyleEffect : PlatformEffect
    {
        UIView nativeView;
        FBKVOController KVOController;
        KVOObserver Observer;

        Dictionary<string, object> OriginalValues;

        protected override void OnAttached()
        {
            try
            {
                nativeView = Control ?? Container;
                RemoveObservers();

                OriginalValues = new Dictionary<string, object>()
                {
                    { "corner_radius", nativeView.Layer.CornerRadius},
                    { "border_width", nativeView.Layer.BorderWidth},
                    { "border_color", nativeView.Layer.BorderColor},
                    { "mask_to_bounds", nativeView.Layer.MasksToBounds},
                };

                UpdateStyle();

                Observer = new KVOObserver();
                KVOController = FBKVOController.ControllerWithObserver(Observer);
                KVOController.Observe(nativeView, "frame", NSKeyValueObservingOptions.New, UpdateFromKVO);
                KVOController.Observe(nativeView, "hidden", NSKeyValueObservingOptions.New, UpdateFromKVO);
                KVOController.Observe(nativeView.Layer, "bounds", NSKeyValueObservingOptions.New, UpdateFromKVO);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Cannot set property on attached control. Error: " + ex.Message);
            }
        }

        private void UpdateFromKVO(NSObject observer, NSObject observed, NSDictionary<NSString, NSObject> change)
        {
            UpdateStyle();
        }

        private void RemoveObservers()
        {
            KVOController?.UnobserveAll();
            KVOController?.Dispose();
            Observer?.Dispose();
        }

        CAShapeLayer borderLayer;

        void UpdateStyle()
        {
            /*
             * CORNERS
             */
            var minX = nativeView.Bounds.GetMinX();
            var minY = nativeView.Bounds.GetMinY();
            var maxX = nativeView.Bounds.GetMaxX();
            var maxY = nativeView.Bounds.GetMaxY();

            nfloat topRightRadius;
            nfloat bottomRightRadius;
            nfloat topLeftRadius;
            nfloat bottomLeftRadius;

            var cornerRadius = ViewEffect.GetCornerRadius(Element);
            if (cornerRadius == -1)
            {
                topRightRadius = topLeftRadius = bottomLeftRadius = bottomRightRadius =
                    (float)Math.Floor(Math.Min(nativeView.Bounds.Size.Width, nativeView.Bounds.Size.Height)) / 2f;
            }
            else if (cornerRadius > 0)
            {
                topRightRadius = topLeftRadius = bottomLeftRadius = bottomRightRadius = (nfloat)cornerRadius;
            }
            else
            {
                topRightRadius = GetValue(ViewEffect.GetTopRightCornerRadius(Element));
                bottomRightRadius = GetValue(ViewEffect.GetBottomRightCornerRadius(Element));
                topLeftRadius = GetValue(ViewEffect.GetTopLeftCornerRadius(Element));
                bottomLeftRadius = GetValue(ViewEffect.GetBottomLeftCornerRadius(Element));
            }

            var path = new UIBezierPath();
            path.MoveTo(new CGPoint(minX + topLeftRadius, minY));
            //  -  
            path.AddLineTo(new CGPoint(maxX - topRightRadius, minY));
            //    \
            path.AddArc(new CGPoint(maxX - topRightRadius, minY + topRightRadius),
                        topRightRadius,
                        new nfloat(3 * Math.PI / 2),
                         0,
                        true);
            //    |
            path.AddLineTo(new CGPoint(maxX, maxY - bottomRightRadius));
            //    / 
            path.AddArc(new CGPoint(maxX - bottomRightRadius, maxY - bottomRightRadius),
                        bottomRightRadius,
                       0,
                        new nfloat(Math.PI / 2),
                        true);
            //  _
            path.AddLineTo(new CGPoint(minX + bottomLeftRadius, maxY));
            // \
            path.AddArc(new CGPoint(minX + bottomLeftRadius, maxY - bottomLeftRadius),
                        bottomLeftRadius,
                        new nfloat(Math.PI / 2),
                        new nfloat(Math.PI),
                        true
                       );
            // |
            path.AddLineTo(new CGPoint(minX, minY + topLeftRadius));
            // /
            path.AddArc(new CGPoint(minX + topLeftRadius, minY + topLeftRadius),
                        topLeftRadius,
                        new nfloat(Math.PI),
                        new nfloat(3 * Math.PI / 2),
                        true
            );
            path.ClosePath();
            var maskLayer = new CAShapeLayer()
            {
                Path = path.CGPath,
                //FillColor = UIColor.Clear.CGColor
            };

            nativeView.Layer.Mask = maskLayer;

            /*
             * BORDER
             */
            if (borderLayer == null)
            {
                borderLayer = new CAShapeLayer()
                {
                    FillColor = UIColor.Clear.CGColor,
                    Name = "Naxam_Border_Layer"
                };
                if (nativeView.Layer.SuperLayer != null)
                {
                    System.Diagnostics.Debug.WriteLine("border in superlayer");
                    //nativeView.Layer.SuperLayer.InsertSublayerAbove(borderLayer, nativeView.Layer);
                    nativeView.Layer.SuperLayer.InsertSublayerBelow(borderLayer, nativeView.Layer);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("border in native layer");
                    nativeView.Layer.AddSublayer(borderLayer);
                }
            }
            else if (nativeView.Layer.SuperLayer != null &&
                     (borderLayer.SuperLayer == null)
                     || !borderLayer.SuperLayer.Equals(nativeView.Layer.SuperLayer))
            {
                borderLayer.RemoveFromSuperLayer();
                //System.Diagnostics.Debug.WriteLine($"{Element.GetType()} -> {nativeView.GetType()} ");
                //System.Diagnostics.Debug.WriteLine($"border in superlayer {nativeView.Layer} {nativeView.Layer.SuperLayer}");
                //nativeView.Layer.SuperLayer.InsertSublayerAbove(borderLayer, nativeView.Layer);
                nativeView.Layer.SuperLayer.InsertSublayerBelow(borderLayer, nativeView.Layer);
            }
            borderLayer.StrokeColor = ViewEffect.GetBorderColor(Element).ToUIColor().CGColor;
            borderLayer.Path = path.CGPath;
            borderLayer.BackgroundColor = nativeView.BackgroundColor?.CGColor;
            borderLayer.FillColor = borderLayer.BackgroundColor;
            var borderWidth = GetValue(ViewEffect.GetBorderWidth(Element));
            if (borderLayer.SuperLayer.Equals(nativeView.Layer))
            {
                borderLayer.Frame = new CGRect(CGPoint.Empty, nativeView.Layer.Bounds.Size);
            }
            else
            {
                borderLayer.Frame = nativeView.Layer.Frame;
            }
            borderLayer.LineWidth = borderWidth * 2;

            /*
             * SHADOW
             */
            borderLayer.ShadowColor = ViewEffect.GetShadowColor(Element).ToUIColor().CGColor;
            borderLayer.ShadowOpacity = (float) GetValue(ViewEffect.GetShadowOpacity(Element));
            borderLayer.ShadowRadius = (float) GetValue(ViewEffect.GetShadowRadius(Element));
            borderLayer.ShadowOffset = new CoreGraphics.CGSize(
                (float)GetValue(ViewEffect.GetShadowOffsetX(Element)),
                (float)GetValue(ViewEffect.GetShadowOffsetY(Element)));
            borderLayer.MasksToBounds = false;
        }

        nfloat GetValue(double elementValue) {
            return (nfloat)Math.Max(0, elementValue);
        }

        protected override void OnDetached()
        {
            RemoveObservers();
            nativeView.Layer.Sublayers = nativeView.Layer.Sublayers?.Where((arg) => !(arg is CAGradientLayer)).ToArray();
            nativeView.BackgroundColor = (Element as VisualElement)?.BackgroundColor.ToUIColor();
            if (OriginalValues != null)
            {
                nativeView.Layer.MasksToBounds = (bool)OriginalValues["mask_to_bounds"];
                nativeView.Layer.CornerRadius = (nfloat)OriginalValues["corner_radius"];
                nativeView.Layer.BorderColor = (CGColor)OriginalValues["border_color"];
                nativeView.Layer.BorderWidth = (nfloat)OriginalValues["border_width"];
            }
            nativeView.Layer.Mask = null;
            borderLayer?.RemoveFromSuperLayer();
        }

        //protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        //{
        //    base.OnElementPropertyChanged(args);
        //    if (_BorderLayer == null) return;
        //    if (args.PropertyName == "Height")
        //    {
        //        var frame = _BorderLayer.Frame;
        //        var newHeight = (double)Element.GetValue(VisualElement.HeightProperty);
        //        _BorderLayer.Frame = new CGRect(0, 0, frame.Width, new nfloat(newHeight));
        //    }
        //    else if (args.PropertyName == "Width")
        //    {
        //        var frame = _BorderLayer.Frame;
        //        var newWidth = (double)Element.GetValue(VisualElement.WidthProperty);
        //        _BorderLayer.Frame = new CGRect(0, 0, new nfloat(newWidth), frame.Height);

        //    }
        //}

        public static void Init()
        {

        }
    }
}
