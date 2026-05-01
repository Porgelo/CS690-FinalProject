namespace TripPlanner;

public class Trip
{
    public string Name { get; }
    public string StartDate { get; }
    public string EndDate { get; }

    public List<ScheduledStop> Stops { get; }
    public List<Reservation> Reservations { get; }
    public List<TripNote> Notes { get; }

    public Trip(string name, string startDate, string endDate)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;

        Stops = new List<ScheduledStop>();
        Reservations = new List<Reservation>();
        Notes = new List<TripNote>();
    }

    public override string ToString()
    {
        return Name;
    }
}

public class ScheduledStop
{
    public string Location { get; }
    public string Date { get; }
    public string Description { get; }

    public ScheduledStop(string location, string date, string desc)
    {
        Location = location;
        Date = date;
        Description = desc;
    }

    public override string ToString()
    {
        return Date + " - " + Location;
    }
}

public class Reservation
{
    public string HotelName { get; }
    public string CheckIn { get; }
    public string CheckOut { get; }
    public string ConfirmationNumber { get; }

    public Reservation(string hotel, string checkIn, string checkOut, string confNum)
    {
        HotelName = hotel;
        CheckIn = checkIn;
        CheckOut = checkOut;
        ConfirmationNumber = confNum;
    }

    public override string ToString()
    {
        return HotelName + " (" + ConfirmationNumber + ")";
    }
}

public class TripNote
{
    public string Text { get; }

    public TripNote(string text)
    {
        Text = text;
    }

    public override string ToString()
    {
        return Text;
    }
}

public class TripData
{
    public string Type { get; }
    public string TripName { get; }
    // Generic fields (so can be used for stop, reservation, note)
    public string Field1 { get; }
    public string Field2 { get; }
    public string Field3 { get; }
    public string Field4 { get; }

    public TripData(string type, string tripName, string field1, string field2, string field3, string field4)
    {
        Type = type;
        TripName = tripName;
        Field1 = field1;
        Field2 = field2;
        Field3 = field3;
        Field4 = field4;
    }
}