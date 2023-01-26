using lab2_mqtt_CS; 

namespace lab2_mqtHexToAscTest
{
    [TestClass]
    public class HexToAscConveTest
    {

        [TestMethod]
        [DataRow("54-65-6D-70-65-72-61-74-75-72-65-20-3D-20-31-35-2E-38")]
        [DataRow("54-65-6D 70-65-72-61-74-75-72-65..20-3D-20-31-35-2E-38")]
        [DataRow("54656D7065726174757265203D2031352E38")]
        [DataRow("54656D7065726174/75-72/65..20-3D-20-31-35-2E-38")]
        public void HexToAscii_ShouldConvertValidValues_WhenDecodingInput(string hexS)

        {

            var actualValue = Converter.ConvertHexToAscii(hexS);

            var expectedValue = "Temperature = 15.8";

            Assert.AreEqual(expectedValue, actualValue);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void HexToAscii_ShouldThrowException_WhenDataIsEmpty()
        {
            string Empptyhex = " ";

            string ascii = Converter.ConvertHexToAscii(Empptyhex);


        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void HexToAscii_ShouldThrowException_WhenUsingBadInput()

        {
            string badInput = "45678";
            string ascii = Converter.ConvertHexToAscii(badInput);


        }



    }
}