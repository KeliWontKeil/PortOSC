# Changelog

## 1.1.1 (Local)
- 更新版本管理体系为 `1.2.3`：
  - `1`：大版本
  - `2`：可推送小版本
  - `3`：本地修订号（每次本地提交 `+1`）
- 文档同步：`docs/AI_WORKFLOW.md`、`docs/RULES.md`、`.github/copilot-instructions.md`、`docs/PROJECT_DESCRIPTION.md`

## 1.1.0 (Local)
- 开始一次性大范围重构（高频曲线更新逻辑、接收端口模块化、接收管线解耦）
- 统一接收端口抽象与接收事件聚合
- 重写数据接收解析到独立管线
- 曲线更新改为批量消费和定时刷新

## 1.0.0
- 原始稳定基线版本
