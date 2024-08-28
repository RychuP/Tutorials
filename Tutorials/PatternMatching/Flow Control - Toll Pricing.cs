using System;

namespace Tutorials.PatternMatching;
// Variation of a tutorial posted at https://docs.microsoft.com/en-gb/dotnet/csharp/fundamentals/tutorials/pattern-matching
// Program creates a list of random vehicles crossing a toll and calculates charges.

static class FlowControl
{
    const int amountOfVehicles = 20,
        maxPassengersInCar = 3,
        maxPassengersInBus = 90,
        maxTruckGrossWeight = 44;
    const decimal carToll = 2.0m,
        taxiToll = 3.5m,
        busToll = 5.0m,
        truckToll = 10.0m;
    static readonly Random randNoGenerator = new();
    static readonly Array vehicleTypes = Enum.GetValues(typeof(VehicleType));
    static readonly int NoOfVehicleTypes = vehicleTypes.Length;

    public static void Test()
    {
        var vehicles = new Vehicle[amountOfVehicles];

        // generate random vehicles crossing the toll
        for (int i = 0; i < amountOfVehicles; i++)
        {
            vehicles[i] = GetRandomVehicle();
        }

        // table header
        DisplayRow("Vehicle:", "Contents:", "Adjustment:", "Toll:");
        DisplayRow("---", "---", "---", "---");

        // calculate tolls
        foreach (var veh in vehicles)
        {
            Toll t = CalculateToll(veh);
            DisplayRow(
                veh.GetType().Name,
                veh.Contents,
                t.Discount switch
                {
                    < 0 => $"-£{-t.Discount:0.0}",
                    0 => "None",
                    _ => $"+£{t.Discount:0.0}",
                },
                $"£{t.Price + t.Discount:0.0}"
            );
        }
    }

    static void DisplayRow(object s1, object s2, object s3, object s4)
    {
        Console.WriteLine(string.Format("{0, -10} | {1, -22} | {2, -13} | {3, -5}", s1, s2, s3, s4));
    }

    static Toll CalculateToll(Vehicle veh) =>
        veh switch
        {
            Car c => c.Passengers switch
            {
                0 => new Toll(carToll, 0.5m),
                1 => new Toll(carToll),
                2 => new Toll(carToll, -0.5m),
                _ => new Toll(carToll, -1.0m)
            },

            Taxi t => t.Fares switch
            {
                0 => new Toll(taxiToll, 1.0m),
                1 => new Toll(taxiToll),
                2 => new Toll(taxiToll, -0.5m),
                _ => new Toll(taxiToll, -1.0m)
            },

            Bus b when b.Riders / (double)b.Capacity < 0.5d => new Toll(busToll, 2.0m),
            Bus b when b.Riders / (double)b.Capacity > 0.9d => new Toll(busToll, -1.0m),
            Bus b => new Toll(busToll),

            Truck t when t.GrossWeight > 7.5f => new Toll(truckToll, 2.0m),
            Truck t when t.GrossWeight < 3.0f => new Toll(truckToll, -1.0m),
            Truck t => new Toll(truckToll),

            { } => throw new ArgumentException("Unable to get toll for an unknown vehicle type: ", nameof(veh)),
            null => throw new ArgumentNullException(nameof(veh))
        };

    static Vehicle GetRandomVehicle()
    {
        int vehNumber = randNoGenerator.Next(NoOfVehicleTypes);
        var vehType = (VehicleType)vehicleTypes.GetValue(vehNumber);
        return vehType switch
        {
            VehicleType.Car => new Car(randNoGenerator.Next(maxPassengersInCar) + 1),
            VehicleType.Taxi => new Taxi(randNoGenerator.Next(maxPassengersInCar) + 1),
            VehicleType.Bus => new Bus(randNoGenerator.Next(maxPassengersInBus), maxPassengersInBus),
            VehicleType.Truck => new Truck(randNoGenerator.Next(maxTruckGrossWeight)),
            _ => throw new ArgumentException("Unrecognised type of a vehicle.")
        };
    }

    enum VehicleType
    {
        Car,
        Taxi,
        Bus,
        Truck
    }

    record Toll(decimal Price, decimal Discount = 0m);

    abstract class Vehicle
    {
        public abstract string Contents { get; }
    }

    class Car : Vehicle
    {
        public int Passengers { get; }

        public Car(int passengerCount)
        {
            Passengers = passengerCount;
        }

        public override string Contents => $"Passengers: {Passengers}";
    }

    class Truck : Vehicle
    {
        public float GrossWeight { get; }

        public Truck(float grossWeight)
        {
            GrossWeight = grossWeight;
        }

        public override string Contents => $"Gross Weight: {GrossWeight:0.0}t";
    }

    class Taxi : Vehicle
    {
        public int Fares { get; }

        public Taxi(int fares)
        {
            Fares = fares;
        }

        public override string Contents => $"Fares: {Fares}";
    }

    class Bus : Vehicle
    {
        public int Capacity { get; }
        public int Riders { get; }

        public Bus(int riders, int capacity)
        {
            Riders = riders;
            Capacity = capacity;
        }

        public override string Contents => $"Occupied seats: {Riders / (double)Capacity * 100:0}%";
    }
}