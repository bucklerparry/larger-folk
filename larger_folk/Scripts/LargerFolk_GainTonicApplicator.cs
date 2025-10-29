using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
    [Serializable]
    public class LargerFolk_GainTonicApplicator : IPart
    {
        public override bool SameAs(IPart p)
        {
            return true;
        }

        public override bool WantEvent(int ID, int cascade)
        {
            if (!base.WantEvent(ID, cascade))
            {
                return ID == GetUtilityScoreEvent.ID;
            }
            return true;
        }

        public override bool HandleEvent(GetUtilityScoreEvent E)
        {
            if (E.Damage == null)
            {
                double num = E.Actor.Health();
                if (E.ForPermission)
                {
                    if (num < 1.0)
                    {
                        E.ApplyScore(1);
                    }
                }
                else if (num < 0.5)
                {
                    E.ApplyScore((int)(5.0 / num));
                }
            }
            return base.HandleEvent(E);
        }

        public override void Register(GameObject Object, IEventRegistrar Registrar)
        {
            Registrar.Register("ApplyTonic");
            base.Register(Object, Registrar);
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "ApplyTonic")
            {
                int intParameter = E.GetIntParameter("Dosage");
                if (intParameter <= 0)
                {
                    return false;
                }
                GameObject gameObjectParameter = E.GetGameObjectParameter("Actor");
                GameObject gameObjectParameter2 = E.GetGameObjectParameter("Subject");
                int @for = GetTonicDurationEvent.GetFor(ParentObject, gameObjectParameter, gameObjectParameter2, "Salve", 18 * intParameter, intParameter, Healing: true);
                // gameObjectParameter2.FireEvent("ApplyingSalve");
                LargerFolk_GainTonic e = new LargerFolk_GainTonic(@for);
                if (!gameObjectParameter2.ApplyEffect(e))
                {
                    return false;
                }
                gameObjectParameter2.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality");
            }
            return base.FireEvent(E);
        }
    }
}