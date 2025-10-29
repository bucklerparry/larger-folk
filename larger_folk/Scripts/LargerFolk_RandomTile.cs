using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World.ObjectBuilders
{
    [Serializable]
    public class LargerFolk_RandomTile : IObjectBuilder
    {
        public string TilesStage0 = "";
        public string TilesStage1 = "";

        // public override void Register(GameObject Object, IEventRegistrar Registrar)
	    // {
        //     Registrar.Register("OnWeightStageInitialized");
                
        //     base.Register(Object, Registrar);
	    // }

        // public override bool WantEvent(int ID, int cascade)
        // {
        //     if (!base.WantEvent(ID, cascade) && ID != ObjectCreatedEvent.ID)
        //     {
        //         return ID == SingletonEvent<RefreshTileEvent>.ID;
        //     }
        //     return true;
        // }
        
        // public override bool HandleEvent(ObjectCreatedEvent E)
        // {
        //     Popup.Show("This works!!!");
        //     RandomizeTile();
        //     return base.HandleEvent(E);
        // }

        // public override bool HandleEvent(RefreshTileEvent E)
        // {
        //     RandomizeTile();
        //     return base.HandleEvent(E);
        // }

        // public override bool FireEvent(Event E)
        // {
        //     if (E.ID == "OnWeightStageInitialized")
        //     {
        //         RandomizeTile();
        //     }

        //     return base.FireEvent(E);
        // }

        public override void Initialize()
        {
            TilesStage0 = "";
            TilesStage1 = "";
        }

        public override void Apply(GameObject Object, string Context)
        {
            List<string> list0 = TilesStage0.CachedCommaExpansion();
            List<string> list1 = TilesStage1.CachedCommaExpansion();

            Object.RequirePart<LargerFolk_WeightStageRender>();

            if (list0.Count > 0 && list1.Count > 0)
            {
                int TileIndex = list0.IndexOf( list0.GetRandomElement() );
                Object.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = list0[TileIndex];
                Object.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = list1[TileIndex];
                Object.Render.Tile = list1[TileIndex];

                Object.GetPart<LargerFolk_WeightStageRender>().UpdateWeightRender(Object.GetPart<LargerFolk_WeightGain>().WeightStage);
                
            }

            // Object.GetPart<LargerFolk_WeightGain>().WeightSetup();

            // if (list.Count > 0)
            // {
            //     Object.Render.Tile = list.GetRandomElement();
            // }
        }

        // public void RandomizeTile()
        // {
        //     List<string> list0 = TilesStage0.CachedCommaExpansion();
        //     List<string> list1 = TilesStage1.CachedCommaExpansion();

        //     ParentObject.RequirePart<LargerFolk_WeightStageRender>();
            
        //     ParentObject.EmitMessage(new string(ParentObject.GetDisplayName(int.MaxValue, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: true, ColorOnly: false, Visible: true, WithoutTitles: false, ForSort: false, Short: true, BaseOnly: false, WithIndefiniteArticle: false, WithDefiniteArticle: false, null, IndicateHidden: false, Capitalize: false, SecondPerson: false, Reflexive: false, true) + " has become fat.") );
        //     if (list0.Count > 0 && list1.Count > 0)
        //     {
        //         int TileIndex = list0.IndexOf( list0.GetRandomElement() );
        //         ParentObject.GetPart<LargerFolk_WeightStageRender>().WeightStage_0 = list0[TileIndex];
        //         ParentObject.GetPart<LargerFolk_WeightStageRender>().WeightStage_1 = list1[TileIndex];
        //         ParentObject.Render.Tile = list1[TileIndex];

        //         //LargerFolk_WeightStageRender>().UpdateWeightRender(ParentObject.GetPart<LargerFolk_WeightGain>().WeightStage);
                
        //     }

        //     ParentObject.GetPart<LargerFolk_WeightGain>().WeightSetup();
        // }

        // public bool IsTileInRandomSet(string Tile)
        // {
        //     return (TilesStage0.CachedCommaExpansion().Contains(Tile) || TilesStage1.CachedCommaExpansion().Contains(Tile) );
        // }
    }
}