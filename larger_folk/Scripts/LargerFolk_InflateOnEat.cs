using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	[Serializable]
	public class LargerFolk_InflateOnEat : IPart
	{
		public int Duration = 40;

		public int SaveTarget = 20;

		// public int Strength = 30;

		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				E.GetGameObjectParameter("Eater").ApplyEffect(new LargerFolk_Inflated(Duration, SaveTarget));
			}
			return base.FireEvent(E);
		}
	}
}