namespace Containers;

public class RefrigeratedContainer : Container
{
    public double Temperature { get; }
    public string ProductType { get; }


    public RefrigeratedContainer(
        double emptyWeight,
        double height,
        double depth,
        double maxLoadWeight,
        string productType,
        double temperature)
        : base(ContainerType.Refrigerated, emptyWeight, height, depth, maxLoadWeight)
    {
        ProductType = productType.ToLower();
        Temperature = temperature;
        
        if (!ProductMinTemperatures.ContainsKey(ProductType))
            throw new ArgumentException($"Unknown product type: {ProductType}");

        double requiredTemp = ProductMinTemperatures[ProductType];
        if (Temperature < requiredTemp)
            throw new ArgumentException($"Temperature too low for {ProductType}. Required: ≥ {requiredTemp}°C");
    }
    
    private static Dictionary<string, double> ProductMinTemperatures = new()
    {
        { "bananas", 13.3 },
        { "chocolate", 18 },
        { "fish", 2 },
        { "meat", -15 },
        { "ice cream", -18 },
        { "frozen pizza", -30 },
        { "cheese", 7.2 },
        { "sausages", 5 },
        { "butter", 20.5 },
        { "eggs", 19 },
    };
    
    public static void AddProductType(string product, double requiredTemperature)
    {
        ProductMinTemperatures[product.ToLower()] = requiredTemperature;
    }

    public override string ToString()
    {
        return $"[{SerialNumber}] Loaded: {CurrentLoad} kg / max: {MaxLoadWeight} kg\n" +
               $"Type of container: Refrigerated container  / Product type: {ProductType} / Temperature: {Temperature} C\n"+
               $"Empty weight: {EmptyWeight} kg, Height: {Height} cm, Depth: {Depth} cm";
    }
}