using System.Collections.Generic;

namespace MagazineApp
{
    // Represents a magazine
    public class Magazine
    {
        public string Title { get; set; }
        public List<Item> Items { get; set; }
        public int MaxItems { get; set; }
        public double MaxWeight { get; set; }

        public Magazine(string title, int maxItems, double maxWeight)
        {
            Title = title;
            MaxItems = maxItems;
            MaxWeight = maxWeight;
            Items = new List<Item>();
        }

        public bool AddItem(Item item)
        {
            if (Items.Count >= MaxItems)
            {
                return false; // Cannot add item, maximum item capacity reached
            }

            double currentWeight = 0;
            foreach (var existingItem in Items)
            {
                currentWeight += existingItem.Weight;
            }

            if (currentWeight + item.Weight > MaxWeight)
            {
                return false; // Cannot add item, maximum weight capacity exceeded
            }

            Items.Add(item);
            return true; // Item added successfully
        }

        public void Display(string style)
        {
            if (style.Equals("Windows 3.1", StringComparison.OrdinalIgnoreCase))
            {
                DisplayWindows31Style();
            }
            else if (style.Equals("DOS", StringComparison.OrdinalIgnoreCase))
            {
                DisplayDOSStyle();
            }
            else if (style.Equals("Atari BASIC", StringComparison.OrdinalIgnoreCase))
            {
                DisplayAtariBasicStyle();
            }
            else
            {
                Console.WriteLine("Invalid style. Defaulting to Windows 3.1 style.");
                DisplayWindows31Style();
            }
        }

        private void DisplayWindows31Style()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray; // Light gray background
            Console.ForegroundColor = ConsoleColor.Black; // Black text
            Console.Clear(); // Apply the background color

            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      MAGAZINE COLLECTION                     ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");

            if (Items.Count == 0)
            {
                Console.WriteLine("║                      No items available.                     ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                Console.ResetColor(); // Reset colors after displaying content
                return;
            }

            foreach (var item in Items)
            {
                Console.WriteLine($"║  - Name       : {item.Name.PadRight(50)} ║");
                Console.WriteLine($"║    Weight     : {item.Weight.ToString().PadRight(50)} ║");
                Console.WriteLine($"║    GoofyLevel : {item.GoofyLevel.ToString().PadRight(50)} ║");
                Console.WriteLine($"║    IsExplotano: {item.IsExplotano.ToString().PadRight(50)} ║");
                Console.WriteLine("╠══════════════════════════════════════════════════════════════╣");
            }

            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.ResetColor(); // Reset colors after displaying content
        }

        private void DisplayDOSStyle()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("========================================");
            Console.WriteLine(" MAGAZINE COLLECTION ");
            Console.WriteLine("========================================");

            if (Items.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" No items available.");
                Console.WriteLine("========================================");
                Console.ResetColor();
                return;
            }

            foreach (var item in Items)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"  - Name       : {item.Name}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"    Weight     : {item.Weight}");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"    GoofyLevel : {item.GoofyLevel}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"    IsExplotano: {item.IsExplotano}");
                Console.WriteLine("----------------------------------------");
            }

            Console.ResetColor();
        }

        private void DisplayAtariBasicStyle()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue; // Blue background
            Console.ForegroundColor = ConsoleColor.White; // White text
            Console.Clear(); // Apply the background color

            Console.WriteLine("READY");

            if (Items.Count == 0)
            {
                Console.WriteLine("?NO ITEMS AVAILABLE ERROR");
                return; // Do not reset colors here to maintain the blue background
            }

            foreach (var item in Items)
            {
                Console.WriteLine($"  NAME={item.Name}");
                Console.WriteLine($"  WEIGHT={item.Weight}");
                Console.WriteLine($"  GOOFYLEVEL={item.GoofyLevel}");
                Console.WriteLine($"  ISEXPLOTANO={item.IsExplotano}");
            }

            Console.WriteLine("READY.");
            // Do not reset colors here to maintain the blue background
        }
        private string MaintainBackgroundWhileTyping()
        {
            Console.BackgroundColor = ConsoleColor.Blue; // Ensure the background remains blue
            Console.ForegroundColor = ConsoleColor.White; // Ensure the text remains white

            string input = "";
            ConsoleKeyInfo key;

            while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter) // Read until Enter is pressed
            {
                if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    // Handle backspace
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b"); // Move cursor back, overwrite with space, and move back again
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar); // Display the character
                }
            }

            Console.WriteLine(); // Move to the next line after Enter is pressed
            return input.Trim(); // Return the trimmed input
        }
    }
}
