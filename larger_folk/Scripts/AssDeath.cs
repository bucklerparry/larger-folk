using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;
using XRL.World.AI;

namespace XRL.World.Conversations.Parts

{
	public class AssDeath : IConversationPart
	{
		public bool BroadcastForHelp;

		public override bool WantEvent(int ID, int Propagation)
		{
			if (!base.WantEvent(ID, Propagation) && ID != GetChoiceTagEvent.ID)
			{
				return ID == EnteredElementEvent.ID;
			}
			return true;
		}

		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = "{{R|[Do your worst.]}}";
			return false;
		}

		public override bool HandleEvent(EnteredElementEvent E)
		{
			if (The.Speaker.PartyLeader == The.Player)
			{
				The.Speaker.PartyLeader = null;
			}
			Cell currentCell = The.PlayerCell;
			int phase = The.Player.GetPhase();
			Physics.ApplyExplosion(currentCell, 15000, null, null, Local: true, Show: true, The.Player, "10d10+250", phase, Neutron: true);
	//		if (The.Speaker.PartyLeader == The.Player)
	//		{
	//			The.Speaker.PartyLeader = null;
	//		}
	//		The.Speaker.AddOpinion<OpinionGoad>(The.Player);
	//		The.Speaker.Target = The.Player;
	//		AIHelpBroadcastEvent.Send(The.Speaker, The.Player);
			return base.HandleEvent(E);
		}
	}
}