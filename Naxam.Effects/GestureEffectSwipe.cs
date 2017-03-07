using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Naxam.Effects
{
	public class GestureEffectSwipe : RoutingEffect
	{
		public const string NAME = "Naxam.Effects.GestureEffectSwipe";

		public GestureEffectSwipe() : base(NAME)
		{

		}

		public ICommand Command { get; set; }

		public bool Enabled { get; set; }
	}

	public enum SwipeDirection
	{
		LeftToRight,
		RightToLeft,
		TopDown,
		BottomUp
	}

	public enum SwipeState
	{
		Began,
		Ended
	}

	public class SwipeGesture
	{
		public SwipeDirection Direction
		{
			get;
			set;
		}

		public SwipeState State
		{
			get;
			set;
		}
	}

	public static class GestureEffect
	{
		public static BindableProperty SwipeCommandProperty = BindableProperty.CreateAttached(
			"SwipeCommand",
			typeof(ICommand),
			typeof(GestureEffect),
			null,
			BindingMode.OneWay,
			propertyChanged: OnSwipeCommandChanged
		);

		public static ICommand GetSwipeCommand(BindableObject obj)
		{
			return obj.GetValue(SwipeCommandProperty) as ICommand;
		}

		public static void SetSwipeCommand(BindableObject obj, ICommand value)
		{
			obj.SetValue(SwipeCommandProperty, value);
		}

		static void OnSwipeCommandChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var view = bindable as View;

			if (view == null) return;

			var effect = (GestureEffectSwipe)view.Effects.FirstOrDefault(x => x is GestureEffectSwipe);

			if (effect != null)
			{
				effect.Command = (ICommand) newValue;
				return;
			}

			view.Effects.Add(new GestureEffectSwipe
			{
				Command = (ICommand)newValue,
				Enabled = true
			});
		}

		public static BindableProperty SwipeEnabledProperty = BindableProperty.CreateAttached(
			"SwipeEnabled",
			typeof(bool?),
			typeof(GestureEffect),
			true,
			BindingMode.OneWay,
			propertyChanged: OnSwipeEnabledChanged
		);

		public static bool? GetSwipeEnabled(BindableObject obj)
		{
			return (bool?)obj.GetValue(SwipeEnabledProperty);
		}

		public static void SetSwipeEnabled(BindableObject obj, bool? value)
		{
			obj.SetValue(SwipeEnabledProperty, value);
		}

		static void OnSwipeEnabledChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var view = (View)bindable;

			var effect = (GestureEffectSwipe)view.Effects.FirstOrDefault(x => x is GestureEffectSwipe);

			if (effect != null)
			{
				effect.Enabled = (bool?)newValue ?? true;
				return;
			}

			view.Effects.Add(new GestureEffectSwipe
			{
				Enabled = (bool?)newValue ?? true
			});
		}
	}
}
