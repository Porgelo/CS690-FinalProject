namespace TripPlanner;

using System.IO;

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

    public void AppendData(TripData tripData)
    {
        File.AppendAllText( this.fileName, tripData.Type + ":" + tripData.TripName + ":" + tripData.Field1 + ":" + 
        tripData.Field2 + ":" + tripData.Field3 + ":" + tripData.Field4 + Environment.NewLine);
    }

    public void DisplaySavedTripData()
    {
        string[] lines = File.ReadAllLines(fileName);

        if (lines.Length == 0)
        {
            Console.WriteLine("No saved trip data found.");
            return;
        }

        Console.WriteLine("===== Viewing Saved Trip Data =====");

        foreach (string line in lines)
        {
            string[] parts = line.Split(":");

            if (parts[0] == "TRIP")
            {
                Console.WriteLine("\nTrip Summary");
                Console.WriteLine("Trip: " + parts[1]);
                Console.WriteLine("Dates: " + parts[2] + " - " + parts[3]);
            }
            else if (parts[0] == "STOP")
            {
                Console.WriteLine("\nStop");
                Console.WriteLine("- " + parts[3] + " : " + parts[2] + " - " + parts[4]);
            }
            else if (parts[0] == "RESERVATION")
            {
                Console.WriteLine("\nReservation");
                Console.WriteLine("- " + parts[2] + " Check-in: " + parts[3] +
                    " Check-out: " + parts[4] + " Conf#: " + parts[5]);
            }
            else if (parts[0] == "NOTE")
            {
                Console.WriteLine("\nNote");
                Console.WriteLine("- " + parts[2]);
            }
        }

        Console.WriteLine();
    }

    public void DisplayStopsByDate(string targetDate)
    {
        string[] lines = File.ReadAllLines(fileName);
        bool found = false;

        Console.WriteLine("\n=== Stops for " + targetDate + " ===");

        foreach (string line in lines)
        {
            string[] parts = line.Split(":");

            if (parts[0] == "STOP" && parts[3] == targetDate)
            {
                found = true;

                Console.WriteLine(
                    "- " + parts[3] + " : " +
                    parts[2] + " - " +
                    parts[4]);
            }
        }

        if (!found)
        {
            Console.WriteLine("No stops found for this date.");
        }

        Console.WriteLine();
    }

    public void DisplayAllReservations()
    {
        string[] lines = File.ReadAllLines(fileName);
        bool found = false;

        Console.WriteLine("\n=== All Reservations ===");

        foreach (string line in lines)
        {
            string[] parts = line.Split(":");

            if (parts[0] == "RESERVATION")
            {
                found = true;

                Console.WriteLine(
                    "- " + parts[2] +
                    " Check-in: " + parts[3] +
                    " Check-out: " + parts[4] +
                    " Conf#: " + parts[5]);
            }
        }

        if (!found)
        {
            Console.WriteLine("No reservations found.");
        }

        Console.WriteLine();
    }

    public void DisplayAllNotes()
    {
        string[] lines = File.ReadAllLines(fileName);
        bool found = false;

        Console.WriteLine("\n=== All Notes ===");

        foreach (string line in lines)
        {
            string[] parts = line.Split(":");

            if (parts[0] == "NOTE")
            {
                found = true;
                Console.WriteLine("- " + parts[2]);
            }
        }

        if (!found)
        {
            Console.WriteLine("No notes found.");
        }

        Console.WriteLine();
    }

    public void SearchTripData(string keyword)
    {
        string[] lines = File.ReadAllLines(fileName);
        bool found = false;

        Console.WriteLine("\n=== Search Results ===");

        foreach (string line in lines)
        {
            if (line.Contains(keyword))
            {
                found = true;
                Console.WriteLine("- " + line);
            }
        }

        if (!found)
        {
            Console.WriteLine("No matching results found.");
        }

        Console.WriteLine();
    }
}