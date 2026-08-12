# AGIT-Test

**Muhammad Alam Basallamah** | Developer

> **Catatan Assessor:** Karena tidak ada Candidate Code spesifik yang tercantum pada email undangan, saya menggunakan token **VEH-ALAM** pada pengerjaan tes ini.

---

## CASE 1

### Pseudocode

```text
MULAI
  IF rencana_awal < 0: //Ini maksudnya kalau ada angka negatif untuk total produksinya
    RETURN error/exception;

  total_prod = TOTAL(rencana_awal) //ambil total dari rencana awal
  idx_aktif = [] //index_aktif ini maksudnya jumlah hari yang aktif (alias tidak ada 0 atau > 0)

  FOREACH idx DARI 0 SAMPAI DENGAN length(rencana_awal): //ini setiap index sampai rencana_awal saya mau tarik
    IF rencana_awal[idx] > 0: //Jika rencana awal ke index tersebut nilainya lebih dari 0
      idx_aktif[idx] = idx //tambakan data index ke index aktif dengan tujuan buat tampung hari mana saja yang nilainya > 0 alias tidak libur

  IF length(idx_aktif) ISNULL: //jika tidak ada produksi setiap harinya alias bisa jadi libur atau data keliru (array kosong)
    RETURN rencana_awal //kembalikan list rencana awal sebelumnya (0 semua)

  avg_prod = total_prod DBAGI length(idx_aktif) //untuk mendapatkan rata rata produksi dalam satu minggu
  sisa = total_prod MODULO length(idx_aktif) // mendapatkan sisa bagi antara total prod dengan panjang index aktif

  SORTING idx_aktif ORDERBY(rencana_awal[idx] DESCENDING, idx ASCENDING) // sorting dengan plan awal itu isi rencana_awal di sorting descending agar totalprod yang terbesar muncul terlebih dahulu, dan plan ke 2 index disorting ascending

  hasil = CREATE list integer dengan length sesuai rencana_awal //buat tampungan akhir untuk hasil yang akan di return

  FOREACH idx WITH idx_aktif: //setiap idx_aktif itu diisi dengan rata rata prod dalam seminggu hasil perhitungan sebelumnya
    hasil[idx] = avg_prod

  FOR i = 0 SAMPAI sisa: //menambahkan sisa prod (+1) pada idx aktif hasil sorting sebelumnya
    temp_idx = idx_aktif[i]
    hasil[temp_idx] = hasil[idx_prioritas] + 1

  RETURN hasil //mengembalikan hasil
SELESAI