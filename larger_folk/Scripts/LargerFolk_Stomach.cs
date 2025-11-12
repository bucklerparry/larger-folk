using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	[Serializable]
	public class LargerFolk_Stomach : IPart
	{



		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AddFood");
			Registrar.Register("BecameHungry");
			Registrar.Register("BecameFamished");
			base.Register(Object, Registrar);
		}

		public override bool FireEvent(Event E)
		{
			// if (E.ID == "BecameHungry")
			// {

			// }

			return base.FireEvent(E);
		}
		
	}
}