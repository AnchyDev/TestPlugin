using BepInEx;

using HarmonyLib;

using TestPlugin.Commands;

namespace TestPlugin;

[BepInPlugin(Plugin.GUID, Plugin.NAME, Plugin.VERSION)]
public class Plugin : BaseUnityPlugin
{
    public const string GUID = "TestPlugin";
    public const string NAME = "TestPlugin";
    public const string VERSION = "1.0.0";

    private void Awake()
    {
        BepInEx.Logging.Logger.CreateLogSource(Plugin.NAME);

        RegisterCommands();
        PatchClient();

        Logger.LogInfo($"Plugin {Plugin.GUID} has finished loading!");
    }

    private void RegisterCommands()
    {
        var handler = CommandHandler.Instance;

        Logger.LogInfo("Registering commands..");

        handler.RegisterCommand(new AddItemCommand());
        handler.RegisterCommand(new LookupItemCommand());
        handler.RegisterCommand(new FogCommand());
        handler.RegisterCommand(new HealCommand());
        handler.RegisterCommand(new TimeCommand());

        Logger.LogInfo($"'{handler.Commands.Count}' commands registered.");
    }

    private void PatchClient()
    {
        Logger.LogInfo("Patching client..");

        var harmony = new Harmony("dev.anchy.testplugin");
        harmony.PatchAll();

        Logger.LogInfo("Done patching.");
    }
}
