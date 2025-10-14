using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	[Serializable]
	public class LargerFolk_Stomach : IPart
	{
		public const int COOKING_INCREMENT = 1200;

		public int CookCount;

		public int Water = 30000;

		public int RegenCounter;

		[NonSerialized]
		private static Event eCalculatingThirst = new Event("CalculatingThirst", "Amount", 0);

		[NonSerialized]
		private static Event eRegenerating = new Event("Regenerating", "Amount", 0);

		public int HungerLevel;

		public int _CookingCounter;

		public int HitCounter;

		public int WasOnWorldMap;

		[NonSerialized]
		private static List<GameObject> containers = new List<GameObject>();

		public int CookingCounter
		{
			get
			{
				return _CookingCounter;
			}
			set
			{
				_CookingCounter = value;
				UpdateHunger();
			}
		}

		public void GetHungry()
		{
			CookingCounter = CalculateCookingIncrement();
			UpdateHunger();
		}

		public void ClearHunger()
		{
			ParentObject.RemoveEffect<Famished>();
			HungerLevel = 0;
			CookingCounter = 0;
			WasOnWorldMap = 0;
		}

		public static void ClearHunger(GameObject Subject = null)
		{
			Subject?.GetPart<Stomach>()?.ClearHunger();
		}

		public int CalculateCookingIncrement()
		{
			int num = 1200;
			if (ParentObject.HasSkill("Discipline_FastingWay"))
			{
				num *= 2;
			}
			if (ParentObject.HasSkill("Discipline_MindOverBody"))
			{
				num *= 6;
			}
			return num;
		}

		public string FoodStatus()
		{
			if (HungerLevel == 0)
			{
				return "{{g|Sated}}";
			}
			if (HungerLevel == 1)
			{
				return "{{W|Hungry}}";
			}
			if (ParentObject.HasPart<PhotosyntheticSkin>())
			{
				return "{{R|Wilted!}}";
			}
			return "{{R|Famished!}}";
		}

		public string WaterStatus()
		{
			if (ParentObject.HasPart<Amphibious>())
			{
				if (Water <= 0)
				{
					return "{{R|Desiccated!}}";
				}
				if (Water <= RuleSettings.WATER_PARCHED)
				{
					return "{{r|Dry}}";
				}
				if (Water <= RuleSettings.WATER_THIRSTY)
				{
					return "{{c|Moist}}";
				}
				if (Water <= RuleSettings.WATER_QUENCHED)
				{
					return "{{b|Wet}}";
				}
				return "{{B|Soaked}}";
			}
			if (Water <= 0)
			{
				return "{{R|Dehydrated!}}";
			}
			if (Water <= RuleSettings.WATER_PARCHED)
			{
				return "{{r|Parched}}";
			}
			if (Water <= RuleSettings.WATER_THIRSTY)
			{
				return "{{Y|Thirsty}}";
			}
			if (Water <= RuleSettings.WATER_QUENCHED)
			{
				return "{{g|Quenched}}";
			}
			return "{{G|Tumescent}}";
		}

		public override bool SameAs(IPart p)
		{
			return false;
		}

		public override bool WantEvent(int ID, int cascade)
		{
			if (!base.WantEvent(ID, cascade) && ID != SingletonEvent<BeginTakeActionEvent>.ID && ID != PooledEvent<CanDrinkEvent>.ID && ID != EnteredCellEvent.ID && ID != PooledEvent<GetCompanionStatusEvent>.ID && ID != SingletonEvent<GetDebugInternalsEvent>.ID && ID != PooledEvent<HealsNaturallyEvent>.ID && ID != PooledEvent<InduceVomitingEvent>.ID && ID != TookDamageEvent.ID)
			{
				return ID == PooledEvent<CanTravelEvent>.ID;
			}
			return true;
		}

		public override bool HandleEvent(HealsNaturallyEvent E)
		{
			if (E.Object == ParentObject)
			{
				return false;
			}
			return base.HandleEvent(E);
		}

		public override bool HandleEvent(InduceVomitingEvent E)
		{
			if (E.Object == ParentObject && E.Object.GetLongProperty("VomitedOnTurn", 0L) <= The.CurrentTurn - 3)
			{
				E.Vomited = true;
				if (E.Object.IsPlayer())
				{
					E.Message("You vomit!");
				}
				else
				{
					E.Message(E.Object.Does("vomit", int.MaxValue, null, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: true, Short: true, BaseOnly: false, WithIndefiniteArticle: false, null, IndicateHidden: false, Pronoun: false, SecondPerson: true, null) + " everywhere!");
				}
				Water = Stat.Random(Math.Min(Water, RuleSettings.WATER_MAXIMUM) * 2 / 5, Math.Min(Water, RuleSettings.WATER_MAXIMUM) * 3 / 5);
				E.Object.ApplyEffect(new LiquidCovered("putrid", 2));
				if (E.Object.CurrentCell != null && !E.Object.OnWorldMap())
				{
					E.Object.CurrentCell.AddObject("VomitPool");
				}
				E.Object.SetLongProperty("VomitedOnTurn", The.CurrentTurn);
				E.Object.UseEnergy(1000, "Vomit", null, null);
				E.InterfaceExit = true;
			}
			return base.HandleEvent(E);
		}

		public override bool HandleEvent(CanDrinkEvent E)
		{
			E.CanDrinkThis = true;
			return base.HandleEvent(E);
		}

		public override bool HandleEvent(GetCompanionStatusEvent E)
		{
			if (E.Object == ParentObject && Water <= 0)
			{
				E.AddStatus(ParentObject.HasPart<Amphibious>() ? "desiccated" : "dehydrated", -80);
			}
			return base.HandleEvent(E);
		}

		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (ParentObject.IsPlayer())
			{
				if (!ParentObject.OnWorldMap())
				{
					if (WasOnWorldMap > 0)
					{
						if (WasOnWorldMap > 2 && HungerLevel < 1)
						{
							CookingCounter = CalculateCookingIncrement();
							ParentObject.FireEvent("BecameHungry");
						}
						WasOnWorldMap = 0;
					}
					else
					{
						ParentObject.SetLongProperty("OnWorldMapSince", XRLCore.CurrentTurn);
					}
				}
				else
				{
					WasOnWorldMap++;
				}
			}
			return base.HandleEvent(E);
		}

		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			int water = Water;
			bool flag = false;
			if (ParentObject.IsPlayer())
			{
				if (!ParentObject.OnWorldMap())
				{
					if (WasOnWorldMap > 0)
					{
						WasOnWorldMap = 0;
						CookingCounter = CalculateCookingIncrement();
						int turns = (int)Math.Min(XRLCore.CurrentTurn - ParentObject.GetLongProperty("OnWorldMapSince", 0L), 100000L);
						foreach (GameObject companion in ParentObject.GetCompanions())
						{
							if (companion.TryGetPart<Stomach>(out var Part) && Part.HitCounter <= 5)
							{
								Part.ProcessNaturalHealing(turns);
							}
						}
					}
					CookingCounter++;
				}
				if (!ParentObject.HasEffect<Asleep>())
				{
					if (ParentObject.HasPart<FattyHump>())
					{
						if (Water > 0)
						{
							Water--;
						}
					}
					else if (ParentObject.Speed != 0)
					{
						int num = 1;
						if (ParentObject.HasPart<Discipline_FastingWay>())
						{
							num *= 2;
						}
						if (ParentObject.HasPart<Discipline_MindOverBody>())
						{
							num *= 6;
						}
						int value = ParentObject.Speed / (ParentObject.HasPart<Amphibious>() ? 3 : 5) / num;
						eCalculatingThirst.SetParameter("Amount", value);
						ParentObject.FireEvent(eCalculatingThirst);
						value = eCalculatingThirst.GetIntParameter("Amount");
						Water -= value;
					}
					else
					{
						Water -= 20;
					}
					if (Water < RuleSettings.WATER_MINIMUM)
					{
						Water = RuleSettings.WATER_MINIMUM;
					}
				}
				if (Water < 0)
				{
					Water = 0;
				}
				if (Options.AutoSip)
				{
					int num2 = RuleSettings.WATER_THIRSTY;
					string autoSipLevel = Options.AutoSipLevel;
					if (!string.IsNullOrEmpty(autoSipLevel))
					{
						switch (autoSipLevel)
						{
						case "Dehydrated":
							num2 = RuleSettings.WATER_MINIMUM;
							break;
						case "Parched":
							num2 = RuleSettings.WATER_PARCHED;
							break;
						case "Thirsty":
							num2 = RuleSettings.WATER_THIRSTY;
							break;
						case "Quenched":
							num2 = RuleSettings.WATER_QUENCHED;
							break;
						case "Tumescent":
							num2 = RuleSettings.WATER_TUMESCENT;
							break;
						}
					}
					if (Water < num2)
					{
						flag = true;
					}
				}
			}
			else
			{
				int wATER_THIRSTY = RuleSettings.WATER_THIRSTY;
				if (Water < wATER_THIRSTY)
				{
					flag = true;
				}
			}
			if (flag && !ParentObject.IsFrozen() && ParentObject.CanMoveExtremities(null, ShowMessage: false, Involuntary: false, AllowTelekinetic: true))
			{
				containers.Clear();
				bool flag2 = ParentObject.HasPart<Amphibious>();
				if (ParentObject.UseDrams(1, "water", null, null, null, containers, !flag2))
				{
					GameObject gameObject = ((containers.Count > 0) ? containers[0] : null);
					if (flag2)
					{
						if (gameObject == null)
						{
							DidXToY("pour", "fresh water over", ParentObject, null, null, null, null, ParentObject);
						}
						else
						{
							DidXToYWithZ("pour", "fresh water from", gameObject, "over", ParentObject, null, null, null, null, ParentObject, null, UseFullNames: false, IndefiniteSubject: false, IndefiniteDirectObject: false, IndefiniteIndirectObject: false, IndefiniteDirectObjectForOthers: true, IndefiniteIndirectObjectForOthers: false, PossessiveDirectObject: false, PossessiveIndirectObject: false, null, ParentObject);
						}
						ParentObject.ApplyEffect(new LiquidCovered("water", 1));
					}
					else
					{
						if (gameObject == null)
						{
							DidX("take", "a sip of fresh water", null, null, null, ParentObject);
						}
						else
						{
							ParentObject.FireEvent(Event.New("DrinkingFrom", "Container", gameObject));
							DidXToY("take", "a sip of fresh water from", gameObject, null, null, null, null, ParentObject, null, UseFullNames: false, IndefiniteSubject: false, IndefiniteObject: false, IndefiniteObjectForOthers: false, PossessiveObject: false, null, ParentObject);
						}
						FireEvent(Event.New("AddWater", "Amount", 10000));
					}
				}
				else if (E.Traveling && !E.TravelMessagesSuppressed)
				{
					E.TravelMessagesSuppressed = true;
					if (Popup.ShowYesNo("You have run out of {{B|water}}! Do you want to stop travelling?") == DialogResult.Yes)
					{
						return false;
					}
				}
				containers.Clear();
			}
			bool flag3 = ProcessNaturalHealing();
			if (IsPlayer())
			{
				if (water > RuleSettings.WATER_THIRSTY && Water < RuleSettings.WATER_THIRSTY)
				{
					PlayWorldSound("sfx_characterMod_thirsty");
				}
				if (!flag3 && Stat.RollPenetratingSuccesses("1d" + ParentObject.Stat("Toughness"), 2) <= 0)
				{
					if (E.Traveling)
					{
						if (!E.TravelMessagesSuppressed)
						{
							E.TravelMessagesSuppressed = true;
							if (ParentObject.HasPart<Amphibious>())
							{
								if (Popup.ShowYesNo("You are drying out! Do you want to stop travelling?") == DialogResult.Yes)
								{
									return false;
								}
							}
							else if (Popup.ShowYesNo("You are dying of thirst! Do you want to stop travelling?") == DialogResult.Yes)
							{
								return false;
							}
						}
					}
					else if (ParentObject.HasPart<Amphibious>())
					{
						Popup.Show("You are drying out!");
					}
					else
					{
						Popup.Show("You are dying of thirst!");
					}
					ParentObject.Physics.LastDamagedByType = "Dessicated";
					ParentObject.Physics.LastDeathReason = "You died of thirst.";
					ParentObject.Physics.LastThirdPersonDeathReason = "";
					ParentObject.Physics.LastDamagedBy = null;
					XRLCore.Core.Game.DeathReason = "You died of thirst.";
					The.Game.DeathCategory = "thirst";
					ParentObject.GetStat("Hitpoints").Penalty += 2;
				}
				if (XRLCore.Core.IDKFA)
				{
					ParentObject.GetStat("Hitpoints").Penalty = 0;
					Water = RuleSettings.WATER_MAXIMUM - 1000;
					ParentObject.Physics.Temperature = 25;
					Sidebar.Update();
				}
			}
			return base.HandleEvent(E);
		}

		public override bool HandleEvent(TookDamageEvent E)
		{
			if (E.Object == ParentObject)
			{
				HitCounter = 10;
			}
			return base.HandleEvent(E);
		}

		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Water", Water);
			E.AddEntry(this, "HungerLevel", HungerLevel);
			E.AddEntry(this, "CookCount", CookCount);
			E.AddEntry(this, "CookingCounter", CookingCounter);
			return base.HandleEvent(E);
		}

		public override bool HandleEvent(CanTravelEvent E)
		{
			if (E.Object == ParentObject && IsFamished() && !E.Object.HasSkill("Discipline_MindOverBody") && !The.Core.IDKFA)
			{
				return E.Object.ShowFailure("You're too famished to travel long distances.");
			}
			return base.HandleEvent(E);
		}

		public override bool AllowStaticRegistration()
		{
			return true;
		}

		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AddFood");
			Registrar.Register("AddWater");
			base.Register(Object, Registrar);
		}

		public override bool FireEvent(Event E)
		{
			if (E.ID == "AddWater")
			{
				int num = E.GetIntParameter("Amount");
				int water = Water;
				int num2 = Water + num;
				bool flag = E.HasFlag("Forced");
				bool flag2 = E.HasFlag("External");
				bool flag3 = false;
				if (ParentObject.HasPart<Amphibious>())
				{
					if (num < 0 && IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("The moisture is sucked out of your body.");
						if (water > RuleSettings.WATER_THIRSTY && num2 < RuleSettings.WATER_THIRSTY)
						{
							PlayWorldSound("sfx_characterMod_thirsty");
						}
					}
					if (!flag2)
					{
						num /= 10;
					}
					Water += num;
					if (Water > RuleSettings.WATER_MAXIMUM)
					{
						Water = RuleSettings.WATER_MAXIMUM;
					}
					else if (Water < RuleSettings.WATER_MINIMUM)
					{
						Water = RuleSettings.WATER_MINIMUM;
					}
				}
				else
				{
					if (num < 0 && IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("The moisture is sucked out of your throat.");
						if (water > RuleSettings.WATER_THIRSTY && num2 < RuleSettings.WATER_THIRSTY)
						{
							PlayWorldSound("sfx_characterMod_thirsty");
						}
					}
					if (!flag && Water + num > RuleSettings.WATER_MAXIMUM && IsPlayer() && Popup.ShowYesNo("Drinking that much will probably make you sick, do you want to continue?") != 0)
					{
						return false;
					}
					Water += num;
					if (Water > RuleSettings.WATER_MAXIMUM)
					{
						StringBuilder parameter = E.GetParameter<StringBuilder>("MessageHolder");
						StringBuilder stringBuilder = parameter ?? Event.NewStringBuilder();
						if (IsPlayer())
						{
							stringBuilder.Compound("You drank way too much!");
						}
						flag3 = InduceVomitingEvent.Send(ParentObject, stringBuilder, E);
						if (Water > RuleSettings.WATER_MAXIMUM)
						{
							Water = RuleSettings.WATER_MAXIMUM;
						}
						if (stringBuilder.Length > 0 && parameter == null)
						{
							EmitMessage(stringBuilder, ' ', FromDialog: true);
						}
					}
					else if (Water < RuleSettings.WATER_MINIMUM)
					{
						Water = RuleSettings.WATER_MINIMUM;
					}
				}
				if (ParentObject.HasRegisteredEvent("AfterDrank"))
				{
					Event @event = Event.New("AfterDrank");
					@event.SetParameter("Amount", num);
					@event.SetFlag("Forced", flag);
					@event.SetFlag("External", flag2);
					@event.SetFlag("Vomited", flag3);
					ParentObject.FireEvent(@event);
				}
				if (num < 0)
				{
					CheckCompanionThirstNotify(HighPriority: true);
				}
				if (flag3)
				{
					return false;
				}
			}
			else if (E.ID == "AddFood")
			{
				bool flag4 = HungerLevel > 0;
				string stringParameter = E.GetStringParameter("Satiation", "Snack");
				bool flag5 = E.HasFlag("Meat");
				bool num3 = ParentObject.HasPart<Carnivorous>();
				if (!num3 || flag5)
				{
					if (stringParameter == "Snack")
					{
						CookingCounter -= Food.SMALL_HUNGER_AMOUNT;
					}
					else if (stringParameter == "Meal")
					{
						CookingCounter = 0;
					}
				}
				if (num3)
				{
					if (flag5)
					{
						if (flag4)
						{
							Campfire.RollTasty(10, bCarnivore: true);
						}
					}
					else if (Stat.Random(0, 1) == 0)
					{
						if (ParentObject.IsPlayer())
						{
							Popup.Show("Ugh, you feel sick.");
						}
						ParentObject.ApplyEffect(new Ill(100));
					}
				}
			}
			return base.FireEvent(E);
		}

		public bool IsFamished()
		{
			return HungerLevel >= 2;
		}

		public void UpdateHunger()
		{
			int num = CalculateCookingIncrement();
			if (CookingCounter < num || ParentObject.HasPropertyOrTag("Robot"))
			{
				if (HungerLevel != 0)
				{
					ParentObject.RemoveEffect<Famished>();
					HungerLevel = 0;
					CookCount = 0;
				}
			}
			else if (CookingCounter >= num * 2)
			{
				if (HungerLevel != 2)
				{
					HungerLevel = 2;
					if (!ParentObject.HasEffect<Famished>() && !ParentObject.HasPart<Discipline_MindOverBody>())
					{
						ParentObject.ApplyEffect(new Famished());
					}
					ParentObject.FireEvent("BecameFamished");
					CookCount = 0;
				}
			}
			else if (CookingCounter >= num && HungerLevel != 1)
			{
				if (IsPlayer())
				{
					PlayWorldSound("sfx_characterMod_hungry");
				}
				ParentObject.RemoveEffect<Famished>();
				HungerLevel = 1;
				ParentObject.FireEvent("BecameHungry");
				CookCount = 0;
			}
		}

		public bool ProcessNaturalHealing(int Turns = 1)
		{
			if (HitCounter > 0)
			{
				HitCounter -= Turns;
			}
			bool flag = ParentObject.HasPart<Regeneration>();
			if (HitCounter <= 0 || flag)
			{
				int value = (20 + 2 * ParentObject.StatMod("Toughness") + 2 * ParentObject.StatMod("Willpower")) * Turns;
				eRegenerating.ID = "Regenerating";
				eRegenerating.SetParameter("Amount", value);
				ParentObject.FireEvent(eRegenerating);
				eRegenerating.ID = "Regenerating2";
				ParentObject.FireEvent(eRegenerating);
				value = eRegenerating.GetIntParameter("Amount");
				if (value < 0)
				{
					value = 0;
				}
				if (HitCounter > 0)
				{
					value /= 2;
				}
				if (ParentObject.HasEffect<Meditating>())
				{
					value *= 3;
				}
				if (ParentObject.HasPart<LuminousInfection>() && IsDay() && ParentObject.CurrentZone != null && ParentObject.CurrentZone.Z <= 10)
				{
					value = value * 85 / 100;
				}
				RegenCounter += value;
				if (RegenCounter > 100)
				{
					int num = (int)Math.Floor((double)RegenCounter / 100.0);
					RegenCounter %= 100;
					if (Water > 0)
					{
						ParentObject.GetStat("Hitpoints").Penalty -= num;
						if (ParentObject.IsPlayer())
						{
							Sidebar.Update();
						}
					}
					else
					{
						if (IsPlayer())
						{
							return false;
						}
						CheckCompanionThirstNotify();
					}
				}
			}
			return true;
		}

		public void CheckCompanionThirstNotify(bool HighPriority = false)
		{
			if (Water <= 0 && !ParentObject.IsPlayer() && !ParentObject.IsTrifling && ParentObject.IsPlayerLed() && IComponent<GameObject>.Visible(ParentObject) && XRLCore.Core.Game.Turns - ParentObject.GetLongProperty("LastCompanionThirstNotify", 0L) >= 100 && ParentObject.GetFreeDrams() < 1)
			{
				string message = "{{R|" + ParentObject.T(int.MaxValue, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: false, WithoutTitles: true, Short: true, BaseOnly: false, null, IndicateHidden: false, SecondPerson: true, Reflexive: false, null) + ParentObject.Is + " dehydrated and will be unable to heal naturally until " + ParentObject.it + ParentObject.GetVerb("get", PrependSpace: true, PronounAntecedent: true) + " water to " + (ParentObject.HasPart<Amphibious>() ? ("douse " + ParentObject.itself + " with") : "drink") + ".}}";
				if (HighPriority)
				{
					Popup.Show(message);
				}
				else
				{
					IComponent<GameObject>.AddPlayerMessage(message);
				}
				ParentObject.SetLongProperty("LastCompanionThirstNotify", XRLCore.Core.Game.Turns);
			}
		}

		public void ResetCookingCounter()
		{
			CookingCounter = 0;
		}
	}
}