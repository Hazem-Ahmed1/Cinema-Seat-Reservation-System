module HallSeats

open System.Drawing
open System.Windows.Forms
open CinemaSeatTypes
open FileReader
open UpdateFileBooking
open Ticket

// Function to create the grid
let createGrid (rowCount: int) (columnCount: int) =
    let grid =
        new TableLayoutPanel(
            RowCount = rowCount,
            ColumnCount = columnCount,
            Dock = DockStyle.Bottom,
            Size = Size(1060, 500),
            CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble
        )

    grid

let rec addRowStyles (grid: TableLayoutPanel) (count: int) =
    if count > 0 then
        grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0f / 6.0f)) |> ignore
        addRowStyles grid (count - 1)



// Function to add column styles recursively
let rec addColumnStyles (grid: TableLayoutPanel) (count: int) =
    if count > 0 then
        grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100.0f / 6.0f))
        |> ignore

        addColumnStyles grid (count - 1)


// Function to create a button for a seat
let createSeatButton (seat: Seat) =
    let button =
        new Button(
            Text = $"{seat.Row},{seat.Column}",
            Dock = DockStyle.Fill,
            Font = new Font("Arial", 20.0f, FontStyle.Bold)
        )

    button.BackColor <-
        if seat.Row <= 2 && seat.Available then Color.Gold
        else if seat.Available then Color.LightBlue
        else Color.Red

    button.Enabled <- seat.Available // Disable
    //button.Enabled <- updateStatus

    button.Click.Add(fun _ ->
        let createTicket = new createTicketForm (seat)

        let UpdateStatus =
            updateLineByCriteria filePath seat.HallName seat.Row seat.Column changeAvailability
        //let status = if seat.Available then "Available" else "Unavailable"
        button.Enabled <- not UpdateStatus
        button.BackColor <- Color.Red)

    button


// Function to add seats to the grid recursively
let rec addSeatsToGrid (grid: TableLayoutPanel) (seats: Seat list) =
    match seats with
    | [] -> () // Base case: no more seats to process
    | seat :: rest ->
        let button = createSeatButton seat
        grid.Controls.Add(button, seat.Column - 1, seat.Row - 1)
        // Recurse with the remaining seats
        addSeatsToGrid grid rest
