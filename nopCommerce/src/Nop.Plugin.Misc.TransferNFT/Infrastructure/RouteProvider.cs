using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.TransferNFT.Infrastructure
{
    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute("Plugins.Misc.TransferNFT.SelectAccount", "Plugins/TransferNFT/SelectAccount",
                new { controller = "TransferNFT", action = "SelectAccount" });

            endpointRouteBuilder.MapControllerRoute("Plugins.Misc.TransferNFT.AccountSelected", "Plugins/TransferNFT/AccountSelected",
              new { controller = "TransferNFT", action = "AccountSelected" });

            endpointRouteBuilder.MapControllerRoute("Plugins.Misc.TransferNFT.ViewNFTs", "Plugins/TransferNFT/ViewNFTs",
              new { controller = "TransferNFT", action = "ViewNFTs" });
            
            endpointRouteBuilder.MapControllerRoute("Plugins.Misc.TransferNFT.GetAddress", "Plugins/TransferNFT/GetAddress",
              new { controller = "TransferNFT", action = "GetAddress" });

            endpointRouteBuilder.MapControllerRoute("Plugins.Misc.TransferNFT.Configure", "Plugins/TransferNFT/Configure",
              new { controller = "TransferNFT", action = "Configure" });

            endpointRouteBuilder.MapControllerRoute("Plugins.Misc.TransferNFT.MintNFT", "Plugins/TransferNFT/MintNFT",
             new { controller = "TransferNFT", action = "MintNFT" });
        }

        public int Priority => 0;
    }
}