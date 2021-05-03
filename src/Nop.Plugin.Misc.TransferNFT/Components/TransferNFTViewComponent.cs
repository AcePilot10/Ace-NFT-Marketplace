using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.TransferNFT.Components
{
    [ViewComponent(Name = "TransferNFTWidget")]

    public class TransferNFTViewComponent : NopViewComponent
    {
        public IViewComponentResult Invoke(string widgetZone)
        {
            return View("~/Plugins/Misc.TransferNFT/Views/TransferNFTViewComponent.cshtml", this);
        }
    }
}
