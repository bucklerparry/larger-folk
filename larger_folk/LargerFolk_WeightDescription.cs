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

namespace XRL.World.Parts
{
    [Serializable]
    public class LargerFolk_WeightDescription: IPart
    {
        public bool IsEnabled => XRL.UI.Options.GetOption("Option_LargerFolk_DynamicWeightGainOn").EqualsNoCase("Yes");

        public string WeightDescription_0 = "";
        public string WeightDescription_1 = "";

        public override void Register(GameObject Object, IEventRegistrar Registrar)
	    {
            if (IsEnabled)
            {
                Registrar.Register("OnWeightStageChange");
                Registrar.Register("OnWeightStageInitialized");
                
                
            }

            base.Register(Object, Registrar);
	    }

        // When the current weight stage changes, update the description to reflect this, if possible
        public override bool FireEvent(Event E)
        {
            if (E.ID == "OnWeightStageChange" || E.ID == "OnWeightStageInitialized")
            {
                UpdateWeightDescription(E.GetIntParameter("WeightStage"));
            }

            return base.FireEvent(E);
        }

        public void UpdateWeightDescription(int newStage)
        {
            if (newStage < 1)
            {
                ParentObject.GetPart<Description>().Short = WeightDescription_0;
            }
            else
            {
                ParentObject.GetPart<Description>().Short = WeightDescription_1;
            }
        }
    }
}