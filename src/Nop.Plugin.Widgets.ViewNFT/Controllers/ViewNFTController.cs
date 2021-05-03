using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AceNFT.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widget.ViewNFT.Models;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Misc.TransferNFT.Controllers
{
    public class ViewNFTController : BasePluginController
    {
        private readonly INFTContract _contract;

        public ViewNFTController(INFTContract contract)
        {
            _contract = contract;
        }

        //public IActionResult AccountSelected(string address)
        //{
        //    try
        //    {
        //        HttpContext.Session.SetString("Address", address);
        //        HttpContext.Session.CommitAsync();
        //        return View("~/Plugins/Misc.TransferNFT/Views/Configure.cshtml");
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        public async Task<ActionResult> MyNFTs()
        {
            NFTEnumerable model = new NFTEnumerable();
            if (HttpContext.Session.GetString("Address") != null)
            {
                var address = HttpContext.Session.GetString("Address");
                var allTokens = await _contract.GetAllTokens(address);
                model.OwnedTokens = allTokens;
            }
            return View("~/Plugins/Widgets.ViewNFT/Views/ViewNFTs.cshtml", model);
        }
    }
}