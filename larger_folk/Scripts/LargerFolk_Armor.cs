using System;
using System.CodeDom.Compiler;
using System.Text;
using Occult.Engine.CodeGeneration;
using XRL.UI;
using XRL.Rules;
using XRL.World.Anatomy;
using XRL.World.Parts;
using XRL.World.Effects;
using LargerFolk.Utilities;

namespace XRL.World.Parts
{
    [Serializable]
    [GenerateSerializationPartial]
    public class LargerFolk_Armor : IPart
    {
        public bool IsEnabled => (XRL.UI.Options.GetOption("Option_LargerFolk_DynamicWeightGainOn").EqualsNoCase("Yes") && XRL.UI.Options.GetOption("Option_LargerFolk_ArmorEffects").EqualsNoCase("Yes"));

        // if armour has buttons it's subject to a button pop roll if it breaks from weight gain
        // button is a throw-range missile projectile that deals 1d2 damage * item tier
        // public bool HasButtons = false;

        // For purposes of determining wardrobe malfunctions, item breakage etc, this value is subtracted from the wearer's weight stage. 
        // the higher it is, the fatter you can be while wearing it.
        public int WeightThresholdModifier = 0;

        //0 = Small, 1 = Medium, 2 = Large, 3 = Extra Large
        // corresponds to weight stages, being over has tightness consequences, being under has looseness consequences
        public int TargetSize = 0;

        public GameObject WornBy;

        public string OutsizeString1 = "Your ";
        public string OutsizeString2 = " feels tighter...";

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
                || ID == GetDisplayNameEvent.ID
                || ID == ObjectCreatedEvent.ID 
            ;
        }

        public override bool HandleEvent(GetDisplayNameEvent E)
        {
            // if (E.Understood() && !E.Object.IsNatural())
            // {
            //     E.AddAdjective("{{Y|" + GetSizeString() + "}}");
            // }
            return base.HandleEvent(E);
        }

        public override bool HandleEvent(ObjectCreatedEvent E)
        {
            int itemTier = ParentObject.GetTier();

            int roll = LargerFolk_Random.Next(1,10);

            if (roll > itemTier)
            {
                TargetSize = 0;
            }
            else if (roll < itemTier)
            {
                roll = LargerFolk_Random.Next(1,itemTier);
                if (roll >= 5)
                {
                    TargetSize = 3;
                }
                else if (roll >= 4)
                {
                    TargetSize = 2;
                }
                else 
                {
                    TargetSize = 1;
                }
            }

            UpdateSize();

            return base.HandleEvent(E);
        }
        
        public override bool HandleEvent(EquippedEvent E)
        {
            if (IsEnabled)
            {
                WornBy = E.Actor;
                E.Actor.RegisterPartEvent(this, "OnWeightStageChange");
                int wearerWeightStage = E.Actor.GetPart<LargerFolk_WeightGain>().WeightStage;
                
                // if the wearer is too fat for the armour, give it the Tight effect.
                if (wearerWeightStage > TargetSize)
                {
                    ParentObject.ApplyEffect(new LargerFolk_Effect_Tight(
                        StageDifference: (wearerWeightStage - TargetSize),
                        FromEquip: true) );
                }
                // else if (wearerWeightStage < TargetSize)
                // {

                // }
            }
            return base.HandleEvent(E);
        }

        public override bool HandleEvent(UnequippedEvent E)
        {
            if (IsEnabled)
            {
                E.Actor.UnregisterPartEvent(this, "OnWeightStageChange");
            }
            return base.HandleEvent(E);
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "OnWeightStageChange")
            {
                // Popup.Show("Weight Stage Changed");
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
                        Actor.EmitMessage(OutsizeString1 + 
                        ParentObject.ShortDisplayNameStripped
                        + OutsizeString2);
                         
                    }

                    // if (Stat.Random(1, 1) == 1) //(10 - (CurrentStage*3) )
                    // {
                    //     if ( (!ParentObject.HasPart<ModSturdy>()) && ParentObject.ApplyEffect(new Broken()) && IsBroken())
                    //     {
                    //         if (Actor.IsPlayer())
                    //         {
                    //             Actor.EmitMessage(new string("Your " + Actor.GetPart<LargerFolk_WeightGain>().GetWeightString() + " belly proves to much for your armor to contain, and flops outward from its ruptured confines."));
                    //         }

