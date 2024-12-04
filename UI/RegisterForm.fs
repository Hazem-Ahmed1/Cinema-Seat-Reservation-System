namespace CinemaReservation

open System.Drawing
open System.Windows.Forms
open UserAuthentication

type RegisterForm(filePath: string) as this =
    inherit Form()

    let createTextBox label yPos isPassword =
        let labelControl =
            new Label(Text = label, Location = Point(50, yPos), AutoSize = true)

        let textBox =
            if isPassword then
                new TextBox(Location = Point(50, yPos + 25), Width = 300, PasswordChar = '*')
            else
                new TextBox(Location = Point(50, yPos + 25), Width = 300)

        labelControl, textBox

    do
        this.Text <- "Cinema Registration"
        this.Size <- Size(400, 450)
        this.StartPosition <- FormStartPosition.CenterScreen

        let usernameLabel, usernameTextBox = createTextBox "Username" 50 false
        let emailLabel, emailTextBox = createTextBox "Email" 120 false
        let passwordLabel, passwordTextBox = createTextBox "Password" 190 true

        let confirmPasswordLabel, confirmPasswordTextBox =
            createTextBox "Confirm Password" 260 true

        let registerButton =
            new Button(
                Text = "Register",
                Location = Point(50, 330),
                Width = 300,
                BackColor = Color.Blue,
                ForeColor = Color.White
            )

        // Add controls
        this.Controls.AddRange(
            [| usernameLabel
               usernameTextBox
               emailLabel
               emailTextBox
               passwordLabel
               passwordTextBox
               confirmPasswordLabel
               confirmPasswordTextBox
               registerButton |]
        )

        // Register button click event
        registerButton.Click.Add(fun _ ->
            if passwordTextBox.Text <> confirmPasswordTextBox.Text then
                MessageBox.Show("Passwords do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                |> ignore
            else
                match registerUser usernameTextBox.Text passwordTextBox.Text emailTextBox.Text filePath with
                | Ok _ ->
                    MessageBox.Show("Registration Successful!", "Success") |> ignore
                    this.DialogResult <- DialogResult.OK
                    this.Close()
                | Error msg ->
                    MessageBox.Show(msg, "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    |> ignore)

