# Project Aether 测试规范

> **文件名：** `06_TestStandard.md`  
> **文档编号：** PAS-006  
> **版本：** v1.0  
> **状态：** Draft  
> **所属分类：** Project Standard  
> **适用项目：** Project Aether  
> **Unity 版本：** Unity 2022.3.51f1c1  
> **最后更新：** 2026-08-06  

---

## 1. 文档目的

本文档定义 Project Aether 的统一测试规范，适用于框架、资源、配置、战斗、角色、网络、UI、工具、构建和发布流程。

本规范的目标是：

- 建立可重复执行的测试体系。
- 保证核心模块在修改后仍保持正确行为。
- 降低回归缺陷。
- 提高架构和代码的可测试性。
- 保证测试结果可以追踪、复现和审计。
- 为版本发布建立明确的质量门禁。
- 防止 AI 生成代码在缺少验证的情况下进入项目。

---

## 2. 适用范围

本规范适用于：

- EditMode Test。
- PlayMode Test。
- 单元测试。
- 集成测试。
- 系统测试。
- 回归测试。
- 冒烟测试。
- 性能测试。
- 内存测试。
- 网络测试。
- 资源测试。
- 配置测试。
- 构建测试。
- 发布验收测试。
- 手动测试。
- 自动化测试。
- AI 生成代码验证。

---

## 3. 核心原则

### 3.1 Test the Contract

测试应优先验证：

- 对外行为。
- 生命周期。
- 状态转换。
- 输入和输出。
- 错误处理。
- 资源所有权。
- 兼容性。

禁止过度依赖私有实现细节。

---

### 3.2 Repeatable

测试必须尽量可重复执行。

相同环境、相同输入下，应得到相同结果。

避免依赖：

- 本地时间。
- 未控制随机数。
- 外部网络状态。
- 测试执行顺序。
- 其他测试残留状态。
- 本地缓存。
- 未清理的静态变量。

---

### 3.3 Isolated

测试之间应相互独立。

每个测试必须自行准备前置条件，并在结束后清理状态。

---

### 3.4 Fast Feedback

测试体系应提供不同层级的反馈速度。

推荐顺序：

```text
Static Validation
  ↓
Unit Test
  ↓
Integration Test
  ↓
PlayMode Test
  ↓
Performance Test
  ↓
Build Test
  ↓
Release Validation
```

---

### 3.5 Failure Must Be Diagnosable

测试失败时，应能够快速判断：

- 失败位置。
- 输入条件。
- 预期结果。
- 实际结果。
- 当前状态。
- 相关模块。
- 相关版本。

---

### 3.6 Test Risk, Not Just Lines

测试重点应覆盖高风险行为，而不是只追求覆盖率数字。

优先测试：

- 核心生命周期。
- 公共 API。
- 状态机。
- 资源加载与释放。
- 配置校验。
- 网络权威。
- 存档兼容。
- 错误恢复。
- 边界条件。
- 已修复 Bug。

---

## 4. 测试分层

Project Aether 使用以下测试层级：

| 层级 | 目的 |
|---|---|
| Static Validation | 检查代码和资源基础规则 |
| Unit Test | 验证单个类或函数 |
| Integration Test | 验证多个模块协作 |
| PlayMode Test | 验证 Unity Runtime 行为 |
| System Test | 验证完整功能流程 |
| Regression Test | 防止已修复问题再次出现 |
| Performance Test | 验证性能预算 |
| Build Test | 验证平台构建 |
| Release Test | 验证正式版本质量 |

---

## 5. 测试目录

推荐目录：

```text
Assets/
└── ProjectAether/
    └── Tests/
        ├── EditMode/
        ├── PlayMode/
        ├── Integration/
        ├── Performance/
        ├── Network/
        ├── Resource/
        ├── Config/
        └── TestData/
```

测试文档存放：

```text
Docs/05_Test/
```

---

## 6. 测试 Assembly Definition

测试代码必须使用独立 asmdef。

推荐：

```text
ProjectAether.Tests.EditMode
ProjectAether.Tests.PlayMode
ProjectAether.Tests.Integration
ProjectAether.Tests.Performance
```

测试 asmdef 可以依赖 Runtime 模块。

Runtime asmdef 禁止依赖测试 asmdef。

---

## 7. 测试命名

测试类命名：

```text
<TypeName>Tests
```

示例：

