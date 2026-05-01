namespace TripPlanner;

using Spectre.Console;

public class ConsoleUI
{
    DataManager dataManager;

    public ConsoleUI()
    {
        dataManager = new DataManager();
    }

    public void Show()
    {
        string mode = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Please select mode")
                .AddChoices(new[] { "create", "view" }));

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

        Trip trip = dataManager.CreateTrip(name, startDate, endDate);

        string command;
        do
        {
            command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What do you want to do?")
                    .AddChoices(new[]
                    {
                        "add stop",
                        "add reservation details",
                        "add note",
                        "delete note",
                        "view trip summary",
                        "exit"
                    }));

            if (command == "add stop")
            {
                AddStop(trip);
            }
            else if (command == "add reservation details")
            {
                AddReservationDetails(trip);
            }
            else if (command == "add note")
            {
                AddNote(trip);
            }
            else if (command == "delete note")
            {
                DeleteNote(trip);
            }
            else if (command == "view trip summary")
            {
                Reporter.ViewSummary(trip);
            }

        } while (command != "exit");
    }

    public void ViewMode()
    {
        string command;
        do
        {
            command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What do you want to view?")
                    .AddChoices(new[]
                    {
                        "view saved trip data",
                        "view reservation details",
                        "view notes",
                        "exit"
                    }));

            if (command == "view saved trip data")
            {
                dataManager.DisplaySavedTripData();
            }
            else if (command == "view reservation details")
            {
                dataManager.DisplayAllReservations();
            }
            else if (command == "view notes")
            {
                dataManager.DisplayAllNotes();
            }

        } while (command != "exit");
    }

    public void AddStop(Trip trip)
    {
        Console.WriteLine("=== Add Stop ===");

        string location = AskForInput("Location: ");
        string date = AskForInput("Date: ");
        string desc = AskForInput("Description: ");

        dataManager.AddStop(trip, location, date, desc);

        Console.WriteLine("Stop added");
    }

    public void AddReservationDetails(Trip trip)
    {
        Console.WriteLine("=== Add Reservation ===");

        string hotel = AskForInput("Hotel name: ");
        string checkIn = AskForInput("Check-in: ");
        string checkOut = AskForInput("Check-out: ");
        string conf = AskForInput("Confirmation #: ");

        dataManager.AddReservation(trip, hotel, checkIn, checkOut, conf);

        Console.WriteLine("Reservation added.");
    }

    public void AddNote(Trip trip)
    {
        Console.WriteLine("=== Add Note ===");

        string noteText = AskForInput("Note: ");

        dataManager.AddNote(trip, noteText);

        Console.WriteLine("Note added");
    }

    public void DeleteNote(Trip trip)
    {
        if (trip.Notes.Count == 0)
        {
            Console.WriteLine("No notes to delete");
            return;
        }

        TripNote selectedNote = AnsiConsole.Prompt(
            new SelectionPrompt<TripNote>()
                .Title("Select a note to delete")
                .AddChoices(trip.Notes));

        dataManager.DeleteNote(trip, selectedNote);

        Console.WriteLine("Note deleted");
    }

    public string AskForInput(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }
}