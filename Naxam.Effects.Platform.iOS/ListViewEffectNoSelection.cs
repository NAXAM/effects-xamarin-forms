using System;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

namespace Naxam.Effects.Platform.iOS
{
	public class ListViewEffectNoSelection : PlatformEffect
	{
		protected override void OnAttached()
		{
			var tableView = Control as UITableView;
			if (tableView != null)
			{
				tableView.AllowsSelection = false;
			}
		}

		protected override void OnDetached()
		{
			var tableView = Control as UITableView;
			if (tableView != null)
			{
				tableView.AllowsSelection = true;
			}
		}
	}

	public class ViewCellSelectionStyleEffect : PlatformEffect
	{
		public static void Preserve() { }

		protected override void OnAttached()
		{
			var view = Control as UITableViewCell;
			var element = Element as ViewCell;

			if (view != null && element != null)
			{
				var effect = element.Effects.FirstOrDefault(x => x is Effects.ViewCellSelectionStyleEffect) as Effects.ViewCellSelectionStyleEffect;

				switch (effect.Style)
				{
					case ViewCellSelectionStyle.None:
						view.SelectionStyle = UITableViewCellSelectionStyle.None;
						break;
					case ViewCellSelectionStyle.Gray:
						view.SelectionStyle = UITableViewCellSelectionStyle.Gray;
						break;
					case ViewCellSelectionStyle.Blue:
						view.SelectionStyle = UITableViewCellSelectionStyle.Blue;
						break;
					default:
						view.SelectionStyle = UITableViewCellSelectionStyle.Default;
						break;
				}

				view.SelectionStyle = UITableViewCellSelectionStyle.None;
			}
		}

		protected override void OnDetached()
		{
			var view = Control as UITableViewCell;

			if (view != null)
			{
				view.SelectionStyle = UITableViewCellSelectionStyle.Default;
			}
		}
	}

	public class ListViewEffectFirstVisibleItemIndex : PlatformEffect
	{
		protected override void OnAttached()
		{
			var listView = Element as ListView;
			var tableView = Control as UITableView;

			if (listView == null || tableView == null || !tableView.ScrollEnabled) return;

			var effect = Element.Effects.FirstOrDefault(x => x is Effects.ListViewEffectFirstVisibleItemIndex) as Effects.ListViewEffectFirstVisibleItemIndex;

			if (effect == null)
			{
				return;
			}

			AttachEffect(listView, tableView, effect);
		}

		protected override void OnDetached()
		{
			var tableView = Control as UITableView;

			if (tableView == null) {
				return;
			}

			tableView.ScrollAnimationEnded -= TableView_Scrolled;
		}

		void AttachEffect(ListView listView, UITableView tableView, Effects.ListViewEffectFirstVisibleItemIndex effect)
		{
			//var firstVisibleItemIndex = PlatformConfiguration.ListViewSelection.GetFirstVisibleItemIndex(listView);

			//TODO: Need to check tableview items count
			//var indexPath = NSIndexPath.FromRowSection(firstVisibleItemIndex ?? 0, 0);
			//tableView.ScrollToRow(indexPath, UITableViewScrollPosition.None, true);

			//tableView.ScrollAnimationEnded += TableView_Scrolled;

		}

		void TableView_Scrolled(object sender, EventArgs e)
		{
			var tableView = Control as UITableView;

			var firstIndexPath = tableView.IndexPathsForVisibleRows[0];

			PlatformConfiguration.ListViewEffect.SetFirstVisibleItemIndex(Element, firstIndexPath.Row);
		}
	}
}
