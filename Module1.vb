Imports System.Data.SqlClient
Module Module1
    Public str, str2 As String
    Public cmd, cmd2 As SqlCommand
    Public sqlcon, sqlcon2, sqlcon3 As SqlConnection
    Public sqlrd, sqlrd2 As SqlDataReader
    Public da As SqlDataAdapter
    Public ds As DataSet
    Public department As String
    'Public UserID As String
    Public level As String
    Public rc As String
    Dim subdept As String

    Public Sub koneksi()
        'str = "Data Source=192.168.3.11\MSSQLPV;Initial Catalog=IPI_CUSTOMDB;Persist Security Info=True;UID=antoni;PWD=ipiserver;Max Pool Size=200;Connect Timeout=200"
        str = "Data Source=" & My.Settings.SERVER & ";Initial Catalog=" & My.Settings.DATABASE & ";Persist Security Info=True;UID=sa;PWD=ipiserver!234;Max Pool Size=200;Connect Timeout=200"
        ' str = "Data Source=localhost;Initial Catalog=IPI_CUSTOMDB;Persist Security Info=True;UID=antoni;PWD=ipiserver"
        sqlcon = New SqlConnection(str)
        If sqlcon.State = ConnectionState.Closed Then
            sqlcon.Open()
        End If
        sqlcon3 = New SqlConnection(str)
        If sqlcon3.State = ConnectionState.Closed Then
            sqlcon3.Open()
        End If

        'str2 = "Data Source=192.168.3.11\MSSQLPV;Initial Catalog=IPI_LIVE;Persist Security Info=True;UID=antoni;PWD=ipiserver;Max Pool Size=200;Connect Timeout=200"
        str2 = "Data Source=" & My.Settings.SERVER & ";Initial Catalog=" & My.Settings.DATABASE_PV & ";Persist Security Info=True;UID=sa;PWD=ipiserver!234;Max Pool Size=200;Connect Timeout=200"
        'str2 = str
        sqlcon2 = New SqlConnection(str2)
        If sqlcon2.State = ConnectionState.Closed Then
            sqlcon2.Open()
        End If
    End Sub


    Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal process As IntPtr, ByVal minimumWorkingSetSize As Integer, ByVal maximumWorkingSetSize As Integer) As Integer
    Public Sub FlushMemory()
        Try
            GC.Collect()
            GC.WaitForPendingFinalizers()
            If (Environment.OSVersion.Platform = PlatformID.Win32NT) Then
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1)
                Dim myProcesses As Process() = Process.GetProcessesByName("Notif")
                Dim myProcess As Process
                'Dim ProcessInfo As Process
                For Each myProcess In myProcesses
                    SetProcessWorkingSetSize(myProcess.Handle, -1, -1)
                Next myProcess
            End If
        Catch ex As Exception
        End Try
    End Sub

End Module

 return (from im in _dbContext.InventoryMovements.AsTracking().Include(x => x.Product).Include(y => y.Supplier).Include(za => za.Customer)
                    join t in _dbContext.TransactionTypeCodes on im.TransactionTypeCode equals t.Id
                    where im.Disabled == false
                    select (im))
[HttpPost]
    public async Task<IActionResult> PostProductRequest([FromBody] ProductRequest productRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Logika membuat Product baru
        var newProduct = new Product
        {
            Name = "New Product from Request", // Kamu bisa menggunakan data dari productRequest untuk ini
            Price = 100 // Sesuaikan sesuai dengan data yang relevan
        };

        // Tambahkan Product ke database
        _context.Products.Add(newProduct);
        await _context.SaveChangesAsync(); // Simpan untuk mendapatkan ProductId

        // Setelah Product disimpan, tambahkan ProductId ke ProductRequest
        productRequest.ProductId = newProduct.ProductId;

        // Simpan ProductRequest
        _context.ProductRequests.Add(productRequest);
        await _context.SaveChangesAsync(); // Simpan perubahan ke database

        return CreatedAtAction("GetProductRequest", new { id = productRequest.ProductRequestId }, productRequest);
    }

    // Metode GetProductRequest untuk melihat ProductRequest yang telah dipost
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductRequest(int id)
    {
        var productRequest = await _context.ProductRequests.FindAsync(id);

        if (productRequest == null)
        {
            return NotFound();
        }

        return Ok(productRequest);
    }
}
Penjelasan:
Route dan Controller: Controller ini ditujukan untuk ProductRequest. Dengan demikian, segala operasi terkait ProductRequest, termasuk pembuatan Product, diletakkan di sini.

POST Metode: Di dalam metode POST ini:

Sebuah Product baru dibuat terlebih dahulu.
Produk tersebut disimpan dan menghasilkan ProductId.
ProductId tersebut kemudian dimasukkan ke dalam ProductRequest.
ProductRequest kemudian disimpan ke database.
API Route: Rute API ini adalah api/ProductRequest, yang mengikuti konvensi RESTful, karena tindakan utama yang terjadi adalah membuat permintaan produk.

Kesimpulan:
Dengan menempatkan logika di dalam ProductRequestController, kamu mempertahankan struktur API yang konsisten dan mudah diikuti. Jika di masa mendatang ada perubahan dalam logika pembuatan produk dari permintaan, kamu akan lebih mudah memodifikasi kode di satu tempat yang relevan.




