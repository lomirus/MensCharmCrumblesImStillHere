return {
	Title = "全世界男性魅力下降一百倍而我不变",
	Author = "mirus",
	Description = "可自定义除主角外所有男性角色的最高魅力等级。\n将原魅力值按比例缩放至选定区间内，保持相对层次感。\n\n[strike]防止玩家被牛[/strike]",
	WorkshopCover = "images.jpg",
	Version = "0.1.1.0",
	GameVersion = "1.0.32.0",
	Source = 0,
	FileId = 3748873190,
	BackendPlugins = {
		[1] = "MensCharmCrumblesImStillHere.dll",
	},
	DefaultSettings = {
		[1] = {
			SettingType = "Dropdown",
			Key = "MaxCharm",
			DisplayName = "最高魅力",
			Description = "其他男性角色的最高魅力",
			GroupName = "模组配置",
			Options = {
				[1] = "非人",
				[2] = "可憎",
				[3] = "不扬",
				[4] = "寻常",
				[5] = "出众",
				[6] = "瑾瑜",
				[7] = "龙姿",
				[8] = "绝世",
				[9] = "天人（无效果）",
			},
			DefaultValue = 2,
		},
		[2] = {
			SettingType = "Toggle",
			Key = "FemboyCharmRemains",
			DisplayName = "男娘魅力不变",
			Description = "非太吾的男身女相角色魅力保持不变",
			GroupName = "模组配置",
			DefaultValue = false,
		},
		[3] = {
			SettingType = "Toggle",
			Key = "TomboyCharmRemains",
			DisplayName = "假小子魅力不变",
			Description = "非太吾的女身男相角色魅力保持不变",
			GroupName = "模组配置",
			DefaultValue = true,
		},
	},
	Visibility = 0,
	HasArchive = false,
	ChangeConfig = false,
	NeedRestartWhenSettingChanged = false,
	TagList = {
		[1] = "Modifications",
		[2] = "Compatible Mods",
	},
	UpdateLogList = {
		[1] = {
			Timestamp = 1750492800,
			LogList = {
				[1] = "项目初始化",
			},
		},
		[2] = {
			Timestamp = 1782017064,
		},
		[3] = {
			Timestamp = 1782017174,
		},
		[4] = {
			Timestamp = 1782480271,
		},
	},
	Cover = "images.jpg",
	SettingGroups = {
		[1] = "模组配置",
	},
}
