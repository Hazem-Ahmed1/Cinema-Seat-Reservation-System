open System
open System.Windows.Forms
open System.Drawing
open FileReader
open MovieCard
open CinemaSeatTypes



let rec addMovieCards (seats: Seat List) (form: Form) (x: int) (y: int) (moviesInRow: int) =
    match seats with
    | [] -> ()  // Base case: no more seats to process
    | seat :: rest ->
        let card = createMovieCard seat x y
        form.Controls.Add(card)

        // Calculate new x and y
        let newX = if moviesInRow = 2 then 10 else x + 220  // Start new row after 3 movies
        let newY = if moviesInRow = 2 then y + 345 else y  // Move to next row after 3 movies

        // Recurse with the remaining seats and updated coordinates
        let newMoviesInRow = if moviesInRow = 2 then 0 else moviesInRow + 1
        addMovieCards rest form newX newY newMoviesInRow


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
    addMovieCards (Seq.toList seats) form 10 10 0 // Convert list to sequence

    Application.Run(form)
    0
