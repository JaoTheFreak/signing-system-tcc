using Nethereum;
using Nethereum.Hex.HexConvertors.Extensions;
using System;
using System.IO;

namespace Signin.System.Tcc.Ethereum.Wallet
{
    public class WalletService
    {
        public string GetWallet() {
            var ecKey =  Nethereum.Signer.EthECKey.GenerateKey();

            var privateKey = ecKey.GetPrivateKeyAsBytes().ToHex();

            var account = new Nethereum.Web3.Accounts.Account(privateKey);

            return $"Address: {account.Address} / PKey: {privateKey}";
        }
    }
}