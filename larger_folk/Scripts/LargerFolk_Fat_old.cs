using System;
using System.Text;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts.Mutation
{
[Serializable]
public class LargerFolk_Fat_old : BaseMutation
{
	public int Bonus;

	public int Penalty;

	public LargerFolk_Fat_old()
	{
		DisplayName = "Fat";
		base.Type = "Physical";
	}

	public override bool WantEvent(int ID, int cascade)
	{
		if (!base.WantEvent(ID, cascade))
		{
			return ID == PooledEvent<GetItemElementsEvent>.ID;
		}
		return true;
	}
	//public int GetWeight()
	//{
	//	if (WeightCache == -1)
	//	{
	//		return RecalculateWeight();
	//	}
	//	return WeightCache + 25;
	//}
	public override bool HandleEvent(GetItemElementsEvent E)
	{
		if (E.IsRelevantCreature(ParentObject))
		{
			E.Add("travel", 1);
		}
		return base.HandleEvent(E);
	}

	public override void Register(GameObject Object, IEventRegistrar Registrar)
	{
		base.Register(Object, Registrar);
	}

	public int GetSpeedBonus(int Level)
	{
		return 5 + 2 * Level;
	}

	public override string GetDescription()
	{
		return "Your figure has widened thanks to excessive snacking. Your plush body has trouble moving quickly, at least, not without undue jiggling.";
	}

	public override string GetLevelText(int Level)
	{
		return "-{{rules|" + GetSpeedBonus(Level) + "}} Quickness";
	}

	public override bool FireEvent(Event E)
	{
		return base.FireEvent(E);
	}

	public override bool ChangeLevel(int NewLevel)
	{
		ParentObject.Statistics["Speed"].BaseValue -= Bonus;
		Bonus = -GetSpeedBonus(base.Level);
		ParentObject.Statistics["Speed"].BaseValue += Bonus;
		return base.ChangeLevel(NewLevel);
	}

	public override bool Mutate(GameObject GO, int Level)
	{
		return base.Mutate(GO, Level);
	}

	public override bool Unmutate(GameObject GO)
	{
		GO.Statistics["Speed"].BaseValue -= Bonus;
		Bonus = 0;
		return base.Unmutate(GO);
	}
}
}