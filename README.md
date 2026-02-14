# 2026-RoomBooking-Backend

## Description

Repositori ini merupakan komponen **Backend** dari sistem manajemen peminjaman ruangan. Aplikasi ini dibangun menggunakan framework **ASP.NET Core** untuk menyediakan layanan API RESTful yang mengelola data ruangan, transaksi peminjaman, serta pencatatan riwayat status (*audit trail*) secara terstruktur .

## Features

* **Room Management**: API CRUD untuk mengelola data master ruangan (Nama dan Lokasi).
* **Booking System**: Fitur reservasi ruangan dengan validasi input yang ketat .
* **Status History & Logging**: Pencatatan otomatis setiap perubahan status peminjaman untuk kebutuhan audit .
* **Soft Delete**: Mekanisme penghapusan data secara logis untuk menjaga integritas database.
* **Search & Pagination**: Dukungan pencarian data dan paginasi pada daftar peminjaman.



## Tech Stack

* **Framework**: ASP.NET Core.
* **Database**: SQL Server / Entity Framework Core.
* **Language**: C#.
* **Tools**: Swagger UI untuk dokumentasi API.

## Installation

1. Pastikan Anda telah menginstal **.NET SDK** terbaru.
2. Clone repositori ini:
```bash
git clone https://github.com/Rasyiid-er/2026-Pra_PBL-backend.git
```


3. Masuk ke direktori proyek:
```bash
cd PRAPBL

```


4. Restore dependencies:
```bash
dotnet restore

```


5. Update database menggunakan migrasi:
```bash
dotnet ef database update

```


6. Jalankan aplikasi:
```bash
dotnet run

```



## Environment Variables

Buat file `.env` berdasarkan `.env.example` (jangan meng-commit file `.env` asli ke repositori):

* `ConnectionStrings__DefaultConnection`: String koneksi ke database SQL Server Anda.

## Usage

Setelah aplikasi berjalan, Anda dapat mengakses dokumentasi API melalui browser di:
`http://localhost:<port>/swagger`

## Workflow & Git Standards

* **Branching Strategy**: Pengembangan dilakukan di branch `feature/*` lalu di-merge ke `develop` sebelum ke `main` .
* **Conventional Commit**: Pesan commit mengikuti format standar industri (contoh: `feat(room): implement add room`) .
* **Semantic Versioning**: Penomoran versi menggunakan format `MAJOR.MINOR.PATCH` (dimulai dari `v1.0.0`).



## License

Distributed under the **MIT License**.
