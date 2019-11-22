using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using SigningSystemTcc.Contracts.SigningSystemContract.ContractDefinition;

namespace SigningSystemTcc.Contracts.SigningSystemContract
{
    public partial class SigningSystemContractService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, SigningSystemContractDeployment signingSystemContractDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<SigningSystemContractDeployment>().SendRequestAndWaitForReceiptAsync(signingSystemContractDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, SigningSystemContractDeployment signingSystemContractDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<SigningSystemContractDeployment>().SendRequestAsync(signingSystemContractDeployment);
        }

        public static async Task<SigningSystemContractService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, SigningSystemContractDeployment signingSystemContractDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, signingSystemContractDeployment, cancellationTokenSource);
            return new SigningSystemContractService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public SigningSystemContractService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> RegisterDocumentRequestAsync(RegisterDocumentFunction registerDocumentFunction)
        {
             return ContractHandler.SendRequestAsync(registerDocumentFunction);
        }

        public Task<TransactionReceipt> RegisterDocumentRequestAndWaitForReceiptAsync(RegisterDocumentFunction registerDocumentFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerDocumentFunction, cancellationToken);
        }

        public Task<string> RegisterDocumentRequestAsync(string authorDocument, string imageHash)
        {
            var registerDocumentFunction = new RegisterDocumentFunction();
                registerDocumentFunction.AuthorDocument = authorDocument;
                registerDocumentFunction.ImageHash = imageHash;
            
             return ContractHandler.SendRequestAsync(registerDocumentFunction);
        }

        public Task<TransactionReceipt> RegisterDocumentRequestAndWaitForReceiptAsync(string authorDocument, string imageHash, CancellationTokenSource cancellationToken = null)
        {
            var registerDocumentFunction = new RegisterDocumentFunction();
                registerDocumentFunction.AuthorDocument = authorDocument;
                registerDocumentFunction.ImageHash = imageHash;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerDocumentFunction, cancellationToken);
        }

        public Task<AuthorImagesOutputDTO> AuthorImagesQueryAsync(AuthorImagesFunction authorImagesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<AuthorImagesFunction, AuthorImagesOutputDTO>(authorImagesFunction, blockParameter);
        }

        public Task<AuthorImagesOutputDTO> AuthorImagesQueryAsync(string returnValue1, BigInteger returnValue2, BlockParameter blockParameter = null)
        {
            var authorImagesFunction = new AuthorImagesFunction();
                authorImagesFunction.ReturnValue1 = returnValue1;
                authorImagesFunction.ReturnValue2 = returnValue2;
            
            return ContractHandler.QueryDeserializingToObjectAsync<AuthorImagesFunction, AuthorImagesOutputDTO>(authorImagesFunction, blockParameter);
        }

        public Task<RegisteredImagesOutputDTO> RegisteredImagesQueryAsync(RegisteredImagesFunction registeredImagesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<RegisteredImagesFunction, RegisteredImagesOutputDTO>(registeredImagesFunction, blockParameter);
        }

        public Task<RegisteredImagesOutputDTO> RegisteredImagesQueryAsync(string returnValue1, BlockParameter blockParameter = null)
        {
            var registeredImagesFunction = new RegisteredImagesFunction();
                registeredImagesFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<RegisteredImagesFunction, RegisteredImagesOutputDTO>(registeredImagesFunction, blockParameter);
        }
    }
}
