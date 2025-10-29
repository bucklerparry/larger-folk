using System;
using System.Collections.Generic;
using System.Text;
using Qud.API;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL.Liquids
{
    [Serializable]
    [IsLiquid]
    public class LargerFolk_Phenomena_AdipocyticFluid : BaseLiquid 
    {
        public new const string ID = "adipocyticFluid";

        [NonSerialized]
	    public static List<string> Colors = new List<string>(3) { "M", "W", "Y" };
        
        public LargerFolk_Phenomena_AdipocyticFluid () : base ("adipocyticFluid") 
        {
            Temperature = 60;
        }

        public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
	    {
            Message.Compound("{{lava|A warm sensation fills your body.}}");
            Target.TemperatureChange(Temperature, Target, Radiant: false, MinAmbient: false, MaxAmbient: false, IgnoreResistance: false, 0, null, null);
            
            if (Target.HasPart<LargerFolk_WeightGain>())
            {
                string dice = Liquid.Proportion("adipocyticFluid") / 100 + 1 + "d100";
                int tempDie = dice.Roll();
                Target.GetPart<LargerFolk_WeightGain>().ChangeCalories(tempDie*tempDie);
            }
            
            ExitInterface = true;
            return true;
	    }

    }
}