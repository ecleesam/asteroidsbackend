using asteroidsbackend.Services;

namespace asteroidsbackend.Menu;

public static class ItemMenu
{
    public static void Start()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===ITEM MANAGEMENT===");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. View All Items");
            Console.WriteLine("3. Update Item");
            Console.WriteLine("4. Delete Item");
            Console.WriteLine("5. Return to Main Menu");
            Console.Write("\nEnter choice: ");

            switch (Console.ReadLine())
            {
                case "1": ItemService.AddItem(); break;
                case "2": ItemService.ViewItems(); break;
                case "3": ItemService.UpdateItem(); break;
                case "4": ItemService.DeleteItem(); break;
                case "5": return;
                default:
                    Console.WriteLine("Invalid choice.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
