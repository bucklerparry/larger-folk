using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using XRL.UI;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Conversations
{
    public class LargerFolk_ShowerBodyDescription : IConversationPart
    {
        public override bool WantEvent(int ID, int Propagation)
        {
            return base.WantEvent(ID, Propagation)
                || ID == PrepareTextEvent.ID
                ;
        }

        public override bool HandleEvent(PrepareTextEvent E)
        {
            string bodyDescription = "";

            string skinType = "";

            if (The.Player.HasPart<Albino>()) skinType = skinType + " pale";
            if (The.Player.HasPart<ThickFur>()) skinType = skinType + " thick fur";
            else skinType = skinType + " skin";

            
            switch (The.Player.GetPart<LargerFolk_WeightGain>().WeightStage)
            {
                case 0:
                    bodyDescription = "You run your hands across your" + skinType + ", working moisture into every nook and cranny of your lithe physique.\nYour body is light, perhaps scrawny- Only the necessary tissues cover it, with no frills to speak of.";
                    break;
                case 1:
                    bodyDescription = "You run your hands across your" + skinType + ", working moisture into every flabby fold of your corpulent frame.\nYour body is well-fed, padded all over with a healthy softness.";
                    break;
                case 2:
                    bodyDescription = "You run your hands over your" + skinType + ", working moisture into every deep crevice of your obese bulk with some measure of difficulty.\nYour body is replete to the point of hindrance, covered in excess poundage that complicates the task of cleaning it. The face, the hands, the feet, all parts that would normally stay slender are starting to swell up in turn with how much adipose you've packed on.";
                    break;
                default:
                    bodyDescription = "You run your hands across your" + skinType + ", working moisture into every nook and cranny of your lithe physique.\nYour body is light, perhaps scrawny- Only the necessary tissues cover it, with no frills to speak of.";
                    break;
                
            }
            
            
            
            // switch (E.Text)
            // {
            //     case "body":
                    
            //         break;
            //     default:
            //         break;
            // }
            E.Text.Append(bodyDescription);
            return base.HandleEvent(E);
        }
    }
}