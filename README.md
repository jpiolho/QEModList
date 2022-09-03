# QEModList
A server / program to easily load third-party mods into Quake Enhanced addons list

## Requirements
* [.NET 6.0 Windows Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0/runtime)
* Port 80 available for localhost hosting

## How to install
1. Extract the zip to a folder.
2. Run the program
3. In your tray, you'll find the application icon.

   <img src="https://github.com/jpiolho/QEModList/blob/master/docs/images/tray.webp?raw=true" width="256" />
4. Right-click it and customize the "Sources"
   
   <img src="https://github.com/jpiolho/QEModList/blob/master/docs/images/sources.webp?raw=true" width="256" />
4. Start Quake with the command line `+ui_addonsBaseURL "http://localhost/"`

   **ALTERNATIVELY** When Quake starts, before you open the Addon's page, type in the console: `ui_addonsBaseURL "http://localhost/"`
   
   <img src="https://github.com/jpiolho/QEModList/blob/master/docs/images/arguments.webp?raw=true" width="256" />

5. Open the addons menu in-game
   
   <img src="https://github.com/jpiolho/QEModList/blob/master/docs/images/addons.webp?raw=true" width="256" />
   
## Known issues
* Quake currently doesn't like when there's too many addons in the list and you might not be able to click "Download" and "Activate" buttons.

## What is the mod loader mod?
When running crossplay mods that use the 'id1' folder, they're always "Active" as far as the game is concerned.
This 'Mod Loader' mod is a dummy mod that helps you to load these mods.
1. Activate Mod Loader
2. Select the multiplayer id1 mod you wish to use
3. Delete it
4. Re-download it
5. Activate it
6. Done!

## Source.json

This json contains a list of all the addon sources it should include in the mod list. The following sources are supported:
* Github
* Addons List
* Folder

### Example file
```json
{
  "version": 1,
  "sources": [
    [ "ADDONLIST", "https://mods.silver.idtech.services/" ],
    [ "ADDONLIST", "https://q1mods.xyz" ],
    [ "GITHUB", "https://github.com/jpiolho/QECTF/" ],
    [ "GITHUB", "https://github.com/jpiolho/QERocketOnly/" ],
    [ "GITHUB", "https://github.com/jpiolho/QENightmarePlus/" ],
    [ "GITHUB", "https://github.com/jpiolho/QEArmsRace/" ]
  ]
}
```
