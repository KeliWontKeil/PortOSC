# PortOSC 项目描述（AI 工作流初始化）

## 1. 项目定位
`PortOSC` 是一个基于 `.NET 8` 的 `Windows Forms` 桌面应用，定位为轻量级串口/网络调试工具，核心是“多协议接收 + 文本显示 + 简易示波器 + 数据转发”。

当前重构版本：`1.1.1`（本地）

版本管理规则：`1.2.3`（1=大版本，2=可推送小版本，3=本地修订号）

当前支持：
- 串口（SerialPort）
- TCP Server（单客户端）
- TCP Client
- UDP

## 2. 当前功能概览
- 多种数据源接入与状态管理（连接、断开、错误事件）
- 接收数据文本显示（ASCII/HEX）
- 发送数据（字符串/HEX）
- OSC 风格帧头帧尾提取并转换为多通道数值数据
- 基于 `ScottPlot` 的实时曲线绘制
- 数据转发服务器（接收后可继续转发）
- 配置保存/加载（JSON）
- 辅助工具窗体：Hex/Char 转换

## 3. 代码结构（现状）
- `src/Program.cs`：程序入口
- `forms/Form1.cs`：主窗体，承载绝大部分业务逻辑
- `forms/Form1.Designer.cs`：主窗体控件定义
- `forms/Form_HexToChar.cs`：Hex/Char 工具窗体
- `src/SerialPortConnect.cs`：串口封装
- `src/TcpConnect.cs`：TCP Client/Server 封装
- `src/UdpConnect.cs`：UDP 封装
- `src/Tools.cs`：线程安全 UI 调用、配置读写、日志显示、值变更事件工具
- `widget/NumBerTextBox/NumberTextBox.cs`：数值输入控件

## 4. 架构特征（关键结论）
1. **UI 与业务强耦合**：`Form1` 同时承担连接管理、协议处理、数据解析、绘图、配置读写。
2. **事件驱动为主**：串口/TCP/UDP 均通过事件把接收数据抛到 `Form1`。
3. **线程模型混合**：后台 Task + UI 线程 `Invoke`；部分发送逻辑使用 fire-and-forget。
4. **可维护性风险点**：
   - `Form1.cs` 文件过大（功能聚合）
   - 协议通道和 UI 控件逻辑重复（状态灯、按钮启停）
   - 错误处理策略分散，异常分类不统一

## 5. 已识别的优先重构方向
1. 抽离 `Form1` 中“连接状态 UI 更新”重复逻辑（减少重复代码）
2. 抽离“数据接收解析管线”（原始字节 -> 文本显示/OSC解析/转发）
3. 引入统一接口（如 `ITransportEndpoint`）抽象串口/TCP/UDP
4. 统一日志与异常策略（可分级、可追踪）
5. 为关键路径补充最小可测单元（解析、配置、边界值）

## 6. 当前已知问题（代码与 README 一致）
- 高频数据（>50Hz）下曲线更新不稳定
- 扩展性不足，UI 与接口实现耦合过深

## 7. AI 协作建议
后续每次改动建议遵循：
1. 先小范围重构（不改行为）
2. 再加功能/修复 bug
3. 每次改动后进行构建验证
4. 对外行为变化必须补文档

## 8. 1.1.x 重构落地（本地）
1. 增加接收端口抽象与聚合：`IReceiveEndpoint` + `ReceiveEndpointHub`
2. 增加接收解析管线：`ReceivePipeline`
3. 曲线更新从“逐帧 Invoke 刷新”改为“队列 + 定时批量刷新”
4. 版本号更新至 `1.1.x` 本地修订序列
