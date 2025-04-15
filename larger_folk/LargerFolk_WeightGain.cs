using System;
using System.Collections.Generic;
using System.Text;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.Rules;
using XRL.World.Parts.Mutation;


namespace XRL.World.Parts
{

[Serializable]
public class LargerFolk_WeightGain : IPart
{
    //If this is disabled, it will not be subject to the weight gain system.
    public bool IsEnabled => XRL.UI.Options.GetOption("Option_LargerFolk_DynamicWeightGainOn").EqualsNoCase("Yes");

    //Can lose weight by walking around
    public bool CanExercise = true;

    //total calories from food eaten
    //degrades over time, current value determines weight stage
    //increased by eating cooking with ingredients or food items
    //and other sources
    public int TotalCalories = 0;
    //0 = thin (default)
    //1 = fat
    //2 = obese
    //3 = immobile
    public int WeightStage = 0;

    public int CalorieThresholdFat = 5000;
    public int CalorieThresholdObese = 12000;
    public int CalorieThresholdImmobile = 20000;

    public LargerFolk_WeightGain()
    {
        if (!IsEnabled)
        {
            WeightStage = 0;
            TotalCalories = 0;


        }
    }

    public override void Register(GameObject Object, IEventRegistrar Registrar)
	{
        if (IsEnabled)
        {
            Registrar.Register("AfterMoved");
            Registrar.Register("OnWeightStageChange");
            Registrar.Register("ActivateMechanicalWings");

        }
		base.Register(Object, Registrar);
	}

   

    public override bool FireEvent(Event E)
    {
        if (E.ID == "AfterMoved")
        {
            if (CanExercise)
            {
                if (ParentObject.HasPart<Stomach>() && ParentObject.GetPart<Stomach>().HungerLevel > 0)
                {
                    ChangeCalories(-2);
                }
            }
            
            if (WeightStage >= 3)
            {
                if (Stat.Random(1, 4) == 1)
                {
                    ParentObject.ApplyEffect(new LargerFolk_ImmobileStuck(12, 25, "Web Stuck Restraint", null, "stuck", "in", ParentObject.ID));
                }
            }
        }

        return base.FireEvent(E);
    }

    

    public void ChangeCalories(int val)
    {
        int last_calories = TotalCalories;
        TotalCalories += val;

        if (TotalCalories < 0)
        {
            TotalCalories = 0;
        }

        ConfirmWeightStage(last_calories, TotalCalories);
    }

