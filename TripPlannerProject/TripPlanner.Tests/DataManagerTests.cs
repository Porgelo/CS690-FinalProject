namespace TripPlanner.Tests;

public class DataManagerTests
{
    DataManager dataManager;

    public DataManagerTests()
    {
        File.Delete("trip-data.txt");
        dataManager = new DataManager();
    }

    [Fact]
    public void Test_CreateTrip()
    {
        Trip trip = dataManager.CreateTrip("Beach", "May 1", "May 5");

        Assert.Equal(1, dataManager.Trips.Count);
        Assert.Equal("Beach", trip.Name);
    }

    [Fact]
    public void Test_AddStop()
    {
        Trip trip = dataManager.CreateTrip("Beach", "May 1", "May 5");

        dataManager.AddStop(trip, "Buccee's", "May 2", "Lunch");

        Assert.Equal(1, trip.Stops.Count);
        Assert.Equal("Buccee's", trip.Stops[0].Location);
    }

    [Fact]
    public void Test_AddReservation()
    {
        Trip trip = dataManager.CreateTrip("Beach", "May 1", "May 5");

        dataManager.AddReservation(trip, "Holiday Inn", "5 PM", "11 AM", "ABC123");

        Assert.Equal(1, trip.Reservations.Count);
        Assert.Equal("Holiday Inn", trip.Reservations[0].HotelName);
    }
}