```csharp
ResourceManagerTests
ModuleManagerTests
ConfigManagerTests
```

测试方法命名：

```text
MethodName_Condition_ExpectedResult
```

示例：

```csharp
Register_WhenModuleAlreadyExists_ThrowsException()
Release_WhenReferenceCountIsZero_ThrowsException()
LoadAsync_WhenAssetDoesNotExist_ReturnsFailedHandle()
```

---

## 8. Arrange Act Assert

推荐使用 AAA 结构。

```csharp
[Test]
public void Register_WhenModuleIsValid_AddsModule()
{
    // Arrange
    var manager = new ModuleManager();
    var module = new FakeGameModule("Resource");

    // Act
    manager.Register(module);

    // Assert
    Assert.That(manager.Count, Is.EqualTo(1));
}
```

复杂测试可以用空行分隔，不强制写注释。

---

## 9. 单元测试规范

单元测试应：

- 测试单个职责。
- 尽量不依赖 Unity 场景。
- 尽量不依赖真实网络。
- 尽量不依赖真实磁盘。
- 执行速度快。
- 结果稳定。
- 失败原因明确。

适合 EditMode 的内容：

- 状态机。
- 配置校验。
- 引用计数。
- 数据转换。
- 数学计算。
- 业务规则。
- 生命周期状态验证。
- 纯 C# 服务。

---

## 10. 集成测试规范

集成测试验证多个模块协作。

示例：

```text
Bootstrap
  ↓
ModuleManager
  ↓
ResourceModule
  ↓
ConfigModule
```

集成测试必须明确：

- 参与模块。
- 初始化顺序。
- 测试环境。
- 依赖替换。
- 清理顺序。
- 失败诊断信息。

---

## 11. PlayMode 测试

PlayMode 测试适合：

- MonoBehaviour 生命周期。
- Scene 加载。
- GameObject 激活与销毁。
- Rigidbody 行为。
- Animator。
- UI 交互。
- Addressables 实例化。
- 网络对象。
- 协程和 UniTask 生命周期。

PlayMode 测试必须考虑：

- 帧等待。
- FixedUpdate。
- 场景切换。
- 对象销毁延迟。
- Domain Reload 设置。
- Time Scale。
- 测试超时。

---

## 12. UnityTest

需要跨帧执行时使用：

```csharp
[UnityTest]
public IEnumerator Object_WhenEnabled_RegistersToService()
{
    var gameObject = new GameObject();
    var component = gameObject.AddComponent<TestComponent>();

    yield return null;

    Assert.That(component.IsRegistered, Is.True);

    Object.Destroy(gameObject);
}
```

使用 UniTask 时，也可以通过适配方式运行异步测试，但必须保证异常能够传回测试框架。

---

## 13. 异步测试

异步测试必须：

- 有超时。
- 支持取消。
- 不使用无限等待。
- 正确传播异常。
- 清理未完成任务。

示例：

```csharp
[Test]
public async Task LoadAsync_WhenAssetExists_ReturnsLoadedHandle()
{
    using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(5));

    var handle = await _resourceManager.LoadAsync<GameObject>(_assetKey, timeout.Token);

    Assert.That(handle.State, Is.EqualTo(ResourceState.Loaded));
}
```

---

## 14. 测试替身

常见测试替身：

- Fake。
- Stub。
- Mock。
- Spy。
- In-Memory 实现。

应优先使用简单 Fake。

只有在需要验证交互时才使用 Mock。

禁止创建比真实系统更复杂的 Mock。

---

## 15. 时间依赖

业务逻辑禁止直接依赖不可控时间。

推荐：

```csharp
public interface ITimeService
{
    float DeltaTime { get; }
    double Time { get; }
}
```

测试中使用：

```csharp
public sealed class FakeTimeService : ITimeService
{
    public float DeltaTime { get; set; }
    public double Time { get; set; }
}
```

---

## 16. 随机数依赖

需要稳定测试时，随机数必须可控。

推荐：

```csharp
public interface IRandomService
{
    int Range(int minInclusive, int maxExclusive);
}
```

测试应使用固定种子或 Fake。

---

## 17. 外部依赖

以下依赖应尽量通过接口隔离：

- 网络。
- 文件系统。
- 时间。
- 随机数。
- Addressables。
- 平台 API。
- Analytics。
- 云存档。
- 支付。
- 用户账号。

---

## 18. 测试数据

测试数据应：

