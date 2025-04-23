using System;
using XRL.Core;
using XRL.UI;
using XRL.World.Parts.Mutation;
using XRL.World.Parts;

namespace XRL.World.Effects
{
    [Serializable]
    public class LargerFolk_GainTonic : ITonicEffect, ITierInitialized
    {
        public float GainAmount;

        public LargerFolk_GainTonic()
        {
        }

        public LargerFolk_GainTonic(int Duration)
        {
            base.Duration = Duration;
        }

        public void Initialize(int Tier)
        {
            Duration = 15;
        }

        public override bool SameAs(Effect e)
        {
            return false;
        }

        public override bool UseStandardDurationCountdown()
        {
            return true;
        }

        public override string GetDescription()
        {
            return "{{Y|adipocytic}} tonic";
        }

        public override string GetStateDescription()
        {
            return "under the effects of {{Y|adipocytic}} tonic";
        }

        public override string GetDetails()
        {
            if (base.Object.IsTrueKin())
            {
                return "Adds 600 calories every turn.";
            }
            return "Adds 400 calories every turn.";
        }

        public override bool Apply(GameObject Object)
        {
            if (Object.IsPlayer())
            {
                Popup.Show("You feel a warm sensation in your gut as your fat cells begin to multiply.");
            }
            if (Object.GetLongProperty("Overdosing", 0L) == 1)
            {
                FireEvent(Event.New("Overdose"));
            }
            return true;
        }

        public override void Remove(GameObject Object)
        {
            if (Object.IsPlayer())
            {
                Popup.Show("The warm sensation fades throughout your softer body.");
            }
            base.Remove(Object);
        }

        public override void Register(GameObject Object, IEventRegistrar Registrar)
        {
            Registrar.Register("EndAction");
            Registrar.Register("Overdose");
            base.Register(Object, Registrar);
        }

        public override void ApplyAllergy(GameObject subject)
        {
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "EndAction")
            {
                if (base.Object.HasPart<LargerFolk_WeightGain>()) //HasStat("Hitpoints") && base.Object.HasStat("Level"))
                {
                    if (base.Object.IsTrueKin())
                    {
                        base.Object.GetPart<LargerFolk_WeightGain>().ChangeCalories(600);
                        //HealAmount += (float)Math.Max(5.0, Math.Ceiling(0.9 * (double)(float)num));
                    }
                    else
                    {
                        base.Object.GetPart<LargerFolk_WeightGain>().ChangeCalories(400);
                    }
                }
            }
            else if (E.ID == "Overdose" && Duration > 0)
            {
                if (base.Object.HasPart<TonicAllergy>())
                {
                    TonicAllergy.SalveOverdose(base.Object);
                }
                else
                {
                    Duration = 0;
                    if (base.Object.IsPlayer())
                    {
                        if (base.Object.GetLongProperty("Overdosing", 0L) == 1)
                        {
                            Popup.Show("Your mutant physiology reacts adversely to the tonic. The warm sensation fades.");
                        }
                        else
                        {
                            Popup.Show("The tonics you ingested react adversely to each other. The warm sensation fades.");
                        }
                    }
                }
            }
            return base.FireEvent(E);
        }

        public override bool Render(RenderEvent E)
        {
            int num = XRLCore.CurrentFrame % 60;
            if (Duration > 0 && num > 15 && num < 25)
            {
                E.Tile = null;
                E.RenderString = "+";
                E.ColorString = "&Y";
            }
            return true;
        }
    }
}