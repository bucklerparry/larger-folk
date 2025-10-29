using System;
using XRL.World.Parts;
using XRL.World.Effects;
using XRL.Rules;


namespace XRL.World.Parts
{
	[Serializable]
	public class LargerFolk_Mod_Stretchy: IModification
	{
		public LargerFolk_Mod_Stretchy()
		{
		}

		public LargerFolk_Mod_Stretchy(int Tier)
			: base(Tier)
		{
		}

		public override void Configure()
		{
			WorksOnSelf = true;
			NameForStatus = "Stretchy";
		}

		public override bool ModificationApplicable(GameObject Object)
		{
			if (Object.HasPart<LargerFolk_Armor>())
			{
				return true;
			}
			return false;
		}

		public override void ApplyModification(GameObject Object)
		{
			
			// Object.RemovePart<BreakableInMelee>();
			// base.StatShifter.SetStatShift(Object, "Hitpoints", Object.hitpoints / 4, baseValue: true);
			if (Object.HasEffect<LargerFolk_Effect_Tight>())
            {
				Object.GetEffect<LargerFolk_Effect_Tight>().RemoveTight();
            }
		}

		public override void Remove()
		{
			base.StatShifter.RemoveStatShifts(ParentObject);
			base.Remove();
		}

		public override bool WantEvent(int ID, int cascade)
		{
			if (!base.WantEvent(ID, cascade) && ID != PooledEvent<GetDisplayNameEvent>.ID)
			{
				return ID == GetShortDescriptionEvent.ID;
			}
			return true;
		}

		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName && !E.Object.IsNatural())
			{
				E.AddAdjective("stretchy");
			}
			return base.HandleEvent(E);
		}

		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(GetDescription(Tier, ParentObject));
			return base.HandleEvent(E);
		}

		public override bool AllowStaticRegistration()
		{
			return true;
		}

		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			// Registrar.Register("ApplyBroken");
			Registrar.Register("CanApplyTightness");
			base.Register(Object, Registrar);
		}

		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanApplyTightness")
			{
				return false;
			} 
			return base.FireEvent(E);
		}

		public static string GetDescription(int Tier)
		{
			return "Stretchy: This item has no tightness penalty when worn.";
		}

		public static string GetDescription(int Tier, GameObject obj)
		{
			// if (obj.HasPart<NoDamage>())
			// {
			// 	return "Stretchy: This item has no tightness penalty when worn.";
			// }
			return "Stretchy: This item has no tightness penalty when worn.";
		}
	}
}