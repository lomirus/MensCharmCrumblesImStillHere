# MensCharmCrumblesImStillHere

> 全世界男性魅力下降一百倍而我不变

![](./images.jpg)

除主角以外，世界上所有男性角色的魅力减半（最高魅力变为出众）。

~~防止玩家被牛。~~

## 实现

后端 Harmony Postfix 补丁，钩住 `Character.GetAttraction()`：
- 非太吾 + 男性 → 返回值除以 2

## 目录结构

```
MensCharmCrumblesImStillHere/
├── Config.lua      # 模组元数据
├── Plugins/        # 编译后的 DLL
├── src/            # C# 源码（.NET 8）
├── .gitignore
└── README.md
```

## 构建

```bash
cd src
dotnet build
```

DLL 自动输出到 `../Plugins/`。

## 安装

将整个文件夹放入：
```
The Scroll Of Taiwu/Mod/
```
