using System;
using System.CodeDom.Compiler;
using System.Text;
using Occult.Engine.CodeGeneration;
using XRL.UI;
using XRL.Rules;
using XRL.World.Anatomy;
using XRL.World.Parts;
using XRL.World.Effects;


namespace XRL.World.Parts
{
    [Serializable]
    [GenerateSerializationPartial]
    public class LargerFolk_Armor : IPart
    {
        public bool IsEnabled => (XRL.UI.Options.GetOption("Option_LargerFolk_DynamicWeightGainOn").EqualsNoCase("Yes") && XRL.UI.Options.GetOption("Option_LargerFolk_ArmorEffects").EqualsNoCase("Yes"));

        // if armour has buttons it's subject to a button pop roll if it breaks from weight gain
        // button is a throw-range missile projectile that deals 1d2 damage * item tier
        public bool HasButtons = false;
        // For purposes of determining wardrobe malfunctions, item breakage etc, this value is subtracted from the wearer's weight stage. 
        // the higher it is, the fatter you can be while wearing it.
        public int WeightThresholdModifier = 0;

        //0 = Small, 1 = Medium, 2 = Large, 3 = Extra Large
        // corresponds to weight stages, being over has tightness consequences, being under has looseness consequences
        public int TargetSize = 0;

        public override bool SameAs(IPart p)
        {
            return false;
        }

        public override bool AllowStaticRegistration()
        {
            return false; // uses dynamic registration
        }

        public override void Register(GameObject Object, IEventRegistrar Registrar)
	    {
            if (IsEnabled)
		    {Registrar.Register("OnWeightStageChange");}
		    base.Register(Object, Registrar);
	    }

        public override bool WantEvent(int ID, int cascade)
        {
            return
                base.WantEvent(ID, cascade)
                || ID == EquippedEvent.ID
                || ID == UnequippedEvent.ID
            ;
        }

        public override bool HandleEvent(EquippedEvent E)
        {
            if (IsEnabled)
            {E.Actor.RegisterPartEvent(this, "OnWeightStageChange");}
            return base.HandleEvent(E);
        }

        public override bool HandleEvent(UnequippedEvent E)
        {
            if (IsEnabled)
            {E.Actor.UnregisterPartEvent(this, "OnWeightStageChange");}
            return base.HandleEvent(E);
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "OnWeightStageChange")
            {
                Popup.Show("Weight Stage Changed");
                // Natural equipment does not change tightness on weight gain nor is it vulnerable to weight related breakage
                if (!ParentObject.HasPart<NaturalEquipment>())
                {
                    // if creature has gained or slimmed into their current weight stage...
                    if (E.GetIntParameter("WeightStage") != E.GetIntParameter("LastWeightStage"))
                    {
                        ChangeTightness( (E.GetIntParameter("WeightStage") - E.GetIntParameter("LastWeightStage")), E.GetIntParameter("WeightStage"), E.GetGameObjectParameter("Actor"));
                    }
                }
            }

            return base.FireEvent(E);
        }

        
        public void ChangeTightness(int StageChange, int CurrentStage, GameObject Actor)
		{
            // if gained
            if (StageChange > 0)
            {
                if (CurrentStage > TargetSize)
                {
                    if (Actor.IsPlayer())
                    {
                        Actor.EmitMessage("Your armor feels tighter...");
                        
                    }

                    if (Stat.Random(1, 1) == 1) //(10 - (CurrentStage*3) )
                    {
                        if ( (!ParentObject.HasPart<ModSturdy>()) && ParentObject.ApplyEffect(new Broken()) && IsBroken())
                        {
                            if (Actor.IsPlayer())
                            {
                                Actor.EmitMessage(new string("Your " + Actor.GetPart<LargerFolk_WeightGain>().GetWeightString() + " belly proves to much for your armor to contain, and flops outward from its ruptured confines."));
                            }

                            if (ParentObject.HasPart<LargerFolk_ButtonPop>())
                            {
                                ParentObject.GetPart<LargerFolk_ButtonPop>().PopButton();
                            }
                        }
                    }
                    //if (ParentObject.ApplyEffect(new Broken()) && IsBroken())
                    //{
                    //}
                }
            }
            // if slimmed
            else if (StageChange < 0)
            {if (Actor.IsPlayer())
                {
                    Actor.EmitMessage("Your armor feels looser.");
                }
            }

			return;
		}
    }
}