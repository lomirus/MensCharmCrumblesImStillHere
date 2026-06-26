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
    /// 缓存的最高魅力档位：1-8 为有效缩放档位，其他值（如 9）不修改。
    /// </summary>
    private static int _maxCharm;

    /// <summary>
    /// 若为 true，男身女相角色魅力保持不变。
    /// </summary>
    private static bool _femboyCharmRemains;

    /// <summary>
    /// 若为 true，女身男相角色魅力保持不变。
    /// </summary>
    private static bool _tomboyCharmRemains;

    /// <summary>
    /// 各魅力档位的上限值，从 1 档到 9 档。
    /// 非人=100, 可憎=200, 不扬=300, 寻常=400, 出众=500, 瑾瑜=600, 龙姿=700, 绝世=800, 天人=900。
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

    /// <summary>游戏中魅力值的绝对上限。</summary>
    private const short MaxCharmValue = 900;

    public override void Initialize()
    {
        ReadAllSettings();

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
        ReadAllSettings();
    }

    /// <summary>
    /// 从模组域读取全部配置并缓存。
    /// 框架传回的下拉选项索引是 0-based，需加 1 对齐 Config.lua 的 Options（1-indexed）。
    /// </summary>
    private void ReadAllSettings()
    {
        if (DomainManager.Mod == null)
            return;

        int val = 0;
        if (DomainManager.Mod.GetSetting(ModIdStr, "MaxCharm", ref val))
        {
            _maxCharm = val + 1;
        }

        bool bVal = false;
        if (DomainManager.Mod.GetSetting(ModIdStr, "FemboyCharmRemains", ref bVal))
        {
            _femboyCharmRemains = bVal;
        }

        bVal = true;
        if (DomainManager.Mod.GetSetting(ModIdStr, "TomboyCharmRemains", ref bVal))
        {
            _tomboyCharmRemains = bVal;
        }
    }

    /// <summary>
    /// Character.GetAttraction() 的后置补丁。
    /// 将非太吾男性角色的魅力按比例缩放至选定档位的上限区间内。
    ///
    /// 性别判据：
    ///   bio=1 display=1 → 男身男相 → 始终缩放
    ///   bio=1 display=0 → 男身女相 → FemboyCharmRemains 为 true 时跳过
    ///   bio=0 display=1 → 女身男相 → TomboyCharmRemains 为 true 时跳过
    ///   bio=0 display=0 → 女身女相 → 始终跳过
    /// </summary>
    private static void GetAttractionPostfix(Character __instance, ref short __result)
    {
        if (__instance.IsTaiwu())
            return;

        int bioGender = __instance.GetGender();
        int displayGender = __instance.GetDisplayingGender();

        if (bioGender == 0)
        {
            // 女身女相跳过，女身男相按 TomboyCharmRemains 判断
            if (displayGender == 0 || _tomboyCharmRemains)
                return;
        }
        else
        {
            // 男身男相缩放，男身女相按 FemboyCharmRemains 判断
            if (displayGender == 0 && _femboyCharmRemains)
                return;
        }

        int maxCharm = _maxCharm;

        if (maxCharm < 1 || maxCharm > 8)
            return;

        short original = __result;
        short upperBound = TierUpperBounds[maxCharm - 1];

        // 按比例缩放：new = floor(original / 900 * upperBound)
        // 边界处理：900 映射到目标档位上限减一（即目标档位内的最大值）
        if (original == MaxCharmValue)
            __result = (short)(upperBound - 1);
        else
            __result = (short)(original * upperBound / MaxCharmValue);
    }
}
