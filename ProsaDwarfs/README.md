# ProsaDwarfs
This a reposetory for the CodeArcade Challenge presented by Prosa.\
The 'Resultatvideo' is a demonstration of running the program. The video is recorded in the MOV container format, which most media players support including VLC.

## Dependencies
This challenge is written in C# and uses .NET 6.0.

## Running
The challenge can be run using the command: `dotnet run` with the .NET CLI installed and being stationed in the folder with the `.csproj` file.

The general procedure for the challenge is:\
1. Choose 2 random dwarfs and add them to the list
2. Repeat until nothing changes for 3 rounds or list is empty
    * Check if some dwarf randomly performs an reaction
    * Check if Snow White will enter the story
    * Perform 1 step of the chain reaction
    * Press any key to continue repetition
3. If the list is unchanged for 3 rounds
    * Snow White lures the remaining dwarfs outside
