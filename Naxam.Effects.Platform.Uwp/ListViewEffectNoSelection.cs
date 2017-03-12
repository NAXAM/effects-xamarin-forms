using Windows.UI.Xaml.Controls;
using Xamarin.Forms.Platform.UWP;

namespace Naxam.Effects.Platform.Uwp
{
    public class ListViewEffectNoSelection : PlatformEffect
    {
        private ListViewSelectionMode originSelectionMode;

        protected override void OnAttached()
        {
            var control = Control as Windows.UI.Xaml.Controls.ListView;

            if (control == null) return;

            originSelectionMode = control.SelectionMode;
            control.SelectionMode = ListViewSelectionMode.None;
        }

        protected override void OnDetached()
        {
            var control = Control as Windows.UI.Xaml.Controls.ListView;

            if (control == null) return;
            
            control.SelectionMode = originSelectionMode;
        }
    }
}
