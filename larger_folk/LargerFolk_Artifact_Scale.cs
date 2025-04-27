using System;
using System.Collections.Generic;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.UI;

namespace XRL.World.Parts
{

    [Serializable]
    public class LargerFolk_Artifact_Scale : IPart
    {

        public override bool WantEvent(int ID, int cascade) 
        {
            // Check if the ID parameter matches one of the events we want
            // The base WantEvent of IPart/Effect will always return false.
            return base.WantEvent(ID, cascade) || ID == ObjectEnteredCellEvent.ID;
        }

        public override bool HandleEvent(ObjectEnteredCellEvent E)
        {
            if (E.Object.HasPart<LargerFolk_WeightGain>())
            {   
                ParentObject.EmitMessage("The " + new string(ParentObject.GetDisplayName(int.MaxValue, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: true, ColorOnly: false, Visible: true, WithoutTitles: false, ForSort: false, Short: true, BaseOnly: false, WithIndefiniteArticle: false, WithDefiniteArticle: false, null, IndicateHidden: false, Capitalize: false, SecondPerson: false, Reflexive: false, true) + " dings, and reads out some statistics:\n- Excess body fat: " + (E.Object.GetPart<LargerFolk_WeightGain>().TotalCalories/32).ToString() + "lbs.\n" + "- Current weight category: " + E.Object.GetPart<LargerFolk_WeightGain>().GetWeightString()  ));
            }

            return base.HandleEvent(E);
        }
    }
}