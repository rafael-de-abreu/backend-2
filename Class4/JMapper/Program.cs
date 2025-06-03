using Ju.Mapper;
using Mapper;

internal class Program
{
    private static void Main(string[] args)
    {
        var employee = new Employee { Id = 1, Name = "Rafael", Department = "IT", BirthDay = new DateTime(1995, 1, 1) };

        var customer = JuMapper.Map<Customer>(employee);

        foreach(var prop in typeof(Customer).GetProperties())
        {
            Console.WriteLine($"{prop.Name} : {prop.GetValue(customer)}");
        }
    }
}