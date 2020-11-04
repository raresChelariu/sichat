using System;
using System.Collections.Generic;


namespace EncryptionLib
{
    public class BlockLists
    {
        private const int BlockSize = Constants.BlockSize;
        public enum EncryptionMode
        {
            ECB
        }

        
        public static LinkedList<CryptoBlock> EncryptBlockList(LinkedList<CryptoBlock> blockList, CryptoBlock key
            , EncryptionMode mode)
        {
            if (blockList?.Count == 0)
                throw new ArgumentNullException(nameof(blockList));
            switch (mode)
            {
                case EncryptionMode.ECB:
                    return EncryptECB_BlockList(blockList, key);
                default:
                    return null;
            }
        }
        public static LinkedList<CryptoBlock> DecryptBlockList(LinkedList<CryptoBlock> blockList, CryptoBlock key
            , EncryptionMode mode)
        {
            if (blockList?.Count == 0)
                throw new ArgumentNullException(nameof(blockList));
            switch (mode)
            {
                case EncryptionMode.ECB:
                    return DecryptECB_BlockList(blockList, key);
                default:
                    return null;
            }
        }
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
        
    }
}