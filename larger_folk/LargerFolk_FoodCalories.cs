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

        public override void Register(GameObject Object, IEventRegistrar Registrar)
	    {
            if (IsEnabled)
            {
		        Registrar.Register("OnEat");
            }
		    base.Register(Object, Registrar);
	    }
        
        public override bool FireEvent(Event E)
        {
            if (E.ID == "OnEat")
            {
                GameObject actor = E.GetGameObjectParameter("Eater");
                if (actor.HasPart<LargerFolk_WeightGain>())
                {
                    LargerFolk_WeightGain weight_part = actor.GetPart<LargerFolk_WeightGain>();
                    weight_part.ChangeCalories(Calories);
                }
            }

            return base.FireEvent(E);
        }
    }
}