using OldPhoneApp.Entity;
using OldPhoneApp.Data;
using System.Text.Json;
using System.Text;
namespace OldPhoneApp.Utils
{
    class Methods
    {
        /// <summary>
        //We use this static method to load json input from Data/Input.cs
        /// </summary>
        public static Traductor LoadTraductorFromJson()
        {
            string jsonInput = Input.generatePrototypeJsonString();
            Traductor traductor = JsonSerializer.Deserialize<Traductor>(jsonInput)!;
            return traductor;
        }

        /// <summary>
        /// Converts an input string to the corresponding output using old phone keypad logic
        /// </summary>
        public static void printOldPhoneDictionary(TupleClass[] dictionary)
        {
            foreach (var ele in dictionary)
            {
                Console.WriteLine($"===================");
                Console.WriteLine($"Ele.key: {ele.key}");
                foreach (var eq in ele.values)
                {
                    Console.WriteLine($"Ele.value: {eq}");
                }
                Console.WriteLine($"===================");
            }
        }

        /// <summary>
        /// Converts an input string to the corresponding output using old phone keypad logic
        /// </summary>
        public static string OldPhone(string? input, TupleClass[] dictionary)
        {
            if (string.IsNullOrEmpty(input) || !input.EndsWith("#"))
                return "?????";

            // Remove the terminating #
            input = input.Substring(0, input.Length - 1);

            StringBuilder result = new StringBuilder();
            char? lastDigit = null;
            int consecutiveCount = 0;

            foreach (char c in input)
            {
                if (c == ' ')
                {
                    // A space allows for a new letter from the same button
                    lastDigit = null;
                    consecutiveCount = 0;
                    continue;
                }
                else if (c == '*')
                {
                    // Backspace - remove the last character if there is one
                    if (result.Length > 0)
                        result.Remove(result.Length - 1, 1);
                    continue;
                }
                else if (c == '#')
                {
                    // End of message
                    break;
                }

                // Find the dictionary entry for this key
                TupleClass? keyEntry = dictionary.FirstOrDefault(entry => entry.key == c.ToString());

                if (keyEntry == null)
                {
                    // If key not found in dictionary, skip it
                    continue;
                }

                if (c.ToString() == lastDigit?.ToString())
                {
                    // Same button pressed again
                    consecutiveCount++;
                }
                else
                {
                    // Different button pressed
                    lastDigit = c;
                    consecutiveCount = 0;
                }

                // Get the values (letters) for this key
                List<string> values = keyEntry.values;

                // Calculate which letter in the sequence to use (cycling through available letters)
                int letterIndex = consecutiveCount % values.Count;

                // If this is a different button than before, add the letter
                if (consecutiveCount == 0)
                {
                    result.Append(values[letterIndex]);
                }
                // If this is the same button, replace the last letter
                else if (result.Length > 0)
                {
                    result.Remove(result.Length - 1, 1);
                    result.Append(values[letterIndex]);
                }
            }

            return result.ToString();
        }
    }
}