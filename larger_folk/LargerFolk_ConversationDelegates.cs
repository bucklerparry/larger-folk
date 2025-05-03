using XRL.World.Parts;
using XRL.World.Conversations;

[HasConversationDelegate] // This is required on the surrounding class to reduce the search complexity.
public static class LargerFolk_ConversationDelegates
{
    public bool GainEnabled => XRL.UI.Options.GetOption("Option_LargerFolk_DynamicWeightGainOn").EqualsNoCase("Yes");

    // A predicate that receives a DelegateContext object with our values assigned, this to protect mods from signature breaks.
    [ConversationDelegate(Speaker = true)]
    public static bool IfFat(DelegateContext Context)
    {
        // Context.Value holds the quoted value from the XML attribute.
        // Context.Target holds the game object.
        // Context.Element holds the parent element. assss
        
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
}