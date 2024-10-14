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











ChatGPT 
