using System;
using System.Linq;
using Android.Content;
using Android.Support.V4.View;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Naxam.Effects.Platform.Droid
{
	public class GestureEffectSwipe : PlatformEffect
	{
		Naxam.Effects.GestureEffectSwipe effect;
		SwipeTouchListener listener;

		public GestureEffectSwipe()
		{
			listener = new SwipeTouchListener(Forms.Context);
			listener.Swiped += OnSwiped;
		}

		protected override void OnAttached()
		{
			effect = (Naxam.Effects.GestureEffectSwipe)(Element.Effects.FirstOrDefault(x => x is Naxam.Effects.GestureEffectSwipe));

			if (effect == null) return;

			(Control ?? Container).SetOnTouchListener(listener);
		}

		protected override void OnDetached()
		{
			(Control ?? Container).SetOnTouchListener(null);
		}

		void OnSwiped(object sender, SwipeGesture e)
		{
			if (effect?.Enabled == false) return;

			if (!effect.Command.CanExecute(e)) return;

			effect.Command.Execute(e);
		}
	}

	public class SwipeTouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
	{
		public event EventHandler<SwipeGesture> Swiped;

		readonly GestureDetectorCompat gestureDetector;

		public SwipeTouchListener(Context context)
		{
			var listener = new SwipeGestureListener();
			listener.Swiped += OnSwiped;

			gestureDetector = new GestureDetectorCompat(context, listener);
		}

		public bool OnTouch(Android.Views.View v, MotionEvent e)
		{
			return gestureDetector.OnTouchEvent(e);
		}

		void OnSwiped(object sender, SwipeGesture e)
		{
			Swiped?.Invoke(this, e);
		}
	}

	public class SwipeGestureListener : GestureDetector.SimpleOnGestureListener
	{
		public event EventHandler<SwipeGesture> Swiped;

		readonly int SWIPE_THRESHOLD = 100;
		readonly int SWIPE_VELOCITY_THRESHOLD = 100;

		public override bool OnDown(MotionEvent e)
		{
			return true;
		}

		public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{
			var result = false;
			try
			{
				float diffY = e2.GetY() - e1.GetY();
				float diffX = e2.GetX() - e1.GetX();
				if (Math.Abs(diffX) > Math.Abs(diffY))
				{
					if (Math.Abs(diffX) > SWIPE_THRESHOLD && Math.Abs(velocityX) > SWIPE_VELOCITY_THRESHOLD)
					{
						if (diffX > 0)
						{
							Swiped?.Invoke(this, new SwipeGesture
							{
								State = SwipeState.Ended,
								Direction = SwipeDirection.LeftToRight
							});
						}
						else
						{
							Swiped?.Invoke(this, new SwipeGesture
							{
								State = SwipeState.Ended,
								Direction = SwipeDirection.RightToLeft
							});
						}
					}
					result = true;
				}
				else if (Math.Abs(diffY) > SWIPE_THRESHOLD && Math.Abs(velocityY) > SWIPE_VELOCITY_THRESHOLD)
				{
					if (diffY > 0)
					{
						Swiped?.Invoke(this, new SwipeGesture
						{
							State = SwipeState.Ended,
							Direction = SwipeDirection.TopDown
						});
					}
					else
					{
						Swiped?.Invoke(this, new SwipeGesture
						{
							State = SwipeState.Ended,
							Direction = SwipeDirection.BottomUp
						});
					}
				}
				result = true;

			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.StackTrace);
				throw;
			}
			return result;
		}
	}
}
