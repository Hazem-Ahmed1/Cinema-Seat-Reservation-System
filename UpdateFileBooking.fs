module UpdateFileBooking

open System
open System.IO

let updateLineByCriteria (filePath: string) (hallName: string) (row: int) (column: int) (updateFunc: string -> string) =

    // Read all lines from the file
    let lines = File.ReadAllLines(filePath)


    // HallName ,  path , class , row , column , price , availabilty , date
    //    0 	     1      2       3     4        5         6          7

    // Find the index of the line that matches the given criteria
    let matchingIndex =
        lines
        |> Array.tryFindIndex (fun line ->
            let parts = line.Split(',')

            parts.Length > 6
            && parts.[0].Trim() = hallName
            && parts.[3].Trim() = row.ToString()
            && parts.[4].Trim() = column.ToString())

    match matchingIndex with
    | Some index ->
        let updatedLine = updateFunc lines.[index]
        lines.[index] <- updatedLine
        File.WriteAllLines(filePath, lines)
        true // Indicate success
    | None -> false

let changeAvailability (line: string) : string =
    let parts = line.Split(',')

    if parts.Length > 7 && parts.[6].Trim() = "true" then
        parts.[6] <- "false"
        String.Join(",", parts)
    else
        line
