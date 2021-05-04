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

namespace Nop.Plugin.Misc.TransferNFT
{
    public class TransferNFTPlugin : BasePlugin, IConsumer<OrderPaidEvent>, IWidgetPlugin
    {
        #region Fields
        private readonly IWebHelper _webHelper;
        private readonly INFTContract _contract;
        private readonly IPFSHelper _ipfs;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IDownloadService _downloadService;

        public bool HideInWidgetList => false;
        #endregion

        #region Ctor
        public TransferNFTPlugin(IWebHelper webHelper, INFTContract contract,
                    IPFSHelper ipfs, IProductService productService, IOrderService orderService,
                    IDownloadService downloadService)
        {
            _webHelper = webHelper;
            _contract = contract;
            _ipfs = ipfs;
            _productService = productService;
            _orderService = orderService;
            _downloadService = downloadService;
        }
        #endregion

        public async void TransferNFT(Order order)
        {
            var orderItems = await _orderService.GetOrderItemsAsync(order.Id);
            foreach (var orderItem in orderItems)
            {
                var address = order.EthereumAddress;
                var product = await _productService.GetProductByIdAsync(orderItem.ProductId);
                var downloadId = product.DownloadId;
                var download = await _downloadService.GetDownloadByIdAsync(downloadId);
                var bytes = download.DownloadBinary;
                await Mint(bytes, address);
                await _productService.DeleteProductAsync(product);
            }
        }

        private async Task<Task> Mint(byte[] bytes, string address)
        {
            var url = await _ipfs.Upload(Guid.NewGuid().ToString(), bytes);
            await _contract.MintToken(address, url);
            return Task.CompletedTask;
        }

        #region Base Methods
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}plugins/TransferNFT/Configure";
        }

        public Task HandleEventAsync(OrderPaidEvent eventMessage)
        {
            TransferNFT(eventMessage.Order);
            return Task.CompletedTask;
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            //return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.OpcContentBefore, AdminWidgetZones.ProductDetailsBlock });
            return Task.FromResult<IList<string>>(new List<string> { }); 
            //{ PublicWidgetZones.OpcContentAfter });
        }

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "TransferNFTWidget";
        }
        #endregion
    }
}