- 小。
- 明确。
- 可读。
- 独立。
- 可清理。
- 不依赖正式数据。

测试数据目录：

```text
Assets/ProjectAether/Tests/TestData/
```

禁止使用真实用户数据。

---

## 19. 边界测试

所有公共 API 应根据风险覆盖：

- 最小值。
- 最大值。
- 零。
- 负数。
- 空集合。
- null。
- 重复调用。
- 无效状态。
- 超时。
- 取消。
- 资源不存在。
- 网络断开。
- 数据损坏。

---

## 20. 状态机测试

状态机至少测试：

- 初始状态。
- 所有合法转换。
- 非法转换。
- 重复进入。
- 重复退出。
- 终止状态。
- 异常状态。
- 恢复流程。

示例：

```text
None → Created → Initialized → Running → Shutdown
```

必须测试：

```text
None → Running
```

是否被拒绝。

---

## 21. 生命周期测试

所有拥有生命周期的对象必须测试：

- Create。
- Initialize。
- Start 或 Running。
- Update。
- Shutdown。
- Dispose。
- 重复调用。
- 错误顺序。
- 部分失败。
- 中途取消。

---

## 22. 资源系统测试

资源系统至少覆盖：

- 首次加载。
- 重复加载。
- 并发加载。
- 加载成功。
- 加载失败。
- 取消加载。
- Retain。
- Release。
- 重复 Release。
- 引用计数下溢。
- Cache 命中。
- Cache 失效。
- Unload。
- Scene 切换。
- Addressables Handle 释放。
- 实例释放。
- 类型不匹配。
- AssetKey 不存在。

---

## 23. ResourceHandle 测试

`ResourceHandle<T>` 至少测试：

- 初始状态。
- `SetLoaded` 后状态。
- `SetFailed` 后状态。
- AssetKey 保留。
- Error 保留。
- Loaded Asset 类型正确。
- Dispose 是否幂等。
- 重复释放是否防护。
- 失败 Handle 是否可诊断。

---

## 24. Pool 测试

Pool 至少测试：

- 首次获取。
- 重复获取。
- 归还。
- 重复归还。
- 容量限制。
- 超容量销毁。
- Reset。
- Clear。
- Shutdown。
- 对象状态清理。
- 异常对象处理。

---

## 25. Config 测试

Config 至少测试：

- 正常加载。
- 空文件。
- 文件不存在。
- 重复 Key。
- 无效枚举。
- 越界数值。
- 缺失字段。
- 引用不存在。
- 循环引用。
- 版本不兼容。
- 热更新覆盖。
- Runtime 只读。
- 错误信息定位。

---

## 26. Bootstrap 测试

Bootstrap 至少测试：

- 初始化入口唯一。
- 重复 Initialize。
- 模块注册顺序。
- 模块 Create 顺序。
- Initialize 顺序。
- Update 顺序。
- Shutdown 逆序。
- 某模块初始化失败。
- 某模块关闭失败。
- Application Quit。
- 场景重载。

---

## 27. ModuleManager 测试

至少覆盖：

- Register 成功。
- 重复注册。
- null 模块。
- Create 调用次数。
- InitializeAll。
- UpdateAll。
- ShutdownAll。
- 状态校验。
- 顺序稳定。
- 部分失败。
- 清理后状态。

---

## 28. 网络测试

网络测试至少覆盖：

- Server 启动。
- Client 连接。
- 多客户端连接。
- 断线。
- 重连。
- 超时。
- Ownership。
- RPC 权限。
- 非法请求。
- Prediction。
- Reconciliation。
- 状态同步。
- 延迟。
- 丢包。
- 带宽。
- 版本不兼容。
- 服务端校验。
- 场景切换。
- Host 模式。
- Dedicated Server。

---

## 29. 网络权威测试

必须验证：

- Client 不能直接修改权威状态。
- 非 Owner 不能执行受限操作。
- Server 验证输入。
- 重复 RPC 不会造成重复结算。
- 延迟或乱序不会破坏状态。
- 客户端预测可以被校正。

---

## 30. 战斗测试

战斗系统至少覆盖：

- 伤害计算。
- 暴击。
- 防御。
- 抗性。
- 无敌。
- 死亡。
- 重复死亡。
- Buff。
- Debuff。
- 叠层。
- 状态免疫。
- Hit Stop。
- 网络同步。
- 多目标。
- 友军规则。
- 边界数值。

