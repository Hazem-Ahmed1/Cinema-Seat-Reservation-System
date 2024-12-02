module BookingForm

open System.Drawing
open System.Windows.Forms
open CinemaSeatTypes
open HallSeats


// Movie details form
type MovieDetailsForm(movie: Seat, allSeats: CiemaSeats) as this =
    inherit Form()

    do
        this.Text <- movie.Movie.Name
        this.Size <- Size(1080, 720)
        this.MaximizeBox <- false
        this.MinimizeBox <- false
        this.MaximumSize <- this.Size
        this.MinimumSize <- this.Size

        // Labels for movie details
        let titleLabel = new Label(Text = $"Title: {movie.Movie.Name}", Location = Point(10, 10), Size = Size(250, 20))
        let genreLabel = new Label(Text = $"Genre: {movie.Movie.Genre}", Location = Point(10, 40), Size = Size(250, 20))
        let ratingLabel = new Label(Text = $"Rating: {movie.Movie.Rating:F1}", Location = Point(10, 70), Size = Size(250, 20))
        let showTimeLabel = new Label(Text = $"Showtime: {movie.ShowTime}", Location = Point(10, 100), Size = Size(250, 20))

        // Add the labels to the form
        this.Controls.Add(titleLabel)
        this.Controls.Add(genreLabel)
        this.Controls.Add(ratingLabel)
        this.Controls.Add(showTimeLabel)

        // Filter seats for the selected movie
        let movieSeats = allSeats |> List.filter (fun s -> s.HallName = movie.HallName)

        // Create the grid
        let rowCount = 3
        let columnCount = 12
        let grid = createGrid rowCount columnCount

        // Add row and column styles
        addRowStyles grid rowCount
        addColumnStyles grid columnCount

        addSeatsToGrid grid movieSeats
        this.Controls.Add(grid)