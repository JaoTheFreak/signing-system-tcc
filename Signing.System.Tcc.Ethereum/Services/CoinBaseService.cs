using Flurl.Http;
using Flurl;
using Signing.System.Tcc.Domain.EtherAggregate;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

namespace Signing.System.Tcc.Ethereum.Services
{
    public class CoinBaseService : IEtherFactory
    {
        private readonly string _baseEndpoint;

        public CoinBaseService(string baseEndpoint)
        {
            _baseEndpoint = baseEndpoint;


        }

        public async Task<EtherValueObject> CreateEtherAsync()
        {
            try
            {
                var resultFromCoinBase = await _baseEndpoint
                        .AppendPathSegment("prices/ETH-BRL/sell")
                        .GetJsonAsync<CoinBasePricesReturn>();

                var etherToReturn = new EtherValueObject(resultFromCoinBase.Data.Amount, resultFromCoinBase.Data.Base, resultFromCoinBase.Data.Currency);

                return etherToReturn;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    internal class CoinBasePricesReturn
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
    }
    
    internal class Data
    {
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }

}
