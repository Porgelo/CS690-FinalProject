namespace TripPlanner;

using System.IO;

class Program
{
    static void Main(string[] args)
    {
        ConsoleUI ui = new ConsoleUI();
        ui.Show();
    }
}

public class ConsoleUI
{
    List<Trip> trips;
    FileSaver fileSaver;

    public ConsoleUI()
    {
        trips = new List<Trip>();
        fileSaver = new FileSaver("trip-data.txt");
    }

    public void Show()
    {
        string mode = AskForInput("Please select mode (create OR view): ");

        if (mode == "create")
        {
            CreateMode();
        }
        else if (mode == "view")
        {
            ViewMode();
        }
    }

    public void CreateMode()
    {
        string name = AskForInput("Trip name: ");
        string startDate = AskForInput("Start date: ");
        string endDate = AskForInput("End date: ");

        Trip trip = new Trip(name, startDate, endDate);
        trips.Add(trip);

        fileSaver.AppendLine("TRIP:" + name + ":" + startDate + ":" + endDate);

        string command;
        do
        {
            Console.WriteLine("1. Add Stop");
            Console.WriteLine("2. Add Reservation Details");
            Console.WriteLine("3. View Trip Summary");
            Console.WriteLine("4. Exit");

            command = AskForInput("Select command (as number): ");

            if (command == "1")
            {
                AddStop(trip);
            }
            else if (command == "2")
            {
                AddReservationDetails(trip);
            }
            else if (command == "3")
            {
                ViewSummary(trip);
            }

        } while (command != "4");
    }

    public void ViewMode()
    {
        fileSaver.DisplaySavedTripData();
    }

    public void AddStop(Trip trip)
    {
        Console.WriteLine("=== Add Stop ===");

        string location = AskForInput("Location: ");
        string date = AskForInput("Date: ");
        string desc = AskForInput("Description: ");

        trip.Stops.Add(new ScheduledStop(location, date, desc));

        fileSaver.AppendLine("STOP:" + trip.Name + ":" + location + ":" + date + ":" + desc);

        Console.WriteLine("Stop added.");
    }

    public void AddReservationDetails(Trip trip)
    {
        Console.WriteLine("=== Add Reservation ===");

        string hotel = AskForInput("Hotel name: ");
        string checkIn = AskForInput("Check-in: ");
        string checkOut = AskForInput("Check-out: ");
        string conf = AskForInput("Confirmation #: ");

        trip.Reservations.Add(new Reservation(hotel, checkIn, checkOut, conf));

        fileSaver.AppendLine("RESERVATION:" + trip.Name + ":" + hotel + ":" + checkIn + ":" + checkOut + ":" + conf);

        Console.WriteLine("Reservation added.");
    }

    public void ViewSummary(Trip trip)
    {
        Console.WriteLine("\n=== Trip Summary ===");
        Console.WriteLine("Trip: " + trip.Name);
        Console.WriteLine("Dates: " + trip.StartDate + " - " + trip.EndDate);

        Console.WriteLine("\nStops:");
        if (trip.Stops.Count == 0)
        {
            Console.WriteLine("No stops added.");
        }
        else
        {
            foreach (var stop in trip.Stops)
            {
                Console.WriteLine("- " + stop.Date + " : " + stop.Location + " - " + stop.Description);
            }
        }

        Console.WriteLine("\nReservations:");
        if (trip.Reservations.Count == 0)
        {
            Console.WriteLine("No reservations added.");
        }
        else
        {
            foreach (var res in trip.Reservations)
            {
                Console.WriteLine("- " + res.HotelName + " Check-in: " + res.CheckIn + 
                " Check-out: " + res.CheckOut + " Conf#: " + res.ConfirmationNumber);
            }
        }

        Console.WriteLine();
    }

    public string AskForInput(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }
}

public class FileSaver
{
    string fileName;

    public FileSaver(string fileName)
    {
        this.fileName = fileName;

        if (!File.Exists(this.fileName))
        {
            File.Create(this.fileName).Close();
        }
    }

    public void AppendLine(string line)
    {
        File.AppendAllText(this.fileName, line + Environment.NewLine);
    }

    public void DisplaySavedTripData()
    {
        string[] lines = File.ReadAllLines(fileName);

        if (lines.Length == 0)
        {
            Console.WriteLine("No saved trip data found.");
            return;
        }

        Console.WriteLine("\n=== Viewing Trip Summary ===");

        foreach (string line in lines)
        {
            string[] parts = line.Split(":");

            if (parts[0] == "TRIP")
            {
                Console.WriteLine("\n=== Trip Summary ===");
                Console.WriteLine("Trip: " + parts[1]);
                Console.WriteLine("Dates: " + parts[2] + " - " + parts[3]);
            }
            else if (parts[0] == "STOP")
            {
                Console.WriteLine("\nStop:");
                Console.WriteLine("- " + parts[3] + " : " + parts[2] + " - " + parts[4]);
            }
            else if (parts[0] == "RESERVATION")
            {
                Console.WriteLine("\nReservation:");
                Console.WriteLine("- " + parts[2] + " Check-in: " + parts[3] + 
                " Check-out: " + parts[4] + " Conf#: " + parts[5]);
            }
        }
        Console.WriteLine();
    }
}

public class Trip
{
    public string Name;
    public string StartDate;
    public string EndDate;

    public List<ScheduledStop> Stops = new List<ScheduledStop>();
    public List<Reservation> Reservations = new List<Reservation>();

    public Trip(string name, string startDate, string endDate)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
    }
}

public class ScheduledStop
{
    public string Location;
    public string Date;
    public string Description;

    public ScheduledStop(string location, string date, string desc)
    {
        Location = location;
        Date = date;
        Description = desc;
    }
}

public class Reservation
{
    public string HotelName;
    public string CheckIn;
    public string CheckOut;
    public string ConfirmationNumber;

    public Reservation(string hotel, string checkIn, string checkOut, string confNum)
    {
        HotelName = hotel;
        CheckIn = checkIn;
        CheckOut = checkOut;
        ConfirmationNumber = confNum;
    }
}