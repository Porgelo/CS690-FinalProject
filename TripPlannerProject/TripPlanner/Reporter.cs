namespace TripPlanner;

public class Reporter
{
    public static void ViewSummary(Trip trip)
    {
        Console.WriteLine("Trip Summary");

        Console.WriteLine("Trip: " + trip.Name);
        Console.WriteLine("Dates: " + trip.StartDate + " - " + trip.EndDate);

        ViewStops(trip);
        ViewReservations(trip);
        ViewNotes(trip);

        Console.WriteLine();
    }

    public static void ViewStops(Trip trip)
    {
        Console.WriteLine("\n[bold]Stops:[/]");

        if (trip.Stops.Count == 0)
        {
            Console.WriteLine("No stops added.");
        }
        else
        {
            foreach (ScheduledStop stop in trip.Stops)
            {
                Console.WriteLine("- " + stop.Date + " : " + stop.Location + " - " + stop.Description);
            }
        }
    }

    public static void ViewReservations(Trip trip)
    {
        Console.WriteLine("\nReservations:");

        if (trip.Reservations.Count == 0)
        {
            Console.WriteLine("No reservations added.");
        }
        else
        {
            foreach (Reservation reservation in trip.Reservations)
            {
                Console.WriteLine("- " + reservation.HotelName + " Check-in: " + reservation.CheckIn +
                    " Check-out: " + reservation.CheckOut + " Conf#: " + reservation.ConfirmationNumber);
            }
        }
    }

    public static void ViewNotes(Trip trip)
    {
        Console.WriteLine("\nNotes:");

        if (trip.Notes.Count == 0)
        {
            Console.WriteLine("No notes added.");
        }
        else
        {
            foreach (TripNote note in trip.Notes)
            {
                Console.WriteLine("- " + note.Text);
            }
        }
    }
}