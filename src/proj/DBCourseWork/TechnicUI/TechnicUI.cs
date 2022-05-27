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

        string menuString = "Технический интерфейс работы с БД с доступом ко всем командам.\nМеню:\n" +
            "\t1)  Получить магазин по Id;\n" +
            "\t2)  Получить все магазины;\n" +
            "\t3)  Добавить магазин;\n" +
            "\t4)  Удалить магазин;\n" +
            "\t5)  Изменить данные о магазине;\n\n\n" +
            "\t6)  Получить продукт по Id;\n" +
            "\t7)  Получить все продукты;\n" +
            "\t8)  Добавить продукт;\n" +
            "\t9)  Удалить продукт;\n" +
            "\t10) " +
            "\t0) Выход.\n\nВведите номер команды: ";

        public TechnicUI(IShopsRepository shops, IProductsRepository products, ISaleReceiptsRepository saleReceipts, ISaleReceiptPositionsRepository saleReceiptPositions, ICostsRepository costs, ICostStoryRepository costStory)
        {
            this.shops = shops;
            this.products = products;
            this.saleReceipts = saleReceipts;
            this.saleReceiptPositions = saleReceiptPositions;
            this.costs = costs;
            this.costStory = costStory;
        }

        public void StartUi()
        {
            bool isFinished = false;

            while (!isFinished)
            {

            }
        }
    }
}
