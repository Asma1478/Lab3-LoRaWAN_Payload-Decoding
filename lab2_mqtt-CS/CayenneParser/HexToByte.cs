namespace Parser
{
    using System;

    internal static class HexToByte
    {
        private static readonly byte[] Empty = Array.Empty<byte>();

        public static byte[] ConvertToByteArray(string value)
        {
            byte[] bytes;
            if (string.IsNullOrEmpty(value))
            {
                bytes = Empty;
            }
            else
            {
                int stringLength = value.Length;
                int characterIndex = (value.StartsWith("0x", StringComparison.Ordinal)) ? 2 : 0; // Does the string define leading HEX indicator '0x'. Adjust starting index accordingly.               
                int numberOfCharacters = stringLength - characterIndex;

                var addLeadingZero = false;
                if (0 != (numberOfCharacters % 2))
                {
                    addLeadingZero = true;

                    numberOfCharacters += 1; // Leading '0' has been striped from the string presentation.
                }

                bytes = new byte[numberOfCharacters / 2]; // Initialize our byte array to hold the converted string.

                var writeIndex = 0;
                if (addLeadingZero)
                {
                    bytes[writeIndex++] = FromCharacterToByte(value[characterIndex], characterIndex);
                    characterIndex += 1;
                }

                for (int readIndex = characterIndex; readIndex < value.Length; readIndex += 2)
                {
                    byte upper = FromCharacterToByte(value[readIndex], readIndex, 4);
                    byte lower = FromCharacterToByte(value[readIndex + 1], readIndex + 1);

                    bytes[writeIndex++] = (byte) (upper | lower);
                }
            }

            return bytes;
        }

        private static byte FromCharacterToByte(char character, int index, int shift = 0)
        {
            var value = (byte) character;
            if (((0x40 < value) && (0x47 > value)) || ((0x60 < value) && (0x67 > value)))
            {
                if (0x40 == (0x40 & value))
                {
                    if (0x20 == (0x20 & value))
                    {
                        value = (byte) (((value + 0xA) - 0x61) << shift);
                    }
                    else
                    {
                        value = (byte) (((value + 0xA) - 0x41) << shift);
                    }
                }
            }
            else if ((0x29 < value) && (0x40 > value))
            {
                value = (byte) ((value - 0x30) << shift);
            }
            else
            {
                throw new InvalidOperationException($"Character '{character}' at index '{index}' is not valid alphanumeric character.");
            }

            return value;
        }
    }
}