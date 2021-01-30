using System;

namespace MyTracker.App
{
    public class Point
    {
        public double Lat { get; }
        public double Lng { get; }
        public Point(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }
    }

    public interface ILocationProvider
    {
        public DateTime LastMeasurement { get; }
        public Point GetLocation();
    }

    public class AppleLocationProvider : ILocationProvider
    {
        private Point currentPoint;
        // Ein set ist möglich, obwohl im Interface nur get "vorgeschrieben" ist.
        public DateTime LastMeasurement { get; private set; } = DateTime.MinValue;

        public Point GetLocation()
        {
            // DEVICE SPECIFIC CODE

            Random rnd = new Random();
            DateTime now = DateTime.Now;
            if ((now - LastMeasurement).TotalSeconds > 5)
            {
                LastMeasurement = now;
                currentPoint = new Point(rnd.NextDouble() * 180 - 90, rnd.NextDouble() * 360);
            }
            return currentPoint;
        }
    }

    public class AndroidLocationProvider : ILocationProvider
    {
        private Point currentPoint;
        public DateTime LastMeasurement { get; private set; } = DateTime.MinValue;

        public Point GetLocation()
        {
            // DEVICE SPECIFIC CODE

            Random rnd = new Random();
            DateTime now = DateTime.Now;
            if ((now - LastMeasurement).TotalSeconds > 2)
            {
                LastMeasurement = now;
                currentPoint = new Point(rnd.NextDouble() * 180 - 90, rnd.NextDouble() * 360);
            }
            return currentPoint;
        }
    }

    public interface ILogger
    {
        void Log(string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class FileLogger : ILogger
    {
        private string filename;
        public FileLogger(string filename)
        {
            this.filename = filename;
        }

        public void Log(string message)
        {
            System.IO.File.AppendAllText(this.filename, message);
        }
    }

    public class MyTracker
    {
        private readonly ILocationProvider locator;

        public ILogger Logger { get; set; }

        public MyTracker(ILocationProvider locator)
        {
            this.locator = locator;
        }

        public void DoTracking(int seconds)
        {
            DateTime start = DateTime.Now;
            while ((DateTime.Now - start).TotalSeconds < seconds)
            {
                Point position = locator.GetLocation();
                Logger?.Log($"Lat: {position.Lat:0.00}°, Lng: {position.Lat:0.00}°");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            MyTracker tracker;
            Console.WriteLine("Tracking mit ANDROID:");
            tracker = new MyTracker(new AndroidLocationProvider());
            tracker.Logger = new ConsoleLogger();
            tracker.DoTracking(10);
            Console.WriteLine("Tracking mit APPLE:");
            tracker = new MyTracker(new AppleLocationProvider());
            tracker.Logger = new FileLogger("locations.txt");
            tracker.DoTracking(10);
        }
    }
}
