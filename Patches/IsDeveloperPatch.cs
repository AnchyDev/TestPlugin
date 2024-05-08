using HarmonyLib;

namespace TestPlugin.Patches;

[HarmonyPatch(typeof(ChatScreenInput))]
[HarmonyPatch("IsDeveloper")]
internal class IsDeveloperPatch
{
    private static void Postfix(ref bool __result)
    {
        __result = true;
    }
}
