using Nethereum.Web3;
using System;
using System.Numerics;

namespace Nethereum.Signer
{
    internal class TransactionChainId
    {
        private BigInteger value;
        private HexBigInteger gasPrice;
        private object gas;
        private string contractAddress;
        private int v1;
        private object functionInput;
        private int v2;

        public TransactionChainId(BigInteger value, HexBigInteger gasPrice, object gas, string contractAddress, int v1, object functionInput, int v2)
        {
            this.value = value;
            this.gasPrice = gasPrice;
            this.gas = gas;
            this.contractAddress = contractAddress;
            this.v1 = v1;
            this.functionInput = functionInput;
            this.v2 = v2;
        }

        internal object Sign(EthECKey ethECKey)
        {
            throw new NotImplementedException();
        }
    }
}