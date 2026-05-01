namespace TripPlanner.Tests;

public class FileSaverTests
{
    FileSaver fileSaver;
    string testFileName;

    public FileSaverTests()
    {
        testFileName = "test-trip-data.txt";
        File.Delete(testFileName);
        fileSaver = new FileSaver(testFileName);
    }

    [Fact]
    public void Test_FileSaver_AppendLine()
    {
        fileSaver.AppendLine("Hello, World!");

        var contentFromFile = File.ReadAllText(testFileName);

        Assert.Equal("Hello, World!" + Environment.NewLine, contentFromFile);
    }

    [Fact]
    public void Test_FileSaver_AppendData()
    {
        TripData sampleData = new TripData("RESERVATION", "Beach", "Holiday Inn", "5 PM", "11 AM", "ABC123");

        fileSaver.AppendData(sampleData);
        var contentFromFile = File.ReadAllText(testFileName);
        Assert.Equal(contentFromFile, "RESERVATION:Beach:Holiday Inn:5 PM:11 AM:ABC123" + Environment.NewLine);
    }
}
