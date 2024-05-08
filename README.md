# Test Plugin
This is a test plugin for the game Savage Lands.

I have not seen a lot of modding for this game so all of the content in this plugin is not for normal gameplay and is intended as research.

## Setup

### Install BepInEx
- Download `BepInEx 64-bit`
- Extract the contents of the zip file into the root path of `Savage Lands`
- Run `avage Lands` once to generate BepInEx configuration
- Exit `Savage Lands`

> [!CAUTION]
> If you get a black screen when starting the game, you must do the following steps:
> - Open the `BepInEx/config/BepInEx.cfg` configuration in a text editor.
> - Search for `[Preloader.Entrypoint]`.
> - Edit `Assembly` from `UnityEngine.dll` to `Assembly-CSharp.dll`.
> - Edit `Type` from `Application` to `GameManager`.
> - Relaunch the game to ensure you no longer get a black screen.
> - Exit the game.

### Setup Plugin
- Clone this repository.
- Compile it using `.NET SDK 7`.
- Move `TestPlugin.dll` into the `Bepinex/plugins` path.