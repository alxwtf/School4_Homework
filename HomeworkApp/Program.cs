using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var product = new Product[8000000];
            var discount = new Discount[8000000];
            var numberofproducts = 1;
            var numberofdiscounts = 1;
            start: Console.WriteLine("Меню:\n1:Работа с продуктами\n2:Работа со скидками\n3:Подсчёт скидки для товара\nЛюбая клавиша - выход");
            int.TryParse(Console.ReadLine(), out var answer);
            switch (answer)
            {
                case 1:
                    {
                        submenu1: Console.WriteLine("1:Добавить продукт\n2:Показать список продуктов\n3:Вернуться в начало\nЛюбая клавиша - выход");
                        int.TryParse(Console.ReadLine(), out answer);
                        switch (answer)
                        {
                            case 1: AddProduct(ref numberofproducts, product); goto submenu1;
                            case 2: ShowProduct(numberofproducts, product); Console.ReadKey(); goto submenu1;
                            case 3:goto start;
                            default: break;
                        }
                        break;
                    }
                case 2:
                    {
                        submenu2:Console.WriteLine("1:Добавить Скидку\n2:Показать список скидок\n3:Вернуться в начало\nЛюбая клавиша - выход");
                        int.TryParse(Console.ReadLine(), out answer);
                        switch (answer)
                        {
                            case 1: AddDiscount(ref numberofdiscounts, discount); goto submenu2;
                            case 2: ShowDiscount(numberofdiscounts, discount); Console.ReadKey(); goto submenu2;
                            case 3: goto start;
                            default: break;
                        }
                        break;
                    }
                case 3:
                    {
                        if (product[1] != null)
                        {
                            for (var i = 1; i < numberofproducts; i++)
                            {
                                Console.WriteLine("-------------------\n");
                                Console.WriteLine($"{i}:{product[i].Name}\n");
                                Console.WriteLine($"Цена:{product[i].Price} рублей\n");
                                if (product[i].HaveDiscount == true)
                                {
                                    Console.WriteLine($"Цена со скидкой {product[i].DiscountValue}% - " +
                                        $"{product[i].Price - (product[i].Price * product[i].DiscountValue / 100)}" +
                                        $"\nСкидка действительна с {product[i].StartSellDate} до {product[i].EndSellDate}");
                                }
                                else Console.WriteLine("На данный товар нет особой скидки, либо она еще не началась или уже истекла");
                                Console.WriteLine("-------------------\n");
                            }
                        }
                        else { Console.WriteLine("Нет продуктов, возврат к началу"); goto start; };
                        Console.Write("Введите номер товара:");
                        int.TryParse(Console.ReadLine(), out var productnumber);
                        if (discount[1] != null)
                        {
                            for (var i = 1; i < numberofdiscounts; i++)
                            {
                                Console.WriteLine("-------------------\n");
                                Console.WriteLine($"{i}:{discount[i].Name}");
                                if (discount[i].ItsPercent == true)
                                {
                                    Console.WriteLine($"Стоимость cкидки:{discount[i].DiscountValue}%");
                                }
                                else Console.WriteLine($"Стоимость скидки:{discount[i].DiscountValue} руб.");
                                if (discount[i].ItsExpirable == true)
                                {
                                    if (discount[i].DiscountStartDate <= DateTime.Now && discount[i].DiscountEndDate > DateTime.Now)
                                    {
                                        Console.WriteLine($"Скидка действительна с {discount[i].DiscountStartDate} до {discount[i].DiscountEndDate}");
                                    }
                                    else Console.Write("Скидка не началась либо закончилась");
                                }
                            }
                        }
                        else { Console.Write("Нет скидок возврат к началу"); goto start; }
                        Console.Write("Введите номер скидки:");
                        int.TryParse(Console.ReadLine(), out var discountnumber);
                        Console.WriteLine(DiscountInformation(discountnumber, productnumber, product, discount));
                        goto start;
                    }
            }

        }
        public static void AddProduct(ref int count, Product[] product)
        {
            product[count] = new Product();
            Console.WriteLine("Введите название продукта");
            product[count].Name = Console.ReadLine();
            Console.WriteLine("Введите цену продукта");
            int.TryParse(Console.ReadLine(), out var price);
            while (price == 0)
            {
                Console.WriteLine("Стоимость продукта не была введена или введена с ошибкой, Введите стоимость");
                int.TryParse(Console.ReadLine(), out price);
            }
            product[count].Price = price;
            mark: Console.WriteLine("Есть ли скидка на продукт [y - да, n - нет]");
            var answer = Console.ReadLine();
            if (answer == "y") product[count].HaveDiscount = true;
            else if (answer == "n") product[count].HaveDiscount = false;
            else { Console.WriteLine("К сожалению неверный ввод"); goto mark; }
            if (product[count].HaveDiscount == true)
            {
                Console.WriteLine("Введите дату начала скидки");
                DateTime.TryParse(Console.ReadLine(), out var startselldate);
                if (startselldate != DateTime.MinValue)
                { 
                    product[count].StartSellDate = startselldate;
                }
                Console.WriteLine("Введите дату окончания скидки");
                DateTime.TryParse(Console.ReadLine(), out var endselldate);
                if (endselldate != DateTime.MinValue)
                {
                    product[count].EndSellDate = endselldate;
                }
                Console.WriteLine("Введите размер скидки в % на товар");
                int.TryParse(Console.ReadLine(), out var discountvalue);
                while(discountvalue>100)
                {
                    Console.WriteLine("Значение скидки не может быть больше 100, введите другое значение");
                    int.TryParse(Console.ReadLine(), out discountvalue);
                }
                product[count].DiscountValue = discountvalue;
            }
            if (product[count].HaveDiscount == true)
            {
                Console.WriteLine($"Товар был успешно добавлен, его цена с учетом скидки {product[count].DiscountValue} % - {product[count].Price - (product[count].Price * product[count].DiscountValue / 100)},\n " +
                    $"дата действия скидки с {product[count].StartSellDate} по {product[count].EndSellDate}");
            }
            else Console.WriteLine($"Товар был успешно добавлен, цена {product[count].Price}, на этот товар не действует особая скидка");
            count++;
        }
        public static void ShowProduct(int count, Product[] product)
        {
            if (product[1]!=null)
            {
                for (int i = 1; i < count; i++)
                {

                    Console.WriteLine("-----------------");
                    Console.Write("Название продукта:");
                    Console.WriteLine(product[i].Name);
                    Console.Write("Цена:");
                    Console.WriteLine(product[i].Price);
                    if(product[i].HaveDiscount == true && product[i].StartSellDate<=DateTime.Now && product[i].EndSellDate>DateTime.Now)
                    {
                        Console.Write($"Цена с учетом скидки {product[i].DiscountValue}%:{product[i].Price - (product[i].Price * product[i].DiscountValue / 100)}" +
                            $"\nСкидка действительна с {product[i].StartSellDate} до {product[i].EndSellDate}");
                    }
                    else { Console.WriteLine("На данный товар нет особой скидки, либо она еще не началась или уже истекла"); }
                }
                Console.WriteLine("-----------------");
            }
            else { Console.WriteLine("Нет продуктов");}
        }
        public static void AddDiscount(ref int count, Discount[] discount)
        {
            discount[count] = new Discount();
            Console.WriteLine("Введите название скидки");
            discount[count].Name = Console.ReadLine();
            mark1: Console.WriteLine("Процентная скидка ? [y - да, n - нет]");
            var answer = Console.ReadLine();
            if (answer == "y") discount[count].ItsPercent = true;
            else if (answer == "n") discount[count].ItsPercent = false;
            else { Console.WriteLine("К сожалению неверный ввод"); goto mark1; }
            if (discount[count].ItsPercent == true)
            {
                Console.WriteLine("Введите сумму скидки в %");
                int.TryParse(Console.ReadLine(), out var disc);
                while (disc > 100)
                {
                    Console.WriteLine("Значение скидки не может быть больше 100, введите другое значение");
                    int.TryParse(Console.ReadLine(), out disc);
                }
                discount[count].DiscountValue = disc;
            }
            else
            {
                Console.WriteLine("Введите сумму скидки в рублях");
                int.TryParse(Console.ReadLine(), out var disc);
                discount[count].DiscountValue = disc;
            }
            mark: Console.WriteLine("Истекаемая скидка ? [y - да, n - нет]");
            answer = Console.ReadLine();
            if (answer == "y") discount[count].ItsExpirable= true;
            else if (answer == "n") discount[count].ItsExpirable = false;
            else { Console.WriteLine("К сожалению неверный ввод"); goto mark; }
            if(discount[count].ItsExpirable == true)
            {
                Console.WriteLine("Введите дату начала скидки");
                DateTime.TryParse(Console.ReadLine(), out var discountstartdate);
                if (discountstartdate != DateTime.MinValue)
                {
                    discount[count].DiscountStartDate = discountstartdate;
                }
                Console.WriteLine("Введите дату окончания скидки");
                DateTime.TryParse(Console.ReadLine(), out var discountenddate);
                if (discountenddate != DateTime.MinValue)
                {
                    discount[count].DiscountEndDate = discountenddate;
                }
            }
            count++;
        }
        public static void ShowDiscount(int count, Discount[] discount)
        {
            if (discount[1] != null)
            {
                for (var i = 1; i < count; i++)
                {
                    Console.WriteLine("-----------------");
                    Console.Write("Название продукта:");
                    Console.WriteLine(discount[i].Name);
                    Console.Write("Размер скидки:");
                    if (discount[i].ItsPercent == true)
                    {
                        Console.WriteLine($"{discount[i].DiscountValue}%");
                    }
                    else Console.WriteLine($"{discount[i].DiscountValue} руб.");
                    if (discount[i].ItsExpirable == true)
                    {
                        Console.WriteLine($"Скидка действительна с {discount[i].DiscountStartDate} по {discount[i].DiscountEndDate}");
                    }
                    Console.WriteLine("\n-----------------");
                }
            }
        }
        static string DiscountInformation(int discnumb,int prodnumb, Product[] product, Discount[] discount)
        {
            if (product[prodnumb].HaveDiscount == false)
            {
                if (discount[discnumb].ItsPercent == true)
                {
                    if (discount[discnumb].ItsExpirable == false)
                    {
                        return $"Скидка составляет {discount[discnumb].DiscountValue}%\n" +
                            $"Цена на данный товар с выбранной скидкой составляет {product[prodnumb].Price - (product[prodnumb].Price * discount[discnumb].DiscountValue / 100)} руб.";
                    }
                    else if (discount[discnumb].DiscountStartDate <= DateTime.Now && discount[discnumb].DiscountEndDate >= DateTime.Now)
                    {
                        return $"Скидка составляет {discount[discnumb].DiscountValue}%\n" +
                     $"Цена на данный товар с выбранной скидкой составляет {product[prodnumb].Price - (product[prodnumb].Price * discount[discnumb].DiscountValue / 100)} руб." +
                     $"\nСкидка действительна с {discount[discnumb].DiscountStartDate} по {discount[discnumb].DiscountEndDate}";
                    }
                    else if(product[prodnumb].EndSellDate>DateTime.Now && discount[discnumb].DiscountStartDate <= DateTime.Now && discount[discnumb].DiscountEndDate >= DateTime.Now)
                    {
                        return $"Скидка составляет {discount[discnumb].DiscountValue}%\n" +
                     $"Цена на данный товар с выбранной скидкой составляет {product[prodnumb].Price - (product[prodnumb].Price * discount[discnumb].DiscountValue / 100)} руб." +
                     $"\nСкидка действительна с {discount[discnumb].DiscountStartDate} по {discount[discnumb].DiscountEndDate}";
                    }
                    else return $"Скидка недействительна Цена продукта:{product[prodnumb].Price}";
                }
                else if (discount[discnumb].ItsExpirable == false)
                {
                    return $"Скидка составляет {discount[discnumb].DiscountValue} руб.\n Цена со скидкой:{product[prodnumb].Price - (product[prodnumb].Price*product[prodnumb].DiscountValue/100) - discount[discnumb].DiscountValue} руб.";
                }
                else if (discount[discnumb].DiscountStartDate <= DateTime.Now && discount[discnumb].DiscountEndDate >= DateTime.Now)
                {
                    return $"Скидка составляет {discount[discnumb].DiscountValue} руб.\n" +
                     $"Цена на данный товар с выбранной скидкой составляет {product[prodnumb].Price - discount[discnumb].DiscountValue} руб." +
                     $"\nСкидка действительна с {discount[discnumb].DiscountStartDate} по {discount[discnumb].DiscountEndDate}";
                }
                else return $"Скидка недействительна Цена продукта:{product[prodnumb].Price}";
            }
            else if (discount[discnumb].ItsPercent == false)
            {
                if (discount[discnumb].ItsExpirable == false)
                {
                    return $"Скидка составляет {discount[discnumb].DiscountValue} руб.\n Цена со скидкой:{product[prodnumb].Price - discount[discnumb].DiscountValue} руб.";
                }
                else if (discount[discnumb].DiscountStartDate <= DateTime.Now && discount[discnumb].DiscountEndDate >= DateTime.Now)
                {
                    return $"Скидка составляет {discount[discnumb].DiscountValue} руб.\n" +
                     $"Цена на данный товар с выбранной скидкой составляет {product[prodnumb].Price - discount[discnumb].DiscountValue} руб." +
                     $"\nСкидка действительна с {discount[discnumb].DiscountStartDate} по {discount[discnumb].DiscountEndDate}";
                }
                else return $"Скидка недействительна Цена продукта:{product[prodnumb].Price}";
            }
            else return $"Процентная скидка на товар не суммируется с скидкой клиента - Цена товара:{product[prodnumb].Price-(product[prodnumb].Price*product[prodnumb].DiscountValue/100)}";
        }
    }
}
