module FileReader

open System.IO
open CinemaSeatTypes
open System

let readLinesLazy (filePath: string) : seq<string> =
    let rec readLines (reader: StreamReader) =
        seq {
            if not reader.EndOfStream then
                yield reader.ReadLine()
                yield! readLines reader
        }

    try
        seq {
            use reader = new StreamReader(filePath)
            yield! readLines reader
        }
    with ex ->
        printfn "An error occurred: %s" ex.Message
        Seq.empty



let parseLineToSeat (line: string) : Seat option =
    try
        let parts = line.Split(',')

        if parts.Length >= 8 then
            // Parsing Movie data
            let movieParts = parts.[1].Split('|') // Assuming 'Sci-Fi 8.8' is the movie name and rating

            let movie =
                { Name = movieParts.[0] // Name part
                  Genre = movieParts.[1] // Genre part
                  Rating = float movieParts.[2] // Rating part (assuming format like "8.8")
                  PosterPath = movieParts.[3] // Assuming the path is the last column in the string
                }

            Some
                { HallName = parts.[0] // Hall Name
                  Movie = movie
                  Class = parts.[2] // Seat Class
                  Row = int parts.[3] // Row
                  Column = int parts.[4] // Column
                  Price = int parts.[5] // Price
                  Available = Boolean.Parse(parts.[6]) // Available status
                  ShowTime = DateTime.Parse(parts.[7]).ToString("yyyy/MM/dd hh:mm tt") // ShowTime
                }
        else
            None
    with ex ->
        printfn "Failed to parse line: %s. Error: %s" line ex.Message
        None


// Read lines from the file
let filePath =
    @"D:\Collage\Abdelwahed\4th\First Term\PL3\Project\Cinema-Seat-Reservation-System\Database\Data.txt"

let loadSeats =
    let lines = readLinesLazy filePath

    // Map lines to Seat objects
    let seats = lines |> Seq.choose parseLineToSeat // Filters out lines that cannot be parsed
    Seq.toList (seats)

let filterSeatsByHall (seats: Seat list) =
    seats |> Seq.distinctBy (fun seat -> seat.HallName) |> Seq.toList
