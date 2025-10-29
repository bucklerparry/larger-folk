using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ConsoleLib.Console;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
    [Serializable]
    public class LargerFolk_Immobile : BaseMutation
    {
        public GameObject ImmobileObject;

        public int oldLevel = 1;



      
        [NonSerialized]
        protected GameObjectBlueprint _Blueprint;


        public string BlueprintName => Variant.Coalesce("Gigantic Belly");



          
        public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
	    {
		    LargerFolk_Immobile obj = base.DeepCopy(Parent, MapInv) as LargerFolk_Immobile;
		    obj.ImmobileObject = null;
		    return obj;
	    }
        public LargerFolk_Immobile()
        {
            DisplayName = "Immobile";
        }


        public GameObjectBlueprint Blueprint
        {
            get
            {
                if (_Blueprint == null)
                {
                    _Blueprint = GameObjectFactory.Factory.GetBlueprint(BlueprintName);
                }
                return _Blueprint;
            }
        }

       

    //	public override void CollectStats(Templates.StatCollector stats, int Level)
    //	{
    //		stats.Set("Quills", ObjectName);
    //		stats.Set("RegenRate", GetRegenRate(Level).ToString());
    //		stats.Set("AVPenalty", GetAVPenalty(Level));
    //		stats.Set("QuillPen", Grammar.InitCap(ObjectNameSingular) + " penetration: " + GetQuillPenetration(Level));
    //		stats.Set("QuillDamage", Grammar.InitCap(ObjectNameSingular) + " damage: 1d3");
    //	}


        public override bool GeneratesEquipment()
        {
            return true;
        }

        public override string GetDescription()
        {
            return Blueprint.GetTag("VariantDescription").Coalesce("Whether through some sketchy meds, psychic alteration or simple gluttony, your weight has ballooned past the limits a normal person could hope, or fear, to reach. So prodigiously fat are you that movement has become impossible. If you're clever, perhaps some workaround can be found?");
        }

        public override void SetVariant(string Variant)
        {
            base.SetVariant(Variant);
            _Blueprint = null;
        }

        
        public override bool ChangeLevel(int NewLevel)
        {
            
            return base.ChangeLevel(NewLevel);
        }

        public override void Register(GameObject Object, IEventRegistrar Registrar)
	    {
		    base.Register(Object, Registrar);
	    }
    

        public override bool Mutate(GameObject GO, int Level)
        {
            BodyPart bodyPart = GO.Body?.GetBody();
            if (bodyPart != null)
            {
                bodyPart.ForceUnequip(Silent: true);
                ImmobileObject = GameObject.Create(Blueprint);
                GO.ForceEquipObject(ImmobileObject, bodyPart, Silent: true, 0);
            }
            return base.Mutate(GO, Level);
        }

        public override bool Unmutate(GameObject GO)
        {
            CleanUpMutationEquipment(GO, ref ImmobileObject);
        
            
            return base.Unmutate(GO);
        }
    }
}