using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MagazineApp
{
    // Handles the application logic
    public class MagazineManager
    {
        private readonly List<Magazine> _magazines;
        private const string FilePath = "magazines.json";

        public MagazineManager()
        {
            _magazines = LoadFromFile();
        }

        public void AddMagazine(Magazine magazine)
        {
            _magazines.Add(magazine);
            SaveToFile();
        }

        public void AddItemToMagazine(string magazineTitle, Item item)
        {
            var magazine = _magazines.Find(m => m.Title.Equals(magazineTitle, StringComparison.OrdinalIgnoreCase));
            if (magazine != null)
            {
                magazine.AddItem(item);
                SaveToFile();
            }
            else
            {
                Console.WriteLine("Magazine not found.");
            }
        }

        public void DisplayMagazines(string style)
        {
            if (style == "Windows 3.1")
            {
                DisplayWindows31Style();
            }
            else if (style == "DOS")
            {
                DisplayDOSStyle();
            }
            else if (style == "Atari BASIC")
            {
                DisplayAtariBasicStyle();
            }
            else
            {
                Console.WriteLine("Invalid style. Defaulting to Windows 3.1 style.");
                DisplayWindows31Style();
            }

            // Pause to allow the user to read the output
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }

        private void DisplayWindows31Style()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray; // Light gray background
            Console.ForegroundColor = ConsoleColor.Black; // Black text
            Console.Clear(); // Apply the background color

            const int boxWidth = 66; // Total width of the box, including borders
            const int contentWidth = boxWidth - 4; // Content width (subtracting 2 borders on each side)

            Console.WriteLine("╔" + new string('═', contentWidth) + "╗");
            Console.WriteLine("║" + "MAGAZINE COLLECTION".PadLeft((contentWidth + "MAGAZINE COLLECTION".Length) / 2).PadRight(contentWidth) + "║");
            Console.WriteLine("╠" + new string('═', contentWidth) + "╣");

            if (_magazines.Count == 0)
            {
                Console.WriteLine("║" + "No magazines available.".PadLeft((contentWidth + "No magazines available.".Length) / 2).PadRight(contentWidth) + "║");
                Console.WriteLine("╚" + new string('═', contentWidth) + "╝");
                Console.ResetColor(); // Reset colors after displaying content
                return;
            }

            foreach (var magazine in _magazines)
            {
                Console.WriteLine("║" + $"Magazine: {magazine.Title}".PadRight(contentWidth) + "║");
                Console.WriteLine("╠" + new string('═', contentWidth) + "╣");

                foreach (var item in magazine.Items)
                {
                    Console.WriteLine("║" + $"  - Name       : {item.Name}".PadRight(contentWidth) + "║");
                    Console.WriteLine("║" + $"    Weight     : {item.Weight}".PadRight(contentWidth) + "║");
                    Console.WriteLine("║" + $"    GoofyLevel : {item.GoofyLevel}".PadRight(contentWidth) + "║");
                    Console.WriteLine("║" + $"    IsExplotano: {item.IsExplotano}".PadRight(contentWidth) + "║");
                    Console.WriteLine("╠" + new string('═', contentWidth) + "╣");
                }
            }

            Console.WriteLine("╚" + new string('═', contentWidth) + "╝");
            Console.ResetColor(); // Reset colors after displaying content
        }

        private void DisplayDOSStyle()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("========================================");
            Console.WriteLine("           MAGAZINE COLLECTION          ");
            Console.WriteLine("========================================");

            if (_magazines.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" No magazines available.");
                Console.WriteLine("========================================");
                Console.ResetColor();
                return;
            }

            foreach (var magazine in _magazines)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" Magazine: {magazine.Title}");
                Console.WriteLine("----------------------------------------");
                foreach (var item in magazine.Items)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"  - Name       : {item.Name}");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"    Weight     : {item.Weight}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"    GoofyLevel : {item.GoofyLevel}");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"    IsExplotano: {item.IsExplotano}");
                    Console.WriteLine("----------------------------------------");
                }
            }

            Console.ResetColor();
            Console.Write("C:\\> ");
            SimulateTyping("Press any key to return to the menu...");
            Console.ReadKey();
        }

        private void DisplayAtariBasicStyle()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue; // Blue background
            Console.ForegroundColor = ConsoleColor.White; // White text
            Console.Clear(); // Apply the background color

            Console.WriteLine("READY.");
            if (_magazines.Count == 0)
            {
                Console.WriteLine("?NO MAGAZINES AVAILABLE ERROR");
                return; // Do not reset colors here to maintain the blue background
            }

            foreach (var magazine in _magazines)
            {
                Console.WriteLine($"MAGAZINE: {magazine.Title}");
                foreach (var item in magazine.Items)
                {
                    Console.WriteLine($"  NAME={item.Name}");
                    Console.WriteLine($"  WEIGHT={item.Weight}");
                    Console.WriteLine($"  GOOFYLEVEL={item.GoofyLevel}");
                    Console.WriteLine($"  ISEXPLOTANO={item.IsExplotano}");
                }
            }

            Console.WriteLine("READY.");
            // Do not reset colors here to maintain the blue background
        }

        private void SaveToFile()
        {
            try
            {
                var json = JsonSerializer.Serialize(_magazines, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        private List<Magazine> LoadFromFile()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var json = File.ReadAllText(FilePath);
                    return JsonSerializer.Deserialize<List<Magazine>>(json) ?? new List<Magazine>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }

            return new List<Magazine>();
        }
        private void SimulateTyping(string text, int delay = 50)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay); // Delay between each character
            }
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
