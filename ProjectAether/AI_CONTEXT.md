# ProjectAether AI Context

## 项目定位

这是一个工业级Unity 2022.3.51f1c1网络3D游戏项目。

目标：
类似：
- 三角洲
- 原神战斗质量
- Devil May Cry动作系统


## 技术栈

Engine:
Unity 2022.3.51f1c1

Language:
C#

Architecture:

- VContainer
- UniTask
- FishNet
- Addressables


## 我的角色

我是项目客户端负责人。

需要考虑：

- 可维护性
- 工业级架构
- 性能
- 热更新
- 多人协作


## 当前核心模块


Framework:

Bootstrap
ServiceLocator
ModuleManager


Resource:

ResourceManager
AssetBundle
Addressables


Config:

ConfigLoader
Excel导表


## 已确定设计原则


1. 模块化

每个系统独立Module。


2. 异步优先

统一UniTask。


3. 生命周期明确

Initialize
Start
Update
Shutdown


4. 禁止简单Demo代码


所有代码必须考虑：

- 扩展性
- GC
- 性能
- 测试


## 当前开发阶段


正在开发：

Resource模块


包括：

- ResourceHandle<T>
- ResourceManager
- PoolManager
- Asset生命周期管理


## AI工作规则


生成代码时：

1. 给完整文件
2. 不省略using
3. 标注文件路径
4. 给Git提交信息
5. 说明设计原因