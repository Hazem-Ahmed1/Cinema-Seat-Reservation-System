module Ticket

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

// Function to create the Ticket GUI
type createTicketForm() as this = 
    inherit Form()

    do
        // Create a new form
        this.Text <- "Cinema Ticket"
        this.Size <- Size(800, 400)
        this.BackColor   <- Color.White
        this.MaximizeBox <- false
        this.MinimizeBox <- false
        this.MaximumSize <- this.Size
        this.MinimumSize <- this.Size
        this.AutoScroll  <- true

        // Add ticket background panel
        let ticketPanel = new Panel(Width = 783, Height = 250, BackColor = Color.White)
        ticketPanel.Location <- Point(0, 0)
        this.Controls.Add(ticketPanel)

        // Add "Customer" label
        let lblCinemaTicket = new Label(Text = "Customer Name", Font = new Font("Arial", 20.0f, FontStyle.Bold), ForeColor = Color.Blue, AutoSize = true)
        lblCinemaTicket.Location <- Point(275, 30)
        ticketPanel.Controls.Add(lblCinemaTicket)

        // Add "CINEMA TICKET" label
        let lblCinemaTicket = new Label(Text = "CINEMA TICKET", Font = new Font("Arial", 16.0f, FontStyle.Bold), ForeColor = Color.Blue, AutoSize = true)
        lblCinemaTicket.Location <- Point(300, 215)
        ticketPanel.Controls.Add(lblCinemaTicket)

        // Add Theater, Seat, Date, and Price details
        let LabelHall = new Label(Text = "Hall: 03 / SEAT: S16", Font = new Font("Arial", 15.0f), AutoSize = true)
        LabelHall.Location <- Point(35, 100)
        ticketPanel.Controls.Add(LabelHall)

        let lblDate = new Label(Text = "DATE: 10/07/2022", Font = new Font("Arial", 15.0f), AutoSize = true)
        lblDate.Location <- Point(35, 150)
        ticketPanel.Controls.Add(lblDate)

        let lblPrice = new Label(Text = "PRICE: 10 USD", Font = new Font("Arial", 15.0f), AutoSize = true)
        lblPrice.Location <- Point(35, 200)
        ticketPanel.Controls.Add(lblPrice)

        // Add Movie Name
        let lblMovieName = new Label(Text = "MOVIE Name", Font = new Font("Arial", 16.0f, FontStyle.Bold), ForeColor = Color.Gray, AutoSize = true)
        lblMovieName.Location <- Point(600, 100)
        ticketPanel.Controls.Add(lblMovieName)

        let lblTicketNo = new Label(Text = "NO: 0123456987", Font = new Font("Arial", 15.0f), AutoSize = true)
        lblTicketNo.Location <- Point(600, 150)
        ticketPanel.Controls.Add(lblTicketNo)

        // Add a placeholder for the QR code
        let barcodePanel = new Panel(Width = 150, Height = 150, BackColor = Color.White)
        barcodePanel.Location <- Point(310, 70)
        ticketPanel.Controls.Add(barcodePanel)

        // Generate and display the QR code
        let ticketData = "Theater: 03, Seat: S16, Date: 10/07/2022, Time: 11:45 PM, Movie: MOVIE 3D, No: 0123456987"
        let qrCodeImage = generateQrCode ticketData

        // Display the QR code in the form
        let qrPictureBox = new PictureBox(Image = qrCodeImage, SizeMode = PictureBoxSizeMode.StretchImage, Width = 150, Height = 150)
        barcodePanel.Controls.Add(qrPictureBox)

        //// Add Date and Time on the right side
        //let lblRightDate = new Label(Text = "DATE: 10/07/2022", Font = new Font("Arial", 10.0f), AutoSize = true)
        //lblRightDate.Location <- Point(600, 190)
        //ticketPanel.Controls.Add(lblRightDate)

        let lblRightTime = new Label(Text = "TIME: 11:45 PM", Font = new Font("Arial", 15.0f), AutoSize = true)
        lblRightTime.Location <- Point(600, 200)
        ticketPanel.Controls.Add(lblRightTime)

        // Add print button
        let btnPrint = new Button(Text = "Save Ticket",BackColor = Color.LightGreen, Font = new Font("Arial", 12.0f), Width = 150, Height = 40)
        btnPrint.Location <- Point(305, 300)
        this.Controls.Add(btnPrint)

        // Event handler for saving the ticket as an image
        btnPrint.Click.Add(fun _ ->
            let outputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "D:\Collage\Abdelwahed\4th\First Term\PL3\Project", "CinemaTicket.png")
            saveControlAsImage ticketPanel outputPath
            MessageBox.Show(sprintf "Ticket saved as image at: %s" outputPath, "Success") |> ignore
        )
        this.Show()



