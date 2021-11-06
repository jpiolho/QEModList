# QEModList
A server / program to easily load third-party mods into Quake Enhanced addons list

## Warning
This program is still in alpha, so very likely there is issues.

## Requirements
* .NET 5.0 Windows Desktop Runtime
* Port 80 available for hosting

## How to install
1. Extract .exe to a folder.
2. Place sources.json in the same folder.
3. Edit sources.json to your liking
4. Start Quake with the command line `+ui_addonsBaseURL "http://localhost/"` **OR** When Quake starts, before you open the Addon's page, type in the console: `ui_addonsBaseURL "http://localhost/"`
5. Open the addons page

## What is the mod loader mod?
When running crossplay mods that use the 'id1' folder, they're always "Active" as far as the game is concerned.
This 'Mod Loader' mod is a dummy mod that helps you to load these mods.
1. Activate Mod Loader
2. Select the multiplayer id1 mod you wish to use
3. Delete it
4. Re-download it
5. Activate it
6. Done!
