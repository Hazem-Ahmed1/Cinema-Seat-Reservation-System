module test
//open System
//open CinemaSeatTypes
//open System.Windows.Forms
//open System.Drawing

//let createSeatGrid (seats: Seat list) rows cols =
//    let form = new Form(Text = "Cinema Seats", Width = 800, Height = 600)
//    let grid = new TableLayoutPanel(
//        RowCount = rows,
//        ColumnCount = cols,
//        Dock = DockStyle.Fill,
//        CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
//    )

//    // Adjust row and column styles
//    for i in 1..rows do grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0f / float rows))
//    for i in 1..cols do grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100.0f / float cols))

//    // Add seats to the grid
//    for seat in seats do
//        let button = new Button(Text = sprintf "%s%d%d" seat.Class seat.Row seat.Column, Dock = DockStyle.Fill)
//        button.BackColor <- if seat.Available then Color.LightGreen else Color.Red
//        button.Click.Add(fun _ -> MessageBox.Show(sprintf "Hall: %s\nMovie: %s\nTime: %O" seat.HallName seat.Movie.Name seat.ShowTime) |> ignore)
//        grid.Controls.Add(button, seat.Column - 1, seat.Row - 1)

//    form.Controls.Add(grid)
//    Application.Run(form)

//// Example usage
//createSeatGrid seats 3 12
