namespace Containers;

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; }
 
    public LiquidContainer(
        double emptyWeight, 
        double height,
        double depth,
        double maxLoadWeight,
        bool isHazardous)
        : base(ContainerType.Liquid, emptyWeight, height, depth, maxLoadWeight)
    {
        IsHazardous = isHazardous;
    }
    
    public override void Load(double weight)
    {
        double limit = IsHazardous ? MaxLoadWeight * 0.5 : MaxLoadWeight * 0.9;

        if (CurrentLoad + weight > limit)
        {
            NotifyDanger($"[Danger] Container {SerialNumber}: attempt to load  {weight} kg,  which exceeds capacity {limit} kg.");
            throw new OverfillException($"Container {SerialNumber} is overloaded!");
        }
        base.Load(weight);
    }

   
    public void NotifyDanger(string message)
    {
        Console.WriteLine(message);
    }
    
    public override string ToString()
    {
        return $"[{SerialNumber}] Loaded: {CurrentLoad} kg / max: {MaxLoadWeight} kg\n" +
               $"Type of container: Liquid container  / IsHazardous: {IsHazardous}\n"+
               $"Empty weight: {EmptyWeight} kg, Height: {Height} cm, Depth: {Depth} cm";
    }

}