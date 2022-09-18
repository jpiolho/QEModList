# QEModList
A server / program to easily load third-party mods into Quake Enhanced addons list

## How to install
1. Go over to the [releases page](https://github.com/jpiolho/QEModList/releases/)
2. Download latest setup & install
3. Start application
   
   <img src="https://github.com/jpiolho/QEModList/blob/master/docs/images/startmenu.webp?raw=true" width="256" />
4. In your tray, you'll find the application icon.

   <img src="https://github.com/jpiolho/QEModList/blob/master/docs/images/tray.webp?raw=true" width="256" />
5. Right-click it and customize the "Sources"
   
   <img src="https://github.com/jpiolho/QEModList/blob/master/docs/images/sources.webp?raw=true" width="256" />
6. Right-click the tray icon again and press "Launch Quake"

   <img src="https://github.com/jpiolho/QEModList/blob/master/docs/images/menu.webp?raw=true" width="256" />
7. Open the addons menu in-game
   
   <img src="https://github.com/jpiolho/QEModList/blob/master/docs/images/addons.webp?raw=true" width="256" />
   
## Known issues
* Quake currently doesn't like when there's too many addons in the list and you might not be able to click "Download" and "Activate" buttons.

## Requirements
* [.NET 6.0 Windows Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0/runtime) (The setup should install this automatically)
* Port 80 available for localhost hosting

## What is the mod loader mod?
When running crossplay mods that use the 'id1' folder, they're always "Active" as far as the game is concerned.
This 'Mod Loader' mod is a dummy mod that helps you to load these mods.
1. Activate Mod Loader
2. Select the multiplayer id1 mod you wish to use
3. Delete it
4. Re-download it
5. Activate it
6. Done!

## Where can I find the sources and options configuration files?
You'll find them at `%appdata%\jpiolho\qemodlist`

This json contai
