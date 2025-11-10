using System;
using XRL.UI;
using XRL.World.Parts;
using LargerFolk.Utilities;

namespace XRL.World.Effects
{
    // this is the effect that actually applies the mechanical effects of weight
    [Serializable]
    public class LargerFolk_Effect_Weight : Effect, ITierInitialized
    {
        public int WeightStage;

        public string DetailString = "";

        public int AVShift = 0;
        public int DVShift = 0;
        public int MSShift = 0;

       
        public LargerFolk_Effect_Weight()
        {
            DisplayName = "{{r|overweight}}";
            Duration = 1;
        }

        public LargerFolk_Effect_Weight(int WeightStage)
            : this()
        {
            this.WeightStage = WeightStage;
        }

        public override int GetEffectType()
        {
            return 67108896; // taken from "liquid-covered" effect, so it should apply to any creature with a weightgain part
        }

       

        public override bool Apply(GameObject Object)
        { 
            ApplyStats();
            
            return true;
        }

        // public override bool WantEvent(int ID, int cascade)
        // {
        //     if (!base.WantEvent(ID, cascade) && ID != AdjustValueEvent.ID && ID != SingletonEvent<BeginTakeActionEvent>.ID && ID != PooledEvent<GetDisplayNameEvent>.ID && ID != PooledEvent<IsRepairableEvent>.ID)
        //     {
        //         return ID == PooledEvent<RepairedEvent>.ID;
        //     }
        //     return true;
        // }

        // public override bool HandleEvent(GetDisplayNameEvent E)
        // {
        //     if (!E.Reference)
        //     {
        //         if (StageDifference > 2)
        //         {
        //             E.AddTag("({{r|bursting}})", 20);
        //         }
        //         else if (StageDifference > 1)
        //         {
        //             E.AddTag("({{r|straining}})", 20);
        //         }
        //         else if (StageDifference >= 0)
        //         {
        //             E.AddTag("({{r|ill-fitting}})", 20);
        //         }
        //     }
        //     return base.HandleEvent(E);
        // }
        // public override bool HandleEvent(GetDisplayNameEvent E)
        // {
        //     if (E.Understood() && !E.Object.HasProperName && !E.Object.IsNatural())
        //     {
        //         if (StageDifference > 2)
        //         {
        //             E.AddAdjective("{{r|bursting}}");
        //         }
        //         else if (StageDifference > 1)
        //         {
        //             E.AddAdjective("{{R|straining}}");
        //         }
        //         else if (StageDifference >= 0)
        //         {
        //             E.AddAdjective("{{o|ill-fitting}}");
        //         }
        //     }
        //     return base.HandleEvent(E);
        // }




        public override void Register(GameObject Object, IEventRegistrar Registrar)
        {
            base.Register(Object, Registrar);
        }

        public override bool FireEvent(Event E)
        {
            
            return base.FireEvent(E);
        }


        public override string GetDetails()
        {
            return DetailString;
        }
        
        void ApplyStats()
        {
            switch (WeightStage)
            {
                case 0:
                    DisplayName = "{{C|lean}}";
                    break;
                case 1:
                    DisplayName = "{{c|fat}}";
                    break;
                case 2:
                    DisplayName = "{{B|obese}}";
                    break;
                case 3:
                    DisplayName = "{{b|barely mobile}}";
                    break;
            }

            if (base.Object.IsPlayer())
            {
                switch (WeightStage)
                {
                    case 0:
                        // StatShifter.DefaultDisplayName = "Lean";
                        AVShift = 0;
                        DVShift = 0;
                        MSShift = 0;
                        break;
                    case 1:
                        // StatShifter.DefaultDisplayName = "Overweight";
                        AVShift = 0;
                        DVShift = 0;
                        MSShift = 10;
                        break;
                    case 2:
                        // StatShifter.DefaultDisplayName = "Obesity";
                        AVShift = 1;
                        DVShift = -1;
                        MSShift = 30;
                        break;
                    case 3:
                        // StatShifter.DefaultDisplayName = "Extreme Obesity";
                        AVShift = 2;
                        DVShift = -3;
                        MSShift = 75;
                        break;
                    default:
                        // StatShifter.DefaultDisplayName = "Lean";
                        AVShift = 0;
                        DVShift = 0;
                        MSShift = 0;
                        break;
                }
            }
            else
            {
                switch (WeightStage)
                {
                    case 0:
                        // StatShifter.DefaultDisplayName = "Lean";
                        AVShift = 0;
                        DVShift = 0;
                        MSShift = 0;
                        break;
                    case 1:
                        // StatShifter.DefaultDisplayName = "Overweight";
                        AVShift = 0;
                        DVShift = 0;
                        MSShift = 15;
                        break;
                    case 2:
                        // StatShifter.DefaultDisplayName = "Obesity";
                        AVShift = 1;
                        DVShift = -2;
                        MSShift = 30;
                        break;
                    case 3:
                        // StatShifter.DefaultDisplayName = "Extreme Obesity";
                        AVShift = 2;
                        DVShift = -3;
                        MSShift = 60;
                        break;
                    default:
                        AVShift = 0;
                        DVShift = 0;
                        MSShift = 0;
                        
                        break;
                }
            }
            
            StatShifter.SetStatShift(base.Object, "AV", AVShift);
            StatShifter.SetStatShift(base.Object, "DV", DVShift);
            StatShifter.SetStatShift(base.Object, "MoveSpeed", MSShift);

            DetailString = "";
            if (AVShift != 0) { DetailString += "+" + AVShift.ToString() + " AV, "; }
            if (DVShift != 0) { DetailString += DVShift.ToString() + " DV, "; }
            if (MSShift != 0) { DetailString += (-MSShift).ToString() + " Movespeed"; }

        }

        private void UnapplyStats()
        {
            // Armor armor = base.Object?.GetPart<Armor>();
            // if (armor != null)
            // {
            //     armor.DV += StageDifference;
            // }
        }

        public void AdjustWeightEffect(int new_WeightStage)
        {
            WeightStage = new_WeightStage;
            ApplyStats();
        }
    }
}