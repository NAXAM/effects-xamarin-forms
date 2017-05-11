using System;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace Naxam.Effects
{
    public static class SnapshotEffect
    {
        public static readonly BindableProperty CanTakeSnapshotProperty = BindableProperty.CreateAttached(
            "CanTakeSnapshot",
            typeof(bool),
            typeof(SnapshotEffect),
            default(bool),
            BindingMode.TwoWay,
            propertyChanged: OnCanTakeSnapshotPropertyChanged
        );

        public static bool GetCanTakeSnapshot(BindableObject element)
        {
            return (bool)element.GetValue(CanTakeSnapshotProperty);
        }

        public static void SetCanTakeSnapshot(BindableObject element, bool value)
        {
            element.SetValue(CanTakeSnapshotProperty, value);
        }

        public static readonly BindableProperty TakeSnapshotFuncProperty = BindableProperty.CreateAttached(
                    "TakeSnapshotFunc",
                    typeof(Func<Stream>),
                    typeof(SnapshotEffect),
            default(Func<Stream>),
            BindingMode.OneWayToSource);

        public static Func<Stream> GetTakeSnapshotFunc(BindableObject element)
        {
            return (Func<Stream>)element.GetValue(TakeSnapshotFuncProperty);
        }

        public static void SetTakeSnapshotFunc(BindableObject element, Func<Stream> value)
        {
            element.SetValue(TakeSnapshotFuncProperty, value);
        }


        static void OnCanTakeSnapshotPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var element = bindable as View;
            if (element == null) return;
            var effect = element.Effects.FirstOrDefault(x => x is TakeSnapshotEffect);
            var canTakeSnapshot = (bool)newValue;
            if (canTakeSnapshot == false && effect != null)
            {
                element.Effects.Remove(effect);
            }
            else if (canTakeSnapshot && effect == null)
            {
                element.Effects.Add(new TakeSnapshotEffect());
            }
        }
    }

    public class TakeSnapshotEffect : RoutingEffect
    {
        const string NAME = "Naxam.Effects.TakeSnapshotEffect";
        public TakeSnapshotEffect() : base(NAME)
        {

        }
    }
}
