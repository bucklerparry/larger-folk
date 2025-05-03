using XRL.World.Parts;
using XRL.World.Conversations;

using System;
using System.Collections.Generic;
using System.Text;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.Rules;
using XRL.World.Parts.Mutation;


[HasConversationDelegate] // This is required on the surrounding class to reduce the search complexity.
public static class LargerFolk_ConversationDelegates
{
    // A predicate that receives a DelegateContext object with our values assigned, this to protect mods from signature breaks.
    [ConversationDelegate(Speaker = true)]
    public static bool IfFat(DelegateContext Context)
    {
        // Context.Value holds the quoted value from the XML attribute.
        // Context.Target holds the game object.
        // Context.Element holds the parent element. assss

        bool GainEnabled = XRL.UI.Options.GetOption("Option_LargerFolk_DynamicWeightGainOn").EqualsNoCase("Yes");

        // if dynamic_gain is disabled, always return that the speaker/player is fat.
        if (Context.Value == "true")
        {
            

            if (GainEnabled)
            {
                return Context.Target.GetPart<LargerFolk_WeightGain>().WeightStage > 0;// HasObjectInInventory(Context.Value);
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (GainEnabled)
            {
                return Context.Target.GetPart<LargerFolk_WeightGain>().WeightStage < 1;// HasObjectInInventory(Context.Value);
            }
            else
            {
                return false;
            }
        }
    }
    
    [ConversationDelegate(Speaker = true)]
    public static bool IfWeightStage(DelegateContext Context)
    {
        // Context.Value holds the quoted value from the XML attribute.
        // Context.Target holds the game object.
        // Context.Element holds the parent element. assss
        
        bool GainEnabled = XRL.UI.Options.GetOption("Option_LargerFolk_DynamicWeightGainOn").EqualsNoCase("Yes");

        // if dynamic_gain is disabled, always return that the speaker/player is fat.
        if (GainEnabled)
        {
            Popup.Show(Context.Target.GetPart<LargerFolk_WeightGain>().WeightStage.ToString());
            return Context.Target.GetPart<LargerFolk_WeightGain>().WeightStage.ToString() == Context.Value;
            
        }
        else
        {
            return "1" == Context.Value;
        }
    }
}