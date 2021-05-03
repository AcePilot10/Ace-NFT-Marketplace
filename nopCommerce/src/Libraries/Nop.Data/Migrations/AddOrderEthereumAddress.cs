using FluentMigrator;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Migrations
{
    [NopMigration("2021/05/02 11:24:16:2551770", "Order. Add Ethereum address for NFT receiver")]
    public class AddOrderEthereumAddress : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Column(nameof(Order.EthereumAddress))
                     .OnTable(nameof(Order))
                     .AsString(250)
                     .Nullable();
        }
    }
}