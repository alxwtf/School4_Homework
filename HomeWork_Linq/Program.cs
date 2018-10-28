using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            List<People> people = new List<People>
            {
                new People
                {
                    Name ="Иван",
                    Age = 31,
                    Sex = "Male",
                    Balance = 400
                },
                new People
                {
                    Name ="Женя",
                    Age = 24,
                    Sex = "Male",
                    Balance = 21000
                },
                new People
                {
                    Name ="Даша",
                    Age = 22,
                    Sex = "Female",
                    Balance = 570
                },
                new People
                {
                    Name ="Леша",
                    Age = 25,
                    Sex = "Male",
                    Balance = 14758
                },
                new People
                {
                    Name ="Соня",
                    Age = 27,
                    Sex = "Female",
                    Balance = 4792
                }
            };
            var query1 = people.
                OrderByDescending(x => x.Age).
                FirstOrDefault();
            Console.WriteLine($"Самый старший:{query1.Name}");
            var query2 = people.
                OrderByDescending(x => x.Balance).
                FirstOrDefault();
            Console.WriteLine($"Самый богатый:{query2.Name}, его баланс:{query2.Balance}");
            var query3 = people.
                OrderByDescending(x=>x.Age).
                ThenByDescending(x=>x.Balance).
                FirstOrDefault();
            Console.WriteLine($"Самый старший и богатый:{query3.Name}, его возраст:{query3.Age}, баланс:{query3.Balance}");
            var query4 = people.
                Where(x => x.Balance > 4000).
                Count();
            Console.WriteLine($"Кол-во человек с балансом больше 4000 Руб:{query4}");
            var query5 = people.
                OrderBy(x => x.Age);
            Console.WriteLine("Сортировка по возрасту");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("|Name|Age|  Sex  |Balance");
            foreach(var humans in query5)
            {
                Console.WriteLine($"|{humans.Name}|{humans.Age} | {humans.Sex}|    {humans.Balance}");
            }
            var query6 = people.
                OrderBy(x => x.Sex);
            Console.WriteLine("Сортировка по полу");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("|Name|Age|  Sex  |Balance");
            foreach (var humans in query6)
            {
                Console.WriteLine($"|{humans.Name}|{humans.Age} | {humans.Sex}|    {humans.Balance}");
            }
            var query7 = people.
                OrderBy(x => x.Balance);
            Console.WriteLine("Сортировка по балансу");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("|Name|Age|  Sex  |Balance");
            foreach (var humans in query7)
            {
                Console.WriteLine($"|{humans.Name}|{humans.Age} | {humans.Sex}|    {humans.Balance}");
            }
            Console.ReadLine();
        }
    }
}
