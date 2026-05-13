# ⚡ ProductivityApp

**Web Tabanlı Kişisel Verimlilik Uygulaması**

> T.C. Bursa Uludağ Üniversitesi — Mühendislik Fakültesi  
> Bilgisayar Mühendisliği Bölümü  
> 2025–2026 Bahar Dönemi · Görsel Programlama Dersi  
> Ara Proje — Öğr. Gör. Koray Aki

---

## 👥 Grup Üyeleri

| Ad Soyad | Öğrenci No |
|---|---|
| İsmail Karatay | 032390029 |
| Berat Çam | 032390052 |
| Sudem Kırmız | 032390102 |
| Ferhat Çelik | 032390039 |

---

## 📌 Proje Hakkında

ProductivityApp; görev yönetimi, alışkanlık takibi ve Pomodoro tekniğiyle odaklanma seansı olmak üzere üç temel modülü tek çatı altında birleştiren kişisel verimlilik uygulamasıdır.

Proje iki sürümden oluşmaktadır:

- **C# Windows Forms** — Masaüstü uygulaması, SQLite tabanlı kalıcı veri saklama
- **Web Sürümü** — Saf HTML / CSS / Vanilla JavaScript, LocalStorage tabanlı kalıcı veri saklama

---

## 📸 Ekran Görüntüleri

| Giriş ve Dashboard | Görevler ve Alışkanlıklar | Odak |
|:---:|:---:|:---:|
| ![Login Ekranı](login.png) <br> *Kullanıcı Girişi* | ![Görevler Ekranı](görevler.png) <br> *Görev Yönetimi* | ![Odak Ekranı](odak.png) <br> *Pomodoro Seansı* |
| ![Dashboard](dashboard.png) <br> *Genel Bakış (Dashboard)* | ![Alışkanlıklar](aliskanliklar.png) <br> *Alışkanlık Takibi* | |

---

## ✨ Özellikler

### ✅ Görev Yönetimi
- Görev ekleme (başlık, öncelik, durum, son tarih)
- Canlı arama (büyük/küçük harf duyarsız)
- Öncelik ve durum filtreleme
- Görev güncelleme (düzenleme modu)
- Görev silme + `Stack` tabanlı Geri Al

### 🔥 Alışkanlık Takibi
- Alışkanlık ekleme ve silme
- Günlük kontrol ile Streak (seri) sayacı
- Arka arkaya gün hesaplama mantığı

### 🎯 Odak / Pomodoro
- 25 / 45 / 60 dakika seçilebilir süre
- Başlat / Duraklat / Sıfırla kontrolü
- SVG animasyonlu halka zamanlayıcı
- Tamamlanan seans geçmişi

### 🏠 Dashboard
- Aktif görev, alışkanlık, seans ve maksimum seri istatistikleri
- Son eklenen görev ve alışkanlık önizlemeleri

---

## 🗂️ Proje Yapısı

```text
ProductivityApp/
├── Database/
│   └── DatabaseHelper.cs       # SQLite CRUD işlemleri
├── Forms/
│   ├── LoginForm.cs/.Designer.cs
│   ├── MainForm.cs/.Designer.cs
│   ├── TaskForm.cs/.Designer.cs
│   ├── HabitForm.cs/.Designer.cs
│   └── FocusForm.cs/.Designer.cs
├── Helpers/
│   └── AppTheme.cs             # Renk, font ve stil sabitleri
├── Models/
│   ├── User.cs
│   ├── TaskItem.cs
│   ├── Habit.cs
│   └── FocusSession.cs
├── web/
│   ├── index.html              # Web sürümü — tek sayfa uygulama
│   ├── style.css
│   └── app.js
├── Program.cs
└── ProductivityApp.csproj
