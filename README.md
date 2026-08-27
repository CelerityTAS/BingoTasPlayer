# BingoTasPlayer

## Install instructions (hopefully)
Download this file via Github Desktop or download the zip and place the entire folder (probably named BingoTasPlayer) into your Celeste Mod folder.
Then download (if not already there) the CelesteTAS and BingoClient mods through olympus.
Open the zip files of those mods and copy CelesteTAS-EverestInterop.dll and BingoClient.dll into the BingoTasPlayer folder in your mod folder.

Open up the BingoTasPlayer.sln file using Visual Studio. In the Solution viewer right click Dependencies (or something similar, my VS is in german) and search for the tab where you can browse files to add as dependencies. Find BingoClient and CelesteTAS dll's and add them to the projects Dependencies. You should now be able to press Control+B to build the project.

Installing the tas files:
Go to https://github.com/CelerityTAS/GMBingoPlayer, download it (and possibly unzip it) and place it inside your Celeste Folder (not your Mods folder).

Start Everest. If a Folder TmpBingoAIFiles was created, it should have worked. You might need to install some dependencies to get it to work. You might also need to enable mods, idk.

## Programming a tas
Open the BingoTASRouter.cs file. The route variable is what you will need to change to make it go a different route.
It is a List of RouteChange Objects, which contains:
- info about a tas file (importantly the file-location, name, start-label and end-label), although there is a helper class called TASObjectiveInfo, which has a constructor of objective name, startlabel and endlabel
- an array of ticks that are supposed to happen during the file, these have a delay as first parameter and then Objective as second, which contains Objective name (irrelevant) and index (0-indexed) of the objective.

I'd recommend looking at T6, T5 and T4 for more info on how you might use them. Some are already made, like rtm, but they are also explained in the file.

## Running a tas
- Build the project again with Control+B and fix all errors that might occur.
- Bind a bind to StartBind in the settings.
- Create a Bingo Board with the seed and copy the link to the board.
- Make sure there are no objectives ticked when starting the tas, as the game will turbo-lag and you will need to force-close the game
- Go to the file creation screen, connect to BingoClient on a save-file and then hover over begin.
- Press the StartBind.
If you are not connected to BingoClient the game will crash.

If you ever need to restart the tas, you need to restart the game and then go back to step 4 above.

## Recording a tas
Just use OBS, don't try tas-recorder