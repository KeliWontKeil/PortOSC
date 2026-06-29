# 在 VSCODE 中编译运行 PortOSC

本文档说明如何在 **Visual Studio Code** 中编译、调试和运行 PortOSC 项目。

## 前置要求

1. **.NET 8 SDK** 或更高版本
   - 在终端运行 `dotnet --version` 确认已安装
   - 本项目目标框架为 `net8.0-windows`，.NET 8/9/10 SDK 均可向下兼容

2. **Visual Studio Code**
   - 推荐安装以下扩展:
     - **C# Dev Kit** (Microsoft) - 提供完整的 C# 项目支持
     - **C#** (Microsoft) - 基本 C# 语言支持
     - **.NET Extension Pack** (可选)

## 快速编译

### 方法一：命令行编译（推荐）

由于系统环境变量 `Platform` 可能被其他工具（如 Python）占用，编译时需显式指定 Platform 参数：

```bash
# 还原 NuGet 包
dotnet restore PortOSC.csproj

# 编译项目（必须指定 Platform 为 "Any CPU"）
dotnet build PortOSC.csproj -p:Platform="Any CPU"

# 编译并运行
dotnet run --project PortOSC.csproj -p:Platform="Any CPU"

# 以 Release 模式编译
dotnet build PortOSC.csproj -p:Platform="Any CPU" -c Release
```

> **注意**: 编译输出路径为 `bin\Any CPU\Debug\net8.0-windows\`，这是正常的。如果省略 `-p:Platform="Any CPU"`，系统环境变量 `Platform` 的值会被 MSBuild 错误地用作平台目录名，导致编译失败。

### 方法二：使用 Visual Studio 2026

直接用 IDE 打开 `PortOSC.slnx` 解决方案文件，选择 `Debug` 配置直接按 F5 运行。Visual Studio 会自动处理平台配置。

## VSCODE 调试配置

1. 在 VSCODE 中打开工作区文件 `PortOSC.code-workspace` 或直接打开项目根目录

2. 确保安装了 **C# Dev Kit** 扩展

3. 按 `F5` 启动调试，或创建 `.vscode/launch.json` 配置：

```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "Launch PortOSC",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            "program": "${workspaceFolder}/bin/Any CPU/Debug/net8.0-windows/PortOSC.dll",
            "args": [],
            "cwd": "${workspaceFolder}",
            "console": "internalConsole",
            "stopAtEntry": false
        }
    ]
}
```

4. **重要**: 创建 `.vscode/tasks.json` 配置编译任务：

```json
{
    "version": "2.0.0",
    "tasks": [
        {
            "label": "build",
            "command": "dotnet",
            "type": "process",
            "args": [
                "build",
                "${workspaceFolder}/PortOSC.csproj",
                "-p:Platform=\"Any CPU\""
            ],
            "problemMatcher": "$msCompile",
            "group": {
                "kind": "build",
                "isDefault": true
            }
        }
    ]
}
```

> **重要**: 编译任务和调试配置中的 `-p:Platform="Any CPU"` 参数**不可省略**，否则会因系统 `Platform` 环境变量冲突导致编译失败。

## 常见问题

### 编译错误：无法创建目录 `bin\C:\Users\...\Scripts\`

**原因**: 系统环境变量 `Platform` 被设置为 Python 或其他工具的路径，MSBuild 读取后作为输出平台目录名。

**解决**: 
- 编译时始终添加 `-p:Platform="Any CPU"` 参数
- 或临时在终端清除 `Platform` 变量：`set Platform=Any CPU`

### 找不到 `System.IO.Ports` 等 NuGet 包

**解决**: 运行 `dotnet restore PortOSC.csproj` 还原所有 NuGet 依赖。

### 窗口一闪而过

**确保**: 使用 `dotnet run` 而不是直接双击生成的 .dll 文件。或者进入输出目录运行 `dotnet PortOSC.dll`。

## 项目结构说明

```
PortOSC/
├── PortOSC.csproj          # 项目文件
├── PortOSC.slnx            # 解决方案文件
├── src/                    # 源代码
│   ├── Program.cs          # 入口
│   ├── SerialPortConnect.cs
│   ├── TcpConnect.cs
│   ├── UdpConnect.cs
│   ├── TransportAbstractions.cs
│   ├── ReceivePipeline.cs
│   └── Tools.cs
├── forms/                  # WinForms 窗体
│   ├── Form1.cs            # 主窗体
│   ├── Form_HexToChar.cs
│   └── Form_SendStringLibrary.cs
└── widget/                 # 自定义控件
    └── NumBerTextBox/