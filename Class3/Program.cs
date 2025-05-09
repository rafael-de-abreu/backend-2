using Class3;

internal class Program
{
    private static void Main(string[] args)
    {
        var videoGameType = typeof(VideoGame);

        var videoGame = new VideoGame();
        videoGame.GetType();

        object obj = videoGame;
        videoGameType = obj.GetType();

        WriteProperties(videoGame);
    }

    public static void WriteProperties<T>(T instance) where T : IBussinessObject
    {
        foreach(var item in typeof(T).GetProperties())
        {
            Console.WriteLine(item.Name);
            Console.WriteLine(item.PropertyType.Name);

            foreach(var param in item.PropertyType.GenericTypeArguments)
            {
                Console.WriteLine($"-> Generic argument type: {param.Name}");
            }

            foreach(var attribute in item.GetCustomAttributes(true))
            {
                Console.WriteLine(attribute.GetType().Name);
                if (attribute is SpecialDescriptionAttribute specialDescriptionAttribute)
                {
                    Console.WriteLine($"{specialDescriptionAttribute.Description}");
                }
            }

            Console.WriteLine("===================================");
        }
    }
}