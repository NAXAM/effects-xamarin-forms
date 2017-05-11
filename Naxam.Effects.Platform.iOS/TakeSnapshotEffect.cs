using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Naxam.Effects.Platform.iOS
{
    public class TakeSnapshotEffect : PlatformEffect
    {
        protected override void OnAttached ()
        {
            try {
                SnapshotEffect.SetTakeSnapshotFunc (Element, TakeViewSnapshot);
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine ("Cannot set property on attached control. Error: " + ex.Message);
            }
        }

        //byte [] TakeViewSnapshot ()
        //{
        //    var view = Control ?? Container;
        //    if (view != null) return view.TakeSnapshot ();
        //    else return null;
        //}

        Stream TakeViewSnapshot ()
        {
            var view = Control ?? Container;
            if (view != null) return view.TakeSnapshotStream ();
            return null;
        }

        protected override void OnDetached ()
        {
            SnapshotEffect.SetTakeSnapshotFunc (Element, null);
        }

        public static void Init() {

        }
    }
}