---

## 31. 角色测试

角色系统至少覆盖：

- 生成。
- 初始化。
- 移动。
- 跳跃。
- 受击。
- 死亡。
- 复活。
- 动画状态。
- 输入禁用。
- 网络 Ownership。
- 场景切换。
- 销毁。

---

## 32. UI 测试

UI 至少覆盖：

- 打开。
- 关闭。
- 重复打开。
- 数据刷新。
- 空数据。
- 输入锁定。
- 层级。
- 遮罩。
- 返回键。
- 分辨率适配。
- Safe Area。
- 本地化。
- 异步加载。
- 场景切换。
- 资源释放。

---

## 33. 存档测试

存档至少覆盖：

- 新建存档。
- 保存。
- 加载。
- 覆盖。
- 删除。
- 数据损坏。
- 版本升级。
- 缺失字段。
- 新增字段。
- 回滚。
- 云同步冲突。
- 平台切换。
- 非法数据。

---

## 34. 序列化兼容测试

修改序列化字段后必须验证：

- 旧 Scene。
- 旧 Prefab。
- 旧 ScriptableObject。
- 旧存档。
- 网络协议。
- 热更新数据。

---

## 35. 回归测试

每个已修复 Bug 应至少留下：

- 自动化测试。
- 或明确手动回归用例。

回归测试名称应能关联问题。

示例：

```csharp
Release_WhenCalledTwice_DoesNotUnderflowReferenceCount()
```

---

## 36. 冒烟测试

每次重要合入或构建后应执行冒烟测试。

最低内容：

- 启动游戏。
- 进入主场景。
- 加载资源。
- 加载配置。
- 创建角色。
- 执行基础移动。
- 打开核心 UI。
- 连接服务器。
- 退出游戏。

---

## 37. 性能测试

性能测试必须明确：

- 测试目标。
- 测试设备。
- Unity 版本。
- Build 配置。
- 场景。
- 持续时间。
- 采样方式。
- 基线。
- 目标。
- 实际结果。
- 结论。

---

## 38. 性能指标

常见指标：

- Average FPS。
- P1 FPS。
- P0.1 FPS。
- Main Thread Time。
- Render Thread Time。
- GPU Time。
- GC Alloc Per Frame。
- Managed Memory。
- Native Memory。
- Draw Calls。
- Batches。
- SetPass Calls。
- Triangle Count。
- Network Bandwidth。
- Loading Time。

---

## 39. 性能测试场景

至少包括：

- 空场景基线。
- 正常游戏场景。
- 高压场景。
- 最坏情况。
- 长时间运行。
- 场景反复切换。
- 大量对象生成销毁。
- 网络多实体。
- UI 大量刷新。
- Addressables 批量加载。

---

## 40. 性能回归

修改关键路径前后必须对比。

性能测试记录至少包含：

```text
Before
After
Difference
Target
Conclusion
```

禁止只提供单次结果。

---

## 41. 内存测试

内存测试重点：

- 场景进入前。
- 场景稳定后。
- 场景退出后。
- 重复进入退出。
- 资源加载前后。
- Addressables Release 后。
- 对象池 Clear 后。
- 网络断开后。
- 模块 Shutdown 后。

---

## 42. 泄漏测试

常见泄漏来源：

- 静态事件。
- 静态集合。
- Addressables Handle。
- 未取消 UniTask。
- CancellationTokenSource。
- 对象池。
- NativeArray。
- Texture。
- RenderTexture。
- Material 实例。
- Scene 引用。
- 网络对象。
- Editor 回调。

---

## 43. 稳定性测试

稳定性测试包括：

- 长时间运行。
- 高频场景切换。
- 高频连接断开。
- 高频资源加载释放。
- 高频生成销毁。
- 极端输入。
- 后台前台切换。
- 设备休眠恢复。
- 网络波动。
- 低内存压力。

---

## 44. 构建测试

构建测试至少验证：

- Unity BatchMode 构建。
- Development Build。
- Release Build。
- IL2CPP。
- 目标平台架构。
- Addressables Build。
- 包体生成。
- 安装。
- 启动。
- 升级安装。
- 卸载重装。
- 日志输出。
- 崩溃收集。

---

## 45. 平台测试

不同平台必须记录：

- OS。
- 设备型号。
- CPU。
- GPU。
- 内存。
- 分辨率。
- 系统版本。
- 图形 API。
- 网络环境。

