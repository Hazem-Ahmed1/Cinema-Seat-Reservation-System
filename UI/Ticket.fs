module Ticket

open System
open System.Drawing
open System.Windows.Forms
open QRCoder
open System.IO
open CinemaSeatTypes
open Ticketdetails

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
type createTicketForm(seat: Seat) as this =
    inherit Form()

    do
        // Create a new form
        this.Text <- "Cinema Ticket"
        this.Size <- Size(800, 400)
        this.BackColor <- Color.White
        this.MaximizeBox <- false
        this.MinimizeBox <- false
        this.MaximumSize <- this.Size
        this.MinimumSize <- this.Size
        this.AutoScroll <- true

        // Add ticket background panel
        let ticketPanel = new Panel(Width = 783, Height = 280, BackColor = Color.White)
        ticketPanel.Location <- Point(0, 0)
        this.Controls.Add(ticketPanel)

        // Add "Customer" label
        let lblCinemaTicket =
            new Label(
                Text = "CINEMA TICKET",
                Font = new Font("Arial", 20.0f, FontStyle.Bold),
                ForeColor = Color.Blue,
                AutoSize = true
            )

        lblCinemaTicket.Location <- Point(275, 30)
        ticketPanel.Controls.Add(lblCinemaTicket)

        // Add "CINEMA TICKET" label
        let lblCinemaTicket =
            new Label(
                Text = "Customer Name",
                Font = new Font("Arial", 16.0f, FontStyle.Bold),
                ForeColor = Color.Blue,
                AutoSize = true
            )

        lblCinemaTicket.Location <- Point(515, 150)
        ticketPanel.Controls.Add(lblCinemaTicket)

        // Add Hall, Seat, Date, and Price details
        let LabelHall =
            new Label(
                Text = $"{seat.HallName} / SEAT: {seat.Class.[0]}{seat.Row},{seat.Column}",
                Font = new Font("Arial", 15.0f),
                AutoSize = true
            )

        LabelHall.Location <- Point(35, 100)
        ticketPanel.Controls.Add(LabelHall)

        let lblDate =
            new Label(Text = $"Date: {seat.ShowTime.Split(' ')[0]}", Font = new Font("Arial", 15.0f), AutoSize = true)

        lblDate.Location <- Point(35, 150)
        ticketPanel.Controls.Add(lblDate)

        let lblPrice =
            new Label(Text = $"PRICE: {seat.Price} USD", Font = new Font("Arial", 15.0f), AutoSize = true)

        lblPrice.Location <- Point(35, 200)
        ticketPanel.Controls.Add(lblPrice)

        // Add Movie Name
        let lblMovieName =
            new Label(
                Text = $"MOVIE: {seat.Movie.Name}",
                Font = new Font("Arial", 16.0f, FontStyle.Bold),
                ForeColor = Color.Gray,
                AutoSize = true
            )

        lblMovieName.Location <- Point(515, 100)
        ticketPanel.Controls.Add(lblMovieName)

        let ticket = ticket seat

        let lblTicketNo =
            new Label(Text = $"Ticket ID: {ticket.TicketID}", Font = new Font("Arial", 15.0f), AutoSize = true)

        lblTicketNo.Location <- Point(170, 240)
        ticketPanel.Controls.Add(lblTicketNo)

        // Add a placeholder for the QR code
        let barcodePanel = new Panel(Width = 150, Height = 150, BackColor = Color.White)
        barcodePanel.Location <- Point(310, 70)
        ticketPanel.Controls.Add(barcodePanel)

        let lblRightTime =
            new Label(
                Text = $"TIME: {seat.ShowTime.Split(' ')[1]} {seat.ShowTime.Split(' ')[2]}",
                Font = new Font("Arial", 15.0f),
                AutoSize = true
            )

        lblRightTime.Location <- Point(515, 200)
        ticketPanel.Controls.Add(lblRightTime)

        // Generate and display the QR code
        let ticketData =
            $"Hall: {seat.HallName}, Seat: {seat.Class.[0]}{seat.Row},{seat.Column} , Date:{seat.ShowTime.Split(' ')[0]}, Time: {seat.ShowTime.Split(' ')[1]} {seat.ShowTime.Split(' ')[2]}, Movie: {seat.Movie.Name}, Ticket No: {ticket.TicketID}"

        let qrCodeImage = generateQrCode ticketData

        // Display the QR code in the form
        let qrPictureBox =
            new PictureBox(Image = qrCodeImage, SizeMode = PictureBoxSizeMode.StretchImage, Width = 150, Height = 150)

        barcodePanel.Controls.Add(qrPictureBox)

        // Add print button
        let btnPrint =
            new Button(
                Text = "Save Ticket",
                BackColor = Color.LightGreen,
                Font = new Font("Arial", 12.0f),
                Width = 150,
                Height = 40
            )

        btnPrint.Location <- Point(305, 300)
        this.Controls.Add(btnPrint)

        // Event handler for saving the ticket as an image
        btnPrint.Click.Add(fun _ ->
            let outputPath =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "Tickets",
                    $"CinemaTicket-{ticket.TicketID.Split('-')[0]}.png"
                )

            let outputFile = @"Database\TicketDetails.txt"

            saveControlAsImage ticketPanel outputPath
            //let TicketFile = customSerializeTicket(ticket)
            saveTicketToTxt ticket outputFile

            MessageBox.Show(sprintf "Ticket saved as image at: %s" outputPath, "Success")
            |> ignore)

        this.Show()
