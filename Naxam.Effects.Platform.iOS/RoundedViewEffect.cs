using System;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Naxam.Effects.Platform.iOS
{
    public class RoundedViewEffect : PlatformEffect
    {
        private nfloat _OldCornerRadius;
        private bool _OldMaskToBounds;
        private IDisposable fObserver;
        protected override void OnAttached()
        {
			try
			{
                var view = Control ?? Container;
                _OldCornerRadius = view.Layer.CornerRadius;
                _OldMaskToBounds = view.Layer.MasksToBounds;
                view.Layer.MasksToBounds = true;
                fObserver = view.Layer.AddObserver("bounds", Foundation.NSKeyValueObservingOptions.Initial | Foundation.NSKeyValueObservingOptions.OldNew, (obj) =>
				{
					UpdateCornerRadius();
				});
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Cannot set property on attached control. Error: " + ex.Message);
			}
        }


        private void UpdateCornerRadius()
        {
            var view = Control ?? Container;
			if (view == null) return;
            Device.BeginInvokeOnMainThread( () => {
                view.Layer.CornerRadius = (nfloat)Math.Min(view.Frame.Height / 2, view.Frame.Width / 2);
            });
        }

        protected override void OnDetached()
        {
            var view = Control ?? Container;
            if (view == null) return;
            Device.BeginInvokeOnMainThread(() =>
            {
                view.Layer.CornerRadius = _OldCornerRadius;
                view.Layer.MasksToBounds = _OldMaskToBounds;
            });
            fObserver?.Dispose();
        }
    }

   
}
