module UserSession

open System.IO

let SessionStorageFilePath = "D:\Collage\Abdelwahed\4th\First Term\PL3\Project\Cinema-Seat-Reservation-System\Database\SessionStorage.txt"

// Clears the file and writes the logged-in user to it
let clearAndWriteUser (filePath: string, loggedInUser: string) =
    File.WriteAllText(filePath, loggedInUser)

let getuser (filePath: string) =
    File.ReadAllLines(filePath).[0]



