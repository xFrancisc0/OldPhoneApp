using NUnit.Framework;
using OldPhoneApp.Entity;
using OldPhoneApp.Utils;

namespace OldPhoneApp.Tests
{
    [TestFixture]
    public class OldPhoneTests
    {
        private TupleClass[] _dictionary = [];

        [SetUp]
        public void Setup()
        {
            // Load dictionary from JSON using the application's method
            Traductor traductor = Methods.LoadTraductorFromJson();
            _dictionary = traductor.dictionary;
        }

        [Test]
        public void LoadTraductorFromJson_ReturnsValidDictionary()
        {
            // Act
            Traductor traductor = Methods.LoadTraductorFromJson();

            // Assert
            Assert.That(traductor, Is.Not.Null);
            Assert.That(traductor.dictionary, Is.Not.Null);
            Assert.That(traductor.dictionary.Length, Is.GreaterThan(0));

            // Verify specific elements from Input.cs
            Assert.That(traductor.dictionary[1].key, Is.EqualTo("2"));
            Assert.That(traductor.dictionary[1].values.Count, Is.EqualTo(3));
            Assert.That(traductor.dictionary[1].values[0], Is.EqualTo("A"));
            Assert.That(traductor.dictionary[1].values[1], Is.EqualTo("B"));
            Assert.That(traductor.dictionary[1].values[2], Is.EqualTo("C"));
        }

        [Test]
        public void OldPhone_NullInput_ReturnsError()
        {
            // Arrange
            string? input = null;

            // Act
            string result = Methods.OldPhone(input, _dictionary);

            // Assert
            Assert.That(result, Is.EqualTo("?????"));
        }

        [Test]
        public void OldPhone_EmptyInput_ReturnsError()
        {
            // Arrange
            string input = "";

            // Act
            string result = Methods.OldPhone(input, _dictionary);

            // Assert
            Assert.That(result, Is.EqualTo("?????"));
        }

        [Test]
        public void OldPhone_InputWithoutHashTerminator_ReturnsError()
        {
            // Arrange
            string input = "123";

            // Act
            string result = Methods.OldPhone(input, _dictionary);

            // Assert
            Assert.That(result, Is.EqualTo("?????"));
        }

        [Test]
        public void OldPhone_SingleCharacter_ReturnsCorrectLetter()
        {
            // Arrange
            string input = "2#";

            // Act
            string result = Methods.OldPhone(input, _dictionary);

            // Assert
            Assert.That(result, Is.EqualTo("A"));
        }

        [Test]
        public void OldPhone_RepeatedKey_CyclesThroughLetters()
        {
            // Arrange
            string input = "2222#"; // Pressing '2' four times should give 'A' -> 'B' -> 'C' -> 'A'

            // Act
            string result = Methods.OldPhone(input, _dictionary);

            // Assert
            Assert.That(result, Is.EqualTo("A"));
        }

        [Test]
        public void OldPhone_DoublePress_ReturnsSecondLetter()
        {
            // Arrange
            string input = "22#";

            // Act
            string result = Methods.OldPhone(input, _dictionary);

            // Assert
            Assert.That(result, Is.EqualTo("B"));
        }

        [Test]
        public void OldPhone_TriplePress_ReturnsThirdLetter()
        {
            // Arrange
            string input = "222#";

            // Act
            string result = Methods.OldPhone(input, _dictionary);

            // Assert
            Assert.That(result, Is.EqualTo("C"));
        }

        [Test]
        public void OldPhone_SpaceAllowsNewLetterFromSameButton()
        {
            // Arrange
            string input = "2 2#";

            // Act
            string result = Methods.OldPhone(input, _dictionary);

            // Assert
            Assert.That(result, Is.EqualTo("AA"));
        }

        [Test]
        public void OldPhone_BackspaceDeletesLastCharacter()
        {
            // Arrange
            string input = "27*#";

            // Act
            string result = Methods.OldPhone(input, _dictionary);

            // Assert
            Assert.That(result, Is.EqualTo("A"));
        }

        [Test]
        public void OldPhone_MultipleBackspaces_DeletesMultipleCharacters()
        {
            // Arrange
            string input = "234***#";

            // Act
            string result = Methods.OldPhone(input, _dictionary);

            // Assert
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void OldPhone_BackspaceOnEmptyString_HandledGracefully()
        {
            // Arrange
            string input = "*#";

            // Act
            string result = Methods.OldPhone(input, _dictionary);

            // Assert
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void OldPhone_ComplexInput_ReturnsCorrectString()
        {
            // Arrange
            string input = "4433555 555666#";

            // Act
            string result = Methods.OldPhone(input, _dictionary);

            // Assert
            Assert.That(result, Is.EqualTo("HELLO"));
        }

        [Test]
        public void OldPhone_AnotherComplexInput_ReturnsCorrectString()
        {
            // Arrange
            string input = "8 8877744466*664#";

            // Act
            string result = Methods.OldPhone(input, _dictionary);

            // Assert
            Assert.That(result, Is.EqualTo("TURING"));
        }

        [Test]
        public void OldPhone_KeyNotInDictionary_IgnoresKey()
        {
            // Arrange
            string input = "2A3#"; // 'A' is not in the dictionary as a key

            // Act
            string result = Methods.OldPhone(input, _dictionary);

            // Assert
            Assert.That(result, Is.EqualTo("AD"));
        }

        [Test]
        public void OldPhone_ProgramExamples_ReturnsExpectedOutput()
        {
            // Arrange & Act & Assert
            // Testing the examples from Program.cs
            Assert.That(Methods.OldPhone("33#", _dictionary), Is.EqualTo("E"));
            Assert.That(Methods.OldPhone("227*#", _dictionary), Is.EqualTo("B"));
            Assert.That(Methods.OldPhone("4433555 555666#", _dictionary), Is.EqualTo("HELLO"));
            Assert.That(Methods.OldPhone("8 8877744466*664#", _dictionary), Is.EqualTo("TURING"));
        }
    }
}