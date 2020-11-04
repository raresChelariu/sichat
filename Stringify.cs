using System;
using System.Collections.Generic;
using System.Linq;

namespace EncryptionLib
{
    public class Stringify
    {
        private const int BlockSize = Constants.BlockSize;
        
        public static LinkedList<CryptoBlock> FromStringToBlockList(string input)
        {
            var result = new LinkedList<CryptoBlock>();
            int index;
            for (index = 0; index + BlockSize < input.Length; index += BlockSize)
            {
                // every char is a byte, so every #BlockSize chars are 1 block (of bytes)
                string currentCharBlock = input.Substring(index, BlockSize);
                result.AddLast(new CryptoBlock(Stringify.FromStringToByteArray(currentCharBlock)));
            }
            
            // last block may have less than #BlockSize bytes 
            var inputLastBlock = input.Substring(index);
            // a CryptoBlock always has #BlockSize bytes due to padding
            result.AddLast(new CryptoBlock(FromStringToByteArray(inputLastBlock)));
           
            return result;
        }

        public static string FromBlockListToString(LinkedList<CryptoBlock> blockList)
        {
            var result = string.Empty;
            foreach (var block in blockList)
            {
                foreach (var currentByte in block.V)
                {
                    result += Convert.ToChar(currentByte);
                }
            }
            return result;
        }
        public static byte[] FromStringToByteArray(string input)
        {
            var output = new byte[input.Length];
            
            for (var i = 0; i < input.Length; i++)
                output[i] = (byte) input[i];
            return output;
        }

    
    }
}