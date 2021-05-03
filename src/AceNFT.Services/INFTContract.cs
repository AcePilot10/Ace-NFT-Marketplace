using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static AceNFT.Services.Structs.AceNFTStructs;

namespace AceNFT.Services
{
    public interface INFTContract
    {
        Task<string> MintToken(string to, string uri);
        Task<List<TokenInfo>> GetAllTokens(string user);
        Task<int> TokenBalanceOf(string owner);
        Task<string> GetTokenUri(uint tokenId);
    }
}