---

## 46. 手动测试用例

手动测试用例格式：

```markdown
## Test Case

**ID:** TC-RESOURCE-001  
**Title:** Load and release a valid prefab  
**Priority:** High  
**Environment:** Unity Editor / Windows  

### Preconditions

### Steps

1. Start the test scene.
2. Request the prefab.
3. Wait for loading.
4. Release the handle.

### Expected Result

### Actual Result

### Status

Pass / Fail / Blocked
```

---

## 47. 测试优先级

测试用例优先级：

| 优先级 | 含义 |
|---|---|
| P0 | 核心流程，失败阻止发布 |
| P1 | 重要功能，失败通常阻止发布 |
| P2 | 普通功能 |
| P3 | 低风险和边缘体验 |

---

## 48. 测试状态

统一使用：

- Not Run。
- Running。
- Pass。
- Fail。
- Blocked。
- Skipped。
- Not Applicable。

Skipped 必须说明原因。

---

## 49. 测试环境

测试文档必须记录：

- Commit 或 Tag。
- Unity 版本。
- Package 版本。
- 平台。
- Build 类型。
- 配置版本。
- 资源版本。
- 服务端版本。
- 测试设备。
- 网络环境。

---

## 50. 测试结果

测试结果必须包含：

- 执行时间。
- 执行人。
- 通过数量。
- 失败数量。
- 阻塞数量。
- 已知问题。
- 结论。

---

## 51. 缺陷报告

缺陷报告必须包含：

```markdown
## Summary

## Environment

## Preconditions

## Steps to Reproduce

## Expected Result

## Actual Result

## Frequency

## Severity

## Logs

## Screenshots or Video

## Related Commit
```

---

## 52. 缺陷等级

| 等级 | 含义 |
|---|---|
| Critical | 崩溃、数据损坏、安全问题、无法发布 |
| High | 核心功能不可用 |
| Medium | 重要功能异常但有替代路径 |
| Low | 轻微功能或表现问题 |
| Cosmetic | 纯视觉或文案问题 |

---

## 53. 不稳定测试

Flaky Test 必须被视为真实问题。

禁止长期通过重复执行掩盖。

处理方式：

1. 记录失败频率。
2. 收集日志。
3. 查找共享状态。
4. 检查时间依赖。
5. 检查异步竞态。
6. 检查外部服务。
7. 修复或隔离。
8. 记录任务。

---

## 54. 测试超时

所有异步或 PlayMode 测试必须有合理超时。

超时应根据实际操作设置，不应过短或无限。

---

## 55. 测试清理

每个测试必须清理：

- GameObject。
- Scene。
- 临时文件。
- 静态状态。
- 事件订阅。
- CancellationTokenSource。
- ResourceHandle。
- Addressables 实例。
- 网络连接。
- 对象池。
- 测试配置。

---

## 56. 测试执行顺序

测试不得依赖顺序。

禁止：

```text
TestB 必须在 TestA 后执行。
```

若确实存在流程依赖，应合并为一个明确的集成测试。

---

## 57. 覆盖率

覆盖率用于发现未测试区域，不作为唯一质量指标。

重点关注：

- 核心框架。
- 公共 API。
- 状态机。
- 数据转换。
- 错误处理。
- 已知高风险逻辑。

禁止为了覆盖率测试无意义 getter/setter。

---

## 58. 自动化门禁

推荐 CI 门禁：

```text
Compile
  ↓
EditMode Tests
  ↓
PlayMode Tests
  ↓
Static Validation
  ↓
Build Validation
```

关键分支失败时禁止合入。

---

## 59. 静态验证

可自动化检查：

- asmdef 循环依赖。
- Namespace 与目录。
- Meta 文件缺失。
- 资源命名。
- 配置重复 Key。
- 文档链接。
- Commit 格式。
- 敏感信息。
- 空目录。
- 超大文件。
- Editor 代码引用。

---

## 60. AI 生成代码测试规则

AI 生成代码必须：

- 先明确当前接口。
- 先明确状态和生命周期。
- 补充对应测试。
- 不得声称未执行的测试通过。
- 不得省略失败路径。
- 不得只测试 happy path。
- 不得修改测试来掩盖实现错误。

---

## 61. AI 测试输出要求

AI 提供测试时必须说明：

