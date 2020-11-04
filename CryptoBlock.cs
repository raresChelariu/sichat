using System;
using System.Security.Cryptography;

namespace EncryptionLib
{
    public class CryptoBlock
    {
        private const int BlockSize = Constants.BlockSize;
        private const int DefaultPadding = Constants.DefaultPadding;
        private const int ByteSize = Constants.ByteSize;
        
        public byte[] V;
        public CryptoBlock(byte[] input, byte padding = DefaultPadding)
        {
            V = new byte[BlockSize];
            if (input == null)
            {
                for (var i = 0; i < BlockSize; i++)
                    V[i] = padding;
                return;
            }
            for (var i = 0; i < input.Length; i++)
                V[i] = input[i];
            for (var i= input.Length; i < BlockSize; i++)
                    V[i] = padding;
        }
        
        public static CryptoBlock operator ^(CryptoBlock a, CryptoBlock b)
        {
            var resultedArray = new byte[16];
            for (var i = 0; i < BlockSize; i++)
            {
                resultedArray[i] = (byte) (a.V[i] ^ b.V[i]);
            }
            return new CryptoBlock(resultedArray);
        }

        public CryptoBlock Encrypt(CryptoBlock key)
        {
            if (key?.V == null)
                throw new ArgumentNullException(nameof(key));
            return new CryptoBlock(AES_encrypt_block(this.V, key.V));
        }


        public CryptoBlock Decrypt(CryptoBlock key)
        {
            if (key?.V == null)
                throw new ArgumentNullException(nameof(key));
            return new CryptoBlock(AES_Decrypt_block(this.V, key.V));
        }
        private static byte[] AES_encrypt_block(byte[] plainText, byte[] key)
        {
            var outputBuffer = new byte[plainText.Length];
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Mode = CipherMode.ECB;

                aesAlg.BlockSize = 128;
                aesAlg.KeySize = 128;
                aesAlg.Padding = PaddingMode.None;
                aesAlg.Key = key;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                encryptor.TransformBlock(plainText, 0, plainText.Length, outputBuffer, 0);
            }

            return outputBuffer;
        }

        private static byte[] AES_Decrypt_block(byte[] cipherText, byte[] key)
        {
            var outputBuffer = new byte[cipherText.Length];

            using (AesManaged aesAlg = new AesManaged())
            {
                
                aesAlg.Mode = CipherMode.ECB;

                aesAlg.BlockSize = 128;
                aesAlg.KeySize = 128;
                aesAlg.Padding = PaddingMode.None;
                aesAlg.Key = key;
                
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                decryptor.TransformBlock(cipherText, 0, cipherText.Length, outputBuffer, 0);
            }
            return outputBuffer;
        }
        public override string ToString()
        {
            if (V == null || V.Length == 0)
                return "Empty block";
            string output = "{" + V[0] + " ";
            for (var i = 1; i < V.Length; i++)
            {
                output += ", " + V[i];
            }
            return output + "}";
        }

        public string ToHexString()
        {
            var output = string.Empty;
            if (null == V || V.Length == 0)
                return "Empty block";
            foreach (var t in V)
            {
                var hexValue = t.ToString("X");
                if (hexValue.Length == 1)
                    hexValue = "0" + hexValue;
                output += hexValue + ",";
            }
            return "{" + output + "}";
        }
    }
}