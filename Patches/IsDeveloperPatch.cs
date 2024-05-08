using HarmonyLib;

namespace TestPlugin.Patches;

[HarmonyPatch(typeof(ChatScreenInput))]
[HarmonyPatch("IsDeveloper")]
class IsDeveloperPatch
{
    static void Postfix(ref bool __result)
    {
        __result = true;
    }
}
