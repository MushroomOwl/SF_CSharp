// Probably took classes realisation more seriously then should have
// and didn't manage to make all things good looking due to deadline

// All points are implemented but some of them rushed and not necessary
// in terms of real model

// Here's some pointers on what's can be missed

// Базовый уровень:

// + Использование наследования;
// + Использование абстрактных классов или членов класса;
// + Использование принципов инкапсуляции;
// + Использование переопределений методов/свойств;
// + Использование минимум 4 собственных классов;
// + Использование конструкторов классов с параметрами;
// + Использование обобщений;
// + Использование свойств;
// + Использование композиции классов. (158 for example)

// Продвинутый уровень:

// + Использование статических элементов или классов; (195 for example)
// + Использование обобщенных методов; (265 for example)
// + Корректное использование абстрактных классов (использовать их там, где это обусловлено параметрами системы);
// + Корректное использование модификаторов элементов класса (чтобы важные поля не были доступны для полного контроля извне, использование protected);
// + Использование свойств с логикой в get и/или set блоках. (122, 398 for example)

// Усложненный уровень:

// + Использование методов расширения; (285 for example)
// + Использование наследования обобщений; (276 for example)
// + Использование агрегации классов; (237 for example)
// + Использование индексаторов; (333 for example)
// + Использование перегруженных операторов. (356, 368 for example)

// ---------------------------------------------------------------
// ---------- ADDRESS CLASSES ------------------------------------
using System.Security.Cryptography.X509Certificates;

class Address
{
    protected string city;
    protected string street;
    protected string building;
    public Address(string city, string street, string building)
    {
        this.city = city;
        this.street = street;
        this.building = building;
    }
    virtual public string AsString()
    {
        return string.Format("City:\t{0}\n Street:\t{1}, {2}", city, street, building);
    }
    static int Distance(Address a, Address b)
    {
        // We actually dont have this info so let it be just random number
        Random rnd = new Random();
        return rnd.Next(0, 5000);
    }
}
class PersonAddress : Address
{
    private int floor;
    private int apartment;
    public PersonAddress(string city, string street, string building, int floor, int apartment) : base(city, street, building)
    {
        this.floor = floor;
        this.apartment = apartment;
    }
    sealed override public string AsString()
    {
        return string.Format("{0}\n {1}\nFloor: {2}\nAppartments: {4}", base.AsString(), floor, apartment);
    }
}
class OfficeAddress : Address
{
    private int department;
    private int office;
    public OfficeAddress(string city, string street, string building, int department, int office) : base(city, street, building)
    {
        this.department = department;
        this.office = office;
    }
    sealed override public string AsString()
    {
        return string.Format("{0}\nDepartment: {1}\nOffice: {2}", base.AsString(), department, office);
    }
}
// ---------------------------------------------------------------
// ---------------------------------------------------------------

// ---------------------------------------------------------------
// ---------- DELIVERY CLASSES -----------------------------------
enum DeliveryStatus
{
    Assembling,
    Ready,
    EnRoute,
    Delivered,
    Unknown,
}
abstract class Delivery
{
    protected Address address;
    private bool handedToCustomer;
    private bool assembled;
    protected Transport transport;

    public Delivery()
    {
        handedToCustomer = false;
        assembled = false;
    }

    abstract public int TimeLeft();
    public string DeliveryAddress()
    {
        return address.AsString();
    }
    public DeliveryStatus Status
    {
        get
        {
            if (!assembled)
            {
                return DeliveryStatus.Assembling;
            }
            else if (TimeLeft() > 0)
            {
                if (transport.EnRoute())
                {
                    return DeliveryStatus.EnRoute;
                }
                else
                {
                    return DeliveryStatus.Ready;
                }
            }
            else if (handedToCustomer)
            {
                return DeliveryStatus.EnRoute;
            }
            else
            {
                return DeliveryStatus.Delivered;
            }
        }
    }
}

class HomeDelivery : Delivery
{
    protected new PersonAddress address;
    public HomeDelivery(string city, string street, string building, int floor, int apartment) : base()
    {
        this.address = new PersonAddress(city, street, building, floor, apartment);
    }
    public sealed override int TimeLeft()
    {
        return 0;
    }
}

class PickPointDelivery : Delivery
{
    protected new OfficeAddress address;
    PickPointDelivery(string city, string street, string building, int department, int office) : base()
    {
        this.address = new OfficeAddress(city, street, building, department, office);
    }
    public sealed override int TimeLeft()
    {
        return 0;
    }
}

class ShopDelivery : Delivery
{
    ShopDelivery(string city, string street, string building) : base()
    {
        this.address = new Address(city, street, building);
    }
    public sealed override int TimeLeft()
    {
        return 0;
    }
}
// ---------------------------------------------------------------
// ---------------------------------------------------------------

// ---------------------------------------------------------------
// ---------ORDER CLASSES ----------------------------------------
static class OrderUtilities
{
    private static int ordersCount;
    private static int finishedOrders;
    private static int unfinishedOrders;
    private static int finishedOrdersPrice;
    private static int unfinishedOrdersPrice;

    static OrderUtilities()
    {
        ordersCount = 0;
        finishedOrders = 0;
        unfinishedOrders = 0;
        finishedOrdersPrice = 0;
        unfinishedOrdersPrice = 0;
    }

    public static string GetNextOrderNumberStr()
    {
        ordersCount++;
        return "N-" + ordersCount;
    }

