module CinemaSeatTypes
open System

// Define a movie type
type Movie = {
    Name: string
    Genre: string
    Rating: float
    PosterPath: string
}
type Seat = {
    HallName: string
    Movie: Movie
    Class: string
    Row: int
    Column: int
    Available: bool
    ShowTime : DateTime
}

// string * string * int * int * bool * DateTime



type CinemaSeats = Seat List

