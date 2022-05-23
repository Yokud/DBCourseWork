using DataBaseUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicUI
{
    public class TechnicUI
    {
        IShopsRepository shops;
        IProductsRepository products;
        ISaleReceiptsRepository saleReceipts;
        ISaleReceiptPositionsRepository saleReceiptPositions;
        ICostsRepository costs;
        ICostStoryRepository costStory;

        public TechnicUI(IShopsRepository shops, IProductsRepository products, ISaleReceiptsRepository saleReceipts, ISaleReceiptPositionsRepository saleReceiptPositions, ICostsRepository costs, ICostStoryRepository costStory)
        {
            this.shops = shops;
            this.products = products;
            this.saleReceipts = saleReceipts;
            this.saleReceiptPositions = saleReceiptPositions;
            this.costs = costs;
            this.costStory = costStory;
        }


    }
}
