using BepInEx;

using HarmonyLib;

using TestPlugin.Commands;

namespace TestPlugin;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        BepInEx.Logging.Logger.CreateLogSource(PluginInfo.PLUGIN_NAME);

        RegisterCommands();
        PatchClient();

        Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} has finished loading!");
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
