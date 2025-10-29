using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Tinkering;
using XRL.World.Capabilities;
using XRL.Rules;
using XRL.World.Effects;
using LargerFolk.Utilities;
using XRL.World.Parts;

namespace XRL.World.Parts.Skill
{
    [Serializable]
    public class LargerFolk_Tailor : BaseSkill, IRepairSifrahHandler
    {
        public static void Init()
        {
        }

        public override bool WantEvent(int ID, int cascade)
        {
            if (!base.WantEvent(ID, cascade) && ID != InventoryActionEvent.ID && ID != OwnerGetInventoryActionsEvent.ID)
            {
                return ID == PooledEvent<GetItemElementsEvent>.ID;
            }
            return true;
        }

        public override bool HandleEvent(GetItemElementsEvent E)
        {
            if (E.IsRelevantCreature(ParentObject))
            {
                E.Add("scholarship", 2);
            }
            return base.HandleEvent(E);
        }

        public override bool HandleEvent(OwnerGetInventoryActionsEvent E)
        {
            if (ParentObject.HasSkill("LargerFolk_Tailor") && E.Object.HasPart<LargerFolk_Armor>())//IsTailorableEvent.Check(E.Actor, E.Object, null, this, null)) // && IsRepairableEvent.Check(E.Actor, E.Object, null, this, null)
            {
                E.AddAction("Tailor", "tailor", "Tailor", null, 'T', FireOnActor: true, 150);
            }
            return base.HandleEvent(E);
        }

        public override bool HandleEvent(InventoryActionEvent E)
        {
            if (E.Command == "Tailor")
            {
                GameObject actor = E.Actor;
                GameObject item = E.Item;
                actor.GetPart<Tinkering>();
                BitLocker bitLocker = actor.RequirePart<BitLocker>();
                if (actor.GetTotalConfusion() > 0)
                {
                    actor.ShowFailure("You're too confused to do that.");
                    return false;
                }
                if (item.CurrentCell != null)
                {
                    if (!actor.FlightMatches(item))
                    {
                        actor.ShowFailure("You cannot reach " + item.t(int.MaxValue, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: true, Short: true, BaseOnly: false, null, IndicateHidden: false, SecondPerson: true, Reflexive: false, null) + " to tailor " + item.them + ".");
                        return false;
                    }
                    if (!actor.PhaseMatches(item))
                    {
                        actor.ShowFailure("You are out of phase with " + item.t(int.MaxValue, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: true, Short: true, BaseOnly: false, null, IndicateHidden: false, SecondPerson: true, Reflexive: false, null) + " and cannot tailor " + item.them + ".");
                        return false;
                    }
                }
                if (actor.AreHostilesNearby() && actor.FireEvent("CombatPreventsRepair"))
                {
                    actor.ShowFailure("You can't tailor with hostile creatures nearby.");
                    return false;
                }
                if (actor.IsPlayer() && !item.Understood())
                {
                    Popup.ShowFail("You cannot tailor " + item.t(int.MaxValue, null, null, AsIfKnown: false, Single: true, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: true, Short: true, BaseOnly: false, null, IndicateHidden: false, SecondPerson: true, Reflexive: false, null) + " until you understand " + item.them + ".");
                    return false;
                }
                if (item.HasTagOrProperty("NoTailor"))
                {
                    Popup.ShowFail("You cannot tailor " + item.t(int.MaxValue, null, null, AsIfKnown: false, Single: true, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: true, Short: true, BaseOnly: false, null, IndicateHidden: false, SecondPerson: true, Reflexive: false, null) + ".");
                    return false;
                }
                if (Options.SifrahRepair && actor.IsPlayer())
                {
                    if (!string.IsNullOrEmpty(item.Owner) && !item.HasPropertyOrTag("DontWarnOnRepair") && Popup.ShowYesNoCancel(item.Does("are", int.MaxValue, null, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: true, Short: true, BaseOnly: false, WithIndefiniteArticle: false, null, IndicateHidden: false, Pronoun: false, SecondPerson: true, null) + " not owned by you, and trying to tailor " + item.them + " risks damaging " + item.them + ". Are you sure you want to do so?") != 0)
                    {
                        return false;
                    }
                    GameObject inInventory = item.InInventory;
                    if (inInventory != null && inInventory != actor && !string.IsNullOrEmpty(inInventory.Owner) && inInventory.Owner != item.Owner && !inInventory.HasPropertyOrTag("DontWarnOnRepair") && Popup.ShowYesNoCancel(inInventory.Does("are", int.MaxValue, null, null, null, AsIfKnown: false, Single: true, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: true, Short: true, BaseOnly: false, WithIndefiniteArticle: false, null, IndicateHidden: false, Pronoun: false, SecondPerson: true, null) + " not owned by you, and trying to tailor " + item.an(int.MaxValue, null, null, AsIfKnown: false, Single: true, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: false, Short: true, BaseOnly: false, IndicateHidden: false, SecondPerson: true, Reflexive: false, null) + " inside " + inInventory.them + " risks causing damage. Are you sure you want to do so?") != 0)
                    {
                        return false;
                    }
                    RepairSifrah repairSifrah = new RepairSifrah(item, item.GetComplexity(), item.GetTier() / 2, actor.Stat("Intelligence"));
                    repairSifrah.HandlerID = ParentObject.ID;
                    repairSifrah.HandlerPartName = base.Name;
                    repairSifrah.Play(item);
                    if (repairSifrah.InterfaceExitRequested)
                    {
                        E.RequestInterfaceExit();
                    }
                }
                else
                {
                    bool flag = false;
                    BitCost bitCost = new BitCost(GetRepairCost(item));
                    ModifyBitCostEvent.Process(actor, bitCost, "Tailor");
                    if (actor.IsPlayer())
                    {
                        if (!bitLocker.HasBits(bitCost))
                        {
                            Popup.ShowFail("You don't have <" + bitCost.ToString() + "> to tailor " + item.t(int.MaxValue, null, null, AsIfKnown: false, Single: true, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: true, Short: true, BaseOnly: false, null, IndicateHidden: false, SecondPerson: true, Reflexive: false, null) + ". You have:\n\n" + bitLocker.GetBitsString());
                            return false;
                        }
                        if (Popup.ShowYesNoCancel("Do you want to spend <" + bitCost.ToString() + "> to tailor " + item.t(int.MaxValue, null, null, AsIfKnown: false, Single: true, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: true, Short: true, BaseOnly: false, null, IndicateHidden: false, SecondPerson: true, Reflexive: false, null) + "? You have:\n\n" + bitLocker.GetBitsString()) == DialogResult.Yes)
                        {
                            flag = true;
                            bitLocker.UseBits(bitCost);
                        }
                    }
                    else if (IsRepairableBy(item, actor, bitCost, null, null))
                    {
                        flag = true;
                    }
                    else
                    {
                        actor.FireEvent(Event.New("UnableToRepair", "Object", item));
                    }
                    if (flag)
                    {
                        RepairResultSuccess(actor, item);
                    }
                }
                actor.UseEnergy(1000, "Skill Tinkering Repair", null, null);
            }
            return base.HandleEvent(E);
        }

