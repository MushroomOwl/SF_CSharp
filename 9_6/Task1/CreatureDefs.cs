using CreatureCommons;

class Cat : Creature
{
    protected sealed override List<Food> EveryFood
    {
        get
        {
            return new List<Food>() {
                            Food.Vegetables,
                            Food.Fruits,
                            Food.Fish,
                            Food.Meat
                };
        }
    }

    protected sealed override List<int> PossibleLimbsCount
    {
        get
        {
            return new List<int>() { 4 };
        }
    }

    protected sealed override List<bool> CanHaveTail
    {
        get
        {
            return new List<bool>() { true };
        }
    }

    protected sealed override List<bool> CanFly
    {
        get
        {
            return new List<bool>() { false };
        }
    }
}
class Human : Creature
{
    protected sealed override List<Food> EveryFood
    {
        get
        {
            return new List<Food>() {
                            Food.Vegetables,
                            Food.Fruits,
                            Food.Fish,
                            Food.Meat,
                            Food.Junkfood,
                };
        }
    }

    protected sealed override List<int> PossibleLimbsCount
    {
        get
        {
            return new List<int>() { 4 };
        }
    }

    protected sealed override List<bool> CanHaveTail
    {
        get
        {
            return new List<bool>() { false };
        }
    }

    protected sealed override List<bool> CanFly
    {
        get
        {
            return new List<bool>() { false };
        }
    }
}
class Cow : Creature
{
    protected sealed override List<Food> EveryFood
    {
        get
        {
            return new List<Food>() {
                            Food.Vegetables,
                            Food.Fruits,
                };
        }
    }

    protected sealed override List<int> PossibleLimbsCount
    {
        get
        {
            return new List<int>() { 4 };
        }
    }

    protected sealed override List<bool> CanHaveTail
    {
        get
        {
            return new List<bool>() { true };
        }
    }

    protected sealed override List<bool> CanFly
    {
        get
        {
            return new List<bool>() { false };
        }
    }
}
class Insect : Creature
{
    protected sealed override List<Food> EveryFood
    {
        get
        {
            return new List<Food>() {
                            Food.Vegetables,
                            Food.Fruits,
                            Food.Fish,
                            Food.Meat,
                            Food.Junkfood,
                };
        }
    }

    protected sealed override List<int> PossibleLimbsCount
    {
        get
        {
            return new List<int>() { 4, 6, 8, 1000 };
        }
    }

    protected sealed override List<bool> CanHaveTail
    {
        get
        {
            return new List<bool>() { true, false };
        }
    }

    protected sealed override List<bool> CanFly
    {
        get
        {
            return new List<bool>() { true, false };
        }
    }
}