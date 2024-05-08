using HarmonyLib;

using UnityEngine;

namespace TestPlugin.Patches;

[HarmonyPatch(typeof(ChatScreenInput))]
[HarmonyPatch("GainFocus")]
internal class ChatGainFocusPatch
{
    private static void Postfix()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
