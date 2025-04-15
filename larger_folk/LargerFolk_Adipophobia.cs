using System;

namespace XRL.World.Parts.Mutation
{
[Serializable]
public class LargerFolk_Adipophobia : BaseMutation
{
	

	public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
	{
		LargerFolk_Adipophobia obj = base.DeepCopy(Parent, MapInv) as LargerFolk_Adipophobia;
		
		return obj;
	}

	public LargerFolk_Adipophobia()
	{
		DisplayName = "Adipophobia";
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
		return "You've kept a trim figure your whole life, and don't welcome any changes that may come to it.\nCertain descriptive elements will reflect this.";
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