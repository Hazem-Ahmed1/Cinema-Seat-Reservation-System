namespace CinemaReservation

open System.Drawing
open System.Windows.Forms
open UserAuthentication


    

type LoginForm(filePath: string) as this =

    inherit Form()

    let createTextBox label yPos =
        let labelControl =
            new Label(Text = label, Location = Point(50, yPos), AutoSize = true)

        let textBox = new TextBox(Location = Point(50, yPos + 25), Width = 300)
        labelControl, textBox

    do
        this.Text <- "Cinema Login"
        this.Size <- Size(400, 300)
        this.StartPosition <- FormStartPosition.CenterScreen

        let usernameLabel, usernameTextBox = createTextBox "Username" 50
        let passwordLabel, passwordTextBox = createTextBox "Password" 120
        passwordTextBox.PasswordChar <- '*'

        let loginButton =
            new Button(
                Text = "Login",
                Location = Point(50, 200),
                Width = 300,
                BackColor = Color.Green,
                ForeColor = Color.White
            )

        let registerLinkLabel =
            new Label(
                Text = "Don't have an account? Register here",
                Location = Point(50, 240),
                ForeColor = Color.Blue,
                AutoSize = true,
                Cursor = Cursors.Hand
            )

        // Add controls
        this.Controls.AddRange(
            [| usernameLabel
               usernameTextBox
               passwordLabel
               passwordTextBox
               loginButton
               registerLinkLabel |]
        )

        // Login button click event
        loginButton.Click.Add(fun _ ->
            match authenticateUser usernameTextBox.Text passwordTextBox.Text filePath with
            | Ok _ ->
                this.Tag <- usernameTextBox.Text // Store the username in the Tag property
                MessageBox.Show("Login Successful!", "Success") |> ignore
                this.DialogResult <- DialogResult.OK
            | Error msg ->
                MessageBox.Show(msg, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                |> ignore)

        // Register link click event
        registerLinkLabel.Click.Add(fun _ ->
            this.Hide() // Hide the login form
            use registerForm = new RegisterForm(filePath)

            if registerForm.ShowDialog() = DialogResult.OK then
                MessageBox.Show(
                    "Registration Successful! Please login.",
                    "Info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )
                |> ignore

            this.Show() // Show the login form again after the register form is closed
        )
