using System;
using XRL.Rules;
using XRL.Core;
using XRL.World.Parts;

using System.Collections.Generic;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{

	[Serializable]
	public class LargerFolk_WeightStageRender : IPart
	{
        public bool IsEnabled => XRL.UI.Options.GetOption("Option_LargerFolk_DynamicWeightGainOn").EqualsNoCase("Yes");
		
        public string WeightStage_0 = null;
        public string WeightStage_1 = null;
        public string WeightStage_2 = null;
        public string WeightStage_3 = null;

        
        
		public LargerFolk_WeightStageRender()
		{
            
            // WeightStage_0 = renderPart.Tile;
            if (IsEnabled)
            {
                if (WeightStage_1 == null)
                {
                    WeightStage_1 = ParentObject.Render.Tile;
                }
                if (WeightStage_2 == null)
                {
                    WeightStage_2 = WeightStage_1;
                }
                if (WeightStage_3 == null)
                {
                    WeightStage_3 = WeightStage_2;
                }
            }
            
			return;
		}

        public override void Register(GameObject Object, IEventRegistrar Registrar)
	    {
            if (IsEnabled)
            {
                Registrar.Register("OnWeightStageChange");
                Registrar.Register("OnWeightStageInitialized");
            }

            base.Register(Object, Registrar);
	    }

        // When the current weight stage changes, update the sprite to reflect this 
        public override bool FireEvent(Event E)
        {
            if (E.ID == "OnWeightStageChange" || E.ID == "OnWeightStageInitialized")
            {
                UpdateWeightRender(E.GetIntParameter("WeightStage"));
            }

            return base.FireEvent(E);
        }

        public void UpdateWeightRender(int newStage)
        {
            if (!ParentObject.HasPart<Render>())
            {
                return;
            }
            if (IsEnabled)
            {
                switch (newStage)
                {
                    case 0:
                        if (WeightStage_0 != null) ParentObject.GetPart<Render>().Tile = WeightStage_0;
                        break;
                    case 1:
                        if (WeightStage_1 != null) ParentObject.GetPart<Render>().Tile = WeightStage_1;
                        else 
                        {
                            WeightStage_1 = WeightStage_0;
                            ParentObject.GetPart<Render>().Tile = WeightStage_1;
                        }
                        break;
                    case 2:
                        if (WeightStage_2 != null) ParentObject.GetPart<Render>().Tile = WeightStage_2;
                        else if (WeightStage_2 == null)
                        {
                            WeightStage_2 = WeightStage_1;
                            ParentObject.GetPart<Render>().Tile = WeightStage_2;
                        }
                        break;
                    case 3:
                        if (WeightStage_3 != null) ParentObject.GetPart<Render>().Tile = WeightStage_3;
                        else if (WeightStage_3 == null)
                        {
                            if (WeightStage_2 == null) WeightStage_2 = WeightStage_1;
                               
                            WeightStage_3 = WeightStage_2;
                            
                            ParentObject.GetPart<Render>().Tile = WeightStage_3;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
		
	}
}