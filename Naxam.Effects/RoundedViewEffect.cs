using System;
using System.Linq;
using Xamarin.Forms;

namespace Naxam.Effects
{
	public class RoundedViewEffect : RoutingEffect
	{
		public const string NAME = "Naxam.Effects.RoundedViewEffect";
		public RoundedViewEffect() : base(NAME)
		{

		}
	}

  //  public static class RoundedEffect {
		//static void AttachEffect(View element)
		//{
		//	IElementController controller = element;
		//	if (controller == null || controller.EffectIsAttached(RoundedViewEffect.NAME))
		//	{
		//		return;
		//	}
		//	element.Effects.Add(new ListViewEffectNoSelection());
		//}

		//static void DetachEffect(View element)
		//{
		//	IElementController controller = element;
		//	if (controller == null || !controller.EffectIsAttached(RoundedViewEffect.NAME))
		//	{
		//		return;
		//	}

		//	var resolveId = Effect.Resolve(RoundedViewEffect.NAME).ResolveId;
		//	var toRemove = element.Effects.FirstOrDefault(e => e.ResolveId == resolveId);
		//	if (toRemove != null)
		//	{
		//		element.Effects.Remove(toRemove);
		//	}
		//}
    //}
}
