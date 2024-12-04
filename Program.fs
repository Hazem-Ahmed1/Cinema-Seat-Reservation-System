open System.Windows.Forms
open System.Drawing
open FileReader
open MovieCard
open UserSession
open CinemaReservation

[<EntryPoint>]
let main argv =
    Application.EnableVisualStyles()
    Application.SetCompatibleTextRenderingDefault(false)

    // Show login form first
    let loginForm = new LoginForm()

    if loginForm.ShowDialog() = DialogResult.OK then
        let loggedInUser = loginForm.Tag :?> string // Get the username from the Tag property
        clearAndWriteUser(SessionStorageFilePath, loggedInUser)
        let form = new Form()
        form.Text <- "Movies"
        form.Size <- Size(690, 600)
        form.AutoScroll <- true
        form.MaximizeBox <- false
        form.MinimizeBox <- false
        form.MaximumSize <- form.Size
        form.MinimumSize <- form.Size

        let seats = loadSeats
        let filteredSeats = filterSeatsByHall seats
        addMovieCards (filteredSeats) form 10 10 0

        Application.Run(form)

    0
