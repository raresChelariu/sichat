using System;
using System.Collections.Generic;

namespace EncryptionLib
{
    public class BlockLists
    {
        public enum EncryptionMode
        {
            ECB, CBC, CFB, OFB
        }

        public static LinkedList<CryptoBlock> EncryptBlockList(LinkedList<CryptoBlock> blockList, CryptoBlock key, CryptoBlock iv
            , EncryptionMode mode)
        {
            if (blockList?.Count == 0)
                throw new ArgumentNullException(nameof(blockList));
            switch (mode)
            {
                case EncryptionMode.ECB:
                    return EncryptECB_BlockList(blockList, key);
                case EncryptionMode.CFB:
                    return EncryptCFB_BlockList(blockList, key, iv);
                case EncryptionMode.CBC:
                    return EncryptCBC_BlockList(blockList, key, iv);
                case EncryptionMode.OFB:
                    return EncryptOFB_BlockList(blockList, key, iv);
                default:
                    throw new NotImplementedException(nameof(mode));
            }
        }
        public static LinkedList<CryptoBlock> DecryptBlockList(LinkedList<CryptoBlock> blockList, CryptoBlock key
            , CryptoBlock iv,EncryptionMode mode)
        {
            if (blockList?.Count == 0)
                throw new ArgumentNullException(nameof(blockList));
            switch (mode)
            {
                case EncryptionMode.ECB:
                    return DecryptECB_BlockList(blockList, key);
                case EncryptionMode.CBC:
                    return DecryptCBC_BlockList(blockList, key, iv);
                case EncryptionMode.CFB:
                    return DecryptCFB_BlockList(blockList, key, iv);
                case EncryptionMode.OFB:
                    return DecryptOFB_BlockList(blockList, key, iv);
                default:
                    return null;
            }
        }

        #region MODE CBC (DONE)
        private static LinkedList<CryptoBlock> EncryptCBC_BlockList(LinkedList<CryptoBlock> blockList, CryptoBlock key, CryptoBlock iv)
        {
            if (blockList?.First?.Value == null)
                throw new ArgumentNullException(nameof(blockList));
            if (iv == null)
                throw new ArgumentNullException(nameof(iv));
            var outputList = new LinkedList<CryptoBlock>();
            var lastCipherBlock = iv;

            for (var block = blockList.First; block != null; block = block.Next)
            {
                var currentOutputBlock = (block.Value ^ lastCipherBlock).Encrypt(key);
                outputList.AddLast(currentOutputBlock);
                lastCipherBlock = currentOutputBlock;
            }
            return outputList;
        }
        private static LinkedList<CryptoBlock> DecryptCBC_BlockList(LinkedList<CryptoBlock> blockList, CryptoBlock key, CryptoBlock iv)
        {
            var lastCipher = iv;
            var outputList = new LinkedList<CryptoBlock>();
            for (var block = blockList.First; block != null; block = block.Next)
            {
                var currentOutputBlock = block.Value.Decrypt(key) ^ lastCipher;
                outputList.AddLast(currentOutputBlock);
                lastCipher = block.Value;
            }
            return outputList;
        }

        #endregion
        
        #region MODE OFB (NOT IMPLEMENTED)
        private static LinkedList<CryptoBlock> EncryptOFB_BlockList(LinkedList<CryptoBlock> blockList, CryptoBlock key, CryptoBlock iv)
        {
            throw new NotImplementedException();
        }
        private static LinkedList<CryptoBlock> DecryptOFB_BlockList(LinkedList<CryptoBlock> blockList, CryptoBlock key, CryptoBlock iv)
        {
            throw new NotImplementedException();
        }
        
        #endregion

        #region MODE CFB (NOT IMPLEMENTED)
        private static LinkedList<CryptoBlock> DecryptCFB_BlockList(LinkedList<CryptoBlock> blockList, CryptoBlock key,
            CryptoBlock iv)
        {
            var outputList = new LinkedList<CryptoBlock>();
            
            return outputList;
        }
        private static LinkedList<CryptoBlock> EncryptCFB_BlockList(LinkedList<CryptoBlock> blockList, CryptoBlock key, CryptoBlock iv)
        {
            var lastCipher = iv;
            var outputList = new LinkedList<CryptoBlock>();
            for (var block = blockList.First; block != null; block = block.Next)
            {
                var outputedBlock = lastCipher.Encrypt(key) ^ block.Value;
                outputList.AddLast(outputedBlock);
                lastCipher = outputedBlock;
            }

            return outputList;
        }
        #endregion
        
        #region MODE ECB (DONE)
        private static LinkedList<CryptoBlock> EncryptECB_BlockList(LinkedList<CryptoBlock> blockList, CryptoBlock key)
        {
            var outputList = new LinkedList<CryptoBlock>();
            foreach (var block in blockList)
            {
                outputList.AddLast(block.Encrypt(key));
            }
            return outputList;
        }
        private static LinkedList<CryptoBlock> DecryptECB_BlockList(LinkedList<CryptoBlock> blockList, CryptoBlock key)
        {
            var outputList = new LinkedList<CryptoBlock>();
            foreach (var block in blockList)
            {
                outputList.AddLast(block.Decrypt(key));
            }
            return outputList;
        }
        #endregion 
        
        
        
    }
}