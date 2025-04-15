using System;

namespace XRL.World.Parts.Mutation
{
[Serializable]
public class LargerFolk_FatFetish : BaseMutation
{
	

	public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
	{
		LargerFolk_FatFetish obj = base.DeepCopy(Parent, MapInv) as LargerFolk_FatFetish;
		
		return obj;
	}

	public LargerFolk_FatFetish()
	{
		DisplayName = "Fat Fetish";
	}

	public override bool CanLevel()
	{
		return false;
	}

	public override bool GeneratesEquipment()
	{
		return true;
	}

	public override void Register(GameObject Object, IEventRegistrar Registrar)
	{
		base.Register(Object, Registrar);
	}

	public override bool FireEvent(Event E)
	{
		return base.FireEvent(E);
	}

	public override string GetDescription()
	{
		return "You harbour a deep-set desire for flabby flesh growing softer, larger, wider.\nOpens certain dialogue options.\n-EGO versus opponents with fat-related mutations (unimplemented)";
	}

	public override string GetLevelText(int Level)
	{
		return "";
	}

	public override bool ChangeLevel(int NewLevel)
	{
		return base.ChangeLevel(NewLevel);
	}

	public override bool Mutate(GameObject GO, int Level)
	{
		return base.Mutate(GO, Level);
	}

	public override bool Unmutate(GameObject GO)
	{
		return base.Unmutate(GO);
	}
}
}