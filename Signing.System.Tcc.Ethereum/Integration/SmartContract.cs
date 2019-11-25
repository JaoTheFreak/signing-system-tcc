using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Signing.System.Tcc.Domain.EtherAggregate;
using Signing.System.Tcc.Ethereum.Interfaces;
using Signing.System.Tcc.Ethereum.SmartContract;
using Signing.System.Tcc.Ethereum.SmartContract.ContractDefinition;
using Signing.System.Tcc.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Signing.System.Tcc.Ethereum.Integration
{
    public class SmartContract : ISmartContract
    {
        private readonly IWeb3 _web3;

        private readonly string _contractAddress;

        private readonly string _userAccountAddress;

        private readonly SigningSystemContractService _myContract;

        public SmartContract(string projectEndPoint, string contractAddress, string userAccountAddress)
        {
            _web3 = new Web3(projectEndPoint);

            _contractAddress = contractAddress;

            _userAccountAddress = userAccountAddress;

            var privateKey = "0xD1042D34B0D0E04E5585D84657CC3CA738E7B735794D68AF66FF2AF132164B3B";

            var account = new Account(privateKey);

            _myContract = new SigningSystemContractService(new Web3(account, projectEndPoint), _contractAddress);
        }

        public async Task RegisterImageAsync(string authorDocument, string hashImage)
        {
            try
            {
                var registerDocumentFunction = new RegisterDocumentFunction
                {
                    ImageHash = hashImage,
                    AuthorDocument = authorDocument,
                    FromAddress = _userAccountAddress
                };

                var priceInGas = await _myContract.ContractHandler.EstimateGasAsync(registerDocumentFunction);

                registerDocumentFunction.GasPrice = priceInGas;

                var eoq = await _myContract.RegisterDocumentRequestAndWaitForReceiptAsync(registerDocumentFunction);
            }
            catch (Exception ex)
            {

                return;
            }
        }

        public async Task<decimal> EstimateTransactionPriceAsync(string authorDocument, string hashImage, EtherValueObject ether)
        {
            try
            {
                var registerDocumentFunction = new RegisterDocumentFunction
                {
                    ImageHash = hashImage,
                    AuthorDocument = authorDocument,
                    FromAddress = _userAccountAddress
                };

                var priceInGas = await _myContract.ContractHandler.EstimateGasAsync(registerDocumentFunction);

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task VerifyImageByAuthorDocumentAsync(string authorDocument)
        {
            try
            {
                var receipts = new List<AuthorImagesOutputDTO>();


                var function = new AuthorImagesFunction
                {
                    FromAddress = _userAccountAddress,
                    AuthorDocument = authorDocument,
                    Index = 0
                };

                AuthorImagesOutputDTO receiptResult;

                do
                {
                    receiptResult = await _myContract.AuthorImagesQueryAsync(function);

                    if (receiptResult == null)
                        break;

                    receipts.Add(receiptResult);

                    function.Index++;
                } while (true);

                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public async Task<(string AuthorDocument, string ImageHash, DateTime CreatedAt)> VerifyImageByHashAsync(string hashImage)
        {
            try
            {
                var function = new RegisteredImagesFunction
                {
                    FromAddress = _userAccountAddress,
                    ReturnValue1 = hashImage
                };

                var receiptResult = await _myContract.RegisteredImagesQueryAsync(function);

                return (receiptResult.AuthorDocument, receiptResult.Hash, receiptResult.CreatedAt.ToDateTime());
            }
            catch (Exception ex)
            {
                return (null, null, DateTime.MinValue);
            }
        }

    }

}
