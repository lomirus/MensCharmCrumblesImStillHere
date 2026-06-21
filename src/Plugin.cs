using GameData.Domains.Character;
using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;

namespace MensCharmCrumblesImStillHere;

[PluginConfig("MensCharmCrumblesImStillHere", "全世界男性魅力下降一百倍而我不变", "1.0.0")]
public class Plugin : TaiwuRemakePlugin
{
    private Harmony? _harmony;

    public override void Initialize()
    {
        _harmony = new Harmony("MensCharmCrumblesImStillHere");
        _harmony.Patch(
            original: AccessTools.Method(typeof(Character), nameof(Character.GetAttraction)),
            postfix: new HarmonyMethod(typeof(Plugin), nameof(GetAttractionPostfix))
        );
    }

    public override void Dispose()
    {
        _harmony?.UnpatchSelf();
    }

    /// <summary>
    /// Postfix for Character.GetAttraction().
    /// Caps attraction below 400 for all non-player male characters.
    /// </summary>
    private static void GetAttractionPostfix(Character __instance, ref short __result)
    {
        // Skip if already below threshold
        if (__result < 400)
            return;

        // Skip the player (Taiwu)
        if (__instance.IsTaiwu())
            return;

        // Skip female characters — use displayed gender
        // Gender.Male == 1, Gender.Female == 0
        if (__instance.GetDisplayingGender() != 1)
            return;

        // Cap at 399 (不得高于或等于400)
        __result = 399;
    }
}
