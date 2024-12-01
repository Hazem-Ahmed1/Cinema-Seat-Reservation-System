open System
open System.Windows.Forms

[<EntryPoint; STAThread>]
let main argv =
    Application.EnableVisualStyles()
    Application.SetCompatibleTextRenderingDefault(false)
    
    // Create the form
    let form = new Form(Text = "Register Form", Width = 300, Height = 200)
    
    // Create a label and textbox for Username
    let lblUsername = new Label(Text = "Username:", Top = 20, Left = 20, AutoSize = true)
    let txtUsername = new TextBox(Top = lblUsername.Top, Left = 120, Width = 150)
    
    // Create a label and textbox for Password
    let lblPassword = new Label(Text = "Password:", Top = 60, Left = 20, AutoSize = true)
    let txtPassword = new TextBox(Top = lblPassword.Top, Left = 120, Width = 150, PasswordChar = '/')
    
    // Create a button to submit the form
    let btnSubmit = new Button(Text = "Register", Top = 100, Left = 120, Width = 150)
    btnSubmit.Click.Add (fun _ -> 
        MessageBox.Show(sprintf "Username: %s\nPassword: %s" txtUsername.Text txtPassword.Text, "Info")
        |> ignore
    )
    
    // Add controls to the form
    form.Controls.AddRange([| lblUsername; txtUsername; lblPassword; txtPassword; btnSubmit |])
    
    // Run the application
    Application.Run(form)
    0 // Return an integer exit code
