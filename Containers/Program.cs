// See https://aka.ms/new-console-template for more information

using Containers;

try
{
    Ship ship1 = new Ship("Titanic", 20, 5, 15);
    Ship ship2 = new Ship("IDK_LOL", 30, 3, 10 );

    RefrigeratedContainer refrigeratedContainer = new RefrigeratedContainer(500, 240, 666, 9934, "bananas", 14);
    refrigeratedContainer.Load(800);

    GasContainer gasContainer = new GasContainer(444, 222, 222, 888, 10);
    gasContainer.Load(700);

    LiquidContainer liquidContainer= new LiquidContainer(300, 300, 300, 999, true);
    liquidContainer.Load(400);
    
    ship1.AddContainer(refrigeratedContainer);
    ship1.AddContainer(liquidContainer);
    ship1.AddContainer(gasContainer);
    
    Console.WriteLine("🔹 Initial state of Ship 1:");
    ship1.PrintInfo();
    
    ship1.UnloadContainer(gasContainer.SerialNumber);
    ship1.PrintInfo();
    
    RefrigeratedContainer refrigeratedContainer2 = new RefrigeratedContainer(550, 250, 260, 1200, "cheese", 8);
    refrigeratedContainer2.Load(900);
    ship1.ReplaceContainer(refrigeratedContainer.SerialNumber, refrigeratedContainer2);
    ship1.PrintInfo();
    
    ship1.TransferContainerToShip(liquidContainer.SerialNumber, ship2);
    ship2.PrintInfo();
    ship1.PrintInfo();

}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}