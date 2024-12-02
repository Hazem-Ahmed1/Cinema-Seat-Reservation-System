open System
open System.Drawing
open System.Windows.Forms
open QRCoder
open System.IO

// Function to generate a QR code as a Bitmap
let generateQrCode (data: string) =
    use qrGenerator = new QRCodeGenerator()
    let qrData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q)
    use qrCode = new QRCode(qrData)
    qrCode.GetGraphic(20)

// Function to save a control as an image (e.g., Panel or Form)
let saveControlAsImage (control: Control) (filePath: string) =
    let bitmap = new Bitmap(control.Width, control.Height)
    control.DrawToBitmap(bitmap, Rectangle(0, 0, control.Width, control.Height))
    bitmap.Save(filePath, Imaging.ImageFormat.Png)
    printfn "Ticket saved as image at: %s" filePath

// Function to create the Ticket Printing GUI
let createTicketPrintingForm () =
    // Create a new form
    let form = new Form(Text = "Cinema Ticket", Width = 800, Height = 400)
    form.BackColor <- Color.Gray

    // Add ticket background panel
    let ticketPanel = new Panel(Width = 750, Height = 300, BackColor = Color.White)
    ticketPanel.Location <- Point(25, 25)
    form.Controls.Add(ticketPanel)

    // Add "CINEMA TICKET" label
    let lblCinemaTicket = new Label(Text = "CINEMA TICKET", Font = new Font("Arial", 16.0f, FontStyle.Bold), ForeColor = Color.HotPink, AutoSize = true)
    lblCinemaTicket.Location <- Point(300, 10)
    ticketPanel.Controls.Add(lblCinemaTicket)

    // Add Theater, Seat, Date, and Price details
    let lblTheater = new Label(Text = "THEATER: 03 / SEAT: S16", Font = new Font("Arial", 12.0f), AutoSize = true)
    lblTheater.Location <- Point(50, 60)
    ticketPanel.Controls.Add(lblTheater)

    let lblDate = new Label(Text = "DATE: 10/07/2022", Font = new Font("Arial", 12.0f), AutoSize = true)
    lblDate.Location <- Point(50, 100)
    ticketPanel.Controls.Add(lblDate)

    let lblPrice = new Label(Text = "PRICE: 10 USD", Font = new Font("Arial", 12.0f), AutoSize = true)
    lblPrice.Location <- Point(50, 140)
    ticketPanel.Controls.Add(lblPrice)

    // Add Movie Name
    let lblMovieName = new Label(Text = "MOVIE 3D", Font = new Font("Arial", 16.0f, FontStyle.Bold), ForeColor = Color.HotPink, AutoSize = true)
    lblMovieName.Location <- Point(50, 200)
    ticketPanel.Controls.Add(lblMovieName)

    let lblMovieNo = new Label(Text = "NO: 0123456987", Font = new Font("Arial", 12.0f), AutoSize = true)
    lblMovieNo.Location <- Point(50, 240)
    ticketPanel.Controls.Add(lblMovieNo)

    // Add a placeholder for the QR code
    let barcodePanel = new Panel(Width = 120, Height = 120, BackColor = Color.White)
    barcodePanel.Location <- Point(600, 60)
    ticketPanel.Controls.Add(barcodePanel)

    // Generate and display the QR code
    let ticketData = "Theater: 03, Seat: S16, Date: 10/07/2022, Time: 11:45 PM, Movie: MOVIE 3D, No: 0123456987"
    let qrCodeImage = generateQrCode ticketData

    // Display the QR code in the form
    let qrPictureBox = new PictureBox(Image = qrCodeImage, SizeMode = PictureBoxSizeMode.StretchImage, Width = 120, Height = 120)
    barcodePanel.Controls.Add(qrPictureBox)

    // Add Date and Time on the right side
    let lblRightDate = new Label(Text = "DATE: 10/07/2022", Font = new Font("Arial", 10.0f), AutoSize = true)
    lblRightDate.Location <- Point(600, 190)
    ticketPanel.Controls.Add(lblRightDate)

    let lblRightTime = new Label(Text = "TIME: 11:45 PM", Font = new Font("Arial", 10.0f), AutoSize = true)
    lblRightTime.Location <- Point(600, 220)
    ticketPanel.Controls.Add(lblRightTime)

    // Add print button
    let btnPrint = new Button(Text = "Save Ticket", Font = new Font("Arial", 12.0f), Width = 150, Height = 40)
    btnPrint.Location <- Point(300, 350)
    form.Controls.Add(btnPrint)

    // Event handler for saving the ticket as an image
    btnPrint.Click.Add(fun _ ->
        let outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "CinemaTicket.png")
        saveControlAsImage ticketPanel outputPath
        MessageBox.Show(sprintf "Ticket saved as image at: %s" outputPath, "Success") |> ignore
    )

    // Show the form
    Application.Run(form)

[<EntryPoint>]
let main argv =
    Application.EnableVisualStyles()
    createTicketPrintingForm() // Runs the form
    0 // Return 0 to indicate successful execution