using System;
using XRL.Rules;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	[Serializable]
	public class LargerFolk_ButtonPop : MissileWeapon
	{
		GameObject User = null;
		int DistanceVariance = 2;

		public LargerFolk_ButtonPop()
		{
			return;
		}

		public override bool WantEvent(int ID, int cascade)
        {
            return
                base.WantEvent(ID, cascade)
                || ID == EquippedEvent.ID
                || ID == UnequippedEvent.ID
            ;
        }

        public override bool HandleEvent(EquippedEvent E)
        {
            
            User = E.Actor;
            return base.HandleEvent(E);
        }

        public override bool HandleEvent(UnequippedEvent E)
        {
            
            User = null;
            return base.HandleEvent(E);
        }

		public void PopButton()
		{
			Event @event = Event.New("CommandFireMissile");
			@event.SetParameter("Actor", User);
			@event.SetParameter("StartCell", User.CurrentCell);
			@event.SetParameter("TargetCell", User.CurrentCell);
			@event.SetFlag("IncludeStart", false); //E.Actor.CurrentCell == cell && Chance.in100());
			@event.SetFlag("ShowEmitMessage", State: true);
			@event.SetFlag("ActivePartsIgnoreSubject", State: true);
			@event.SetFlag("UsePopups", State: false);
			FireEvent(@event);
		}
			// ParentObject.PerformThrow(new , Cell TargetCell, GameObject ApparentTarget = null, MissilePath MPath = null, 0, 0, int? DistanceVariance, 0)
	}
}
