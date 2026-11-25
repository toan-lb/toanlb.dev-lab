# 🏛️ Unity Architecture & Engineering Lab

<div align="center">
  <img src="Assets/Images/Thumbnail_Engineering_Lab.png" alt="Unity Architecture Engineering Lab" width="100%" />

  <p align="center">
    <b>Kho lưu trữ mã nguồn kiểm thử cho các bài viết trên blog toanlb.dev.</b><br />
    Tác giả: <b>Lương Bảo Toàn</b> • Blog: <a href="https://toanlb.dev">toanlb.dev</a>
  </p>
</div>

---

## 📌 Giới thiệu dự án

Dự án này là chuỗi mã nguồn mẫu đi kèm các bài phân tích chuyên sâu về **Kiến trúc phần mềm trong Unity (Unity Game Architecture)** trên [toanlb.dev](https://toanlb.dev).

Để giữ cho repository luôn tinh gọn, sạch sẽ và tối ưu hiệu năng cho Unity Editor, mã nguồn được phân chia theo từng **Topic lớn tương ứng với từng Git Branch**. Mỗi branch sẽ bao gồm từ 5–7 bài demo hoàn chỉnh cùng unit test và scene tương tác trực quan.

---

## 🗺️ Mục lục các Chuyên đề (Topic Branches)

| # | Chuyên đề kiến trúc (Topic) | Git Branch | Số bài | Trạng thái |
|---|---|---|---|---|
| **01** | **Dependency Injection & Decoupling** | [`unity-architecture-di`](https://github.com/toan-lb/toanlb.dev-lab/tree/unity-architecture-di) | 5–7 bài | 🚀 Đang phát triển |
| **02** | Observer Pattern & Late-Join State Replay | `unity-architecture-observer` | 5 bài | 📅 Sắp ra mắt |
| **03** | Model-View-Presenter (MVP) & Clean UI | `unity-architecture-mvp` | 5 bài | 📅 Sắp ra mắt |
| **04** | Object Pooling & Zero GC Memory Alloc | `unity-architecture-pooling` | 5 bài | 📅 Sắp ra mắt |
| **05** | UniTask & Async Execution Control | `unity-architecture-async` | 5 bài | 📅 Sắp ra mắt |
| **06** | ScriptableObject & Data-Driven Architecture | `unity-architecture-scriptable` | 5 bài | 📅 Sắp ra mắt |

---

## 💻 Cách tải và chuyển sang Topic bạn quan tâm

Khi clone repository về máy, bạn chỉ cần chuyển sang branch của topic mình muốn xem:

```bash
# 1. Clone repository
git clone https://github.com/toan-lb/toanlb.dev-lab.git
cd toanlb.dev-lab

# 2. Chuyển sang topic Dependency Injection (Bài 01 - Singleton Problem & Pure C# DI)
git checkout unity-architecture-di
```

Mở project bằng **Unity 6 (hoặc Unity 2022.3 LTS trở lên)**, mở Scene tương ứng trong thư mục `Assets/01_Singleton_Problem/` và nhấn **Play** để xem demo trực quan trên GameView.

---

## ✍️ Tác giả

* **Lương Bảo Toàn** — Senior Game Architect & Technical Writer
* **Website / Tech Blog:** [https://toanlb.dev](https://toanlb.dev)
* **Chủ đề chính:** Unity Architecture, Mobile Optimization, Memory/GC, Automotive HMI.
