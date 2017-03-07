using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;

namespace Naxam.Effects
{
	public class ListViewEffectNoSelection : RoutingEffect
	{
		public const string NAME = "Naxam.Effects.ListViewEffectNoSelection";

		public ListViewEffectNoSelection() : base(NAME)
		{

		}
	}

	public class ViewCellSelectionStyleEffect : RoutingEffect
	{
		public const string NAME = "Naxam.Effects.ViewCellSelectionStyleEffect";
		readonly ViewCellSelectionStyle style;

		public ViewCellSelectionStyleEffect(ViewCellSelectionStyle style) : base(NAME)
		{
			this.style = style;
		}

		public ViewCellSelectionStyle Style
		{
			get
			{
				return style;
			}
		}
	}

	public enum ViewCellSelectionStyle { 
		None,
		Gray,
		Blue
	}

	
}
