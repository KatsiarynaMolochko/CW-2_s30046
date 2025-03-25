namespace Containers;

public class GasContainer : Container, IHazardNotifier
{
    public Double Pressure { get; }
    public GasContainer(
        double emptyWeight, 
        double height, 
        double depth, 
        double maxLoadWeight,
        double pressure)
        : base(ContainerType.Gas, emptyWeight, height, depth, maxLoadWeight)
    {
        Pressure = pressure;
    }

    public void NotifyDanger(string message)
    {
        Console.WriteLine(message);
    }


    public override void Load(double weight)
    {
        if (CurrentLoad + weight > MaxLoadWeight)
        {
            NotifyDanger($"[Danger] Container {SerialNumber}: attempt to load {weight} kg, which exceeds capacity.");
            throw new OverfillException($"Container {SerialNumber} is overloaded!");
        }

        base.Load(weight);
    }

    public override void Unload()
    {
        CurrentLoad *= 0.05;
    }

    public override string ToString()
    {
        return $"[{SerialNumber}] Loaded: {CurrentLoad} kg / max: {MaxLoadWeight} kg\n" +
               $"Type of container: Gas container  / Pressure: {Pressure} atm.\n"+
               $"Empty weight: {EmptyWeight} kg, Height: {Height} cm, Depth: {Depth} cm";
    }
}