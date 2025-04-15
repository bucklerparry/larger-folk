using System;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.Parts
{

[Serializable]
public class LargerFolk_StickyFat : IPart
{
	public bool DestroyOnBreak;

	[Obsolete("Retired sticky checking weight")]
	public int MaxWeight = 1000;

	public int SaveTarget = 15;

	public string SaveVs = "Web Stuck Restraint";

	public int Duration = 12;

	//1 = Fat+ get stuck
	//2 = Obese+ get stuck
	//3 = Blob/Immobile+ get stuck
	public int SizeThreshold = 1;

	public override bool SameAs(IPart p)
	{
		return false;
	}

	public override bool WantEvent(int ID, int cascade)
	{
		if (!base.WantEvent(ID, cascade) && ID != GetNavigationWeightEvent.ID && ID != LeftCellEvent.ID && ID != ObjectEnteredCellEvent.ID)
		{
			return ID == OnDestroyObjectEvent.ID;
		}
		return true;
	}

	public override bool HandleEvent(GetNavigationWeightEvent E)
	{
		if (E.Smart && !E.Juggernaut && E.PhaseMatches(ParentObject))
		{
			E.MinWeight(E.Autoexploring ? 70 : 2);
		}
		return base.HandleEvent(E);
	}

	public override bool HandleEvent(ObjectEnteredCellEvent E)
	{	if (SizeThreshold >= 1)
		{
			if (E.Object.HasPart<LargerFolk_WeightGain>() && (E.Object.GetPart<LargerFolk_WeightGain>()).WeightStage >= SizeThreshold)
			{
				if (!E.Object.HasEffect<Greased>() && E.Object.GetMatterPhase() <= 1 && !E.Object.HasTag("ExcavatoryTerrainFeature") && E.Object.PhaseMatches(ParentObject) && !ParentObject.IsBroken() && !ParentObject.IsRusted())
				{
					if (Stat.Random(1, 4) == 1)
					{
						E.Object.ApplyEffect(new LargerFolk_FatStuck(Duration*E.Object.GetPart<LargerFolk_WeightGain>().WeightStage, SaveTarget + (E.Object.GetPart<LargerFolk_WeightGain>().WeightStage * 4), SaveVs, DestroyOnBreak ? ParentObject : null, "stuck", "in", ParentObject.ID));
					}

				}
			}
		}
		return base.HandleEvent(E);
	}

	public override bool HandleEvent(LeftCellEvent E)
	{
		StripStuck(E.Cell);
		return base.HandleEvent(E);
	}

	public override bool HandleEvent(OnDestroyObjectEvent E)
	{
		StripStuck(ParentObject.CurrentCell);
		return base.HandleEvent(E);
	}

	public override void Register(GameObject Object, IEventRegistrar Registrar)
	{
		Registrar.Register("ApplyStuck");
		base.Register(Object, Registrar);
	}

	public override bool FireEvent(Event E)
	{
		if (E.ID == "ApplyStuck")
		{
			return false;
		}
		return base.FireEvent(E);
	}

	private bool IsOurs(Effect GFX)
	{
		if (GFX is Stuck stuck && ParentObject.IDMatch(stuck.DependsOn))
		{
			return true;
		}
		return false;
	}

	private void StripStuck(Cell C)
	{
		if (C == null)
		{
			return;
		}
		int i = 0;
		for (int count = C.Objects.Count; i < count; i++)
		{
			Effect effect = C.Objects[i].GetEffect(typeof(Stuck), IsOurs);
			if (effect != null)
			{
				effect.Duration = 0;
			}
		}
	}
}
}