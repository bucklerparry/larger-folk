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
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/firefrond_fat_stage1.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_2 = "bucklerparry_largerfolk/firefrond_fat.png";
            }
            else if (myRender.Tile == "Creatures/sw_bzzzt.bmp" || myRender.Tile == "bucklerparry_largerfolk/bzzzt_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/bzzzt_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "Creatures/sw_bzzzt.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/bzzzt_fat_stage1.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_2 = "bucklerparry_largerfolk/bzzzt_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_3 = "bucklerparry_largerfolk/bzzzt_fat_stage3.png";
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

             // True Kin Castes
            else if (myRender.Tile == "creatures/caste_1.bmp" || myRender.Tile == "bucklerparry_largerfolk/horticulturalist_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/horticulturalist_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_1.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/horticulturalist_fat.png";
            }
            else if (myRender.Tile == "creatures/caste_2.bmp" || myRender.Tile == "bucklerparry_largerfolk/priest_of_all_suns_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/priest_of_all_suns_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_2.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/priest_of_all_suns_fat.png";
            }
            else if (myRender.Tile == "creatures/caste_3.bmp" || myRender.Tile == "bucklerparry_largerfolk/priest_of_all_moons_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/priest_of_all_moons_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_3.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/priest_of_all_moons_fat.png";
            }
            else if (myRender.Tile == "creatures/caste_4.bmp" || myRender.Tile == "bucklerparry_largerfolk/syzygyrior_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/syzygyrior_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_4.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/syzygyrior_fat.png";
            }
            else if (myRender.Tile == "creatures/caste_5.bmp" || myRender.Tile == "bucklerparry_largerfolk/artifex_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/artifex_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_5.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/artifex_fat.png";
            }
            else if (myRender.Tile == "creatures/caste_6.bmp" || myRender.Tile == "bucklerparry_largerfolk/consul_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/consul_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_6.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/consul_fat.png";
            }
            else if (myRender.Tile == "creatures/caste_7.bmp" || myRender.Tile == "bucklerparry_largerfolk/praetorian_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/praetorian_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_7.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/praetorian_fat.png";
            }
            else if (myRender.Tile == "creatures/caste_8.bmp" || myRender.Tile == "bucklerparry_largerfolk/eunuch_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/eunuch_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_8.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/eunuch_fat.png";
            }
            else if (myRender.Tile =="creatures/caste_9.bmp" || myRender.Tile == "bucklerparry_largerfolk/hearthchild_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/hearthchild_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_9.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/hearthchild_fat.png";
            }
            else if (myRender.Tile == "creatures/caste_10.bmp" || myRender.Tile == "bucklerparry_largerfolk/child_of_the_wheel_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/child_of_the_wheel_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_10.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/child_of_the_wheel_fat.png";
            }
            else if (myRender.Tile == "creatures/caste_11.bmp" || myRender.Tile == "bucklerparry_largerfolk/deepchild_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/deepchild_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_11.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/deepchild_fat.png";
            }
            else if (myRender.Tile == "creatures/caste_12.bmp" || myRender.Tile == "bucklerparry_largerfolk/fuming_godchild_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/fuming_godchild_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_12.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/fuming_godchild_fat.png";
            }

            // Mutated Human Callings
            else if (myRender.Tile == "creatures/caste_13.bmp" || myRender.Tile == "bucklerparry_largerfolk/apostle_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/apostle_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_13.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/apostle_fat.png";
            }

            else if (myRender.Tile == "creatures/caste_14.bmp" || myRender.Tile == "bucklerparry_largerfolk/arconaut_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/arconaut_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_14.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/arconaut_fat.png";
            }

            else if (myRender.Tile == "creatures/caste_15.bmp" || myRender.Tile == "bucklerparry_largerfolk/greybeard_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/greybeard_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_15.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/greybeard_fat.png";
            }

            else if (myRender.Tile == "creatures/caste_16.bmp" || myRender.Tile == "bucklerparry_largerfolk/gunslinger_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/gunslinger_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_16.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/gunslinger_fat.png";
            }

            else if (myRender.Tile == "creatures/caste_17.bmp" || myRender.Tile == "bucklerparry_largerfolk/marauder_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/marauder_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_17.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/marauder_fat.png";
            }
            
            else if (myRender.Tile == "creatures/caste-pilgrim.png" || myRender.Tile == "bucklerparry_largerfolk/pilgrim_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/pilgrim_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste-pilgrim.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/pilgrim_fat.png";
            }

            else if (myRender.Tile == "creatures/caste_19.bmp" || myRender.Tile == "bucklerparry_largerfolk/nomad_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/nomad_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_19.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/nomad_fat.png";
            }
            else if (myRender.Tile == "creatures/caste_20.bmp" || myRender.Tile == "bucklerparry_largerfolk/scholar_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/scholar_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_20.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/scholar_fat.png";
            }
            else if (myRender.Tile == "creatures/caste_21.bmp" || myRender.Tile == "bucklerparry_largerfolk/tinker_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/tinker_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_21.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/tinker_fat.png";
            }

            else if (myRender.Tile == "creatures/caste_22.bmp" || myRender.Tile == "bucklerparry_largerfolk/caste_warden_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/caste_warden_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_22.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/caste_warden_fat.png";
            }
            else if (myRender.Tile == "creatures/caste_23.bmp" || myRender.Tile == "bucklerparry_largerfolk/water_merchant_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/water_merchant_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_23.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/water_merchant_fat.png";
            }
            else if (myRender.Tile == "creatures/caste_24.bmp" || myRender.Tile == "bucklerparry_largerfolk/farmer1_fat.png")
            {
                player.GetPart<Render>().Tile = "bucklerparry_largerfolk/farmer1_fat.png";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = "creatures/caste_24.bmp";
                player.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = "bucklerparry_largerfolk/farmer1_fat.png";
            }

            player.GetPart<LargerFolk_WeightGain>().TotalCalories = 1000;
            player.GetPart<LargerFolk_WeightGain>().InitializeWeightStage();
        }

    }
}
// "creatures/caste_1.bmp"
// "bucklerparry_largerfolk/horticulturalist_fat.png"

