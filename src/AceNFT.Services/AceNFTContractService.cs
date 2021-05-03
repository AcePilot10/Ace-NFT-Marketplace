using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static AceNFT.Services.Structs.AceNFTStructs;

namespace AceNFT.Services
{
    public class AceNFTContractService : INFTContract
    {
        //private string _contractAbi;
        private string _url;
        private string _contractAddress;
        private string _ownerAddress;

        private string _contractAbi;
        private const string contractAbiPath = "D:\\Ethereum\\TokenTesting\\build\\contracts\\NFTTesting.json";

        private Contract contract;
        private Web3 web3;

        public AceNFTContractService(string contractAbi, string url, string contractAddress, string ownerAddress)
        {
            //_contractAbi = contractAbi;
            //SetContractAbi();
            _url = url;
            _contractAddress = contractAddress;
            _ownerAddress = ownerAddress;
            web3 = new Web3(url);
            //web3.TransactionManager.DefaultGas = Nethereum.Util.UnitConversion.Convert.ToWei(0.5);
            web3.TransactionManager.DefaultGas = 6721975;
            web3.TransactionManager.DefaultGasPrice = 100000000000;
            contract = web3.Eth.GetContract(contractAbi, contractAddress);
        }

        //private void SetContractAbi()
        //{
        //    var json = JObject.Load(new JsonTextReader(new StreamReader(File.OpenRead(contractAbiPath))));
        //    var abi = json["abi"].ToString();
        //    _contractAbi = abi;
        //}

        public async Task<string> MintToken(string to, string uri)
        {
            var function = contract.GetFunction("giveToken");
            var id = GenerateRandomTokenId();
            try
            {
                var reciept = await function.SendTransactionAndWaitForReceiptAsync(_ownerAddress, null, to, id, uri);
                return reciept.BlockHash;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<List<TokenInfo>> GetAllTokens(string user)
        {
            var balance = await TokenBalanceOf(user);
            List<TokenInfo> tokens = new List<TokenInfo>();
            var function = contract.GetFunction("tokenOfOwnerByIndex");
            for (int i = 0; i < balance; i++)
            {
                try
                {
                    var tokenId = await function.CallAsync<long>(user, i);
                    var tokenUri = await GetTokenUri((uint)tokenId);
                    TokenInfo token = new TokenInfo()
                    {
                        TokenId = tokenId,
                        URI = tokenUri
                    };
                    tokens.Add(token);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return tokens;
        }

        public async Task<int> TokenBalanceOf(string owner)
        {
            var function = contract.GetFunction("balanceOf");
            var result = await function.CallAsync<int>(owner);
            return result;
        }

        #region DTOs
        [FunctionOutput]
        public class TokenUriOutput : IFunctionOutputDTO
        {
            [Parameter("string", "tokenUri", 1)]
            public string TokenUri { get; set; }
        }

        [Function("tokenURI", "string")]
        public class TokenUriFunction : FunctionMessage
        {
            [Parameter("uint", "tokenId", 1)]
            public uint TokenId { get; set; }
        }
        #endregion

        public async Task<string> GetTokenUri(uint tokenId)
        { 
            TokenUriFunction tokenUriFunctionMessage = new TokenUriFunction()
            {
                TokenId = tokenId
            };
            var tokenUriHandler = web3.Eth.GetContractQueryHandler<TokenUriFunction>();
            var result = tokenUriHandler.QueryDeserializingToObjectAsync<TokenUriOutput>(tokenUriFunctionMessage, _contractAddress);
             var uri = await result;
            return uri.TokenUri;
        }

        private uint GenerateRandomTokenId()
        {
            Random random = new Random();
            uint thirtyBits = (uint)random.Next(1 << 30);
            uint twoBits = (uint)random.Next(1 << 2);
            uint tokenId = (thirtyBits << 2) | twoBits;
            return tokenId;
        }
    }
}