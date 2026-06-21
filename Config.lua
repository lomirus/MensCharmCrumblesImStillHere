return {
	-- ====== 基本信息 ======
	Title = "全世界男性魅力下降一百倍而我不变",
	Author = "",
	Description = "除主角以外，世界上所有男性角色的魅力除以2。",
	Cover = nil,
	WorkshopCover = nil,
	Version = "0.1.0.0",

	-- ====== 兼容性 ======
	GameVersion = "1.0.10",
	Source = 0,  -- 0 = 本地模组
	FileId = 0,

	-- ====== 加载方式 ======
	FrontendPlugins = {
		-- [1] = "MyModFrontend.dll",
	},
	BackendPlugins = {
		[1] = "MensCharmCrumblesImStillHere.dll",
	},

	-- ====== 用户设置 ======
	DefaultSettings = { },

	-- ====== 高级选项 ======
	Visibility = 0,
	HasArchive = false,
	ChangeConfig = false,
	NeedRestartWhenSettingChanged = false,

	-- ====== 标签 ======
	TagList = {
		[1] = "Modifications",
		[2] = "Compatible Mods",
	},

	-- ====== 更新日志 ======
	UpdateLogList = {
		[1] = {
			Timestamp = 1750492800, -- 2025-06-21 (可替换为当前时间戳)
			LogList = {
				[1] = "项目初始化",
			},
		},
	},
}
