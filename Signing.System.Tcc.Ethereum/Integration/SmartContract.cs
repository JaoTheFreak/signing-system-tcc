using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Signing.System.Tcc.Ethereum.Interfaces;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace Signing.System.Tcc.Ethereum.Integration
{
    public class SmartContract : ISmartContract
    {
        private readonly IWeb3 _web3;

        private readonly string _contractAddress;

        private readonly string _userAccountAddress;

        public SmartContract(string projectEndPoint, string contractAddress, string userAccountAddress)
        {
            _web3 = new Web3(projectEndPoint);

            _contractAddress = contractAddress;

            _userAccountAddress = userAccountAddress;
        }

        public async Task RegisterNewImage(string authorDocument, string hashImage)
        {
            var abi = @"[{""constant"":false,""inputs"":[{""internalType"":""string"",""name"":""authorDocument"",""type"":""string""},{""internalType"":""string"",""name"":""imageHash"",""type"":""string""}],""name"":""registerDocument"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""},{""constant"":true,""inputs"":[{""internalType"":""string"",""name"":""authorDocument"",""type"":""string""}],""name"":""getImageByAuthor"",""outputs"":[{""components"":[{""internalType"":""string"",""name"":""Hash"",""type"":""string""},{""internalType"":""uint256"",""name"":""CreatedAt"",""type"":""uint256""},{""internalType"":""string"",""name"":""AuthorDocument"",""type"":""string""}],""internalType"":""struct SigningSystemContract.Image[]"",""name"":"""",""type"":""tuple[]""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""internalType"":""string"",""name"":""imageHash"",""type"":""string""}],""name"":""verifyImage"",""outputs"":[{""components"":[{""internalType"":""string"",""name"":""Hash"",""type"":""string""},{""internalType"":""uint256"",""name"":""CreatedAt"",""type"":""uint256""},{""internalType"":""string"",""name"":""AuthorDocument"",""type"":""string""}],""internalType"":""struct SigningSystemContract.Image"",""name"":"""",""type"":""tuple""}],""payable"":false,""stateMutability"":""view"",""type"":""function""}]";
            
            var theContract = _web3.Eth.GetContract(abi, _contractAddress);

            var registerFunction = theContract.GetFunction("");

            var transactionHash = await registerFunction.EstimateGasAsync(_userAccountAddress, authorDocument, hashImage);            

        }

        public async Task<string> VerifyImageByHashAsync(string hashImage)
        {
            try
            {
                var abi = @"[{""constant"":false,""inputs"":[{""internalType"":""string"",""name"":""authorDocument"",""type"":""string""},{""internalType"":""string"",""name"":""imageHash"",""type"":""string""}],""name"":""registerDocument"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""},{""constant"":true,""inputs"":[{""internalType"":""string"",""name"":""authorDocument"",""type"":""string""}],""name"":""getImageByAuthor"",""outputs"":[{""components"":[{""internalType"":""string"",""name"":""Hash"",""type"":""string""},{""internalType"":""uint256"",""name"":""CreatedAt"",""type"":""uint256""},{""internalType"":""string"",""name"":""AuthorDocument"",""type"":""string""}],""internalType"":""struct SigningSystemContract.Image[]"",""name"":"""",""type"":""tuple[]""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""internalType"":""string"",""name"":""imageHash"",""type"":""string""}],""name"":""verifyImage"",""outputs"":[{""components"":[{""internalType"":""string"",""name"":""Hash"",""type"":""string""},{""internalType"":""uint256"",""name"":""CreatedAt"",""type"":""uint256""},{""internalType"":""string"",""name"":""AuthorDocument"",""type"":""string""}],""internalType"":""struct SigningSystemContract.Image"",""name"":"""",""type"":""tuple""}],""payable"":false,""stateMutability"":""view"",""type"":""function""}]";

                var theContract = _web3.Eth.GetContract(abi, _contractAddress);

                var registerFunction = theContract.GetFunction("registerDocument");
                
                var transactionHash = await registerFunction.EstimateGasAsync(_userAccountAddress, 8000029.ToHexBigInteger(), 120.ToHexBigInteger(), "06914456992", hashImage);
                
                var verifyImageFunction = new VerifyImageFunction
                {
                    ImageHash = hashImage
                };

                var verifyImageHandler = _web3.Eth.GetContractQueryHandler<VerifyImageFunction>();

                var result = await verifyImageHandler.QueryDeserializingToObjectAsync<ImageSmartContract>(verifyImageFunction, _contractAddress);

                return result.Hash;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }



    [Function("verifyImage", "string")]
    public class VerifyImageFunction : FunctionMessage
    {
        [Parameter("string", "imageHash", 1)]
        public string ImageHash { get; set; }
    }

    [FunctionOutput]
    public class ImageSmartContract : IFunctionOutputDTO
    {
        [Parameter("string", "Hash", 1)]
        public string Hash { get; set; }

        [Parameter("uint256", "CreatedAt", 2)]
        public int CreatedAt { get; set; }

        [Parameter("string", "AuthorDocument", 3)]
        public string AuthorDocument { get; set; }
    }
}
