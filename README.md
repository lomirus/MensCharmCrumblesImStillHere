# MensCharmCrumblesImStillHere

> 全世界男性魅力下降一百倍而我不变

除主角以外，世界上所有男性角色的魅力不得高于或等于400。

## 目录结构

```
MensCharmCrumblesImStillHere/
├── Config.lua      # 模组元数据与设置定义
├── Cover.png       # 封面图片（256x256）
├── Plugins/        # 编译后的 DLL 文件（.gitignore 已忽略）
├── src/            # C# 源代码（待创建）
├── .gitignore
└── README.md
```

## 开发

模组 DLL 编译后放入 `Plugins/` 目录，然后在 `Config.lua` 中注册：
- 前端插件（Unity 侧）：`FrontendPlugins`
- 后端插件（GameData 侧）：`BackendPlugins`

## 安装

将整个文件夹放入：
```
The Scroll Of Taiwu/Mod/
```
