
Clone the repository with the big green button for whatever latest hottest changes i've pushed recently, check releases for the old beta and (eventually) big major releases with all-tested changes!

To install, go to the user folder of your computer, and navigate to 
AppData>LocalLow>Freehold Games>CavesOfQud>Mods. 
drag the larger_folk folder from the zip into the Mods folder
open up caves of qud, go to options and click modding: enable mods.
launch the game click the Mods option on the title screen, if you see larger folk appear there you've succeed, otherwise relaunch the game and check again, click enable. relaunch the game, and go to town. safe travels!
other places describe the process of installing qud mods more effectively, so search around if this doesn't work for you, i'm just some dope on a website and explaining stuff isn't my strong suit

MOD DETAILS

Right now, there are fat sprites for every player preset/calling/class, and a bunch of the creatures including almost all merchants and early game enemies.
There is a dynamic weight system, and weight effects such as breaking chairs and getting stuck in doors based on weight. Weight gain visual changes only apply to the preset characters and a (very) small amount of creatures that i added it to for testing purposes, it's kind of tedious to add it in for each creature and I want to be totally sure of the process first before i do it, in case i change how the visuals work later.
Some NPCs have new silly erotic dialogue and conversation options.
Some creatures have new descriptions reflecting their new visage.

-- Weight Gain --
dynamic weight gain can be enabled in the larger folk options at the bottom of the qud options menu!
eating food items increases your internal calories, walking around while hungry or famished burns calories. eating at a campfire does not increase calories, but as it reduces hunger it can prevent weight loss.
certain tonics can affect weight, such as the adipocytic injector. 
Creatures' current weight stage will be displayed next to their name, and you can check your own internal calories & estimated poundage at a Weight Scale.
consuming enough calories increases your weight stage, which you'll be notified of with a popup. current weight stages are slim -> Fat -> Obese -> Immobile/Morbidly Obese (Immobility causes frequent tripping/exhaustion based on strength and movespeed, forcing you to stop and rest for a bit after a certain number of steps.)
when weight gain is enabled becoming "fat" or heavier gives you the fat version of your sprite and creatures may have new descriptions for higher weight stages, though there are no more visual stages planned beyond simply "fatter than usual". 

CHANGELOG

V 3.02

- When creatures randomly determine their weight they will also add some extra calories randomly, so they aren't just at the exact threshold every time.


V 0.3.01

- weight gain now applies / alters a weight stage effect that displays in your / creatures properties, so it shows when you Look at them but isn't visible all the time (unless enabled in options)
- effects of current weight stage are a bit clearer now, since you can see it in your effects tab.

Release V 0.3.0

- Added changelog

- Added creatures for "Adipophilials" faction (not fully implemented yet)

- more sprite replacements

- added conversation delegate hooks for npcs to notice that your clothes are too tight

- Increased rate of weight loss at lower movement speeds (the idea being lower movement speed has the character
exerting more effort to perform that movement, the player loses weight faster while heavier, etc)

- Redo of how the mod interacts with armour, armour can now have a particular size it's made for and
will give an increasing tightness debuff as the wearer outsizes it. Both size and tightness are visible by 
examining the armour. Size/tightness only applies to body slot armour as of yet, since I felt like it would be weird to outsize
boots and helmets unless you were like, MASSIVE, which the mod just doesn't currently mechanically support the fantasy of being 
that large, so maybe it will be added later.  I wish qud had pants...

- Tightness debuff: armour loses DV for each weight stage you outsize it by, and has a slim chance of breaking outright when athletic/movement heavy skills are used

- New associated tinkering skill, Tailoring: allows you to tailor armour to be larger, smaller, or to fit your current size, consuming Bits.

- New armour mod, Stretchy. Stretchy negates tightness penalties for the affected armour.
(planned armour mod for the future, hammerspace, negates ALL effects of being overweight while powered, including resetting sprite to lean. be wary of normality gas...)

- Breaking a chair via sitting on it while being too fat for it now causes the sitter to be knocked prone

- the Clay Pots worn by some plants are now subject to LargerFolk_Armor effects

- Altered Adipocytic Tonic tinkering recipe to require [REDACTED] as an ingredient

- added new armours, the Bikini and Fullerite Bikini.

- moved all scripts to the Scripts folder, for organizaiton purposes

- altered movement penalty for weight gain, now a movespeed debuff rather than getting stuck

- fixed errors with sixshrew and gunwing sprites