- 测试目标。
- 测试文件。
- 测试依赖。
- 测试数据。
- 预期行为。
- 未验证部分。
- Unity 中的执行方式。

---

## 62. 测试文档结构

推荐测试文档：

```markdown
# Test Plan

## 1. Objective
## 2. Scope
## 3. Environment
## 4. Preconditions
## 5. Test Data
## 6. Test Cases
## 7. Performance Targets
## 8. Risks
## 9. Results
## 10. Conclusion
```

---

## 63. 测试文档命名

推荐：

```text
YYYYMMDD_Module_Test.md
```

示例：

```text
20260806_ResourceLifecycle_Test.md
20260806_ConfigImport_Test.md
```

大型长期测试计划可以使用：

```text
Resource_TestPlan.md
```

---

## 64. 测试评审

以下测试必须评审：

- 核心框架测试。
- 网络测试。
- 存档兼容测试。
- 性能测试。
- 发布验收测试。
- 安全相关测试。
- 高风险 Bug 回归测试。

---

## 65. 测试完成定义

测试完成必须满足：

- 用例已执行。
- 结果已记录。
- 失败已创建缺陷。
- Blocker 已处理。
- 环境已记录。
- 文档已保存。
- 结论明确。
- 相关 Review 已完成。

---

## 66. 功能完成定义

一个功能不能仅因“代码写完”而完成。

Definition of Done：

- [ ] 设计已批准。
- [ ] 代码已完成。
- [ ] Code Review 已通过。
- [ ] 单元测试已添加。
- [ ] 集成测试已添加。
- [ ] PlayMode 验证完成。
- [ ] 错误路径已验证。
- [ ] 性能影响已确认。
- [ ] 文档已更新。
- [ ] 回归测试已记录。
- [ ] 合入后验证通过。

---

## 67. 发布测试

发布前必须执行：

- 冒烟测试。
- 核心功能回归。
- 存档兼容。
- 网络连接。
- 资源更新。
- 配置更新。
- 性能测试。
- 内存测试。
- 安装与升级。
- 崩溃验证。
- 平台专项测试。
- 回滚验证。

---

## 68. 发布阻塞条件

以下情况阻止发布：

- 存在 Critical 缺陷。
- 存在未接受的 High 缺陷。
- 核心流程失败。
- 构建失败。
- 存档损坏。
- 网络权威失效。
- 明显资源泄漏。
- 严重性能回退。
- 安全问题。
- 回滚不可执行。
- 测试环境与版本不一致。

---

## 69. 测试数据保留

需要长期保留：

- 性能基线。
- 发布测试结果。
- 关键回归结果。
- 存档兼容样本。
- 网络协议兼容样本。
- 崩溃日志。
- 内存快照。
- 高风险问题证据。

禁止无规则保存大量无价值临时数据。

---

## 70. 测试工具

推荐工具：

- Unity Test Framework。
- Unity Performance Testing Extension。
- Unity Profiler。
- Memory Profiler。
- Frame Debugger。
- RenderDoc。
- Platform Profiler。
- Network Simulator。
- CI Build Pipeline。
- 自定义验证工具。

引入新工具前应评估维护成本和项目兼容性。

---

## 71. 测试日志

测试日志应：

- 使用统一模块前缀。
- 包含测试 ID。
- 包含关键输入。
- 包含失败状态。
- 避免过量输出。
- 避免敏感信息。

---

## 72. 测试可视化

对于车辆、战斗、物理、网络预测等复杂系统，可以增加：

- Gizmos。
- Debug Overlay。
- 状态面板。
- 曲线。
- 统计窗口。
- Replay。
- 输入录制。

调试可视化不得改变正式逻辑。

---

## 73. 物理测试

物理测试必须明确：

- Fixed Timestep。
- Rigidbody 设置。
- Collider。
- Physics Material。
- Layer Matrix。
- 初始位置。
- 初始速度。
- 质量。
- 重力。
- 测试帧数。

物理结果通常允许合理浮点误差。

---

## 74. 浮点测试

使用容差：

```csharp
Assert.That(actual, Is.EqualTo(expected).Within(0.001f));
```

容差必须根据业务意义选择。

---

## 75. 帧相关测试

避免只等待固定帧数而不检查条件。

不推荐：

```csharp
yield return new WaitForSeconds(2f);
```

推荐：

```csharp
yield return WaitUntilOrTimeout(() => handle.IsLoaded, 5f);
```

---

## 76. 场景测试

