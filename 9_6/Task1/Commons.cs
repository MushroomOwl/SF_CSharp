namespace CreatureCommons
{
    public enum Food
    {
        Vegetables,
        Fruits,
        Junkfood,
        Fish,
        Meat,
    }

    static public class FoodInfo
    {
        static public string Name(Food food)
        {
            switch (food)
            {
                case Food.Vegetables: return "Vegetables";
                case Food.Meat: return "Meat";
                case Food.Junkfood: return "Junkfood";
                case Food.Fruits: return "Fruits";
                case Food.Fish: return "Fish";
            }
            return "";
        }
    }
}