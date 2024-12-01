# Cinema-Seat-Reservation-System

## **Overview**

This project is a Cinema Seat Reservation System built in F# to allow users to manage cinema bookings effectively. The system provides features for seat visualization, reservation, user registration, login, and ticket management.

---

## **Features**

### **1. User Authentication**

-   **Registration Form**:

    -   Allows new users to register by providing a username and password.
    -   Stores user credentials securely (e.g., hashed passwords for security).

-   **Login Form**:
    -   Users log in with their registered credentials to access the booking system.
    -   Prevents unauthorized access to the system.

### **2. Seat Layout**

-   **Seat Representation**: Seats are represented using a 2D array or a list of tuples to map rows and columns in the cinema hall.
-   **Seat Display**: Displays available and reserved seats in a simple and interactive GUI or console interface.

### **3. Booking System**

-   **Seat Selection**: Enables users to select specific seats by row and column indices.
-   **Reservation Handling**:
    -   Marks selected seats as reserved.
    -   Prevents double-booking by validating seat availability before confirming the reservation.

### **4. Ticket Management**

-   **Unique Ticket Generation**: Generates a unique ticket ID for each successful booking.
-   **Ticket Details**:
    -   Stores booking details such as seat number, showtime, and customer name.
    -   persists data to a file for reference and backup.

---

## **Requirements**

### **Development Environment**

-   **F# Compiler** : Ensure the F# runtime and compiler are installed.
-   **IDE or CLI** : Visual Studio (with Windows Forms support), JetBrains Rider, or any editor supporting F#.
-   **.NET Runtime** : Required to execute the F# application.

### **Dependencies**

-   FSharp.Core: Core library for F# development.
-   System.Console: For console-based operations (optional if GUI-only).
-   Windows Forms: For building the interactive graphical interface for seat selection and user interaction.

## **Usage**

### **Run the Program**

1. Clone the repository.
2. Compile and run the F# code using your preferred IDE or CLI tools.
3. Interact with the System:
    - Register or Login: Register a new account or log in with an existing one.
    - View the Seating Chart: View available and reserved seats.
    - Reserve Seats: Select seats by entering row and column indices.
    - Generate Tickets: Receive a unique ticket for every successful reservation.
    - Logout: Securely log out from the system.
