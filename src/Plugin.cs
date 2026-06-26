using GameData.Domains;
using GameData.Domains.Character;
using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;

namespace MensCharmCrumblesImStillHere;

[PluginConfig("MensCharmCrumblesImStillHere", "全世界男性魅力下降一百倍而我不变", "1.0.0")]
public class Plugin : TaiwuRemakePlugin
{
    private Harmony? _harmony;

    /// <summary>
    /// Cached MaxCharm setting:
    /// 1-8 = proportionally scale charm to the selected tier,
    /// other (9, etc.) = no modification.
    /// </summary>
    private static int _maxCharm;

    /// <summary>
    /// Upper bounds of each charm tier, 0-indexed (tier 1..9).
    /// 非人=100, 可憎=200, ..., 绝世=800, 天人=900.
    /// </summary>
    private static readonly short[] TierUpperBounds =
    [
        100, // 非人    [0, 100)
        200, // 可憎    [100, 200)
        300, // 不扬    [200, 300)
        400, // 寻常    [300, 400)
        500, // 出众    [400, 500)
        600, // 瑾瑜    [500, 600)
        700, // 龙姿    [600, 700)
        800, // 绝世    [700, 800)
        900, // 天人    [800, 900]
    ];

    /// <summary>Absolute maximum charm value in the game.</summary>
    private const short MaxCharmValue = 900;

    public override void Initialize()
    {
        ReadMaxCharmSetting();

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

    public override void OnModSettingUpdate()
    {
        ReadMaxCharmSetting();
    }

    /// <summary>
    /// Reads the MaxCharm setting from the mod domain and caches it.
    /// The framework passes dropdown indices as 0-based, so we add 1
    /// to align with Config.lua Options (which are 1-indexed).
    /// </summary>
    private void ReadMaxCharmSetting()
    {
        if (DomainManager.Mod != null)
        {
            int val = 0;
            if (DomainManager.Mod.GetSetting(ModIdStr, "MaxCharm", ref val))
            {
                // Convert from 0-based framework index to 1-based tier index
                _maxCharm = val + 1;
            }
        }
    }

    /// <summary>
    /// Postfix for Character.GetAttraction().
    /// Proportionally scales attraction for non-player male characters
    /// down to the configured tier's upper bound.
    /// </summary>
    private static void GetAttractionPostfix(Character __instance, ref short __result)
    {
        // Skip the player (Taiwu)
        if (__instance.IsTaiwu())
            return;

        // Skip female characters — use displayed gender
        // Gender.Male == 1, Gender.Female == 0
        if (__instance.GetDisplayingGender() != 1)
            return;

        int maxCharm = _maxCharm;

        // Only scale when a valid tier (1-8) is configured
        if (maxCharm < 1 || maxCharm > 8)
            return;

        short original = __result;
        short upperBound = TierUpperBounds[maxCharm - 1];

        // Proportional scaling: new = floor(original / 900 * upperBound)
        // Edge case: 900 maps to upperBound - 1 (the highest value within the target tier)
        if (original == MaxCharmValue)
            __result = (short)(upperBound - 1);
        else
            __result = (short)(original * upperBound / MaxCharmValue);
    }
}