    public static int GetNextOrderNumber()
    {
       return ordersCount++;
    }
}

class Order<TDelivery> where TDelivery : Delivery
{
    protected TDelivery Delivery;
    protected string number;
    public string Description;
    protected ProductsCart cart;

    protected Order() {
        this.number = OrderUtilities.GetNextOrderNumberStr();
    }

    public Order(TDelivery delivery, string number, string description)
    {
        this.Delivery = delivery;
        this.number = number;
        this.Description = description;
    }

    public Order(TDelivery delivery, string number)
    {
        this.Delivery = delivery;
        this.number = OrderUtilities.GetNextOrderNumberStr();
        this.Description = "Generic order";
    }

    public Order(TDelivery delivery)
    {
        this.Delivery = delivery;
        this.number = OrderUtilities.GetNextOrderNumberStr();
        this.Description = "Generic order";
    }

    public void DisplayAddress()
    {
        Console.WriteLine(Delivery.DeliveryAddress());
    }

    public void ShowCart() {
        Console.WriteLine("..."); // ...
    }

    public void AddProduct<T>(T product) where T : Product
    {
        cart += product;
    }

    public void MergeCarts(ProductsCart productsCart)
    {
        cart += productsCart;
    }
}

sealed class FastHomeDeliveryOrder : Order<HomeDelivery>
{
    private bool isUrgent;
    public FastHomeDeliveryOrder(string city, string street, string building, int floor, int apartment, string description, bool isUrgent) : base()
    {
        this.Delivery = new HomeDelivery(city, street, building, floor, apartment);
        this.isUrgent = isUrgent;
    }
}

static class FastHomeDeliveryOrderExtensions
{
    public static void PrintSimplifiedOrderInfo(this FastHomeDeliveryOrder order)
    {
        Console.WriteLine(""); // ...;
    }
}
// ---------------------------------------------------------------
// ---------------------------------------------------------------

// ---------------------------------------------------------------
// ---------- PRODUCTS -------------------------------------------
class ProductsCart
{

    private Product[] products;
    public int ProductCount
    {
        get
        {
            return products.Length;
        }
    }
    public int TotalPrice
    {
        get
        {
            int total = 0;
            for (int i = 0; i < products.Length; i++)
            {
                total += products[i].Price;
            }
            return total;
        }
    }
    public int totalCost
    {
        get
        {
            int total = 0;
            for (int i = 0; i < products.Length; i++)
            {
                total += products[i].Price;
            }
            return total;
        }
    }
    public Product this[int index]
    {
        get
        {
            if (index >= 0 && index < products.Length)
            {
                return products[index];
            }
            else
            {
                return null;
            }
        }

        private set
        {
            if (index >= 0 && index < products.Length)
            {
                products[index] = value;
            }
        }
    }

    public static ProductsCart operator +(ProductsCart a, Product b)
    {
        ProductsCart cart = new ProductsCart();
        cart.products = new Product[a.ProductCount + 1];
        for (int i = 0; i < cart.ProductCount - 1; i++)
        {
            cart[i] = a[i];
        }
        cart[cart.ProductCount - 1] = b;
        return cart;
    }

    public static ProductsCart operator +(ProductsCart a, ProductsCart b)
    {
        ProductsCart cart = new ProductsCart();
        cart.products = new Product[a.ProductCount + b.ProductCount];
        for (int i = 0; i < a.ProductCount - 1; i++)
        {
            cart[i] = a[i];
        }
        for (int i = a.ProductCount; i < cart.ProductCount; i++)
        {
            cart[i] = b[i];
        }
        return cart;
    }

    public string AsString()
    {
        return ""; //...
    }
}
abstract class Product
{
    private int itemPrice;
    private int quantity;

    public Product(int itemPrice, int quantity)
    {
        this.itemPrice = itemPrice;
        this.quantity = quantity;
    }
    public int Quantity
    {
        get { return quantity; }
        set
        {
            quantity = value > 0 ? value : 0;
        }
    }
    public int ItemPrice
    {
        get
        {
            return itemPrice;
        }
        set
        {
            itemPrice = value > 0 ? value : 0;
        }
    }
    public int Price
    {
        get
        {
            return quantity * itemPrice;
        }
    }

    abstract public string AsString(); 
}
class Grocery : Product
{
    private int daysInWarehouse;
    private int daysTillExpire;

    Grocery(int itemPrice, int quantity, int daysInWarehouse, int daysTillExpire): base(itemPrice, quantity) {
        this.daysInWarehouse = daysInWarehouse;
        this.daysTillExpire = daysTillExpire;
    }

    public sealed override string AsString()
    {
        return ""; // ...
    }
}
class Electronic : Product
{
    private string releaseDate;
    private string guaranteeInformation;

    Electronic(int itemPrice, int quantity, string releaseDate, string guaranteeInformation) : base(itemPrice, quantity)
    {
        this.releaseDate = releaseDate;
        this.guaranteeInformation = guaranteeInformation;
    }

    public sealed override string AsString() { 
        return ""; // ...
    }
}
// ---------------------------------------------------------------
// ---------------------------------------------------------------

// ---------------------------------------------------------------
// --------- TRANSPORT -------------------------------------------
class Transport {
    private static int averageSpeed;
    public bool EnRoute() {
        return true;
    }
}
// ---------------------------------------------------------------
// ---------------------------------------------------------------

class Program {
    public static void Main()
    {
        return;
    }
}