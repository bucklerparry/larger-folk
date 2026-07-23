using System;
using System.CodeDom.Compiler;
using System.Text;
using Occult.Engine.CodeGeneration;
using XRL.UI;
using XRL.Rules;
using XRL.World.Anatomy;
using XRL.World.Parts;
using XRL.World.Effects;
using LargerFolk.Utilities;

namespace LargerFolk.Utilities
{
    public static class LargerFolk_Language
    {
        public static string AjSize(int size)
        {
            switch (size)
            {
                case 0: // lean
                {
                    return new string[] {"trim", "lean", "slender", "skinny", "fit"}[LargerFolk_Random.Next(0, 5)];
                }
                case 1: // fat
                {
                    return new string[] {"chubby", "pudgy", "plump", "portly", "soft", "tubby"}[LargerFolk_Random.Next(0, 6)];
                }
                case 2: // obese
                {
                    return new string[] {"porky", "fat", "plump", "flabby", "heavy", "bulging"}[LargerFolk_Random.Next(0, 6)];
                    
                }
                case 3: // barely mobile
                {
                    return new string[] {"enormous", "massive", "cumbersome", "ponderous", "elephantine", "lard-filled", "fat-swaddled"}[LargerFolk_Random.Next(0, 7)];
                }
                default: // greater
                {
                    return new string[] {"enormous", "massive", "ponderous", "elephantine", "lard-filled", "fat-swaddled"}[LargerFolk_Random.Next(0, 6)];
                }
            }
        }
    }
}