    void ConfirmWeightStage(int old_calories, int new_calories)
    {
        int last_weight_stage = WeightStage;
        if (CaloriesToStage(old_calories) == CaloriesToStage(new_calories))
        {
            return;
        }
        else if (TotalCalories >= CalorieThresholdImmobile)
        {
            if (WeightStage < 3)
            {
                if (ParentObject.IsPlayer())
                {
                    if (ParentObject.HasPart<LargerFolk_Adipophobia>())
                    {
                        StageChangePlayerPopup("Despite your 'Best Efforts', your overindulgence has wreaked havoc on your figure.\nYour body has swollen well past the point any conscious being would find worrying, and you have grown so flabby that your overfed form can no longer ambulate on its own power.");
                    }
                    else if (ParentObject.HasPart<LargerFolk_FatFetish>())
                    {
                        StageChangePlayerPopup("Your efforts have paid dividends. You can feel your plush body spread out far past its original bounds, testament to your incredible appetite.\nThe price of your decadence is your mobility, but you can't help but blush even at that.\n You are now immobile, and have never felt better.");
                    }
                    else
                    {
                        StageChangePlayerPopup("Your body has swollen with adipose to the point that you have become immobile.");
                    }
                }
                else
                {
                    ParentObject.EmitMessage(new string(ParentObject.GetDisplayName(int.MaxValue, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: true, ColorOnly: false, Visible: true, WithoutTitles: false, ForSort: false, Short: true, BaseOnly: false, WithIndefiniteArticle: false, WithDefiniteArticle: false, null, IndicateHidden: false, Capitalize: false, SecondPerson: false, Reflexive: false, true) + " has gained weight and become immobile.") );
                }
            }
            
            WeightStage = 3;
        }
        else if (TotalCalories >= CalorieThresholdObese)
        {
            if (WeightStage < 2)
            {
                if (ParentObject.IsPlayer()) StageChangePlayerPopup("Your excessive snacking has widened your figure. You've become obese!");
                else ParentObject.EmitMessage(new string(ParentObject.GetDisplayName(int.MaxValue, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: true, ColorOnly: false, Visible: true, WithoutTitles: false, ForSort: false, Short: true, BaseOnly: false, WithIndefiniteArticle: false, WithDefiniteArticle: false, null, IndicateHidden: false, Capitalize: false, SecondPerson: false, Reflexive: false, true) + " has gained weight, and become obese.") );

            }
            else if (WeightStage > 2)
            {
                if (ParentObject.IsPlayer()) StageChangePlayerPopup("Your lack of caloric intake has started showing, even on your plush form.\nYou have slimmed down to mere obesity.");
                else ParentObject.EmitMessage(new string(ParentObject.GetDisplayName(int.MaxValue, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: true, ColorOnly: false, Visible: true, WithoutTitles: false, ForSort: false, Short: true, BaseOnly: false, WithIndefiniteArticle: false, WithDefiniteArticle: false, null, IndicateHidden: false, Capitalize: false, SecondPerson: false, Reflexive: false, true) + " has lost weight, and become obese.") );
                
            }
            
            WeightStage = 2;
        }
        else if ((TotalCalories >= CalorieThresholdFat) && (WeightStage < 1))
        {
            if (ParentObject.IsPlayer())
            {
                Popup.Show("You've gotten fat!");
            }
            else
            {
                ParentObject.EmitMessage(new string(ParentObject.GetDisplayName(int.MaxValue, null, null, AsIfKnown: false, Single: false, NoConfusion: false, NoColor: false, Stripped: true, ColorOnly: false, Visible: true, WithoutTitles: false, ForSort: false, Short: true, BaseOnly: false, WithIndefiniteArticle: false, WithDefiniteArticle: false, null, IndicateHidden: false, Capitalize: false, SecondPerson: false, Reflexive: false, true) + " has become fat.") );
            }
            
            WeightStage = 1;

            // BodyPart bodyPart = ParentObject.Body
		    // if (bodyPart != null)
		    // {
		    // 	string managerID = ManagerID;
		    // 	bool? extrinsic = true;
		    // 	string[] orInsertBefore = new string[2] { "Feet", "Roots" };
		    // 	bodyPart.AddPartAt("Icy Outcrop", 0, null, null, null, null, managerID, null, null, null, null, null, null, null, extrinsic, null, null, null, null, null, "Icy Outcrop", orInsertBefore);
		    // 	TryGrowMushroom();
		    // }
        }
        else if ((TotalCalories < CalorieThresholdFat) && (WeightStage >= 1))
        {
            StageChangePlayerPopup("You've become thin.");
            WeightStage = 0;
        }

        if ((last_weight_stage - WeightStage) != 0)
        {
            Event @event = Event.New("OnWeightStageChange");
            @event.SetParameter("Actor", ParentObject);
            @event.SetParameter("WeightStage", WeightStage);
            @event.SetParameter("LastWeightStage", last_weight_stage);
            @event.SetParameter("TotalCalories", TotalCalories);
            ParentObject.FireEvent(@event);
        }
    }

    public void InitializeWeightStage()
    {
        if (TotalCalories >= CalorieThresholdImmobile)
        {
            WeightStage = 3;
        }
        else if (TotalCalories >= CalorieThresholdObese)
        {
            WeightStage = 2;
        }
        else if ((TotalCalories >= CalorieThresholdFat) && (WeightStage < 1))
        {
            WeightStage = 1;
        }
        else if ((TotalCalories < CalorieThresholdFat) && (WeightStage >= 1))
        {
            WeightStage = 0;
        }

      
        Event @event = Event.New("OnWeightStageInitialized");
        @event.SetParameter("Actor", ParentObject);
        @event.SetParameter("WeightStage", WeightStage);
        @event.SetParameter("TotalCalories", TotalCalories);
        ParentObject.FireEvent(@event);
        
    }

    public int CaloriesToStage(int cal)
    {
        if (cal >= CalorieThresholdImmobile)
        {
            return 3;
        }
        else if (cal >= CalorieThresholdObese)
        {
            return 2;
        }
        if (cal >= CalorieThresholdFat)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    
    public void SetWeightStage(int n)
    {
        var last_calories = TotalCalories;

        switch (n)
        {
            case 0:
                TotalCalories = 1000;
                break;
            case 1:
                TotalCalories = CalorieThresholdFat;
                break;
            case 2:
                TotalCalories = CalorieThresholdObese;
                break;
            case 3:
                TotalCalories = CalorieThresholdImmobile;
                break;
            default:
                break;
        }
        
        ConfirmWeightStage(last_calories, TotalCalories);
    }
    public string GetWeightString()
    {
        if (WeightStage >= 3)
        {
            return "immobile";
        }
        else if (WeightStage == 2)
        {
            return "obese";
        }
        else if (WeightStage == 1)
        {
            return "fat";
        }
        else
        {
            return "lean";
        }
    }

    private void StageChangePlayerPopup(string s)
    {
        if (ParentObject.IsPlayer())
        {
            Popup.Show(s);
        }
    }
}

}