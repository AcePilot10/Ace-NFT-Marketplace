using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AceNFT.Services;
using AceNFT.Services.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Messages;
using Nop.Plugin.Misc.TransferNFT.Models;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.TransferNFT.Controllers
{
    //[AutoValidateAntiforgeryToken]
    //[AuthorizeAdmin] //confirms access to the admin panel
    //[Area(AreaNames.Admin)] //specifies the area containing a controller or action
    //[Route("plugin/{controller}")]
    public class TransferNFTController : BasePluginController
    {
        private INFTContract _contract;
        private IPFSHelper _ipfs;

        public TransferNFTController(INFTContract contract, IPFSHelper ipfs)
        {
            _contract = contract;
            _ipfs = ipfs;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        public IActionResult SelectAccount()
        {
            return View("~/Plugins/Misc.TransferNFT/Views/SelectAccount.cshtml", null);
        }

        public IActionResult Configure()
        {
            return View("~/Plugins/Misc.TransferNFT/Views/Configure.cshtml");
        }

        public IActionResult AccountSelected(string address)
        {
            try
            {
                HttpContext.Session.SetString("Address", address);
                HttpContext.Session.CommitAsync();
                return View("~/Plugins/Misc.TransferNFT/Views/Configure.cshtml");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public async Task<ActionResult> ViewNFTs(string address)
        {
            var allTokens = await _contract.GetAllTokens(address);
            NFTEnumerable model = new NFTEnumerable()
            {
                OwnedTokens = allTokens
            };
            return View("~/Plugins/Misc.TransferNFT/Views/ViewNFTs.cshtml", model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> MintNFT(List<IFormFile> files)
        {
            var file = files[0];

            var filePath = Path.GetTempFileName();

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);
            var url = await _ipfs.Upload(Guid.NewGuid().ToString(), bytes);
            var address = HttpContext.Session.GetString("Address");

            await _contract.MintToken(address, url);

            return RedirectToAction("ViewNFTs", new { address = address });
        }

        public IActionResult MintNFT()
        {
            return View("~/Plugins/Misc.TransferNFT/Views/TestSelectFile.cshtml");
        }

        public string GetAddress()
        {
            string address = HttpContext.Session.GetString("Address");
            return address;
        }
    }
}