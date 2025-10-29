using System;
using XRL.UI;
using XRL.World.Parts;
using LargerFolk.Utilities;

namespace XRL.World.Effects
{
    // basically this is just to display the armour's size in the status lol
    [Serializable]
    public class LargerFolk_Effect_ClothingSize : Effect, ITierInitialized
    {
        public string SizeString = "S";

        public LargerFolk_Effect_ClothingSize()
        {
            DisplayName = "{{C|size: " + SizeString + "}}";
            Duration = 1;
        }

        // public LargerFolk_Effect_Tight(string SizeString : s)
        //     : this()
        // {
        //     this.SizeString = SizeString;
        // }

        public override int GetEffectType()
        {
            return 100664320;
        }

       

        public override bool Apply(GameObject Object)
        {
            SizeString = Object.GetPart<LargerFolk_Armor>().GetSizeString();
            DisplayName = "{{C|size: " + SizeString + "}}";
            Duration = 1;
            return true;
        }

        public override bool WantEvent(int ID, int cascade)
        {
            return
                base.WantEvent(ID, cascade)
                || ID == GetDisplayNameEvent.ID
            ;
        }

        public override bool HandleEvent(GetDisplayNameEvent E)
        {
            // if (E.Understood() && !E.Object.HasProperName && !E.Object.IsNatural())
            // {
            //     if (StageDifference > 2)
            //     {
            //         E.AddAdjective("{{r|bursting}}");
            //     }
            //     else if (StageDifference > 1)
            //     {
            //         E.AddAdjective("{{R|straining}}");
            //     }
            //     else if (StageDifference >= 0)
            //     {
                    
            //     }
            // }
            return base.HandleEvent(E);
        }

        public override string GetDetails()
	    {
		    return "size: " + SizeString;
	    }
        
        public void RemoveSize()
        {
            base.Object.RemoveEffect(this);
        }
    }
}