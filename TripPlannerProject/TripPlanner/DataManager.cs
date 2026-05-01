namespace TripPlanner;

public class DataManager
{
    public List<Trip> Trips { get; }
    FileSaver fileSaver;

    public DataManager()
    {
        Trips = new List<Trip>();
        fileSaver = new FileSaver("trip-data.txt");
    }

    public Trip CreateTrip(string name, string startDate, string endDate)
    {
        Trip trip = new Trip(name, startDate, endDate);
        Trips.Add(trip);

        fileSaver.AppendLine("TRIP:" + name + ":" + startDate + ":" + endDate);

        return trip;
    }

    public void AddStop(Trip trip, string location, string date, string desc)
    {
        ScheduledStop stop = new ScheduledStop(location, date, desc);
        trip.Stops.Add(stop);

        fileSaver.AppendData(new TripData("STOP",trip.Name, location, date, desc, ""));
    }

    public void AddReservation(Trip trip, string hotel, string checkIn, string checkOut, string conf)
    {
        Reservation reservation = new Reservation(hotel, checkIn, checkOut, conf);
        trip.Reservations.Add(reservation);

        fileSaver.AppendData(new TripData("RESERVATION", trip.Name, hotel, checkIn, checkOut, conf));
    }

    public void AddNote(Trip trip, string noteText)
    {
        TripNote note = new TripNote(noteText);
        trip.Notes.Add(note);

        fileSaver.AppendData(new TripData("NOTE", trip.Name, note.Text, "", "", ""));
    }

    public void DeleteNote(Trip trip, TripNote note)
    {
        trip.Notes.Remove(note);
        fileSaver.AppendData(new TripData("DELETE_NOTE", trip.Name, note.Text, "", "", ""));
    }

    public void DisplaySavedTripData()
    {
        fileSaver.DisplaySavedTripData();
    }

    public void DisplayAllReservations()
    {
        fileSaver.DisplayAllReservations();
    }

    public void DisplayAllNotes()
    {
        fileSaver.DisplayAllNotes();
    }
}