# 01_Singleton_Problem: Tại Sao Dự Án Lớn Hạn Chế Dùng Singleton?

Dự án này là mã nguồn mẫu chuẩn theo bài viết **Blog 01** trong vault kỹ thuật của ToanLB:
*"Tại sao dự án lớn hạn chế dùng Singleton dù nó rất tiện?"* (File: `Blog_01_Tai_sao_du_an_lon_han_che_dung_Singleton.md`).

---

## 🎯 Đối Chiếu Trực Tiếp Với Code Mẫu Trong Bài Viết

Toàn bộ code trong thư mục này được tổ chức bám sát **100% từng dòng code mẫu** trong bài viết:

### 1. Code Trước Khi Refactor (Mục 3 trong bài viết)
- [Scripts/Before_Refactor/CombatController.cs](file:///Users/toanlb/toanlb_game/unity-architecture-di/Assets/01_Singleton_Problem/Scripts/Before_Refactor/CombatController.cs):
  Khớp 100% với code mẫu ban đầu trong bài:
  - Gọi `GameManager.Instance.CurrentEnemyDefense`
  - Tính `finalDamage = Mathf.Max(1, baseDamage - defense)`
  - Gọi `SoundManager.Instance.PlaySfx("Hit_Sound")`
  - Sửa trực tiếp `GameManager.Instance.PlayerScore += finalDamage * 10`
- [Scripts/Before_Refactor/GameManager.cs](file:///Users/toanlb/toanlb_game/unity-architecture-di/Assets/01_Singleton_Problem/Scripts/Before_Refactor/GameManager.cs): Singleton mẫu chứa `CurrentEnemyDefense` và `PlayerScore`.
- [Scripts/Before_Refactor/SoundManager.cs](file:///Users/toanlb/toanlb_game/unity-architecture-di/Assets/01_Singleton_Problem/Scripts/Before_Refactor/SoundManager.cs): Singleton âm thanh mẫu.

### 2. Code Sau Khi Refactor (Mục 3 trong bài viết)
- **1. Interfaces:**
  - [IEnemyStatsProvider.cs](file:///Users/toanlb/toanlb_game/unity-architecture-di/Assets/01_Singleton_Problem/Scripts/After_Refactor/Interfaces/IEnemyStatsProvider.cs): `int Defense { get; }`
  - [IAudioService.cs](file:///Users/toanlb/toanlb_game/unity-architecture-di/Assets/01_Singleton_Problem/Scripts/After_Refactor/Interfaces/IAudioService.cs): `void PlaySfx(string soundName);`
  - [IScoreTracker.cs](file:///Users/toanlb/toanlb_game/unity-architecture-di/Assets/01_Singleton_Problem/Scripts/After_Refactor/Interfaces/IScoreTracker.cs): `void AddScore(int amount);`
- **2. Pure C# Core Logic:**
  - [CombatProcessor.cs](file:///Users/toanlb/toanlb_game/unity-architecture-di/Assets/01_Singleton_Problem/Scripts/After_Refactor/Core/CombatProcessor.cs): Nhận 3 interface qua Constructor, tính toán `ExecuteAttack(int baseAttack)` độc lập hoàn toàn với `UnityEngine`.
- **3. MonoBehaviour trên Scene:**
  - [CombatController.cs](file:///Users/toanlb/toanlb_game/unity-architecture-di/Assets/01_Singleton_Problem/Scripts/After_Refactor/Controllers/CombatController.cs): Có hàm `Initialize(CombatProcessor)` và `DealDamage(int baseDamage)`.
- **Composition Root & Implementations:**
  - [CombatBootstrapper.cs](file:///Users/toanlb/toanlb_game/unity-architecture-di/Assets/01_Singleton_Problem/Scripts/After_Refactor/Bootstrapper/CombatBootstrapper.cs): Composition Root kết nối toàn bộ dependency trên Scene.
  - [EnemyStats.cs](file:///Users/toanlb/toanlb_game/unity-architecture-di/Assets/01_Singleton_Problem/Scripts/After_Refactor/Implementations/EnemyStats.cs): Cung cấp `Defense` cho Enemy trên Scene.
  - [SoundService.cs](file:///Users/toanlb/toanlb_game/unity-architecture-di/Assets/01_Singleton_Problem/Scripts/After_Refactor/Implementations/SoundService.cs): Triển khai `IAudioService`.
  - [ScoreService.cs](file:///Users/toanlb/toanlb_game/unity-architecture-di/Assets/01_Singleton_Problem/Scripts/After_Refactor/Implementations/ScoreService.cs): Triển khai `IScoreTracker`.

### 3. Unit Tests Tự Động (Mục 3 trong bài viết)
- [Tests/CombatProcessorTests.cs](file:///Users/toanlb/toanlb_game/unity-architecture-di/Assets/01_Singleton_Problem/Tests/CombatProcessorTests.cs):
  Khớp 100% với code test mẫu trong bài viết:
  - `FakeEnemyStats` triển khai `IEnemyStatsProvider`
  - `SpyAudioService` triển khai `IAudioService`
  - `SpyScoreTracker` triển khai `IScoreTracker`
  - Test 1: `ExecuteAttack_AttackHigherThanDefense_DealsDamageAndAddsScore()`
  - Test 2: `ExecuteAttack_DefenseHigherThanAttack_ClampsMinimumOneDamage()`
  - Chạy trên **Unity Test Runner (EditMode)** với tốc độ **dưới 1 mili-giây** mà không cần Scene Unity!

---

## 🎬 Hướng Dẫn Quay Video YouTube

1. Mở Scene [01_Singleton_Problem_Demo.unity](file:///Users/toanlb/toanlb_game/unity-architecture-di/Assets/01_Singleton_Problem/01_Singleton_Problem_Demo.unity).
2. Nhấn **Play**: Panel OnGUI xuất hiện trên góc trái màn hình Game View.
3. Nhấn **"🔥 1. Bắn Đòn Đánh (Bad Singleton)"**: Minh họa cách cũ gọi trực tiếp Singleton và sửa biến toàn cục.
4. Nhấn **"✨ 2. Bắn Đòn Đánh (Clean Architecture DI)"**: Minh họa cách mới xử lý qua `CombatProcessor`.
5. Mở **Window > General > Test Runner**: Bấm **Run All** ở tab **EditMode** để chứng minh bộ Unit Test chạy xanh lét trong 0.01s.
