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


namespace XRL.World.Parts
{

    [Serializable]
    public class LargerFolk_WeightStatMod : IPart
    {
        //If this is disabled, it will not be subject to the weight gain system.
        public bool IsEnabled => XRL.UI.Options.GetOption("Option_LargerFolk_DynamicWeightGainOn").EqualsNoCase("Yes");

    

        // public LargerFolk_WeightStatMod()
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
                Registrar.Register("OnWeightStageChange");
            }
            base.Register(Object, Registrar);
        }


    

        public override bool FireEvent(Event E)
        {

            // when weight stage changes, adjust the stat mod
            if (E.ID == "OnWeightStageChange")
            {
                AfterWeightChange(E.GetIntParameter("WeightStage"));
            }

            return base.FireEvent(E);
        }

        // apply effects of current weight stage that aren't dependant on how weight stage was entered
        public void AfterWeightChange(int WeightStage)
        {
            switch (WeightStage)
            {
                case 0:
                    // StatShifter.DefaultDisplayName = "Lean";
                    StatShifter.SetStatShift(ParentObject, "Ego", 0);
                    StatShifter.SetStatShift(ParentObject, "Strength", 0);
                    break;
                case 1:
                    // StatShifter.DefaultDisplayName = "Overweight";
                    StatShifter.SetStatShift(ParentObject, "Ego", 2);
                    StatShifter.SetStatShift(ParentObject, "Strength", 2);
                    break;
                case 2:
                    // StatShifter.DefaultDisplayName = "Obesity";
                    StatShifter.SetStatShift(ParentObject, "Ego", 4);
                    StatShifter.SetStatShift(ParentObject, "Strength", 4);
                    break;
                case 3:
                    // StatShifter.DefaultDisplayName = "Extreme Obesity";
                    StatShifter.SetStatShift(ParentObject, "Ego", 6);
                    StatShifter.SetStatShift(ParentObject, "Strength", 6);
                    break; 
                default:
                    // StatShifter.DefaultDisplayName = "Lean";
                    StatShifter.SetStatShift(ParentObject, "Ego", 0);
                    StatShifter.SetStatShift(ParentObject, "Strength", 0);
                    break;
            }
        }
    }
}