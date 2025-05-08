using Class2;

internal class Program
{
    private static void Main(string[] args)
    {
        var gameType = typeof(Game);

        var game = new Game();
        game.GetType();

        object obj = game;
        gameType = obj.GetType();

        foreach (var item in gameType.GetProperties())
        {
            Console.WriteLine(item.Name);
            Console.WriteLine(item.PropertyType.Name);
            foreach (var attribute in item.GetCustomAttributes(true))
            {
                Console.WriteLine(attribute.GetType().Name);
                if (attribute is SpecialDescriptionAttribute specialDescriptionAttribute)
                {
                    Console.WriteLine($"{specialDescriptionAttribute.Description}");
                }
            }
            Console.WriteLine("============================");
        }
    }
}