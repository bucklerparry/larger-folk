using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Effects
{

[Serializable]
public class LargerFolk_Inflated : Effect, ITierInitialized
{
	public int DVPenalty;

	public int SaveTarget;

	public LargerFolk_Inflated()
	{
		DisplayName = "{{C|inflated}}";
	}

	public LargerFolk_Inflated(int Duration, int SaveTarget)
		: this()
	{
		this.SaveTarget = SaveTarget;
		base.Duration = Duration;
	}

	public void Initialize(int Tier)
	{
		Tier = Stat.Random(Tier - 1, Tier + 1);
		if (Tier < 1)
		{
			Tier = 1;
		}
		if (Tier > 8)
		{
			Tier = 8;
		}
		if (Tier < 2)
		{
			Duration = Stat.Roll("1d3+1");
		}
		else
		{
			Duration = Stat.Roll("1d3+" + Math.Min(7, Tier * 2 / 3 + 1));
		}
		SaveTarget = 14 + Tier * 2 * 2;
	}

	public override int GetEffectType()
	{
		return 117440516;
	}

	public override bool SameAs(Effect e)
	{
		LargerFolk_Inflated inflated = e as LargerFolk_Inflated;
		if (inflated.DVPenalty != DVPenalty)
		{
			return false;
		}
		if (inflated.SaveTarget != SaveTarget)
		{
			return false;
		}
		return base.SameAs(e);
	}

	public override bool UseStandardDurationCountdown()
	{
		return true;
	}

	public override string GetDetails()
	{
		if (DVPenalty < 0)
		{
			return "Blown up big and round. Can't move or attack.\n" + DVPenalty + " DV";
		}
		return "Blown up big and round. Can't move or attack.\nDV set to 0.";
	}

	public override bool Apply(GameObject Object)
	{
		if (Object.HasEffect<LargerFolk_Inflated>())
		{
			LargerFolk_Inflated effect = Object.GetEffect<LargerFolk_Inflated>();
			if (Duration > effect.Duration)
			{
				effect.Duration = Duration;
			}
			return true;
		}
		if (!Object.FireEvent("ApplyInflate"))
		{
			return false;
		}
		ApplyStats();
		DidX("are", "inflated", "!", null, null, null, Object);
		Object.ParticleText("*inflated*", IComponent<GameObject>.ConsequentialColorChar(null, Object));
		return true;
	}

	public override void Remove(GameObject Object)
	{
		UnapplyStats();
		base.Remove(Object);
	}

	private void ApplyStats()
	{
		int combatDV = Stats.GetCombatDV(base.Object);
		if (combatDV > 0)
		{
			DVPenalty += combatDV;
			base.StatShifter.SetStatShift(base.Object, "DV", -DVPenalty);
		}
		else
		{
			DVPenalty = 0;
		}
	}

	private void UnapplyStats()
	{
		DVPenalty = 0;
		base.StatShifter.RemoveStatShifts(base.Object);
	}

	public override bool WantEvent(int ID, int cascade)
	{
		if (!base.WantEvent(ID, cascade) && ID != SingletonEvent<BeginTakeActionEvent>.ID && ID != PooledEvent<CanChangeMovementModeEvent>.ID && ID != PooledEvent<GetCompanionStatusEvent>.ID)
		{
			return ID == PooledEvent<IsConversationallyResponsiveEvent>.ID;
		}
		return true;
	}

	public override bool HandleEvent(CanChangeMovementModeEvent E)
	{
		if (Duration > 0 && !E.Involuntary && E.Object == base.Object)
		{
			if (E.ShowMessage)
			{
				E.Object.Fail("You have blown up like a balloon!");
			}
			return false;
		}
		return base.HandleEvent(E);
	}

	public override bool HandleEvent(GetCompanionStatusEvent E)
	{
		if (E.Object == base.Object)
		{
			E.AddStatus("inflated", 90);
		}
		return base.HandleEvent(E);
	}

	public override bool HandleEvent(BeginTakeActionEvent E)
	{
		if (Duration > 0)
		{
			if (base.Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("You are {{C|inflated}}.");
			}
			E.PreventAction = true;
		}
		return base.HandleEvent(E);
	}

	public override bool HandleEvent(IsConversationallyResponsiveEvent E)
	{
		if (E.Speaker == base.Object && !E.Mental)
		{
			E.Message = base.Object.T(int.MaxValue, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: true, Short: true, BaseOnly: false, null, IndicateHidden: false, SecondPerson: true, Reflexive: false, null) + base.Object.Is + " utterly unresponsive.";
			return false;
		}
		return base.HandleEvent(E);
	}

	public override void Register(GameObject Object, IEventRegistrar Registrar)
	{
		Registrar.Register("AfterDeepCopyWithoutEffects");
		Registrar.Register("BeforeDeepCopyWithoutEffects");
		Registrar.Register("CanChangeBodyPosition");
		Registrar.Register("CanMoveExtremities");
		Registrar.Register("IsMobile");
		base.Register(Object, Registrar);
	}

	public override bool Render(RenderEvent E)
	{
		int num = XRLCore.CurrentFrame % 60;
		if (num > 15 && num < 30)
		{
			E.Tile = "bucklerparry_largerfolk/effect_inflated.png";
			E.RenderString = "X";
			E.ColorString = "&C^c";
		}
		return true;
	}

	public override bool FireEvent(Event E)
	{
		if (E.ID == "IsMobile")
		{
			if (Duration > 0)
			{
				return false;
			}
		}
		else if (E.ID == "CanChangeBodyPosition" || E.ID == "CanMoveExtremities")
		{
			if (Duration > 0 && !E.HasFlag("Involuntary"))
			{
				if (E.HasFlag("ShowMessage") && base.Object.IsPlayer())
				{
					Popup.ShowFail("You are {{C|inflated}}!");
				}
				return false;
			}
		}
		else if (E.ID == "BeforeDeepCopyWithoutEffects")
		{
			UnapplyStats();
		}
		else if (E.ID == "AfterDeepCopyWithoutEffects")
		{
			ApplyStats();
		}
		return base.FireEvent(E);
	}
}
}