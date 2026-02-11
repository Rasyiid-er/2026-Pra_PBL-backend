# Changelog

Semua perubahan penting pada proyek ini akan didokumentasikan dalam file ini.
Format ini didasarkan pada [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
dan proyek ini mematuhi [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2026-02-11

### Added

- **Room Management**: Implementasi API CRUD lengkap untuk entitas Ruangan.
- **Booking System**: Implementasi fitur pembuatan peminjaman ruangan dengan validasi `ModelState` .
- **Status History**: Pencatatan otomatis setiap perubahan status peminjaman (Pending, Approved, Rejected) untuk kebutuhan audit trail .
- **Booking Logs**: Endpoint khusus untuk mengambil riwayat peminjaman lengkap dengan informasi relasi ruangan.
- **Soft Delete**: Implementasi penghapusan logis pada entitas Booking agar data tetap terjaga di database.
- **Pagination & Search**: Fitur pencarian nama peminjam dan paginasi data pada list peminjaman.

### Changed

- **Database Schema**: Migrasi awal untuk tabel `Rooms`, `Bookings`, dan `BookingStatusHistories`.
- **Standardization**: Mengubah semua format waktu transaksi menggunakan UTC secara konsisten di tingkat database.

### Fixed

- **Validation**: Menambahkan validasi agar ruangan tidak dapat dihapus jika masih memiliki referensi peminjaman aktif.
- **Data Integrity**: Memastikan setiap pembuatan booking baru selalu menghasilkan satu entri histori status awal.