场景测试应：

- 使用专用测试场景。
- 明确加载模式。
- 清理当前场景。
- 避免污染 Build Settings。
- 验证退出后对象是否残留。

---

## 77. Addressables 测试环境

Addressables 测试必须区分：

- Use Asset Database。
- Simulate Groups。
- Existing Build。
- Packed Play Mode。
- 真机构建。

Editor 成功不代表正式构建成功。

---

## 78. 配置热更新测试

至少验证：

- 新配置下载。
- 版本比较。
- 校验失败。
- 下载中断。
- 回滚。
- 本地缓存。
- 配置不兼容。
- 服务端版本不匹配。

---

## 79. 网络模拟

网络测试应模拟：

- 高延迟。
- 抖动。
- 丢包。
- 乱序。
- 断线。
- 重连。
- 带宽限制。

---

## 80. 长时间运行测试

长时间测试应记录：

- 开始时间。
- 结束时间。
- 帧率趋势。
- 内存趋势。
- GC 趋势。
- 网络状态。
- 错误数量。
- 崩溃。
- 场景循环次数。

---

## 81. 测试失败处理

测试失败后：

1. 保存日志。
2. 保存环境。
3. 保存 Commit。
4. 判断是否可复现。
5. 缩小范围。
6. 创建缺陷。
7. 分配负责人。
8. 修复后新增回归测试。
9. 重新执行相关测试。

---

## 82. 测试跳过规则

测试只能在明确原因下跳过。

必须记录：

- 跳过原因。
- 负责人。
- 恢复条件。
- 关联任务。
- 截止时间。

禁止永久忽略失败测试。

---

## 83. 测试维护

代码变化时必须同步维护测试。

测试失效时应：

- 更新测试。
- 更新测试数据。
- 更新文档。
- 确认行为变化已批准。

禁止因为测试失败而直接删除测试。

---

## 84. 测试 Review Checklist

### Test Design

- [ ] 测试目标明确。
- [ ] 测试范围明确。
- [ ] 风险优先级正确。
- [ ] 正常路径覆盖。
- [ ] 失败路径覆盖。
- [ ] 边界条件覆盖。
- [ ] 兼容性覆盖。
- [ ] 回归风险覆盖。

### Test Code

- [ ] 命名符合规范。
- [ ] AAA 清晰。
- [ ] 测试相互独立。
- [ ] 没有顺序依赖。
- [ ] 没有共享污染。
- [ ] 异步有超时。
- [ ] 资源正确清理。
- [ ] 失败信息清晰。
- [ ] 没有无意义 Mock。

### Environment

- [ ] Unity 版本正确。
- [ ] Package 版本正确。
- [ ] 平台明确。
- [ ] Build 类型明确。
- [ ] 配置版本明确。
- [ ] 资源版本明确。
- [ ] 服务端版本明确。

### Results

- [ ] 结果可复现。
- [ ] 日志已保存。
- [ ] 失败已创建缺陷。
- [ ] 性能数据完整。
- [ ] 内存数据完整。
- [ ] 最终结论明确。
- [ ] 文档已更新。

---

## 85. 最终测试清单

- [ ] 编译通过。
- [ ] EditMode Test 通过。
- [ ] PlayMode Test 通过。
- [ ] 集成测试通过。
- [ ] 核心回归通过。
- [ ] 冒烟测试通过。
- [ ] 性能测试通过。
- [ ] 内存测试通过。
- [ ] 网络测试通过。
- [ ] 构建测试通过。
- [ ] 安装与升级通过。
- [ ] 回滚验证通过。
- [ ] 已知问题已记录。
- [ ] 测试文档已保存。
- [ ] Release Review 已完成。

---

## 86. 验收标准

本规范执行后，应达到：

- 核心模块拥有稳定自动化测试。
- 关键生命周期和状态转换可验证。
- 资源、配置和网络问题可重复测试。
- 每个已修复 Bug 尽量拥有回归测试。
- 性能和内存拥有可比较基线。
- 发布前拥有明确测试门禁。
- 测试结果可以追踪到 Commit、环境和版本。
- AI 生成代码不会绕过人工验证。
- 测试体系能够支持 Project Aether 长期迭代。

---

## 87. Change Log

| Version | Date | Description |
|---|---|---|
| v1.0 | 2026-08-06 | 创建 Project Aether 测试规范正式初稿 |

---

# End
