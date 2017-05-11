using System;
using System.IO;
using CoreGraphics;
using UIKit;

namespace Naxam.Effects.Platform.iOS
{
    public static class UIViewExtensions
    {
        public static byte [] TakeSnapshot (this UIView view)
        {
            var image = GetSnapshotImage (view);
            if (image != null) {
                using (var imageData = image.AsPNG ()) {
                    var bytes = new byte [imageData.Length];
                    System.Runtime.InteropServices.Marshal.Copy (imageData.Bytes, bytes, 0, Convert.ToInt32 (imageData.Length));
                    return bytes;
                }
            } else {
                return null;
            }
        }

        public static Stream TakeSnapshotStream (this UIView view)
        {
        var image = view.GetSnapshotImage();
            if (image != null) {
                return image.AsPNG ().AsStream ();
            } else {
                return null;
            }
        }

        public static UIImage GetSnapshotImage (this UIView view)
        {
            var viewToSnap = view;
            if (view.Subviews.Length == 1 && view.Subviews [0] is UIScrollView) {
                viewToSnap = view.Subviews [0];
            }
            if (viewToSnap is UIScrollView) {
                var scrollView = viewToSnap as UIScrollView;
                UIGraphics.BeginImageContextWithOptions (scrollView.ContentSize, false, 0);
                var savedContentOffset = scrollView.ContentOffset;
                var savedFrame = scrollView.Frame;
                var savedBackground = scrollView.BackgroundColor;
                scrollView.ContentOffset = CGPoint.Empty;
                scrollView.Frame = new CGRect (0, 0, scrollView.ContentSize.Width, scrollView.ContentSize.Height);
                scrollView.Layer.RenderInContext (UIGraphics.GetCurrentContext ());
                //scrollView.DrawViewHierarchy (scrollView.Frame, true);
                var image = UIGraphics.GetImageFromCurrentImageContext ();
                scrollView.ContentOffset = savedContentOffset;
                scrollView.Frame = savedFrame;
                UIGraphics.EndImageContext ();
                return image;
            } else {
                UIGraphics.BeginImageContextWithOptions (viewToSnap.Bounds.Size, false, 0);
                viewToSnap.DrawViewHierarchy (viewToSnap.Bounds, true);
                var image = UIGraphics.GetImageFromCurrentImageContext ();
                UIGraphics.EndImageContext ();
                return image;
            }
        }
    }
}
