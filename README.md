# Test Plugin
This is a test plugin for the game Savage Lands.

I have not seen a lot of modding for this game so all of the content in this plugin is not for normal gameplay and is intended as research.

## Setup

### Install BepInEx
- Download `BepInEx 64-bit`
- Extract the contents of the zip file into the root path of `Savage Lands`
- Run `Savage Lands` once to generate BepInEx configuration
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
- Install `.NET SDK 7`.
- Open solution/project file.
- Compile project.
- Move `TestPlugin.dll` into the `Bepinex/plugins` path.

> [!NOTE]
> You may have to add a project reference to the `Steam/../SavageLands/SavageLands_Data/Managed/Assembly-CSharp.dll` assembly to compile.