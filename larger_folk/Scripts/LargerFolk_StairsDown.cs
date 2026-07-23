using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Effects;
using LargerFolk.Utilities;

namespace XRL.World.Parts
{
    [Serializable]
    public class LargerFolk_StairsDown : IPart
    {
        public override bool WantEvent(int ID, int cascade) 
        {
            // Check if the ID parameter matches one of the events we want
            // The base WantEvent of IPart/Effect will always return false.
            return base.WantEvent(ID, cascade) || ID == ObjectEnteringCellEvent.ID;
        }

        public override bool HandleEvent(ObjectEnteringCellEvent E)
        {
            
            Cell cell = ParentObject.CurrentCell;
			Cell DestinationCell = ParentObject.GetPart<StairsDown>().GetPullDownCell(cell, out var Distance);
            for (int i = 0; i < E.Cell.GetObjects().Count; i++)
            {
                ParentObject.EmitMessage(DestinationCell.GetObjects()[i].pRender.DisplayName);
            }

            E.Object.GetPart<LargerFolk_WeightPhysics>().TryFullButtImpact(DestinationCell.GetAdjacentCells(false));

            return base.HandleEvent(E);
        }
    }
    
}