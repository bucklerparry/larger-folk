using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
    [Serializable]
    public class LargerFolk_CursedMirror: IPart
    {
        public bool Activated = false;

        public override void Register(GameObject Object, IEventRegistrar Registrar)
        {
            Registrar.Register("AfterLookedAt");
            base.Register(Object, Registrar);
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "AfterLookedAt")
            {
                GameObject gameObjectParameter = E.GetGameObjectParameter("Looker");
                if (!Activated && gameObjectParameter != null && gameObjectParameter.HasPart<LargerFolk_WeightGain>())
                {
                    Activated = true;
                    
                }
            }
            return base.FireEvent(E);
        }
    }
}