using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Naxam.Effects
{
	public static class ListViewEffect
	{
		public static readonly BindableProperty AllowsSelectionProperty =
			BindableProperty.CreateAttached("AllowsSelection",
											typeof(bool?),
											typeof(ListViewEffect),
											true,
											propertyChanged: OnAllowsSelectionPropertyPropertyChanged);

		public static bool? GetAllowsSelection(BindableObject element)
		{
			return (bool?)element.GetValue(AllowsSelectionProperty);
		}

		public static void SetAllowsSelection(BindableObject element, bool? value)
		{
			element.SetValue(AllowsSelectionProperty, value);
		}

		static void OnAllowsSelectionPropertyPropertyChanged(BindableObject element, object oldValue, object newValue)
		{
			if ((bool?)newValue == false)
			{
				AttachEffect(element as ListView);
			}
			else
			{
				DetachEffect(element as ListView);
			}
		}

		static void AttachEffect(ListView element)
		{
			IElementController controller = element;
			if (controller == null || controller.EffectIsAttached(ListViewEffectNoSelection.NAME))
			{
				return;
			}
			element.Effects.Add(new ListViewEffectNoSelection());
		}

		static void DetachEffect(ListView element)
		{
			IElementController controller = element;
			if (controller == null || !controller.EffectIsAttached(ListViewEffectNoSelection.NAME))
			{
				return;
			}

			var resolveId = Effect.Resolve(ListViewEffectNoSelection.NAME).ResolveId;
			var toRemove = element.Effects.FirstOrDefault(e => e.ResolveId == resolveId);
			if (toRemove != null)
			{
				element.Effects.Remove(toRemove);
			}
		}

		public static readonly BindableProperty ViewCellSelectionStyleProperty =
			BindableProperty.CreateAttached("ViewCellSelectionStyle",
											typeof(ViewCellSelectionStyle?),
											typeof(ListViewEffect),
			                                ViewCellSelectionStyle.None,
											propertyChanged: OnViewCellSelectionStylePropertyPropertyChanged);

		public static ViewCellSelectionStyle? GetViewCellSelectionStyle(BindableObject element)
		{
			return (ViewCellSelectionStyle?)element.GetValue(ViewCellSelectionStyleProperty);
		}

		public static void SetViewCellSelectionStyle(BindableObject element, bool value)
		{
			element.SetValue(ViewCellSelectionStyleProperty, value);
		}

		static void OnViewCellSelectionStylePropertyPropertyChanged(BindableObject element, object oldValue, object newValue)
		{
			AttachViewCellSelectionStyleEffect(element as ViewCell, new ViewCellSelectionStyleEffect((ViewCellSelectionStyle?)newValue ?? ViewCellSelectionStyle.None));
		}

		static void AttachViewCellSelectionStyleEffect(ViewCell element, RoutingEffect effect)
		{
			var xeffect = element.Effects.FirstOrDefault(x => x is ViewCellSelectionStyleEffect);
			if (xeffect != null)
			{
				element.Effects.Remove(xeffect);
			}
			element.Effects.Add(effect);
		}

		public static BindableProperty ShouldScrollToTopProperty = BindableProperty.CreateAttached(
			"ShouldScrollToTop",
			typeof(bool?),
			typeof(ListViewEffect),
			false,
			propertyChanged: OnShouldScrollToTopPropertyChanged);

		public static bool GetShouldScrollToTop(BindableObject element)
		{
			return (bool)element.GetValue(ShouldScrollToTopProperty);
		}

		public static void SetShouldScrollToTop(BindableObject element, bool? value)
		{
			element.SetValue(ShouldScrollToTopProperty, value);
		}

		static void OnShouldScrollToTopPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var listView = bindable as ListView;
			if (listView != null && (newValue as bool?) == true) {
				var first = listView.ItemsSource?.Cast<object>().FirstOrDefault();

				if (first != null) {
					listView.ScrollTo(first, ScrollToPosition.Start, true);
				}
			}
		}

		public static BindableProperty FirstVisibleItemIndexProperty = BindableProperty.CreateAttached(
			"FirstVisibleItemIndex",
			typeof(int?),
			typeof(ListViewEffect),
			0,
			propertyChanged: OnFirstVisibleItemIndexPropertyChanged);

		public static int? GetFirstVisibleItemIndex(BindableObject element)
		{
			return element.GetValue(FirstVisibleItemIndexProperty) as int?;
		}

		public static void SetFirstVisibleItemIndex(BindableObject element, int? value)
		{
			element.SetValue(FirstVisibleItemIndexProperty, value);
		}

		static void OnFirstVisibleItemIndexPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var element = bindable as ListView;

			if (element == null) {
				return;
			}

			var xeffect = element.Effects.FirstOrDefault(x => x is ViewCellSelectionStyleEffect);
			if (xeffect != null)
			{
				element.Effects.Remove(xeffect);
			}

			element.Effects.Add(new ListViewEffectFirstVisibleItemIndex());
		}
	}
}
