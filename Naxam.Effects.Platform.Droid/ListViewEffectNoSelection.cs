
using Android.Widget;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;

namespace Naxam.Effects.Platform.Droid
{
	public class ListViewEffectNoSelection : PlatformEffect
	{
		protected override void OnAttached()
		{
			UpdateSelectionColor();
		}

		protected override void OnDetached()
		{
		}

		protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
		{
			base.OnElementPropertyChanged(args);
		}

		void UpdateSelectionColor() { 
			var listView = Control as ListView;

			if (listView != null)
			{
				listView.SetSelector(Android.Resource.Color.Transparent);
				listView.CacheColorHint = Xamarin.Forms.Color.Transparent.ToAndroid();
			}
		}
	}
}
