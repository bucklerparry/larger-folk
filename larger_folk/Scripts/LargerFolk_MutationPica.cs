using System;

namespace XRL.World.Parts.Mutation
{
[Serializable]
public class LargerFolk_MutationPica : BaseMutation
{


	public LargerFolk_MutationPica()
	{
		DisplayName = "Everything-Eater";
	}

	public override bool CanLevel()
	{
		return false;
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
		if (!Variant.IsNullOrEmpty())
		{
			return "Your stomach is hardy, and can digest nearly anything as though it was food.";
		}
		return "Your stomach is hardy, and can digest nearly anything as though it was food.";
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