namespace Containers;

public enum ContainerType
{
    Liquid,       
    Gas,          
    Refrigerated 
}

public abstract class Container
{
    private static int counter = 0; 
    
    public string SerialNumber { get; }
    public ContainerType Type { get; }
    public double EmptyWeight { get; }
    public double Height { get; }
    public double Depth { get; }
    public double MaxLoadWeight { get; }
    public double CurrentLoad { get; protected set; }

    protected Container(ContainerType type, double emptyWeight, double height, double depth, double maxLoadWeight)
    {
        SerialNumber =  $"KON-{GetTypeCode((type))}-{++counter}";
        EmptyWeight = emptyWeight;
        Height = height;
        Depth = depth;
        MaxLoadWeight = maxLoadWeight;
        CurrentLoad = 0;
    }
    
    
    private string GetTypeCode(ContainerType type)
    {
        return type switch
        {
            ContainerType.Liquid => "L",
            ContainerType.Gas => "G",
            ContainerType.Refrigerated => "C",
            _ => "X" 
        };
    }

    public virtual void Load(double weight)
    {
        if (CurrentLoad + weight > MaxLoadWeight)
            throw new OverfillException($"Container {SerialNumber} is overloaded!");
        CurrentLoad += weight;
    }
    
    public virtual void Unload()
    {
        CurrentLoad = 0;
    }
    
    public override string ToString()
    {
        return $"[{SerialNumber}] Loaded: {CurrentLoad} kg / max: {MaxLoadWeight} kg\n" +
               $"Empty weight: {EmptyWeight} kg, Height: {Height} cm, Depth: {Depth} cm";
    }
}