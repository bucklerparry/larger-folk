using System;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Effects
{

[Serializable]
public class LargerFolk_FatStuck : Stuck, ITierInitialized
{
	
	public LargerFolk_FatStuck()
	{
		DisplayName = Adjective;
		BonusSetup();
	}

	public LargerFolk_FatStuck(int Duration, int SaveTarget = 15, string SaveVs = "Stuck Restraint", GameObject DestroyOnBreak = null, string Adjective = "stuck", string Preposition = "in", string DependsOn = null, string DefendingSaveBonusVs = "Move", float DefendingSaveBonusFactor = 0.25f, float KineticResistanceLinearBonus = 1f, float KineticResistancePercentageBonus = 1f, bool DependsOnMustBeFrozen = false, bool DependsOnMustBeSolid = false)
		: this()
	{
		base.Duration = Duration;
		this.SaveTarget = SaveTarget;
		this.SaveVs = SaveVs;
		this.DestroyOnBreak = DestroyOnBreak;
		this.Adjective = Adjective;
		this.Preposition = Preposition;
		this.DependsOn = DependsOn;
		this.DefendingSaveBonusVs = DefendingSaveBonusVs;
		this.DefendingSaveBonusFactor = DefendingSaveBonusFactor;
		KineticResistanceLinearBonusFactor = KineticResistanceLinearBonus;
		KineticResistancePercentageBonusFactor = KineticResistancePercentageBonus;
		this.DependsOnMustBeFrozen = DependsOnMustBeFrozen;
		this.DependsOnMustBeSolid = DependsOnMustBeSolid;
		BonusSetup();
		DisplayName = GetStateDescription();
	}

	private void BonusSetup()
	{
		DefendingSaveBonus = (int)Math.Round((float)SaveTarget * DefendingSaveBonusFactor);
		KineticResistanceLinearBonus = (int)Math.Round((float)SaveTarget * KineticResistanceLinearBonusFactor);
		KineticResistancePercentageBonus = (int)Math.Round((float)SaveTarget * KineticResistancePercentageBonusFactor);
	}


	public override bool Render(RenderEvent E)
	{
		if (Duration > 0)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (num > 50 && num < 60)
			{
				E.Tile = "bucklerparry_largerfolk/effect_fatstuck.png";
				E.RenderString = "\u000f";
				E.ColorString = "&Y^K";
			}
		}
		return true;
	}
}
}