                    //         // if (ParentObject.HasPart<LargerFolk_ButtonPop>())
                    //         // {
                    //         //     ParentObject.GetPart<LargerFolk_ButtonPop>().PopButton();
                    //         // }
                    //     }
                    // }
                    //if (ParentObject.ApplyEffect(new Broken()) && IsBroken())
                    //{
                    //}
                }
                else
                {
                    if (Actor.IsPlayer())
                    {
                        Actor.EmitMessage("Your " + 
                        ParentObject.ShortDisplayNameStripped
                        + " feels a bit more comfortable. You're growing into it nicely.");
                         
                    }
                }
            }
            // if slimmed
            else if (StageChange < 0)
            {
                if (CurrentStage >= TargetSize)
                {
                    if (Actor.IsPlayer())
                    {
                        Actor.EmitMessage("Your " + 
                        ParentObject.ShortDisplayNameStripped
                        + " aren't feeling quite so tight.");
                    }
                }
                else
                {
                    if (Actor.IsPlayer())
                    {
                        Actor.EmitMessage("Your " + 
                        ParentObject.ShortDisplayNameStripped
                        + " are feeling a bit looser");
                    }
                }
            }

            UpdateTightness(CurrentStage);

			return;
		}

        public void UpdateTightness(int CurrentStage)
        {
            if (CurrentStage > TargetSize)
            {
                if (ParentObject.HasEffect<LargerFolk_Effect_Tight>())
                {
                    ParentObject.RemoveEffect(ParentObject.GetEffect<LargerFolk_Effect_Tight>());
                }
                ParentObject.ApplyEffect(new LargerFolk_Effect_Tight(
                StageDifference: (CurrentStage - TargetSize),
                FromGrowth: true) );
            }
            else if (CurrentStage <= TargetSize && ParentObject.HasEffect<LargerFolk_Effect_Tight>())
            {
                ParentObject.RemoveEffect(ParentObject.GetEffect<LargerFolk_Effect_Tight>());
            }

        }
        // public string GetDescription(int Tier)
	    // {
        //     int weightStage = ParentObject.GetPart<LargerFolk_WeightGain>().WeightStage;
            
        //     if (weightStage > TargetSize)
        //     {
        //         return "Tight: Your figure is rounder than this item can comfortably handle, item may break under duress.";
        //     }
        //     else if (weightStage < TargetSize)
        //     {
        //         return "Loose: This item is too large for you, and impedes movement. -1 DV";
        //     }
        //     else
        //     {
        //         return "";
        //     }
		    
	    // }

        public void Tailor(int sizeChange)
        {
            TargetSize += sizeChange;
            if (TargetSize < 0){ TargetSize = 0; }
            UpdateSize();
            UpdateTightness(WornBy.GetPart<LargerFolk_WeightGain>().WeightStage);
        }
        public void TailorToWearer()
        {
            TargetSize = WornBy.GetPart<LargerFolk_WeightGain>().WeightStage;
            UpdateSize();
            UpdateTightness(TargetSize);
        }

        public void UpdateSize()
        {
            if (ParentObject.HasEffect<LargerFolk_Effect_ClothingSize>())
            {
                ParentObject.GetEffect<LargerFolk_Effect_ClothingSize>().RemoveSize();
            }
            ParentObject.ApplyEffect(new LargerFolk_Effect_ClothingSize());
        }
        
        public string GetSizeString()
        {

            switch (TargetSize)
            {
                case 0:
                    return "S";
                case 1:
                    return "M";
                case 2:
                    return "L";
                case 3:
                    return "XL";
                // if greater than 3, just add as many Xs as higher than 3 it is loooll
                default: 
                    string s = "XL";
                    if (TargetSize > 3)
                    {
                        int n = 0;
                        for (int i = 0; i < TargetSize - 3; i++)
                        {
                            s = 'X' + s;
                        }
                    }
                    
                    return s;

            }

            
        }

    }
}