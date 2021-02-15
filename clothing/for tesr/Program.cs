using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace for_tesr
{
    public enum Size
    {
        S,
        M,
        L,
        XL,
    }
    abstract public class Item
    {
        private double price;
        private string name;

        public double Price
        {
            get { return price; }
            set
            {
                if (value > 0) { value = price; }
                else { throw new Exception(); }
            }
        }

        public string Name
        {
            get { return name; }
            set { value = name; }
        }
        public Item(double _price, string _name)
        {
            price = _price;
            name = _name;
        }
    }

    public class Clothing_item : Item
    {
        private Size size;
        public Size sizes
        { get { return size; } }

        public Clothing_item(Size _size, double _price, string _name) : base(_price, _name)
        {
            size = _size;
        }
    }

    public class Electric_item: Item
    {
        private int power;
        public int Power
        { get { return power; } }

        public Electric_item(int _power,double _price,string _name) : base(_price,_name)
        {
            power = _power;
        }
    }
    public class Store
    {
        List<Clothing_item> clothes = new List<Clothing_item>();
        Electric_item[] electrics = new Electric_item[100];

        public int is_null(Electric_item [] electrics)
        {
            
            for (int i = 0; i < electrics.Length; i++)
            {
                if (electrics[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public void add_electric()
        {
                if (is_null(electrics) == -1)
                {
                    Console.WriteLine("no place");
                }
                else
                {
                        Console.WriteLine("add new electric");
                        Console.WriteLine("please enter name");
                        string name = Console.ReadLine();
                        Console.WriteLine("please enter price");
                        double price = double.Parse(Console.ReadLine());
                        Console.WriteLine("please enter power");
                        int power = int.Parse(Console.ReadLine());
                        Electric_item e = new Electric_item(power, price, name);
           }
        }
        public void get_cheapest_electric(Electric_item[] electrics)
        {
            int cell =0 ;
            double price = electrics[0].Price;
            for (int i = 1; i < electrics.Length; i++)
            {
             if(price> electrics[i].Price)
                {
                    cell = i;
                    price = electrics[i].Price;
                }
            }
            Console.WriteLine("electric item: name={0},price={1},power={2}", electrics[cell].Name, electrics[cell].Price,electrics[cell].Power);
        }

        public void Remove_Cheap_Electric(int min_price)
        {
            for (int i = 0; i < electrics.Length; i++)
            {
                if(electrics[i].Price<min_price)
                {
                    electrics[i] = null;
                }
            }
        }
     }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}

