using HarmonyLib;

using UnityEngine;

namespace TestPlugin.Patches;

[HarmonyPatch(typeof(ChatScreenInput))]
[HarmonyPatch("LoseFocus")]
internal class ChatLoseFocusPatch
{
    private static void Postfix()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
