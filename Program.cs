using OldPhoneApp.Entity;
using OldPhoneApp.Utils;
using System.Collections.Generic;
using System.Text;

namespace OldPhoneApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load dictionary from JSON
            Traductor traductor = Methods.LoadTraductorFromJson();
            TupleClass[] dictionary = traductor.dictionary;

            // Display dictionary for debugging
            printOldPhoneDictionary(dictionary);

            // Test examples from the problem statement
            Console.WriteLine("\nTesting OldPhonePad implementation:");
            Console.WriteLine($"OldPhonePad(\"33#\") => output: {Methods.OldPhone("33#", dictionary)}");
            
            Console.WriteLine($"OldPhonePad(\"227*#\") => " +
                                $"output: {Methods.OldPhone("227*#", dictionary)}");
            
            Console.WriteLine($"OldPhonePad(\"4433555 555666#\") => " +
                                $"output: {Methods.OldPhone("4433555 555666#", dictionary)}");
            
            Console.WriteLine($"OldPhonePad(\"8 8877744466*664#\") => " +
                                $"output: {Methods.OldPhone("8 8877744466*664#", dictionary)}");
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
    }
}