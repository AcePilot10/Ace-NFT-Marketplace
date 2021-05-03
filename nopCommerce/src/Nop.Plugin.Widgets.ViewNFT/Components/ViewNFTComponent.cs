using AceNFT.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widget.ViewNFT.Models;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widget.ViewNFT.Components
{
    [ViewComponent(Name = "ViewNFTComponent")]
    public class ViewNFTComponent : NopViewComponent
    {
        //private IHttpContextAccessor _httpContextAccessor;
        //private INFTContract _contract;

        //public ViewNFTComponent(IHttpContextAccessor httpContextAccessor, INFTContract contract)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //    _contract = contract;
        //}

        public IViewComponentResult Invoke(string widgetZone)
        {
            //NFTEnumerable model = new NFTEnumerable();
            //if (_httpContextAccessor.HttpContext.Session.GetString("Address") != null)
            //{
            //    var address = _httpContextAccessor.HttpContext.Session.GetString("Address");
            //    var allTokens = _contract.GetAllTokens(address).GetAwaiter().GetResult();
            //    model.OwnedTokens = allTokens;
            //}
            //return View("~/Plugins/Widgets.ViewNFT/Views/ViewNFTs.cshtml", model);
            return View("~/Plugins/Widgets.ViewNFT/Views/ViewNFTWidget.cshtml");
        }
    }
}