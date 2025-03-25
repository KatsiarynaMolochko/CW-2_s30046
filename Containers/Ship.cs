using System.ComponentModel;

namespace Containers;

public class Ship
{
    public string Name { get; }
    public double MaxSpeed { get; }
    public int MaxContainerCount { get; }
    public double MaxWeight { get; }

    private List<Container> containers;
    
    public Ship(string name, double maxSpeed, int maxContainerCount, double maxWeight)
    {
        Name = name;
        MaxSpeed = maxSpeed;
        MaxContainerCount = maxContainerCount;
        MaxWeight = maxWeight;
        containers = new List<Container>();
    }
    
    public void AddContainer(Container container)
    {
        if (containers.Count >= MaxContainerCount)
        {
            throw new OverfillException($"[{Name}]  Max number of containers reached.");
        }

        double currentWeightTons = containers.Sum(c => c.EmptyWeight + c.CurrentLoad) / 1000.0;
        double containerWeightTons = (container.EmptyWeight + container.CurrentLoad) / 1000.0;

        if (currentWeightTons + containerWeightTons > MaxWeight)
        {
            throw new OverfillException($"[{Name}]  Too heavy to add container {container.SerialNumber}.");
        }

        containers.Add(container);
        Console.WriteLine($"[{Name}]  Added {container.SerialNumber}.");
    }

    public List<Container> AddContainers(List<Container> сontainersToAdd)
    {
        var notAdded = new List<Container>();
        
        foreach (var c in сontainersToAdd)
        {
            try
            {
                AddContainer(c);
            }
            catch (Exception e)
            {
                notAdded.Add(c);
            }
        }
        
        return notAdded;
    }
    
    public void RemoveContainer(string serialNumber)
    {
        var toRemove = containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (toRemove == null)
        {
            throw new InvalidOperationException($"There isn't container with this serial number{serialNumber}");
        }

        containers.Remove(toRemove);
        Console.WriteLine($"[{Name}] 🗑 Removed {serialNumber}.");
    }
    
    public void TransferContainerToShip(string serialNumber, Ship targetShip)
    {
        var container = containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
        {
            throw new InvalidOperationException($"There isn't container with this serial number{serialNumber}");

        }

        targetShip.AddContainer(container);
        containers.Remove(container);
        
        Console.WriteLine($"[{Name}] Transferred {serialNumber} to {targetShip.Name}");
    }

    public void UnloadContainer(string serialNumber)
    {
        var container = containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
        {
           throw new InvalidOperationException($"Container {serialNumber} not found on ship {Name}.");
        }
        container.Unload();
        Console.WriteLine($"{Name} Unloaded container {serialNumber}.");
    }

    public void ReplaceContainer(String serialNumber, Container newContainer)
    {
        var container = containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
        {
            throw new InvalidOperationException($"There isn't container with this serial number{serialNumber}");
        }

        containers.Remove(container);
        try
        {
            AddContainer(newContainer);
            Console.WriteLine($"{Name} Replaced container {serialNumber} with {newContainer.SerialNumber}");
        }
        catch (Exception e)
        {
            containers.Add(container);
            throw new InvalidOperationException("Failed to replace container: {e.Message}");
        }
    }

    public void PrintInfo()
    {
        Console.WriteLine($"\nShip: {Name} ; MaxSpeed: {MaxSpeed} knots ; MaxWeight: {MaxWeight} tons ; MaxCount: {MaxContainerCount}");
        Console.WriteLine($"Containers loaded: {containers.Count}");
        foreach (var c in containers)
        {
            Console.WriteLine($"  - {c}");
        }
        Console.WriteLine();
    }
    
    
}