Kelebihan
Akurasi Hasil Parafrase yang Baik
Model IndoT5 yang dituning dengan GPT-2 menunjukkan performa yang unggul dalam menghasilkan parafrase yang tetap mempertahankan makna dari teks asli. Hasil parafrase yang dihasilkan memiliki struktur kalimat yang berbeda namun tetap mudah dipahami oleh pembaca.

Penggunaan Metrik Evaluasi BLEU dan ROUGE
Evaluasi berbasis metrik BLEU dan ROUGE memberikan pendekatan yang objektif dalam mengukur kualitas hasil parafrase. Metrik BLEU sangat cocok untuk menilai kesamaan antara hasil parafrase dengan referensi, sementara ROUGE membantu mengevaluasi cakupan dan urutan kata yang relevan. Kombinasi kedua metrik ini memastikan hasil yang lebih komprehensif.

Kemampuan Generalisasi yang Baik
Dengan tuning model IndoT5 berbasis GPT-2, aplikasi memiliki kemampuan generalisasi yang baik untuk berbagai jenis teks, mulai dari teks formal hingga semi-formal. Hal ini membuat aplikasi dapat digunakan dalam berbagai konteks, seperti pengolahan teks artikel berita, jurnal ilmiah, hingga konten media sosial.

Fleksibilitas dalam Penanganan Teks Panjang
Aplikasi ini dapat menangani teks dengan panjang yang bervariasi, meskipun memerlukan waktu pemrosesan tambahan untuk teks yang sangat panjang. Fleksibilitas ini memungkinkan pengguna untuk memproses berbagai ukuran dokumen tanpa hambatan yang signifikan.

Kemudahan Integrasi dengan Aplikasi Lain
Berkat arsitektur berbasis API dan modularitas sistem, aplikasi ini dapat dengan mudah diintegrasikan dengan aplikasi berbasis teks lainnya, seperti chatbot, sistem manajemen konten, atau aplikasi pembelajaran berbasis teks.

Kemampuan Tuning Model yang Efektif
Proses tuning model menggunakan GPT-2 memberikan peningkatan yang signifikan dalam kualitas parafrase dibandingkan model yang tidak dituning. Dengan tuning yang lebih lanjut, kualitas aplikasi ini dapat terus ditingkatkan.

Antarmuka Pengguna yang Sederhana dan Intuitif
Antarmuka aplikasi dirancang dengan mempertimbangkan kenyamanan pengguna. Tampilan yang sederhana memudahkan pengguna dalam melakukan parafrase tanpa memerlukan panduan yang rumit.

Kekurangan
Waktu Pemrosesan yang Relatif Lama
Salah satu kekurangan utama aplikasi ini adalah waktu pemrosesan yang relatif lama, terutama ketika menangani teks dalam jumlah besar. Hal ini disebabkan oleh kompleksitas model Transformers yang digunakan. Dalam implementasi nyata, waktu tunggu yang panjang dapat mengurangi kenyamanan pengguna.

Kebutuhan Komputasi yang Tinggi
Model deep learning seperti IndoT5 yang dituning dengan GPT-2 memerlukan perangkat keras dengan spesifikasi tinggi, seperti GPU dengan memori besar, agar dapat berjalan secara optimal. Pada perangkat keras yang kurang memadai, performa aplikasi dapat menurun drastis.

Ketergantungan pada Dataset Pelatihan
Kualitas hasil parafrase sangat bergantung pada dataset yang digunakan untuk tuning model. Jika dataset yang digunakan kurang representatif, hasil parafrase dapat menjadi kurang akurat atau tidak sesuai dengan konteks yang diinginkan.

Kesulitan dalam Menangani Bahasa Non-Formal
Model masih mengalami kesulitan dalam menangani teks dengan gaya bahasa non-formal atau slang. Hal ini menjadi tantangan jika aplikasi digunakan untuk memproses teks dari media sosial atau komunikasi informal lainnya.

Evaluasi Manual Masih Diperlukan
Meskipun metrik BLEU dan ROUGE memberikan evaluasi otomatis yang cukup baik, evaluasi manual tetap diperlukan untuk memastikan bahwa hasil parafrase benar-benar sesuai dengan makna teks asli. Hal ini memerlukan waktu dan tenaga tambahan, terutama untuk evaluasi dalam skala besar.

Keterbatasan dalam Konteks Semantik yang Kompleks
Model dapat menghasilkan parafrase yang tidak sepenuhnya mempertahankan makna asli ketika teks memiliki konteks semantik yang kompleks. Ini menjadi tantangan khusus dalam teks ilmiah atau dokumen hukum yang membutuhkan presisi tinggi.

Kendala dalam Penanganan Multibahasa
Meskipun model IndoT5 dirancang untuk bahasa Indonesia, aplikasi ini belum optimal untuk menangani teks yang mengandung campuran bahasa (code-switching) atau bahasa daerah. Ini menjadi kendala jika pengguna ingin memproses teks yang multibahasa.

Pemeliharaan Model yang Kompleks
Untuk menjaga kualitas hasil parafrase, model memerlukan pemeliharaan dan pembaruan secara berkala, termasuk retraining dengan dataset terbaru. Hal ini dapat menjadi tantangan dalam jangka panjang.

https://learn.microsoft.com/en-us/training/courses/ai-900t00
