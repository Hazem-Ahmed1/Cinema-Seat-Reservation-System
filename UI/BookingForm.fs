module BookingForm

open System.Drawing
open System.Windows.Forms
open CinemaSeatTypes
open HallSeats


// Movie details form
type MovieDetailsForm(movie: Seat, allSeats: CinemaSeats) as this =
    inherit Form()

    do
        this.Text <- movie.Movie.Name
        this.Size <- Size(1080, 720)
        this.MaximizeBox <- false
        this.MinimizeBox <- false
        this.MaximumSize <- this.Size
        this.MinimumSize <- this.Size

        // Labels for movie details
        let titleLabel =
            new Label(Text = $"Title: {movie.Movie.Name}", Location = Point(10, 10), Size = Size(250, 20))

        let genreLabel =
            new Label(Text = $"Genre: {movie.Movie.Genre}", Location = Point(10, 40), Size = Size(250, 20))

        let ratingLabel =
            new Label(Text = $"Rating: {movie.Movie.Rating:F1}", Location = Point(10, 70), Size = Size(250, 20))

        let showTimeLabel =
            new Label(Text = $"Showtime: {movie.ShowTime}", Location = Point(10, 100), Size = Size(250, 20))

        let availableIndicator =
            new Label(
                Text = "Available",
                Location = Point(this.ClientSize.Width - 200, 20),
                Font = new Font("Arial", 15.0f, FontStyle.Bold),
                Size = Size(200, 30),
                BackColor = Color.LightBlue,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            )

        let notAvailableIndicator =
            new Label(
                Text = "Not Available",
                Location = Point(this.ClientSize.Width - 200, 60),
                Font = new Font("Arial", 15.0f, FontStyle.Bold),
                Size = Size(200, 30),
                BackColor = Color.Red,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            )

        // Add seat class indicators
        let vipIndicator =
            new Label(
                Text = "VIP (Rows 1-2)",
                Location = Point(this.ClientSize.Width - 200, 100),
                Font = new Font("Arial", 15.0f, FontStyle.Bold),
                Size = Size(200, 30),
                BackColor = Color.Gold,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter
            )

        // Add the labels to the form
        this.Controls.Add(titleLabel)
        this.Controls.Add(genreLabel)
        this.Controls.Add(ratingLabel)
        this.Controls.Add(showTimeLabel)
        this.Controls.Add(availableIndicator)
        this.Controls.Add(notAvailableIndicator)
        this.Controls.Add(vipIndicator)

        // Filter seats for the selected movie
        let movieSeats = allSeats |> List.filter (fun s -> s.HallName = movie.HallName)

        // Create the grid
        let rowCount = 6
        let columnCount = 6
        let grid = createGrid rowCount columnCount

        // Add row and column styles
        addRowStyles grid rowCount
        addColumnStyles grid columnCount

        addSeatsToGrid grid movieSeats
        this.Controls.Add(grid)