        public static bool IsRepairableBy(GameObject obj, GameObject actor, BitCost RepairCost = null, Tinkering_Repair Skill = null, int? MaxRepairTier = null)
        {
            if (!MaxRepairTier.HasValue && Skill == null)
            {
                Skill = actor.GetPart<Tinkering_Repair>();
                if (Skill == null)
                {
                    return false;
                }
            }
            if (!IsRepairableEvent.Check(actor, obj, null, Skill, MaxRepairTier))
            {
                return false;
            }
            int num = ((RepairCost != null) ? GetRepairTier(RepairCost) : GetRepairTier(GetRepairCost(obj)));
            Repair part = obj.GetPart<Repair>();
            if (part != null)
            {
                int num2 = Math.Min(Math.Max(part.Difficulty / 2, 0), 8);
                if (num2 > num)
                {
                    num = num2;
                }
            }
            if (num > 0)
            {
                if (MaxRepairTier.HasValue)
                {
                    if (MaxRepairTier < num)
                    {
                        return false;
                    }
                }
                else if (!actor.HasSkill(DataDisk.GetRequiredSkill(num)))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsRepairableBy(GameObject actor, string RepairCost)
        {
            return IsRepairableBy(ParentObject, actor, new BitCost(RepairCost), null, null);
        }

        public static int GetRepairTier(BitCost RepairCost)
        {
            int num = 0;
            foreach (char key in RepairCost.Keys)
            {
                int bitTier = BitType.GetBitTier(key);
                if (bitTier > num)
                {
                    num = bitTier;
                }
            }
            return num;
        }

        public static int GetRepairTier(string RepairCost)
        {
            int num = 0;
            int i = 0;
            for (int length = RepairCost.Length; i < length; i++)
            {
                int bitTier = BitType.GetBitTier(RepairCost[i]);
                if (bitTier > num)
                {
                    num = bitTier;
                }
            }
            return num;
        }



        public static string GetRepairCost(GameObject obj)
        {
            TinkerItem part = obj.GetPart<TinkerItem>();
            bool flag = obj.HasEffect<Rusted>();
            string name = (flag ? "RustedRepairCost" : "RepairCost");
            string text = ((part == null) ? obj.GetTagOrStringProperty(name) : (flag ? part.RustedRepairCost : part.RepairCost));
            if (!string.IsNullOrEmpty(text))
            {
                return text;
            }
            text = "";
            string text2 = ((part == null) ? BitType.ToRealBits("BC", obj.Blueprint) : part.Bits);
            int num = 0;
            while (text == "")
            {
                Random random = new Random(The.Game.GetWorldSeed(obj.Blueprint + "RepairCost"));
                int num2 = (flag ? 75 : 50);
                int num3 = (flag ? (text2.Length - 1) : random.Next(0, text2.Length));
                for (int i = 0; i < text2.Length; i++)
                {
                    if (num3 == i || random.Next(0, 100) < num2)
                    {
                        text += text2[i];
                    }
                }
                num++;
                if (text == "" && num >= 10)
                {
                    text = "BC";
                }
            }
            if (part == null)
            {
                obj.SetStringProperty(name, text);
            }
            else if (flag)
            {
                part.RustedRepairCost = text;
            }
            else
            {
                part.RepairCost = text;
            }
            return text;
        }

        public override bool AddSkill(GameObject GO)
        {
            return true;
        }

        public override bool RemoveSkill(GameObject GO)
        {
            return true;
        }

        public void RepairResultSuccess(GameObject who, GameObject obj)
        {
            who.PlayWorldOrUISound("Sounds/Misc/sfx_interact_artifact_repair", null);
            if (who.IsPlayer())
            {
                int num = 0;
				List<string> list = new List<string>();
				list.Add("Larger.");
				list.Add("Smaller.");
				list.Add("To fit.");
				num = Popup.PickOption("How would you like to tailor it?", null, "", "Sounds/UI/ui_notification", list.ToArray(), null, null, null, null, null, null, 0, 60, 0, -1, AllowEscape: true);
                
                
                Popup.ShowBlock("You tailor " + obj.t(int.MaxValue, null, null, AsIfKnown: false, Single: true, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: true, Short: true, BaseOnly: false, null, IndicateHidden: false, SecondPerson: true, Reflexive: false, null) + ".");
                switch (num)
				{
					case 0:
						obj.GetPart<LargerFolk_Armor>().Tailor(1);
						break;
					case 1:
						obj.GetPart<LargerFolk_Armor>().Tailor(-1);
						break;
					case 2:
						obj.GetPart<LargerFolk_Armor>().TailorToWearer();
						break;
				}
            }
            RepairedEvent.Send(who, obj, null, this);
        }

        public void RepairResultExceptionalSuccess(GameObject who, GameObject obj)
        {
            RepairResultSuccess(who, obj);
            string randomBits = BitType.GetRandomBits("3d6".Roll(), obj.GetComplexity());
            if (!string.IsNullOrEmpty(randomBits))
            {
                who.RequirePart<BitLocker>().AddBits(randomBits);
                if (who.IsPlayer())
                {
                    Popup.Show("You receive tinkering bits <{{|" + BitType.GetDisplayString(randomBits) + "}}>");
                }
            }
        }

        public void RepairResultPartialSuccess(GameObject who, GameObject obj)
        {
            if (who.IsPlayer())
            {
                Popup.Show("You make some progress tailoring" + obj.t(int.MaxValue, null, null, AsIfKnown: false, Single: true, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: true, Short: true, BaseOnly: false, null, IndicateHidden: false, SecondPerson: true, Reflexive: false, null) + ".");
            }
        }

        public void RepairResultFailure(GameObject who, GameObject obj)
        {
            if (who.IsPlayer())
            {
                Popup.Show("You can't figure out how to tailor " + obj.t(int.MaxValue, null, null, AsIfKnown: false, Single: true, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: true, Short: true, BaseOnly: false, null, IndicateHidden: false, SecondPerson: true, Reflexive: false, null) + ".");
            }
        }

        public void RepairResultCriticalFailure(GameObject who, GameObject obj)
        {
            if (!RepairCriticalFailureEvent.Check(who, obj))
            {
                return;
            }
            if (obj.IsBroken())
            {
                if (!obj.HasPropertyOrTag("CantDestroyOnRepair"))
                {
                    obj.PotentiallyAngerOwner(who, "DontWarnOnRepair");
                    IComponent<GameObject>.XDidYToZ(who, "accidentally", "destroy", obj, null, "!", null, null, null, who);
                    obj.Destroy();
                }
                else
                {
                    RepairResultFailure(who, obj);
                }
                return;
            }
            string message = "You think you broke " + obj.them + "...";
            if (obj.ApplyEffect(new Broken()) && obj.IsBroken())
            {
                if (who.IsPlayer())
                {
                    Popup.Show(message);
                }
                obj.PotentiallyAngerOwner(who, "DontWarnOnRepair");
            }
            else
            {
                RepairResultFailure(who, obj);
            }
        }
    }
}