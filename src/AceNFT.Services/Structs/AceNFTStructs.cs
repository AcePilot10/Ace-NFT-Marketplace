using System;
using System.Collections.Generic;
using System.Text;

namespace AceNFT.Services.Structs
{
    public class AceNFTStructs
    {
        public struct TokenInfo
        {
            public long TokenId { get; set; }
            public string URI { get; set; }
        }

        public struct IPFPinResult
        { 
            public string IpfsHash { get; set; }
            public int PinSize { get; set; }
            public DateTime Timestamp { get; set; }
        }
    }
}
