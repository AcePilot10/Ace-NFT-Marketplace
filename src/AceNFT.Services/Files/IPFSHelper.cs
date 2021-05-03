using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using static AceNFT.Services.Structs.AceNFTStructs;

namespace AceNFT.Services.Files
{
    public class IPFSHelper
    { 
        public async Task<string> Upload(string name, byte[] image)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("pinata_api_key", "8850b177125d64a77d6a");
                client.DefaultRequestHeaders.Add("pinata_secret_api_key", "503a5a876776e4bd1146af097c26970baef70184c20d43c39cce5fbadb69984a");

                using (var content =
                    new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
                {
                    content.Add(new StreamContent(new MemoryStream(image)), "file", name);

                    using (
                       var message =
                           await client.PostAsync("https://api.pinata.cloud/pinning/pinFileToIPFS", content))
                    {
                        var response = await message.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<IPFPinResult>(response);
                        return result.IpfsHash;
                    }
                }
            }
        }
    }
}