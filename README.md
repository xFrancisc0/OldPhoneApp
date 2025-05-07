OldPhonePad Text Converter
This application simulates the text input method of old mobile phones, where each numeric button is associated with multiple letters, and pressing the same button multiple times cycles through those letters.
Problem Description
On old mobile phones, users typed text messages using a numeric keypad. Each button was mapped to multiple letters:

1 -> &'(
2 -> ABC
3 -> DEF
4 -> GHI
5 -> JKL
6 -> MNO
7 -> PQRS
8 -> TUV
9 -> WXYZ
0 -> (space)

To input a letter, the user would press the corresponding button one or more times. For example:

Pressing 2 once produces 'A'
Pressing 2 twice in succession produces 'B'
Pressing 2 three times in succession produces 'C'

To input two different characters that are located on the same button, the user must pause between button presses.
For example:

222 2 22 would produce "CAB"

Special characters:

"Asterisc" key functions as a backspace key (deletes the last character)
"Sharp" key marks the end of the input
Spaces are used to pause between inputs for the same button

Algorithm Explanation
The OldPhonePad method converts strings simulating old phone keypad inputs into their corresponding text messages by following these steps:

Validation:

Check if the input is empty or doesn't end with Sharp Key (the end marker)
Remove the Sharp Key from the end of the input


Dictionary Loading:

Load the mapping of numeric keys to letters from a JSON structure
Each numeric key is mapped to an array of characters that can be cycled through


Character Processing:

Track the last digit pressed and how many consecutive times it's been pressed
For each character in the input:

If it's a space: Reset tracking to allow inputting a new letter from the same button
If it's a *: Remove the last character (backspace functionality)
If it's a normal digit: Look up its corresponding letters in the dictionary
Determine which letter to use based on how many consecutive times the button was pressed
Either add a new letter or replace the previous one if it's the same button




Output Generation:

Return the constructed string as the final output



Examples

OldPhonePad("33Sharp Key") → E (Press 3 twice for 'E')
OldPhonePad("227*Sharp Key") → B (Press 2 twice for 'B', press 7 once for 'P', then backspace with '*')
OldPhonePad("4433555 555666Sharp Key") → HELLO (Press 4 twice for 'H', 3 twice for 'E', 5 three times for 'L', etc.)
OldPhonePad("8 8877744466*664Sharp Key") → ????? (Press 8 once for 'T', pause, press 8 twice for 'U', etc.)

File Structure

Program.cs: Contains the main program logic and the OldPhonePad method implementation
Entity/Traductor.cs: Defines entity classes for working with the keypad dictionary
Utils/Methods.cs: Contains utility methods, including JSON loading
Data/Input.cs: Provides the JSON data structure for the keypad mapping

How to Run

Ensure you have .NET SDK installed
Clone the repository
Navigate to the project directory
Run dotnet run

The program will display the loaded dictionary and run several test examples to demonstrate the functionality.
