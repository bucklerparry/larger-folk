using System;
using XRL.Language;
using XRL.World.Parts.Skill;
using XRL.World.Parts;

namespace XRL.World.Parts
{
	[Serializable]
	public class LargerFolk_ModFattening : IModification
	{
		public int Chance = 3;

		public LargerFolk_ModFattening()
		{
		}

		public LargerFolk_ModFattening(int Tier)
			: base(Tier)
		{
		}

		public override bool SameAs(IPart p)
		{
			if ((p as LargerFolk_ModFattening).Chance != Chance)
			{
				return false;
			}
			return base.SameAs(p);
		}

		public override void Configure()
		{
			WorksOnSelf = true;
		}

		public override bool ModificationApplicable(GameObject Object)
		{
			MeleeWeapon part = Object.GetPart<MeleeWeapon>();
			if (part == null)
			{
				return false;
			}
			if (part.Skill != "LongBlades" && part.Skill != "Axe")
			{
				return false;
			}
			return true;
		}

		public override void ApplyModification(GameObject Object)
		{
			IncreaseDifficultyAndComplexityIfComplex(1, 1);
		}

		public override bool WantEvent(int ID, int cascade)
		{
			if (!base.WantEvent(ID, cascade) && ID != PooledEvent<GetDisplayNameEvent>.ID && ID != PooledEvent<GetItemElementsEvent>.ID)
			{
				return ID == GetShortDescriptionEvent.ID;
			}
			return true;
		}

		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName && !E.Object.HasPart<ModMicroserrated>())
			{
				E.AddAdjective("{{Y|fat{{R|ten}}ing}}");
			}
			return base.HandleEvent(E);
		}

		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!E.Object.HasPart<ModMicroserrated>())
			{
				E.Postfix.AppendRules(GetInstanceDescription());
			}
			return base.HandleEvent(E);
		}

		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		public override bool AllowStaticRegistration()
		{
			return true;
		}

		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" && !ParentObject.HasPart<ModMicroserrated>())
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject parentObject = ParentObject;
				GameObject subject = gameObjectParameter2;
				if (subject.HasPart<LargerFolk_WeightGain>())
				{
					subject.GetPart<LargerFolk_WeightGain>().ChangeCalories(6000);
				}
				// if (GetSpecialEffectChanceEvent.GetFor(gameObjectParameter, parentObject, "Modification ModSerrated Dismember", Chance, subject).in100() && IsReady(UseCharge: true, IgnoreCharge: false, IgnoreLiquid: false, IgnoreBootSequence: false, IgnoreBreakage: false, IgnoreRust: false, IgnoreEMP: false, IgnoreRealityStabilization: false, IgnoreSubject: false, IgnoreLocallyDefinedFailure: false, 1, null, UseChargeIfUnpowered: false, 0L, null))
				// {
				// 	bool flag = 1.in1000();
				// 	Axe_Dismember.Dismember(gameObjectParameter, gameObjectParameter2, null, null, ParentObject, null, "sfx_characterTrigger_dismember", flag, !flag);
				// }
			}
			return base.FireEvent(E);
		}

		public static string GetDescription(int Tier)
		{
			return "Fattening: This weapon fattens an opponent on hit.";
		}

		public string GetInstanceDescription()
		{
			// int @for = GetSpecialEffectChanceEvent.GetFor(ParentObject.Equipped ?? ParentObject.Implantee, ParentObject, "Modification ModSerrated Dismember", Chance);
			return "Fattening: This weapon fattens an opponent on hit.";
		}
	}
}