// "bucklerparry_largerfolk/priest_of_all_suns_fat.png"

// "bucklerparry_largerfolk/priest_of_all_moons_fat.png"

// "bucklerparry_largerfolk/syzygyrior_fat.png"

// "bucklerparry_largerfolk/artifex_fat.png"

// "bucklerparry_largerfolk/consul_fat.png"

// "bucklerparry_largerfolk/praetorian_fat.png"

// "bucklerparry_largerfolk/eunuch_fat.png"

// "bucklerparry_largerfolk/hearthchild_fat.png"

// "bucklerparry_largerfolk/child_of_the_wheel_fat.png"

// "bucklerparry_largerfolk/deepchild_fat.png"

// "creatures/caste_12.bmp"
// "bucklerparry_largerfolk/fuming_godchild_fat.png"




            // "creatures/caste_13.bmp"
            // "bucklerparry_largerfolk/apostle_fat.png"
            
            // "creatures/caste_14.bmp"
            // "bucklerparry_largerfolk/arconaut_fat.png"

            // "creatures/caste_15.bmp"
            // "bucklerparry_largerfolk/greybeard_fat.png"

            // "creatures/caste_16.bmp"
            // "bucklerparry_largerfolk/gunslinger_fat.png"

            // "creatures/caste_17.bmp"
            // "bucklerparry_largerfolk/marauder_fat.png"

            // "creatures/caste-pilgrim.png"
            // "bucklerparry_largerfolk/pilgrim_fat.png"

            // "creatures/caste_19.bmp"
            // "bucklerparry_largerfolk/nomad_fat.png"

            // "creatures/caste_20.bmp"
            // "bucklerparry_largerfolk/scholar_fat.png"

            // "creatures/caste_21.bmp"
            // "bucklerparry_largerfolk/tinker_fat.png"

            // "creatures/caste_22.bmp"
            // "bucklerparry_largerfolk/caste_warden_fat.png"

            // "creatures/caste_23.bmp"
            // "bucklerparry_largerfolk/water_merchant_fat.png"

            // "creatures/caste_24.bmp"
            // "bucklerparry_largerfolk/farmer1_fat.png"