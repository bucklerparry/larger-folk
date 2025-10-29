using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
    [Serializable]
    public class LargerFolk_MutationFatHealing: IPart
    {
        // public bool Activated = false;

        public override void Register(GameObject Object, IEventRegistrar Registrar)
        {
            Registrar.Register("Healing");
            base.Register(Object, Registrar);
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "Healing")
            {
                // GameObject gameObjectParameter = E.GetGameObjectParameter("Looker");
                if (ParentObject.HasPart<LargerFolk_WeightGain>())
                {
                    ParentObject.GetPart<LargerFolk_WeightGain>().ChangeCalories(100 * E.GetIntParameter("Amount"));
                    
                }
            }
            return base.FireEvent(E);
        }
    }
}