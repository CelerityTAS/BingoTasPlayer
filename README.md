# BingoTasPlayer

## Install instructions (hopefully)
Download this file via Github Desktop or download the zip and place the entire folder (probably named BingoTasPlayer) into your Celeste Mod folder.
Then download (if not already there) the CelesteTAS and BingoClient mods through olympus.
Open the zip files of those mods and copy CelesteTAS-EverestInterop.dll and BingoClient.dll into the BingoTasPlayer folder in your mod folder.

Open up the BingoTasPlayer.sln file using Visual Studio. In the Solution viewer right click Dependencies (or something similar, my VS is in german) and search for the tab where you can browse files to add as dependencies. Find BingoClient, BingoUI's and CelesteTAS dll's and add them to the projects Dependencies. You should now be able to press Control+B to build the project.

Installing the tas files:
Go to https://github.com/CelerityTAS/GMBingoPlayer, download it (and possibly unzip it) and place it inside your Celeste Folder (not your Mods folder).

Start Everest. If a Folder TmpBingoAIFiles was created, it should have worked. You might need to install some dependencies to get it to work. You might also need to enable mods, idk.

## Programming a tas
Open the BingoTASRouter.cs file. The Add function is what you need to call to make it go a different route.
The way to play one of the tas files from the "objective repository" directory is to just enter it's filename into the Add command.
IF you want it to start or stop at different points, you need to add a start- and endlabel argument.
- startlabel is mostly going to be Start, if entered from previous checkpoint, or map OR RTM for entering from the map-select.
There is also DTS for farewell for all files that can have DTS.

- the endlabel is where to end, like at the Cassette, at the end of a theo cutscene (TheoEnd) or at heart.
For ticking objectives you would use the functions 
- GetAfterCompletion with an int[] of objectives (0 indexed) that you want to tick.
- GetAfterLabel with a dictionary of string,int, which gets objective number x after label y.
These can just go after name, startlabel or endlabel to tell the tas when to tick objectives.

I'd recommend looking at what is already there to get an idea of what these Add functions can look like.

## Running a tas
- Build the project again with Control+B and fix all errors that might occur.
- Bind a bind to StartBind in the settings.
- Create a Bingo Board with the seed and copy the link to the board.
- Go to the file creation screen, connect to BingoClient on a save-file and then hover over begin.
- Press the StartBind.

## Recording a tas
Just use OBS, don't try tas-recorder