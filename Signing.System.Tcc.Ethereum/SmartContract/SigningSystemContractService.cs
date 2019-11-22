using Nethereum.Contracts.ContractHandlers;
using Nethereum.RPC.Eth.DTOs;
using Signing.System.Tcc.Ethereum.SmartContract.ContractDefinition;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Signing.System.Tcc.Ethereum.SmartContract
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
                authorImagesFunction.AuthorDocument = returnValue1;
                authorImagesFunction.Index = returnValue2;
            
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
