using System;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
    

    [Serializable]
    public class LargerFolk_FoodCalories : IPart
    {
        public bool IsEnabled => XRL.UI.Options.GetOption("Option_LargerFolk_DynamicWeightGainOn").EqualsNoCase("Yes");

        public int Calories = 1000;

        GameObject next_eater = null;

        public override void Register(GameObject Object, IEventRegistrar Registrar)
	    {
            if (IsEnabled)
            {
		        Registrar.Register("OnEat");
                Registrar.Register("AfterEat");
                Registrar.Register("ThrownProjectileHit");
            }
		    base.Register(Object, Registrar);
	    }
        
        public override bool FireEvent(Event E)
        {
            if (E.ID == "OnEat")
            {
                next_eater = E.GetGameObjectParameter("Eater");
                
            }
            else if (E.ID == "AfterEat")
            {
                if (next_eater != null && next_eater.HasPart<LargerFolk_WeightGain>())
                {
                    LargerFolk_WeightGain weight_part = next_eater.GetPart<LargerFolk_WeightGain>();
                    weight_part.ChangeCalories(Calories);
                    next_eater = null;
                }
            }
            // throwing food at a weight-gaining creature with an uncovered face makes them eat it, applying the calories
            else if (E.ID == "ThrownProjectileHit")
            {
                GameObject target = E.GetGameObjectParameter("Defender");
                if (target.HasPart<LargerFolk_Stomach>())
                {
                    LargerFolk_WeightGain weight_part = target.GetPart<LargerFolk_WeightGain>();
                    weight_part.ChangeCalories(Calories);

                    ParentObject.Destroy();
                }
            }

            return base.FireEvent(E);
        }
    }
}