using System;
using XRL.Language;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
    [Serializable]
    public class LargerFolk_WeightPreach: IPart
    {
        public bool GainEnabled => XRL.UI.Options.GetOption("Option_LargerFolk_DynamicWeightGainOn").EqualsNoCase("Yes");

        public int Volume = 20;

        public int Chance = 4;

        public int ChatWait = 350;

        public string Range = "10";

        public string Duration = "350";

        public string Book = "LargerFolk_WeightSounds";

        public string Filter;

        public string FilterExtras;

        public string Prefix = "=subject.T= =verb:yell= {{W|'";

        public string Frozen = "You hear inaudible panting.";

        public string Postfix = "'}}";

        public bool SmartUse = true;

        public bool inOrder;

        public int MinWeightStage = 2;

        [NonSerialized]
        private int Line = -1;

        public int LastTalk;

        [NonSerialized]
        private string[] _Lines;

        public string[] Lines
        {
            get
            {
                return _Lines;
            }
            set
            {
                _Lines = value;
            }
        }

        public string Quotes
        {
            set
            {
                _Lines = value?.Split(";;");
            }
        }

        public int GetCount()
        {
            if (!_Lines.IsNullOrEmpty())
            {
                return _Lines.Length;
            }
            if (!Book.IsNullOrEmpty() && BookUI.Books.TryGetValue(Book, out var value))
            {
                return value.Pages.Count;
            }
            return 0;
        }

        public string GetLineText()
        {
            if (!_Lines.IsNullOrEmpty())
            {
                return _Lines[Line];
            }
            if (!Book.IsNullOrEmpty() && BookUI.Books.TryGetValue(Book, out var value))
            {
                return value.Pages[Line].FullText.Replace("\n", " ").Replace("  ", " ").Trim();
            }
            return null;
        }

        public override bool SameAs(IPart p)
        {
            return false;
        }

        public override bool AllowStaticRegistration()
        {
            return true;
        }

        public override void Write(GameObject Basis, SerializationWriter Writer)
        {
            base.Write(Basis, Writer);
            string[] lines = _Lines;
            int num = ((lines != null) ? lines.Length : 0);
            Writer.WriteOptimized(num);
            for (int i = 0; i < num; i++)
            {
                Writer.WriteOptimized(_Lines[i]);
            }
        }

        public override void Read(GameObject Basis, SerializationReader Reader)
        {
            base.Read(Basis, Reader);
            int num = Reader.ReadOptimizedInt32();
            if (num > 0)
            {
                _Lines = new string[num];
                for (int i = 0; i < num; i++)
                {
                    _Lines[i] = Reader.ReadOptimizedString();
                }
            }
        }

        public override void Register(GameObject Object, IEventRegistrar Registrar)
        {
            Registrar.Register("CanSmartUse");
            Registrar.Register("CommandSmartUseEarly");
            Registrar.Register("BeginTakeAction");
            base.Register(Object, Registrar);
        }

        public void PreacherHomily(GameObject who, bool Dialog)
        {
            // if parent object isn't fat enough, don't emit these preaches
            if (!ParentObject.HasPart<LargerFolk_WeightGain>())
            {
                return;
            }
            if (GainEnabled && ParentObject.GetPart<LargerFolk_WeightGain>().WeightStage < MinWeightStage)
            {
                return;
            }


            if ((!Dialog && !ParentObject.IsAudible(IComponent<GameObject>.ThePlayer, Volume)) || !ParentObject.FireEvent("CanPreach"))
            {
                return;
            }
            if (ParentObject.IsFrozen())
            {
                EmitMessage(Frozen, ' ', Dialog);
                return;
            }
            int count = GetCount();
            if (!inOrder || Line == -1)
            {
                Line = Stat.Random(0, count - 1);
            }
            string lineText = GetLineText();
            if (lineText.IsNullOrEmpty())
            {
                return;
            }
            lineText = lineText.StartReplace().AddObject(ParentObject).ToString();
            if (Filter != null)
            {
                lineText = TextFilters.Filter(lineText, Filter, FilterExtras);
            }
            string text = Prefix.StartReplace().AddObject(ParentObject).ToString();
            string text2 = Postfix.StartReplace().AddObject(ParentObject).ToString();
            IComponent<GameObject>.EmitMessage(who ?? ParentObject, text + lineText + text2, ' ', Dialog);
            if (lineText.EndsWith(".") && !lineText.EndsWith("..."))
            {
                lineText = lineText.Substring(0, lineText.Length - 1);
            }
            if (!Dialog)
            {
                ParentObject.ParticleText("{{W|'" + lineText + "'}}", IgnoreVisibility: true);
            }
            if (inOrder)
            {
                if (Line < count - 1)
                {
                    Line++;
                }
                else
                {
                    Line = 0;
                }
            }
        }

        public void PreacherHomily(bool Dialog)
        {
            PreacherHomily(null, Dialog);
        }

        public override bool FireEvent(Event E)
        {
            if (E.ID == "CanSmartUse")
            {
                if (SmartUse && !ParentObject.IsPlayerLed() && ConversationScript.IsPhysicalConversationPossible(ParentObject))
                {
                    return false;
                }
            }
            else if (E.ID == "CommandSmartUseEarly")
            {
                if (SmartUse && ConversationScript.IsPhysicalConversationPossible(ParentObject))
                {
                    GameObject gameObjectParameter = E.GetGameObjectParameter("User");
                    if (gameObjectParameter.IsPlayer())
                    {
                        if (!ParentObject.IsPlayerLed())
                        {
                            PreacherHomily(gameObjectParameter, Dialog: false);
                        }
                    }
                    else
                    {
                        PreacherHomily(Dialog: false);
                    }
                }
            }
            else if (E.ID == "BeginTakeAction" && ParentObject.InActiveZone())
            {
                LastTalk--;
                int chance_mod = 1;

                // if gain is not enabled, weight huffs n puffs always happen at a reduced rate
                if (!GainEnabled)
                {
                    chance_mod = 2;
                }
                if (LastTalk < 0 && (Chance/chance_mod).in100())
                {
                    LastTalk = ChatWait;
                    PreacherHomily(Dialog: false);
                }
                
            }
            return base.FireEvent(E);
        }
    }
}