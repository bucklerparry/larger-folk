using System;
using System.Collections.Generic;
using System.Text;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.Rules;
using XRL.World.Parts.Mutation;

using LargerFolk.Utilities;


namespace XRL.World.Parts
{

[Serializable]
public class LargerFolk_WeightPhysics : IPart
{
    //If this is disabled, it will not be subject to the weight gain system.
    public bool IsEnabled => XRL.UI.Options.GetOption("Option_LargerFolk_DynamicWeightGainOn").EqualsNoCase("Yes");

    bool Initialized = false;

 

    // public LargerFolk_WeightPhysics()
    // {
    //     if (!IsEnabled)
    //     {
    //         WeightStage = 0;
    //         TotalCalories = 0;
    //     }
    // }

    public override void Register(GameObject Object, IEventRegistrar Registrar)
	{
        if (IsEnabled)
        {
            Registrar.Register("AfterMoved");
            Registrar.Register("OnWeightStageChange");
            
        }
		base.Register(Object, Registrar);
	}

    // Listens for one event always.
    public override bool WantEvent(int ID, int cascade) 
    {
	    // Check if the ID parameter matches one of the events we want
	    // The base WantEvent of IPart/Effect will always return false.
	    return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID || ID == AdjustWeightEvent.ID || ID == FellDownEvent.ID || ID == EnteringCellEvent.ID;
    }

    // public override bool WantEvent(int ID, int cascade) 
    // {
	//     return base.WantEvent(ID, cascade) || ID == GetDisplayNameEvent.ID;
    // }

    // crush enemies with ur butt by falling on them. mayyyyy be multiphase lol
    public override bool HandleEvent(FellDownEvent E)
    {
        
        //  impactCells = ParentObject.CurrentCell.GetAdjacentCells(false);
        // Popup.Show("hit groundssss!" + impactCells.Count.ToString());
        // for (int i = 0; i < impactCells.Count; i++)
        // {
        //     TryButtImpact(impactCells[i]);
        // }
        // List<Cell>

        return base.HandleEvent(E);
    }

    public void TryFullButtImpact(List<Cell> impactCells)
    {

        for (int i = 0; i < impactCells.Count; i++)
        {
            TryButtImpact(impactCells[i]);
        }
    }
    
    public void TryButtImpact(Cell c)
    {
        List<GameObject> objs = c.GetObjects();
        int actorSize = ParentObject.GetPart<LargerFolk_WeightGain>().WeightStage;
        if (actorSize > 0)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                ParentObject.EmitMessage("just sat on " + objs[i].pRender.DisplayName);
                if (objs[i] != ParentObject)
                {
                    GameObject obj = objs[i];
                    int dmg = 20; //LargerFolk_Random.d6x(actorSize * actorSize) * (E.Distance)*2;
                    obj.TakeDamage(dmg, "from the impact of %t " + LargerFolk_Language.AjSize(actorSize) + " rump!", "Crushing", "You were flattened under " + Grammar.MakePossessive(ParentObject.an(int.MaxValue, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: false, Short: true, BaseOnly: false, IndicateHidden: false, SecondPerson: true, Reflexive: false, null)) + LargerFolk_Language.AjSize(actorSize) + " rump.", obj.It + " was flattened  under " + Grammar.MakePossessive(ParentObject.an(int.MaxValue, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: false, Short: true, BaseOnly: false, IndicateHidden: false, SecondPerson: true, Reflexive: false, null)) + LargerFolk_Language.AjSize(actorSize) + " ass.", Attacker: ParentObject, Owner: ParentObject, Source: ParentObject);
                    // obj.TakeDamage(dmg, "from the impact of %t " + LargerFolk_Language.AjSize(actorSize) + " rump!", "Crushing", "You were flattened under " + ParentObject.an(int.MaxValue, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: false, Short: true, BaseOnly: false, IndicateHidden: false, SecondPerson: true, Reflexive: false, null) + "'s girth.", obj.Its + " was flattened  under " + ParentObject.an(int.MaxValue, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: false, Short: true, BaseOnly: false, IndicateHidden: false, SecondPerson: true, Reflexive: false, null) + " fat.", Attacker: ParentObject, Owner: ParentObject, Source: ParentObject);
                    //TakeDamage(ref int Amount, string Attributes = null, string DeathReason = null, string ThirdPersonDeathReason = null, GameObject Owner = null, GameObject Attacker = null, GameObject Source = null, GameObject Perspective = null, GameObject DescribeAsFrom = null, string Message = "from %t attack.", bool Accidental = false, bool Environmental = false, bool Indirect = false, bool ShowUninvolved = false, bool IgnoreVisibility = false, bool ShowForInanimate = false, bool SilentIfNoDamage = false, bool NoSetTarget = false, bool UsePopups = false, int Phase = 5, string ShowDamageType = null)

                }
            }
        }
    }

    public override bool HandleEvent(EnteringCellEvent E)
    {

        // for (int i = 0; i < E.Cell.GetObjects().Count; i++)
        // {
        //     ParentObject.EmitMessage(E.Cell.GetObjects()[i].pRender.DisplayName);
        // }

        return base.HandleEvent(E);
    }

    public override bool HandleEvent(ObjectCreatedEvent E)
    {
        // WeightSetup();

        return base.HandleEvent(E);
    }

    // public override bool HandleEvent(GetIntrinsicWeightEvent E)
	// {
	// 	// E.Weight += TotalCalories/32;
	// 	return base.HandleEvent(E);
	// }
     public override bool HandleEvent(AdjustWeightEvent E)
    {
        // E.Weight *= 1 + TotalCalories/10000.0;
        E.Weight += TotalCalories/32;
        return base.HandleEvent(E);
    }

    public override bool FireEvent(Event E)
    {
        // if (E.ID == "BeforeApplyDamage")
        // {
        //     Damage damage = E.GetParameter("Damage") as Damage;
		// 	if (damage.HasAttribute("Falling"))
		// 	{
		// 		if (ParentObject.GetPart<LargerFolk_WeightGain>().WeightStage > 0)
		// 		{
		// 			damage.Amount -= damage.Amount/5 * ParentObject.GetPart<LargerFolk_WeightGain>().WeightStage;
		// 		}
        //     }
            
        // }
        return base.FireEvent(E);
    }

}

}