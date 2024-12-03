module Ticketdetails
open CinemaSeatTypes
open System
open System.IO

type Ticket = {
    TicketID: string
    Seat: Seat
    // CustomerName: string
}

let ticket seat = {TicketID = Guid.NewGuid().ToString(); Seat = seat}

let customSerializeTicket (ticket: Ticket) =
    $"{ticket.TicketID}|{ticket.Seat.Class[0]}{ticket.Seat.Row},{ticket.Seat.Column}|{ticket.Seat.ShowTime}|Customer Name"

let saveTicketToTxt (ticket: Ticket) (outputPath: string) =
    let existingTickets =
        if File.Exists(outputPath) then
            File.ReadAllLines(outputPath)
            |> Array.toList
        else
            []

    let serializedTicket = customSerializeTicket ticket
    let updatedTickets = serializedTicket :: existingTickets

    File.WriteAllLines(outputPath, updatedTickets |> List.rev)