// See https://aka.ms/new-console-template for more information

using System.IO.Compression;
using System.Linq.Expressions;

Console.WriteLine("Hello bitches");

List<Plant> plants = new()
{
                new Plant () { Species = "Rose", LightNeeds = 4, AskingPrice = 15.99, City = "Springfield", ZIP = "12345", Sold = false },
                new Plant () { Species = "Fern", LightNeeds = 2, AskingPrice = 8.50, City = "Greenville", ZIP = "54321", Sold = true },
                new Plant () { Species = "Cactus", LightNeeds = 5, AskingPrice = 12.00, City = "Desertville", ZIP = "67890", Sold = false },
                new Plant () { Species = "Lily", LightNeeds = 3, AskingPrice = 9.99, City = "Meadowville", ZIP = "13579", Sold = false },
                new Plant () { Species = "Maple", LightNeeds = 4, AskingPrice = 25.00, City = "Woodland", ZIP = "24680", Sold = true }
            };
Random random = new Random();

while (true)
{
    Console.WriteLine("Menu:");
    Console.WriteLine("a. Display all plants");
    Console.WriteLine("b. Post a plant for adoption");
    Console.WriteLine("c. Adopt a plant");
    Console.WriteLine("d. Delist a plant");
    Console.WriteLine("e. Random Plant");
    Console.WriteLine("f. Filter and display plants by light needs. ");
    Console.WriteLine("g. Stats for plants. ");
    Console.WriteLine("x. Exit");
    Console.Write("Select an option: ");
    char choice = Console.ReadLine().ToLower()[0];

    switch (choice)
    {
        case 'a':
            DisplayAllPlants(plants);
            break;

        case 'b':
            PostPlantForAdoption(plants);
            break;

        case 'c':
            AdoptPlant(plants);
            break;

        case 'd':
            DelistPlant(plants);
            break;

        case 'e':
            int randomIndex = random.Next(0, plants.Count);
            DisplayRandomPlant(plants, randomIndex);
            break;

        case 'f':
            FilterByLight(plants);
            break;

        case 'g':
            DisplayStats(plants);
            break;

        case 'x': // Changed from 'e' to 'x'
            Console.WriteLine("Exiting the program...");
            return; // Exit the program

        default:
            Console.WriteLine("Invalid option. Please select again.");
            break;
    }
}

static void DisplayStats(List<Plant> plants)
{
    Console.WriteLine("App Statistics");
    Plant lowestPricePlant = plants.OrderBy(plant => plant.AskingPrice).FirstOrDefault();
    Console.WriteLine($"Lowest price plant name: {lowestPricePlant?.Species}");

    DateTime now = DateTime.Now;
    int numberOfAvailablePlants = plants.Count(plant => !plant.Sold && plant.AvailableUntil >= now);
    Console.WriteLine($"Number of Plants Available: {numberOfAvailablePlants}");

    Plant highestLightNeedsPlant = plants.OrderByDescending(plant => plant.LightNeeds).FirstOrDefault();
    Console.WriteLine($"Name of plant with highest light needs: {highestLightNeedsPlant?.Species}");

    double averageLightNeeds = plants.Average(plant => plant.LightNeeds);
    Console.WriteLine($"Average light needs: {averageLightNeeds:F2}");

    int totalPlants = plants.Count;
    int adoptedPlants = plants.Count(plant => plant.Sold);
    double percentageAdopted = (double)adoptedPlants / totalPlants * 100;
    Console.WriteLine($"Percentage of plants adopted: {percentageAdopted:F2}");



}

