using System;

namespace XRL.World.Parts.Mutation
{
[Serializable]
public class LargerFolk_Fat : BaseDefaultEquipmentMutation
{
	public GameObject BellyObject;

	public string BodyPartType = "Body";

	public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
	{
		LargerFolk_Fat obj = base.DeepCopy(Parent, MapInv) as LargerFolk_Fat;
		obj.BellyObject = null;
		return obj;
	}

	public LargerFolk_Fat()
	{
		DisplayName = "Fat";
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
		if (!Variant.IsNullOrEmpty())
		{
			return "Your figure has widened thanks to excessive snacking. Your body is burdened with plush adipose.";
		}
		return "Your figure has widened thanks to excessive snacking. Your body is burdened with plush adipose.";
	}

	public override string GetLevelText(int Level)
	{
		return "";
	}

	public override void OnRegenerateDefaultEquipment(Body body)
	{
		if (!TryGetRegisteredSlot(body, BodyPartType, out var Part))
		{
			Part = body.GetFirstPart(BodyPartType);
			if (Part != null)
			{
				RegisterSlot(BodyPartType, Part);
			}
		}
		if (Part != null)
		{
			BellyObject = GameObjectFactory.Factory.CreateObject(Variant ?? "Fat Belly");
			Armor part2 = BellyObject.GetPart<Armor>();
			
			
			part2.WornOn = Part.Type;
			part2.AV = 2;
			//part2.CarryBonus = -60;
			Part.DefaultBehavior = BellyObject;
			Part.DefaultBehavior.SetStringProperty("TemporaryDefaultBehavior", "Fat Belly");
			DisplayName = GetVariantName() ?? "Fat Belly";
		}
		base.OnRegenerateDefaultEquipment(body);
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
		CleanUpMutationEquipment(GO, ref BellyObject);
		return base.Unmutate(GO);
	}
}
}