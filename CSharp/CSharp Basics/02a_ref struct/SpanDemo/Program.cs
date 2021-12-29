using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

public class Program
{
    public static void Main(string[] args)
    {
        var bm = BenchmarkRunner.Run<Benchy>();
    }
}

[MemoryDiagnoser]
public class Benchy
{
    private readonly string _dateString = "08 07 2021";

    [Benchmark]
    public (int day, int month, int year) SplitDateAsString()
    {
        string dayText = _dateString.Substring(0, 2);
        string monthText = _dateString.Substring(3, 2);
        string yearText = _dateString.Substring(6, 4);
        var day = int.Parse(dayText);
        var month = int.Parse(monthText);
        var year = int.Parse(yearText);

        return (day, month, year);
    }

    [Benchmark]
    public (int day, int month, int year) SplitDateAsSpan()
    {
        ReadOnlySpan<char> dateSpan = _dateString;

        var dayText = dateSpan.Slice(0, 2);
        var monthText = dateSpan.Slice(3, 2);
        var yearText = dateSpan.Slice(6, 4);
        var day = int.Parse(dayText);
        var month = int.Parse(monthText);
        var year = int.Parse(yearText);

        return (day, month, year);
    }
}