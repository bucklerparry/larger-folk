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

            // this looks stupid buuuuut lemme know if u figure out a less stupid way of doing this LOL
            // weight stages for Mutated Human presets
            if (myRender.Tile == "creatures/sw_marsh_taur.bmp" || myRender.Tile == "bucklerparry_largerfolk/marshtaur_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/marshtaur_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/sw_marsh_taur.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/marshtaur_fat_stage1.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_2 = "bucklerparry_largerfolk/marshtaur_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_3 = "bucklerparry_largerfolk/marshtaur_fat_stage3.png";
            }
            else if (myRender.Tile == "Creatures/sw_dream_tortoise.bmp" || myRender.Tile == "bucklerparry_largerfolk/dream_tortoise_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/dream_tortoise_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "Creatures/sw_dream_tortoise.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/dream_tortoise_fat.png";
            }
            else if (myRender.Tile == "Creatures/sw_gunwing.bmp" || myRender.Tile == "bucklerparry_largerfolk/gunwing_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/gunwing_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "Creatures/sw_gunwing.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/gunwing_fat.png";
            }
            else if (myRender.Tile == "creatures/sw_stareye_esper.bmp" || myRender.Tile == "bucklerparry_largerfolk/star_eye_esper_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/star_eye_esper_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/sw_stareye_esper.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/star_eye_esper_fat.png";
            }
            else if (myRender.Tile == "creatures/sw_firefrond.bmp" || myRender.Tile == "bucklerparry_largerfolk/firefrond_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/firefrond_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/sw_firefrond.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/firefrond_fat.png";
            }
            else if (myRender.Tile == "Creatures/sw_bzzzt.bmp" || myRender.Tile == "bucklerparry_largerfolk/bzzzt_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/bzzzt_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "Creatures/sw_bzzzt.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/bzzzt_fat.png";
            }

            // True Kin presets
            else if (myRender.Tile == "creatures/sw_praetorian_prime.bmp" || myRender.Tile == "bucklerparry_largerfolk/praetorian_prime_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/praetorian_prime_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/sw_praetorian_prime.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/praetorian_prime_fat.png";
            }
            else if (myRender.Tile == "creatures/sw_first_horticulturalist.bmp" || myRender.Tile == "bucklerparry_largerfolk/first_gardener_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/first_gardener_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/sw_first_horticulturalist.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/first_gardener_fat.png";
            }
            else if (myRender.Tile == "creatures/sw_first_child_of_the_hearth.bmp" || myRender.Tile == "bucklerparry_largerfolk/first_child_of_the_hearth_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/first_child_of_the_hearth_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/sw_first_child_of_the_hearth.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/first_child_of_the_hearth_fat.png";
            }

            player.GetPart<LargerFolk_WeightGain>().TotalCalories = 1000;
            player.GetPart<LargerFolk_WeightGain>().InitializeWeightStage();
        }

    }
}