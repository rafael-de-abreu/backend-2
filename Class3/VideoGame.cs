


namespace Class3
{
    public class VideoGame : IBussinessObject
    {

        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;

        [SpecialDescription("This is the name.")]
        public string Name { get; set; } = string.Empty;
        public Dictionary<string, int> Plataforms { get; set; }
    }
}