static void FilterByLight(List<Plant> plants)
{
    List<Plant> availablePlants = plants.Where(plant => !plant.Sold).ToList();

    if (availablePlants.Count == 0)
    {
        Console.WriteLine("No available plants to filter. ");
        return;
    }
    Console.Write("Enter a maximum light needs with (1-5): ");
    if (int.TryParse(Console.ReadLine(), out int maxLightNeeds) && maxLightNeeds >= 1 && maxLightNeeds <= 5)
    {
        List<Plant> filteredPlants = availablePlants.Where(plant => plant.LightNeeds <= maxLightNeeds).ToList();
        Console.WriteLine($"Plants with maximum llight needs of {maxLightNeeds} or lower: ");
        foreach (var plant in filteredPlants)
        {
            Console.WriteLine($"Species: {plant.Species}, light needs: {plant.LightNeeds}");

        }
    }
    else
    {
        Console.WriteLine("Invalid input for maximum light needs. ");

    }
}
static void DisplayRandomPlant(List<Plant> plants, int randomIndex)
{
    List<Plant> availablePlants = plants.Where(plant => !plant.Sold).ToList();

    if (availablePlants.Count == 0)
    {
        Console.WriteLine("No available plants to display. ");
        return;
    }
    if (randomIndex >= 0 && randomIndex < availablePlants.Count)
    {
        Plant randomPlant = availablePlants[randomIndex];
        Console.WriteLine($"Randomly selected plant details:");
        Console.WriteLine($"Species: {randomPlant.Species}");
        Console.WriteLine($"Location: {randomPlant.City}, ZipArchive: {randomPlant.ZIP}");
        Console.WriteLine($"Price: ${randomPlant.AskingPrice}");
    }
    else
    {
        Console.WriteLine("Invalid random index. ");
    }
}
static void DisplayAllPlants(List<Plant> plants)
{

    DateTime now = DateTime.Now;


    Console.WriteLine("Available plants for adoption:");
    foreach (var plant in plants)
    {
        // if (!plant.Sold && plant.AvailableUntil >= now)
        {
            string plantDetails = PlantDetails(plant);
            Console.WriteLine(plantDetails);
        }
    }
}
static string PlantDetails(Plant plant)
{
    string availability = plant.Sold ? "Sold" : "Available";
    string plantString = $"{plant.Species} ({availability}) - Price: ${plant.AskingPrice}";

    return plantString;
}

static void PostPlantForAdoption(List<Plant> plants)
{
    Console.Write("Enter species of the plant: ");
    string species = Console.ReadLine();

    Console.Write("Enter light needs of the plant: ");
    if (!int.TryParse(Console.ReadLine(), out int lightNeeds))
    {
        Console.WriteLine("Invalid input for light needs. Plant posting aborted.");
        return;
    }
    Console.Write("Enter asking price of the plant: ");
    if (!double.TryParse(Console.ReadLine(), out double askingPrice))
    {
        Console.WriteLine("Invalid input for asking price. Plant posting aborted.");
        return;

    }
    Console.Write("Enter city of the plant: ");
    string city = Console.ReadLine();

    Console.Write("Enter Zip code of the plant: ");
    string zip = Console.ReadLine();

    Console.Write("Enter the year the post will expire: ");
    if (!int.TryParse(Console.ReadLine(), out int year))
    {
        Console.WriteLine("Invalid input for year. Plant posting aborted. ");
        return;
    }
    Console.Write("Enter the month the post will expire: ");
    if (!int.TryParse(Console.ReadLine(), out int month))
    {
        Console.WriteLine("Invalid input for month. Plant posting aborted.");
        return;
    }
    Console.Write("Enter the day the post will expire: ");
    if (!int.TryParse(Console.ReadLine(), out int day))
    {
        Console.WriteLine("Invalid input for day. Plant posting aborted. ");
        return;
    }
    try
    {
        DateTime availableUntil = new DateTime(year, month, day);
        plants.Add(new Plant
        {
            Species = species,
            LightNeeds = lightNeeds,
            AskingPrice = askingPrice,
            City = city,
            ZIP = zip,
            Sold = false,
            AvailableUntil = availableUntil

        });

        Console.WriteLine($"{species} has been posted for adoption.");
    }
    catch (ArgumentOutOfRangeException)
    {
        Console.WriteLine("Invalid date. Plant posting aborted.");
    }


}

static void AdoptPlant(List<Plant> plants)
{
    List<Plant> availablePlants = plants.Where(plant => !plant.Sold).ToList();

    if (availablePlants.Count == 0)
    {
        Console.WriteLine("No available plants for adoption.");
        return;
    }

    Console.WriteLine("Available plants for adoption:");
    for (int i = 0; i < availablePlants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {availablePlants[i].Species} - Price: ${availablePlants[i].AskingPrice}");
    }

    Console.Write("Enter the index of the plant to adopt: ");
    if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= availablePlants.Count)
    {
        Plant chosenPlant = availablePlants[selectedIndex - 1];
        chosenPlant.Sold = true;
        Console.WriteLine($"{chosenPlant.Species} has been adopted!");
    }
    else
    {
        Console.WriteLine("Invalid index.");
    }
}

static void DelistPlant(List<Plant> plants)
{
    DisplayAllPlants(plants);

    Console.Write("Enter the index of the plant to delist: ");
    if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < plants.Count)
    {
        plants.RemoveAt(index);
        Console.WriteLine("Plant has been delisted.");
    }
    else
    {
        Console.WriteLine("Invalid index.");
    }
}




