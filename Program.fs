open System
open System.Windows.Forms
open System.Drawing
open FileReader
open MovieCard
open CinemaSeatTypes


[<EntryPoint>]
let main argv =
    Application.EnableVisualStyles()
    Application.SetCompatibleTextRenderingDefault(false)
    let form = new Form()
    form.Text <- "Movies"
    form.Size <- Size(690, 600)
    form.AutoScroll <- true
    form.MaximizeBox <- false
    form.MinimizeBox <- false
    form.MaximumSize <- form.Size
    form.MinimumSize <- form.Size
    let seats = loadSeats
    // Display movies
    let filteredSeats = filterSeatsByHall seats
    addMovieCards (filteredSeats) form 10 10 0
    Application.Run(form)
    0
