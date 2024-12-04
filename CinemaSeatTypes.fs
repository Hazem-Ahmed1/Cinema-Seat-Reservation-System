module CinemaSeatTypes

// Define a movie type
type Movie =
    { Name: string
      Genre: string
      Rating: float
      PosterPath: string }

type Seat =
    { HallName: string
      Movie: Movie
      Class: string
      Row: int
      Column: int
      Price: int
      Available: bool
      ShowTime: string }


// string * string * int * int * bool * DateTime



type CinemaSeats = Seat List
