﻿module MovieCard

open System
open System.Windows.Forms
open System.Drawing
open BookingForm
open CinemaSeatTypes
open FileReader


// Function to create a movie card
let createMovieCard (seat: Seat) (x: int) (y: int)=
    let panel = new Panel()
    panel.Size <- Size(200, 325)
    panel.Location <- Point(x, y)
    panel.BorderStyle <- BorderStyle.FixedSingle

    let pictureBox = new PictureBox()
    pictureBox.Size <- Size(200, 150)
    pictureBox.Location <- Point(0, 0)
    pictureBox.Image <- Image.FromFile(seat.Movie.PosterPath)
    pictureBox.SizeMode <- PictureBoxSizeMode.StretchImage

    let titleLabel = new Label()
    titleLabel.Text <- seat.Movie.Name
    titleLabel.Font <- new Font("Arial", 12.0f, FontStyle.Bold)
    titleLabel.Location <- Point(5, 160)
    titleLabel.Size <- Size(190, 20)

    let genreLabel = new Label()
    genreLabel.Text <- $"Genre: {seat.Movie.Genre}"
    genreLabel.Location <- Point(5, 185)
    genreLabel.Size <- Size(190, 20)

    let ratingLabel = new Label()
    ratingLabel.Text <- $"Rating: {seat.Movie.Rating:F1}"
    ratingLabel.Location <- Point(5, 210)
    ratingLabel.Size <- Size(190, 20)

    let showTimeLabel = new Label()
    showTimeLabel.Text <- $"Showtime: {seat.ShowTime}"
    showTimeLabel.Location <- Point(5, 235)
    showTimeLabel.Size <- Size(190, 20)

    let HallLabel = new Label()
    HallLabel.Text <- $"Hall Name: {seat.HallName}"
    HallLabel.Location <- Point(5, 260)
    HallLabel.Size <- Size(190, 20)

    let ReserveButton = new Button()
    ReserveButton.Text <- $"Check For Seat"
    ReserveButton.Location <- Point(5, 285)
    ReserveButton.Size <- Size(190, 30)

    panel.Controls.Add(pictureBox)
    panel.Controls.Add(titleLabel)
    panel.Controls.Add(genreLabel)
    panel.Controls.Add(ratingLabel)
    panel.Controls.Add(showTimeLabel)
    panel.Controls.Add(HallLabel)
    panel.Controls.Add(ReserveButton)


    // Click event handler
    ReserveButton.Click.Add(fun _ ->
        // Create and show the new window/form
        let detailsForm = new MovieDetailsForm (seat, loadSeats)
        detailsForm.Show()
    )

    panel