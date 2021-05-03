using AceNFT.Services;
using AceNFT.Services.Files;
using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Services.Catalog;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Events;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widget.ViewNFT
{
    public class ViewNFTPlugin : BasePlugin, IWidgetPlugin
    {

        public bool HideInWidgetList => false;

        #region Base Methods

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string>
            {
                PublicWidgetZones.AccountNavigationAfter
            });
        }

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "ViewNFTComponent";
        }

        #endregion
    }
}