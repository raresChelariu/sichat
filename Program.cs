using System;

namespace EncryptionLib
{
    internal static class Program
    {
        private static void Main()
        {
            #region TESTING XOR ON 2 BLOCKS (WORKS!)
            //var c = new CryptoBlock(null, 20);
            // var b = new CryptoBlock(new byte[]{0, 1, 0, 1});
            // Console.WriteLine(c^b);
            #endregion

            #region TESTING SINGLE BLOCK ENCRYPTION (WORKS!)
            // var a = new CryptoBlock(null, 20);
            // var myKey = new CryptoBlock(null, 7);
            // var encryptedA = a.Encrypt(myKey);
            // var decryptedA = encryptedA.Decrypt(myKey);
            // Console.WriteLine("A           : {0}" + Environment.NewLine, a);
            // Console.WriteLine("Encrypted A : {0}" + Environment.NewLine, encryptedA);
            // Console.WriteLine("Decrypted A : {0}" + Environment.NewLine, decryptedA);
            #endregion

            #region TESTING STRING<->BLOCKLIST CONVERSION (WORKS!)
            // var inputString = "hey this is the input";
            // Console.WriteLine("Input string          : {0}", inputString);
            // var inputBlocks = Stringify.FromStringToBlockList(inputString);
            //
            // Console.WriteLine("Input from block list : {0}", Stringify.FromBlockListToString(inputBlocks));
            #endregion

            #region TESTING ECB ON BLOCKLIST (WORKS!)
            
            const string input = "Ginko biloba, aloha";
            var keyForList = new CryptoBlock(null, 8);
            var inputList = Stringify.FromStringToBlockList(input);
            var encryptedList = BlockLists.EncryptBlockList(inputList, keyForList, BlockLists.EncryptionMode.ECB);
            var decryptedList = BlockLists.DecryptBlockList(encryptedList, keyForList, BlockLists.EncryptionMode.ECB);
            Console.WriteLine("Input : {0}", input);
            Console.WriteLine("Input list: {0}", Stringify.FromBlockListToString(inputList));
            Console.WriteLine("Encrypted list: {0}", Stringify.FromBlockListToString(encryptedList));
            Console.WriteLine("Decrypted string: {0}", Stringify.FromBlockListToString(decryptedList));
            
            #endregion
        }

        // ReSharper disable once UnusedMember.Local
        private static string PrintBytes(byte[] arr)
        {
            if (arr == null || arr.Length == 0)
                throw new ArgumentNullException(nameof(arr));
            var output = "{" + arr[0];
            for (var i = 1; i < arr.Length; i++)
            {
                output += "," + arr[i];
            }
            return output + "}";
        }
    }
}