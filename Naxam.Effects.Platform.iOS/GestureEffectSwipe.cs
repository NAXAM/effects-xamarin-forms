using System.Linq;
using Naxam.Effects;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace Naxam.Effects.Platform.iOS
{
	public class GestureEffectSwipe : PlatformEffect
	{
		Naxam.Effects.GestureEffectSwipe effect;
		readonly UISwipeGestureRecognizer rightSwipeGestureRecognizer;
		readonly UISwipeGestureRecognizer leftSwipeGestureRecognizer;

		public GestureEffectSwipe()
		{
			rightSwipeGestureRecognizer = new UISwipeGestureRecognizer(OnSwiped)
			{
				Enabled = true,
				CancelsTouchesInView = false,
				Direction = UISwipeGestureRecognizerDirection.Right
			};
			leftSwipeGestureRecognizer = new UISwipeGestureRecognizer(OnSwiped)
			{
				Enabled = true,
				CancelsTouchesInView = false,
				Direction = UISwipeGestureRecognizerDirection.Left
			};
		}

		protected override void OnAttached()
		{
			effect = (Naxam.Effects.GestureEffectSwipe)(Element.Effects.FirstOrDefault(x => x is Naxam.Effects.GestureEffectSwipe));

			if (effect == null) return;

			(Control ?? Container).AddGestureRecognizer(rightSwipeGestureRecognizer);
			(Control ?? Container).AddGestureRecognizer(leftSwipeGestureRecognizer);
		}

		protected override void OnDetached()
		{
			(Control ?? Container).RemoveGestureRecognizer(rightSwipeGestureRecognizer);
			(Control ?? Container).RemoveGestureRecognizer(leftSwipeGestureRecognizer);
		}

		void OnSwiped(UISwipeGestureRecognizer gesture)
		{
			if (effect?.Enabled == false)
			{
				return;
			}

			var command = effect?.Command;
			var xgesture = new SwipeGesture();

			switch (gesture.Direction)
			{
				case UISwipeGestureRecognizerDirection.Down:
					xgesture.Direction = SwipeDirection.TopDown;
					break;
				case UISwipeGestureRecognizerDirection.Left:
					xgesture.Direction = SwipeDirection.RightToLeft;
					break;
				case UISwipeGestureRecognizerDirection.Right:
					xgesture.Direction = SwipeDirection.LeftToRight;
					break;
				case UISwipeGestureRecognizerDirection.Up:
					xgesture.Direction = SwipeDirection.BottomUp;
					break;
			}

			switch (gesture.State)
			{
				case UIGestureRecognizerState.Began:
					xgesture.State = SwipeState.Began;
					break;
				case UIGestureRecognizerState.Ended:
					xgesture.State = SwipeState.Ended;
					break;
			}

			if (command.CanExecute(xgesture) == true)
			{
				command.Execute(xgesture);
			}
		}
	}
}
