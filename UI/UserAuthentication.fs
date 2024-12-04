namespace CinemaReservation

open System.Text.RegularExpressions
open System
open System.IO
open System.Security.Cryptography

module UserAuthentication =

    type User =
        { Username: string
          PasswordHash: string
          Email: string
          CreatedAt: DateTime }

    let private hashPassword (password: string) =
        use sha256 = SHA256.Create()

        password
        |> System.Text.Encoding.UTF8.GetBytes
        |> sha256.ComputeHash
        |> Array.map (fun b -> b.ToString("x2"))
        |> String.concat ""

    let isValidEmail (email: string) =
        // Regular expression pattern for email validation
        let emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"

        // Check if the email is not null or empty
        if System.String.IsNullOrWhiteSpace(email) then
            false
        else
            // Use Regex to match the email pattern
            Regex.IsMatch(email, emailPattern)

    

    let registerUser (username: string) (password: string) (email: string) (userDatabasePath : string) =
        if
            String.IsNullOrWhiteSpace(username)
            || String.IsNullOrWhiteSpace(password)
            || String.IsNullOrWhiteSpace(email)
        then
            Error "Username, password, and email cannot be empty"
        elif not (isValidEmail (email)) then
            Error "Invalid email format"
        else
            try
                // Check if the email already exists
                if File.Exists(userDatabasePath) then
                    let existingUsers = File.ReadAllLines(userDatabasePath)

                    if
                        existingUsers
                        |> Array.exists (fun line ->
                            let fields = line.Split(',')
                            fields.Length > 2 && fields.[2].Trim() = email)
                    then
                        Error "This email is already registered."
                    elif
                        existingUsers
                        |> Array.exists (fun line ->
                            let fields = line.Split(',')
                            fields.Length > 2 && fields.[0].Trim() = username)
                    then
                        Error "This Username is already registered."
                    else
                        // Proceed to register the user
                        let passwordHash = hashPassword password

                        let newUser =
                            { Username = username
                              PasswordHash = passwordHash
                              Email = email
                              CreatedAt = DateTime.Now }

                        let userEntry =
                            sprintf
                                "%s,%s,%s,%s"
                                newUser.Username
                                newUser.PasswordHash
                                newUser.Email
                                (newUser.CreatedAt.ToString("o"))

                        File.AppendAllLines(userDatabasePath, [ userEntry ])
                        Ok newUser
                else
                    // If the database file doesn't exist, create it and register the user
                    let passwordHash = hashPassword password

                    let newUser =
                        { Username = username
                          PasswordHash = passwordHash
                          Email = email
                          CreatedAt = DateTime.Now }

                    let userEntry =
                        sprintf
                            "%s,%s,%s,%s"
                            newUser.Username
                            newUser.PasswordHash
                            newUser.Email
                            (newUser.CreatedAt.ToString("o"))

                    File.WriteAllLines(userDatabasePath, [ userEntry ])
                    Ok newUser
            with ex ->
                Error(sprintf "Registration failed: %s" ex.Message)


    let authenticateUser (username: string) (password: string) (userDatabasePath : string) =
        try
            let users =
                File.ReadAllLines(userDatabasePath)
                |> Array.choose (fun line ->
                    let parts = line.Split(',')

                    if parts.Length >= 4 && parts.[0] = username then
                        let username = parts.[0]
                        let storedHash = parts.[1]
                        let email = parts.[2]
                        let createdAt = parts.[3..] |> String.concat "," // Handle additional commas in timestamps
                        Some(username, storedHash, email, createdAt)
                    else
                        None)

            match users with
            | [| (_, storedHash, _, _) |] ->
                let inputHash = hashPassword password

                if inputHash = storedHash then
                    Ok username
                else
                    Error "Invalid credentials"
            | _ -> Error "User not found"
        with ex ->
            Error(sprintf "Authentication failed: %s" ex.Message)
