using System;
using XRL.UI;
using XRL.World.Parts;
using LargerFolk.Utilities;

namespace XRL.World.Effects
{
    [Serializable]
    public class LargerFolk_Effect_Tight : Effect, ITierInitialized
    {
        public bool FromGrowth;

        public bool FromEquip; 

        // the weight stages over the target size the wearer is
        public int StageDifference = 0;

        public LargerFolk_Effect_Tight()
        {
            DisplayName = "{{r|tight}}";
            Duration = 1;
        }

        public LargerFolk_Effect_Tight(int StageDifference = 0, bool FromGrowth = false, bool FromEquip = false)
            : this()
        {
            this.StageDifference = StageDifference;
            this.FromGrowth = FromGrowth;
            this.FromEquip = FromEquip;
        }

        public override int GetEffectType()
        {
            return 100664320;
        }

       

        public override bool Apply(GameObject Object)
        {
            // if (Object.IsBroken())
            // {
            // 	return false;
            // }
            // // if (!Object.HasTagOrProperty("Breakable"))
            // // {
            // // 	return false;
            // // }
            // if (!Object.FireEvent("ApplyBroken"))
            // {
            // 	return false;
            // }
            if (base.Object.HasPart<LargerFolk_Mod_Stretchy>())
            {
                return false;
            }

            Object?.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_mechanicalRupture");
            GameObject holder = Object.Holder;
            if (holder != null)
            {
                int temp = LargerFolk_Random.Next(1,2);
                string pt = "";


                switch (temp)
                {
                    case 1:
                        pt = "*creeaak*";
                        break;
                    default:
                        pt = "*streeeeeeetch*";
                        break;
                }
                holder.ParticleText(pt, IComponent<GameObject>.ConsequentialColorChar(null, holder));
                holder.RegisterEffectEvent(this, "BeforeCooldownActivatedAbility");
                // Event @event = Event.New("CommandUnequipObject");
                // @event.SetParameter("BodyPart", holder.FindEquippedObject(Object));
                // @event.SetFlag("SemiForced", State: true);
                // holder.FireEvent(@event);
            }
            else
            {
                Object.ParticleText("*" + Object.ShortDisplayNameStripped + " is tight...*", 'R');
            }

            ApplyStats();
            
            return true;
        }

        public override bool WantEvent(int ID, int cascade)
        {
            // if (!base.WantEvent(ID, cascade) && ID != AdjustValueEvent.ID && ID != SingletonEvent<BeginTakeActionEvent>.ID && ID != PooledEvent<GetDisplayNameEvent>.ID && ID != PooledEvent<IsRepairableEvent>.ID)
            // {
            //     return ID == PooledEvent<RepairedEvent>.ID;
            // }
            // return true;
            return ID == GetShortDescriptionEvent.ID || (ID == PooledEvent<GetDisplayNameEvent>.ID) || base.WantEvent(ID, cascade);
        }


        public override bool HandleEvent(GetShortDescriptionEvent E)
	    {
            // if (!E.Object.HasPart<ModKeen>())
            // {
            //     E.Postfix.AppendRules(GetDescription(Tier));
            // }

            // E.Postfix.AppendLine("it's tight!!! " + StageDifference.ToString());

            return base.HandleEvent(E);
	    }
        
        public override bool HandleEvent(GetDisplayNameEvent E)
        {
            if (E.Understood() && !E.Object.HasProperName && !E.Object.IsNatural())
            {
                if (StageDifference > 2)
                {
                    E.AddAdjective("{{r|bursting}}");
                }
                else if (StageDifference > 1)
                {
                    E.AddAdjective("{{R|straining}}");
                }
                else if (StageDifference >= 0)
                {
                    E.AddAdjective("{{o|ill-fitting}}");
                }
            }
            return base.HandleEvent(E);
        }




        public override void Register(GameObject Object, IEventRegistrar Registrar)
        {
            Registrar.Register("BeginBeingUnequipped");
            base.Register(Object, Registrar);
        }

        public override bool FireEvent(Event E)
        {
            // if (E.ID == "BeginBeingEquipped")
            // {
            // 	string text = "You struggle to fit into your " + base.Object.t(int.MaxValue, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: true, Short: true, BaseOnly: false, null, IndicateHidden: false, SecondPerson: true, Reflexive: false, null) + ", " + base.Object.itis + " broken!";
            // 	if (E.GetIntParameter("AutoEquipTry") > 0)
            // 	{
            // 		E.SetParameter("FailureMessage", text);
            // 	}
            // 	else if (E.GetGameObjectParameter("Equipper").IsPlayer())
            // 	{
            // 		Popup.ShowFail(text);
            // 	}
            // 	return false;
            // }

            if (E.ID == "BeforeCooldownActivatedAbility")
            {
                if (E.GetStringParameter("Tags") == "Agility" && LargerFolk_Random.Next(1, 100) <= (StageDifference*5))
                {
                    if ( (!base.Object.HasPart<ModSturdy>()) && base.Object.ApplyEffect(new Broken()) && IsBroken())
                    {
                        IComponent<GameObject>.AddPlayerMessage("Your athletics strain the overtaxed " + base.Object.ShortDisplayNameStripped + " and break it.");
                        return false;
                    }
                    return true;
                }
            }

            if (E.ID == "BeginBeingUnequipped")
            {
                RemoveTight();
            }

            return base.FireEvent(E);
        }


        public override string GetDetails()
	    {
		    return "-" + StageDifference + " DV";
	    }
        
        private void ApplyStats()
        {
            Armor armor = base.Object?.GetPart<Armor>();
            if (armor != null)
            {
                armor.DV -= StageDifference;
            }
        }

        private void UnapplyStats()
        {
            Armor armor = base.Object?.GetPart<Armor>();
            if (armor != null)
            {
                armor.DV += StageDifference;
            }
        }

        public void RemoveTight()
        {
            UnapplyStats();
            base.Object.RemoveEffect(this);
        }
    }
}