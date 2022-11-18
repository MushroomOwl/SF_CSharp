using CreatureCommons;
using CreatureExceptions;

abstract class Creature
{
    protected abstract List<Food> EveryFood { get; }
    protected abstract List<int> PossibleLimbsCount { get; }
    protected abstract List<bool> CanHaveTail { get; }
    protected abstract List<bool> CanFly { get; }

    private int? limbsCount;
    private bool? hasTail;
    private bool? flying;
    private Food[]? diet;

    public string? Name { get; set; }
    public int? LimbsCount
    {
        get { return limbsCount; }
        set
        {
            if (value == null || !PossibleLimbsCount.Contains((int)value))
            {
                Exception ex = new InvalidLimbsCountException(String.Format("Invalid limbs count for {0}", this.GetType().Name));
                ex.Data.Add("Input value for limbs", value);
                ex.Data.Add("Can have only", string.Join(",", PossibleLimbsCount));
                throw ex;
            }
            limbsCount = value;
        }
    }
    public bool? HasTail
    {
        get { return hasTail; }
        set
        {
            if (CanHaveTail.Count == 0 || CanHaveTail[0] != value)
            {
                if (this is Human && value == true)
                {
                    throw new HumansDontHaveTailsException("Humans don't have tails!");
                }
                else
                {
                    throw new MutantException(String.Format("{0} {1} tail!", this.GetType().Name, value == true ? "can't have" : "should have"));
                }
            }
            hasTail = value;
        }
    }
    public bool? Flying
    {
        get { return flying; }
        set
        {
            if (CanHaveTail.Count == 0 && CanHaveTail[0] != value)
            {
                if (this is Cat && value == true)
                {
                    throw new CatsCantFlyException("Cats cant fly!");
                }
                else
                {
                    throw new MutantException(String.Format("{0} {1} fly!", this.GetType().Name, value == true ? "can't" : "actually can"));
                }
            }
            flying = value;
        }
    }
    public Food[]? Diet
    {
        get { return diet; }
        set
        {
            if (value == null || value.Length == 0)
            {
                throw new MutantException("Every creature eats something!");
            }
            foreach (Food food in value)
            {
                if (!EveryFood.Contains(food))
                {
                    Exception ex = new CantEatThatException(String.Format("{0} can't eat {1}", this.GetType().Name, FoodInfo.Name(food)));
                    string foods = FoodInfo.Name(EveryFood[0]);
                    for (int i = 1; i < EveryFood.Count; i++)
                    {
                        foods += ", " + FoodInfo.Name(EveryFood[i]);
                    }
                    ex.Data.Add("Can eat", string.Join(",", foods));
                    throw ex;
                }
                diet = value;
            }
        }
    }

    public void Info()
    {
        if (Name == null || LimbsCount == null || HasTail == null || Flying == null || Diet == null)
        {
            Exception ex = new MissingFieldException(String.Format("Missing {0} info.", this.GetType().Name));
            if (Name == null) ex.Data.Add("Missing data", "Name");
            if (LimbsCount == null) ex.Data.Add("Missing data", "LimbsCount");
            if (HasTail == null) ex.Data.Add("Missing data", "HasTail");
            if (Flying == null) ex.Data.Add("Missing data", "Flying");
            if (Diet == null) ex.Data.Add("Missing data", "Diet");
            throw ex;
        }
    }
}