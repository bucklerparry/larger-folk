using System;
using System.Collections.Generic;
using System.Text;
using HistoryKit;
using Qud.API;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
    

    [Serializable]
    public class LargerFolk_WingsWeightInteraction : IPart
    {
       public GameObject User = null;
        IFlightSource flight_source;

        public LargerFolk_WingsWeightInteraction()
        {
            if (ParentObject.HasPart<MechanicalWings>())
            {
                flight_source = ParentObject.GetPart<MechanicalWings>();
            }
        }


        public override void Register(GameObject Object, IEventRegistrar Registrar)
	    {
            Registrar.Register("MovementModeChanged");

		    base.Register(Object, Registrar);
	    }

        public override bool WantEvent(int ID, int cascade)
        {
            return
                base.WantEvent(ID, cascade)
                || ID == EquippedEvent.ID
                || ID == UnequippedEvent.ID
                || ID == MovementModeChangedEvent.ID
            ;
        }

        public override bool HandleEvent(EquippedEvent E)
        {
            
            E.Actor.RegisterPartEvent(this, "OnWeightStageChange");
            User = E.Actor;
            
            return base.HandleEvent(E);
        }

        public override bool HandleEvent(UnequippedEvent E)
        {
            
            E.Actor.UnregisterPartEvent(this, "OnWeightStageChange");
            User = null;
            
            return base.HandleEvent(E);
        }

        
        public override bool FireEvent(Event E)
        {
            // if (E.ID == "MovementModeChanged");
            // {
            //     Popup.Show("Your corpulent body proves too heavy to take flight.");
            //     Flight.StopFlying(ParentObject, User, flight_source);
            // }
            
            return base.FireEvent(E);
        }

        public override bool HandleEvent(MovementModeChangedEvent E)
        {
            if (User != null && User.GetPart<LargerFolk_WeightGain>().WeightStage >= 2 && E.To == "Flying")
            {
                Popup.Show("Your corpulent body proves too heavy to take flight.");
                Flight.StopFlying(ParentObject, User, flight_source);
                
            }

            return base.HandleEvent(E);
        }
    }
}