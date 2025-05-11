using System;
using MagazineApp;

namespace MagazineApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new MagazineManager();

            Console.WriteLine("Choose application style:");
            Console.WriteLine("1. Windows 3.1");
            Console.WriteLine("2. DOS");
            Console.WriteLine("3. Atari BASIC");
            Console.Write("Enter your choice: ");
            var styleChoice = Console.ReadLine();

            string appStyle = styleChoice switch
            {
                "1" => "Windows 3.1",
                "2" => "DOS",
                "3" => "Atari BASIC",
                _ => "Windows 3.1" // Default to Windows 3.1
            };

            while (true)
            {
                DisplayMenu(appStyle);

                var choice = appStyle == "Atari BASIC" ? MaintainBackgroundWhileTyping() : Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter magazine title: ");
                        var title = Console.ReadLine();
                        Console.Write("Enter maximum number of items: ");
                        if (int.TryParse(Console.ReadLine(), out var maxItems))
                        {
                            Console.Write("Enter maximum weight: ");
                            if (double.TryParse(Console.ReadLine(), out var maxWeight))
                            {
                                manager.AddMagazine(new Magazine(title, maxItems, maxWeight));
                            }
                            else
                            {
                                DisplayMessage("Invalid weight.", appStyle);
                            }
                        }
                        else
                        {
                            DisplayMessage("Invalid number of items.", appStyle);
                        }
                        break;

                    case "2":
                        Console.Write("Enter magazine title: ");
                        var magazineTitle = Console.ReadLine();
                        Console.Write("Enter item name: ");
                        var itemName = Console.ReadLine();
                        Console.Write("Enter item weight: ");
                        if (double.TryParse(Console.ReadLine(), out var weight))
                        {
                            Console.Write("Enter item GoofyLevel: ");
                            if (int.TryParse(Console.ReadLine(), out var goofyLevel))
                            {
                                Console.Write("Is the item Explotano (true/false): ");
                                if (bool.TryParse(Console.ReadLine(), out var isExplotano))
                                {
                                    manager.AddItemToMagazine(magazineTitle, new Item(itemName, weight, goofyLevel, isExplotano));
                                }
                                else
                                {
                                    DisplayMessage("Invalid value for IsExplotano.", appStyle);
                                }
                            }
                            else
                            {
                                DisplayMessage("Invalid GoofyLevel.", appStyle);
                            }
                        }
                        else
                        {
                            DisplayMessage("Invalid weight.", appStyle);
                        }
                        break;

                    case "3":
                        manager.DisplayMagazines(appStyle);
                        break;

                    case "4":
                        return;

                    default:
                        DisplayMessage("Invalid option.", appStyle);
                        break;
                }
            }
        }

        static void DisplayMenu(string style)
        {
            if (style == "Windows 3.1")
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Gray; // Light gray background
                Console.ForegroundColor = ConsoleColor.Black; // Black text
                Console.Clear(); // Apply the background color

                Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                   MAGAZINE MANAGER                     ║");
                Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                Console.WriteLine("║  1. Add Magazine                                       ║");
                Console.WriteLine("║  2. Add Item to Magazine                               ║");
                Console.WriteLine("║  3. Display Magazines                                  ║");
                Console.WriteLine("║  4. Exit                                               ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                Console.Write("Choose an option: ");
                Console.ResetColor(); // Reset colors after displaying content
            }
            else if (style == "DOS")
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("========================================");
                Console.WriteLine("           MAGAZINE MANAGER             ");
                Console.WriteLine("========================================");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. Add Magazine");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("2. Add Item to Magazine");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("3. Display Magazines");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("4. Exit");
                Console.WriteLine("========================================");

                Console.ResetColor();
                Console.Write("C:\\> ");
                SimulateTyping("Choose an option: ");


            }
            else if (style == "Atari BASIC")
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Blue; // Blue background
                Console.ForegroundColor = ConsoleColor.White; // White text
                Console.Clear(); // Apply the background color

                Console.WriteLine("READY.");
                Console.WriteLine("10 PRINT \"1. Add Magazine\"");
                Console.WriteLine("20 PRINT \"2. Add Item to Magazine\"");
                Console.WriteLine("30 PRINT \"3. Display Magazines\"");
                Console.WriteLine("40 PRINT \"4. Exit\"");
                Console.Write("INPUT: ");
            }

        }

        static void DisplayMessage(string message, string style)
        {
            if (style == "Windows 3.1")
            {
                Console.BackgroundColor = ConsoleColor.Gray; // Light gray background
                Console.ForegroundColor = ConsoleColor.Black; // Black text
                Console.Clear(); // Apply the background color

                Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                Console.WriteLine($"║ {message.PadRight(54)} ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                Console.ResetColor(); // Reset colors after displaying content
            }
            else if (style == "DOS")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($" {message} ");
                Console.ResetColor();
            }
            else if (style == "Atari BASIC")
            {
                Console.BackgroundColor = ConsoleColor.Blue; // Blue background
                Console.ForegroundColor = ConsoleColor.White; // White text
                Console.Clear(); // Apply the background color

                Console.WriteLine($"?{message}");
                Console.ResetColor();
            }

        }
        static void SimulateTyping(string text, int delay = 50)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay); // Delay between each character
            }
        }
        static string MaintainBackgroundWhileTyping()
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
