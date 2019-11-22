using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace SigningSystemTcc.Contracts.SigningSystemContract.ContractDefinition
{
    public class Base 
    {
        [Parameter("string", "Hash", 1)]
        public virtual string Hash { get; set; }
        [Parameter("uint256", "CreatedAt", 2)]
        public virtual BigInteger CreatedAt { get; set; }
        [Parameter("string", "AuthorDocument", 3)]
        public virtual string AuthorDocument { get; set; }
    }
}
