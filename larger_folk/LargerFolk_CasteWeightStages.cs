using XRL; // to abbreviate XRL.PlayerMutator and XRL.IPlayerMutator
using XRL.World; // to abbreviate XRL.World.GameObject
using XRL.World.Parts;
[PlayerMutator]
public class LargerFolk_CasteWeightStages : IPlayerMutator
{
    public void mutate(GameObject player)
    {
        if (!XRL.UI.Options.GetOption("Option_LargerFolk_DynamicWeightGainOn").EqualsNoCase("Yes"))
        {
            return;
        }
        // modify the player object when a New Game begins
        // for example, add a custom part to the player:
        player.RequirePart<LargerFolk_WeightStageRender>();

        LargerFolk_WeightStageRender weightRender = player.GetPart<LargerFolk_WeightStageRender>();

        if (player.HasPart<Render>())
        {
            Render myRender = player.GetPart<Render>();

            if (myRender.Tile == "creatures/sw_marsh_taur.bmp" || myRender.Tile == "bucklerparry_largerfolk/marshtaur_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/marshtaur_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/sw_marsh_taur.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/marshtaur_fat.png";
            }

            player.GetPart<LargerFolk_WeightGain>().InitializeWeightStage();
        }

    }
}