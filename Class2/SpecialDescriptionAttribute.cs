
namespace Class2
{
   public  class SpecialDescriptionAttribute : Attribute
    {
        public string Description { get; private set; }

        public SpecialDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
