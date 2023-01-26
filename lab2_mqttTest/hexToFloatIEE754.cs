using lab2_mqtt_CS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab2_mqtHexToAscTest
{
    [TestClass]
    public class hexToFloatIEE754
    {

        [TestMethod]
        public void FloatIEE754_ShouldConvertValidValues_WhenDecodingInput()

        {
            // Arrange
            var input = new byte[] { 0x00, 0x00, 0x80, 0x3F }; // represents the value 1.0 in IEEE 754 format
            var expectedOutput = 1.0f;

            // Act
            var result = Converter.FloatIEE754(input);

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }


        [TestMethod]
        public void FloatIEE754_ShouldThrowException_WhenUsingBadInpu()
        {


            byte[] input1 = null;
            byte[] input2 = new byte[3];
            byte[] input3 = new byte[5];

            // Act and Assert
            Assert.ThrowsException<NullReferenceException>(() => Converter.FloatIEE754(input1));
            Assert.ThrowsException<Exception>(() => Converter.FloatIEE754(input2));
            Assert.ThrowsException<Exception>(() => Converter.FloatIEE754(input3));

        }

        [TestMethod]
        public void ByteSwapX4_ShouldConvertValidValues_WhenDecodingInput()
       {

            var input = new byte[] {0x00, 0x01, 0x02, 0x03};

            var expectedOutput = new byte[] { 0x03, 0x02, 0x01, 0x00 };
            // Act
            var result = Converter.ByteSwapX4(input);
          

            // Assert
            CollectionAssert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void ByteSwapX4_ShouldThrowException_WhenUsingBadInput()

        {
            byte[] input1 = null;
            byte[] input2 = new byte[3];
            byte[] input3 = new byte[5];

            // Act and Assert
            Assert.ThrowsException<NullReferenceException>(() => Converter.ByteSwapX4(input1));
            Assert.ThrowsException<Exception>(() => Converter.ByteSwapX4(input2));
            Assert.ThrowsException<Exception>(() => Converter.ByteSwapX4(input3));
        }
     




    }
}