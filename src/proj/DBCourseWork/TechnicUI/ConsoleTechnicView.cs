using DataBaseUI.SysEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicUI
{
    public class ConsoleTechnicView : ITechnicView
    {
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
            "\t10) Обновить данные о продукте;\n\n\n" +
            "\t11) Получить чек по Id;\n" +
            "\t12) Получить все чеки;\n" +
            "\t13) Добавить чек;\n" +
            "\t14) Удалить чек;\n" +
            "\t15) Обновить чек;\n\n\n" +
            "\t16) Получить позицию чека по Id;\n" +
            "\t17) Получить все позиции чеков;\n" +
            "\t18) Добавить позицию чека;\n" +
            "\t19) Удалить позицию чека;\n" +
            "\t20) Обновить позицию чека;\n\n\n" +
            "\t21) Получить цену по Id;\n" +
            "\t22) Получить все цены;\n" +
            "\t23) Добавить цену;\n" +
            "\t24) Удалить цену;\n" +
            "\t25) Обновить цену;\n\n\n" +
            "\t26) Получить историю цены по Id;\n" +
            "\t27) Получить все истории цен;\n" +
            "\t28) Добавить элемент истории цен;\n" +
            "\t29) Удалить элемент истории цен;\n" +
            "\t30) Обновить элемент истории цен;\n\n\n" +
            "\t31) Получить элемент отношения наличия по Id;\n" +
            "\t32) Получить все элементы отношения наличия;\n" +
            "\t33) Добавить элемент отношения наличия;\n" +
            "\t34) Удалить элемент отношения наличия;\n" +
            "\t35) Обновить элемент отношения наличия;\n\n\n" +
            "\t0) Выход.\n\nВведите номер команды: ";


        public ConsoleTechnicView()
        {

        }

        public ConsoleTechnicView(ITechnicPresenter presenter)
        {
            Presenter = presenter;
        }

        public ITechnicPresenter Presenter { get; set; }

        public void StartUi()
        {
            bool isFinished = false;

            while (!isFinished)
            {
                Console.Write(menuString);

                bool cmdGot = int.TryParse(Console.ReadLine(), out int cmd);

                if (!cmdGot)
                {
                    Console.WriteLine("Ошибка ввода команды!");
                    continue;
                }

                switch (cmd)
                {
                    case 0:
                        Console.WriteLine("Выход из программы.");
                        isFinished = true;
                        break;
                    case 1:
                        GetShop();
                        break;
                    case 2:
                        GetAllShops();
                        break;
                    case 3:
                        AddShop();
                        break;
                    case 4:
                        DeleteShop();
                        break;
                    case 5:
                        UpdateShop();
                        break;
                    case 6:
                        GetProduct();
                        break;
                    case 7:
                        GetAllProducts();
                        break;
                    case 8:
                        AddProduct();
                        break;
                    case 9:
                        DeleteProduct();
                        break;
                    case 10:
                        UpdateProduct();
                        break;
                    case 11:
                        GetSaleReceipt();
                        break;
                    case 12:
                        GetAllSaleReceipts();
                        break;
                    case 13:
                        AddSaleReceipt();
                        break;
                    case 14:
                        DeleteSaleReceipt();
                        break;
                    case 15:
                        UpdateSaleReceipt();
                        break;
                    case 16:
                        GetSaleReceiptPosition();
                        break;
                    case 17:
                        GetAllSaleReceiptPositions();
                        break;
                    case 18:
                        AddSaleReceiptPosition();
                        break;
                    case 19:
                        DeleteSaleReceiptPosition();
                        break;
                    case 20:
                        UpdateSaleReceiptPosition();
                        break;
                    case 21:
                        GetCost();
                        break;
                    case 22:
                        GetAllCosts();
                        break;
                    case 23:
                        AddCost();
                        break;
                    case 24:
                        DeleteCost();
                        break;
                    case 25:
                        UpdateCost();
                        break;
                    case 26:
                        GetCostStory();
                        break;
                    case 27:
                        GetAllCostStories();
                        break;
                    case 28:
                        AddCostStory();
                        break;
                    case 29:
                        DeleteCostStory();
                        break;
                    case 30:
                        UpdateCostStory();
                        break;
                    case 31:
                        GetAvailability();
                        break;
                    case 32:
                        GetAllAvailabilities();
                        break;
                    case 33:
                        AddAvailability();
                        break;
                    case 34:
                        DeleteAvailability();
                        break;
                    case 35:
                        UpdateAvailability();
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }
            }
        }

        public void Show(string message)
        {
            Console.WriteLine(message);
        }

        public void AddShop()
        {
            try
            {
                string name = Console.ReadLine();
                string descr = Console.ReadLine();

                Presenter.AddShop(new Shop(name, descr));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteShop()
        {
            try
            {
                string name = Console.ReadLine();
                string descr = Console.ReadLine();

                Presenter.DeleteShop(new Shop(name, descr));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateShop()
        {
            try
            {
                string name = Console.ReadLine();
                string descr = Console.ReadLine();

                Presenter.UpdateShop(new Shop(name, descr));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetShop()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());

                Presenter.GetShop(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetAllShops()
        {
            Presenter.GetAllShops();
        }

        public void AddProduct()
        {
            try
            {
                string name = Console.ReadLine();
                string prodtype = Console.ReadLine();

                Presenter.AddProduct(new Product(name, prodtype));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteProduct()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());
                string name = Console.ReadLine();
                string prodtype = Console.ReadLine();

                Presenter.DeleteProduct(new Product(id, name, prodtype));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateProduct()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());
                string name = Console.ReadLine();
                string prodtype = Console.ReadLine();

                Presenter.UpdateProduct(new Product(id, name, prodtype));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetProduct()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());

                Presenter.GetProduct(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetAllProducts()
        {
            Presenter.GetAllProducts();
        }

        public void AddAvailability()
        {
            try
            {
                int prodid = int.Parse(Console.ReadLine());
                int shopid = int.Parse(Console.ReadLine());

                Presenter.AddAvailability(new Availability(shopid, prodid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteAvailability()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());
                int prodid = int.Parse(Console.ReadLine());
                int shopid = int.Parse(Console.ReadLine());

                Presenter.DeleteAvailability(new Availability(id, shopid, prodid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateAvailability()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());
                int prodid = int.Parse(Console.ReadLine());
                int shopid = int.Parse(Console.ReadLine());

                Presenter.UpdateAvailability(new Availability(id, shopid, prodid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetAvailability()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());

                Presenter.GetAvailability(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetAllAvailabilities()
        {
            Presenter.GetAllAvailabilities();
        }

        public void AddSaleReceipt()
        {
            try
            {
                string fio = Console.ReadLine();
                DateOnly d = DateOnly.Parse(Console.ReadLine());
                int shopid = int.Parse(Console.ReadLine());

                Presenter.AddSaleReceipt(new SaleReceipt(fio, d, shopid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteSaleReceipt()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());
                string fio = Console.ReadLine();
                DateOnly d = DateOnly.Parse(Console.ReadLine());
                int shopid = int.Parse(Console.ReadLine());

                Presenter.DeleteSaleReceipt(new SaleReceipt(id, fio, d, shopid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateSaleReceipt()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());
                string fio = Console.ReadLine();
                DateOnly d = DateOnly.Parse(Console.ReadLine());
                int shopid = int.Parse(Console.ReadLine());

                Presenter.UpdateSaleReceipt(new SaleReceipt(id, fio, d, shopid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetSaleReceipt()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());

                Presenter.GetSaleReceipt(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetAllSaleReceipts()
        {
            Presenter.GetAllSaleReceipts();
        }

        public void AddSaleReceiptPosition()
        {
            try
            {
                int srid = int.Parse(Console.ReadLine());
                int avid = int.Parse(Console.ReadLine());

                Presenter.AddSaleReceiptPosition(new SaleReceiptPosition(avid, srid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteSaleReceiptPosition()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());
                int srid = int.Parse(Console.ReadLine());
                int avid = int.Parse(Console.ReadLine());

                Presenter.DeleteSaleReceiptPosition(new SaleReceiptPosition(id, avid, srid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateSaleReceiptPosition()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());
                int srid = int.Parse(Console.ReadLine());
                int avid = int.Parse(Console.ReadLine());

                Presenter.UpdateSaleReceiptPosition(new SaleReceiptPosition(id, avid, srid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetSaleReceiptPosition()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());

                Presenter.GetSaleReceiptPosition(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetAllSaleReceiptPositions()
        {
            Presenter.GetAllSaleReceiptPositions();
        }

        public void AddCost()
        {
            try
            {
                int avid = int.Parse(Console.ReadLine());
                int cost = int.Parse(Console.ReadLine());

                Presenter.AddCost(new Cost(avid, cost));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteCost()
        {
            try
            {
                int avid = int.Parse(Console.ReadLine());
                int cost = int.Parse(Console.ReadLine());

                Presenter.DeleteCost(new Cost(avid, cost));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateCost()
        {
            try
            {
                int avid = int.Parse(Console.ReadLine());
                int cost = int.Parse(Console.ReadLine());

                Presenter.UpdateCost(new Cost(avid, cost));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetCost()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());

                Presenter.GetCost(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetAllCosts()
        {
            Presenter.GetAllCosts();
        }

        public void AddCostStory()
        {
            try
            {
                int year = int.Parse(Console.ReadLine());
                int month = int.Parse(Console.ReadLine());
                int cost = int.Parse(Console.ReadLine());
                int avid = int.Parse(Console.ReadLine());

                Presenter.AddCostStory(new CostStory(year, month, cost, avid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteCostStory()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());
                int year = int.Parse(Console.ReadLine());
                int month = int.Parse(Console.ReadLine());
                int cost = int.Parse(Console.ReadLine());
                int avid = int.Parse(Console.ReadLine());

                Presenter.DeleteCostStory(new CostStory(id, year, month, cost, avid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateCostStory()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());
                int year = int.Parse(Console.ReadLine());
                int month = int.Parse(Console.ReadLine());
                int cost = int.Parse(Console.ReadLine());
                int avid = int.Parse(Console.ReadLine());

                Presenter.UpdateCostStory(new CostStory(id, year, month, cost, avid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetCostStory()
        {
            try
            {
                int id = int.Parse(Console.ReadLine());

                Presenter.GetCostStory(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetAllCostStories()
        {
            Presenter.GetAllCostStories();
        }
    }
}
