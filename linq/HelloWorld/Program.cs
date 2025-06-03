using HelloWorld;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        //var customers = CustomerData.Customers;
        //var filterByCountries = customers.Where(x => x.Country == "Brazil" || x.Country == "United States of America");
        //Console.WriteLine("--------------COUNTRIES CUSTUMERS------------------------");
        //foreach (var customerCountry in filterByCountries)
        //{
        //    Console.WriteLine($"ID: {customerCountry.ID}");
        //    Console.WriteLine($"FirstName: {customerCountry.FirstName}");
        //    Console.WriteLine($"LastName: {customerCountry.LastName}");
        //    Console.WriteLine($"Email: {customerCountry.Email}");
        //    Console.WriteLine($"Phone: {customerCountry.PhoneNumber}");
        //    Console.WriteLine($"Address1: {customerCountry.AddressLine1}");
        //    Console.WriteLine($"Address2: {customerCountry.AddressLine2}");
        //    Console.WriteLine($"City: {customerCountry.City}");
        //    Console.WriteLine($"PostalCode: {customerCountry.PostalCode}");
        //    Console.WriteLine($"Country: {customerCountry.Country}");
        //    Console.WriteLine("--------------------------------------");
        //}
        //Console.WriteLine("--------------COUNTRIES CUSTUMERS------------------------");

        //var filterByFirstLetter = customers.Where(x => x.FirstName.StartsWith("A"));
        //Console.WriteLine("--------------FIRST NAME A CUSTUMERS------------------------");
        //foreach (var customer in filterByFirstLetter)
        //{
        //    Console.WriteLine($"ID: {customer.ID}");
        //    Console.WriteLine($"FirstName: {customer.FirstName}");
        //    Console.WriteLine($"LastName: {customer.LastName}");
        //    Console.WriteLine($"Email: {customer.Email}");
        //    Console.WriteLine($"Phone: {customer.PhoneNumber}");
        //    Console.WriteLine($"Address1: {customer.AddressLine1}");
        //    Console.WriteLine($"Address2: {customer.AddressLine2}");
        //    Console.WriteLine($"City: {customer.City}");
        //    Console.WriteLine($"PostalCode: {customer.PostalCode}");
        //    Console.WriteLine($"Country: {customer.Country}");
        //    Console.WriteLine("--------------------------------------");
        //}

        Order[] orders = [
            new Order { Id = 1, CustomerId = 140, OrderDescription = "Coke" },
            new Order { Id = 2, CustomerId = 10, OrderDescription = "Tea" },
            new Order { Id = 3, CustomerId = 15, OrderDescription = "Coffee" },
            new Order { Id = 4, CustomerId = 70, OrderDescription = "Milk" },
        ];

        var groupedCustomers = CustomerData.Customers
        .GroupBy(c => c.Country);

        foreach (var group in groupedCustomers)
        {
            Console.WriteLine($"Country: {group.Key}");
            foreach (var customer in group)
            {
                Console.WriteLine($" - {customer.FirstName} {customer.LastName}");
            }
        }

        



        //int[] numbers = [0, 2, 3, 4, 7, 8, 9];

        ////var firstNumber = numbers.FirstOrDefault();
        ////var orderBy = numbers.OrderBy();
        //var whereQuery = numbers.Where(x => x > 3);

        //whereQuery.ToList().ForEach(x =>  Console.WriteLine(x));
    }
}