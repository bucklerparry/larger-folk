#define NLOG_ALL
using System;
using System.Collections.Generic;
using HistoryKit;
using Qud.API;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
    [Serializable]
    public class LargerFolk_SultanShrine : IPart
    {
        public override bool SameAs(IPart p)
        {
            return false;
        }

        public override bool WantEvent(int ID, int cascade) 
        {
            return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
        }

        public override bool HandleEvent(ObjectCreatedEvent E)
        {
            
            return base.HandleEvent(E);
        }

        public override bool HandleEvent(EnteredCellEvent E)
        {
            if (ParentObject.GetPart<SultanShrine>().Initialized)
            {
                if (ParentObject.Render.Tile == "Terrain/sw_resheph_sultanstatue.bmp")
                {
                    ParentObject.Render.Tile = "bucklerparry_largerfolk/statue_resheph_fat.png";
                }
            }
            return base.HandleEvent(E);
        }

        // public override void Register(GameObject Object, IEventRegistrar Registrar)
        // {
        //     Registrar.Register("AfterLookedAt");
        //     Registrar.Register("SpecialInit");
        //     base.Register(Object, Registrar);
        // }

    }
}