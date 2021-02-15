using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_3
{
    public enum GazType
    {
        cooking_gaz,
        soler,
        benzin,
    }
    public class Vehicle
    {
        private int rishuy;
        private GazType gaz_type;

        public Vehicle(int _rishuy, GazType _gaz_type)
        {
            rishuy = _rishuy;
            gaz_type = _gaz_type;
        }
        public int Rishuy
        {
            get { return rishuy; }

        }
        public GazType Gaz_Type
        {
            get { return gaz_type; }

        }

        public virtual void Print()
        {
            Console.WriteLine("vehicle#{0}", rishuy);
            Console.WriteLine("rides on{0}", gaz_type);
        }
        public static GazType ParseGazType(string str)
        {

            if (str == "COOKING_GAZ")
            {
                return GazType.cooking_gaz;
            }
            else if (str == "BENZIN")
            {
                return GazType.benzin;
            }
            else if (str == "SOLER")
            {
                return GazType.soler;
            }

            else
            {
                throw new Exception(string.Format("Can not parse value {0} into a GasType", str));
            }

        }
        public static bool Parse(string str, ref Vehicle res)
        {
            string[] stam = { "|" };
            string[] split = str.Split(stam, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length; i++)
            {
                split[i] = split[i].Trim();
            }
            long splitCount = split.LongCount();


            if (splitCount < 2 || splitCount > 4)
            {
                res = null;
                return false;
            }

            if (splitCount == 4)
            {
                if (split[0] == "-1")
                {
                    try
                    {
                        int rishuy = int.Parse(split[0]);
                        GazType gt = ParseGazType(split[1]);
                        string name = split[2];
                        int lenght = int.Parse(split[3]);
                        res = new Boat(gt, lenght, name, rishuy);
                        return true;

                    }
                    catch (Exception)
                    {
                        res = null;
                        return false;
                    }
                }
            }
            
                if (splitCount == 3)
                {
                    try
                    {
                        int rishuy = int.Parse(split[0]);
                        GazType gt = ParseGazType(split[1]);
                        ConsoleColor cc = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), split[2]);
                        res = new MotorCycle(rishuy, gt, cc);
                        return true;
                    }
                    catch (Exception)
                    {
                        res = null;
                        return false;
                    }
                }

                if (splitCount == 2)
                {
                    try
                    {
                        int rishuy = int.Parse(split[0]);
                        GazType gt = ParseGazType(split[1]);
                        res = new Vehicle(rishuy, gt);
                        return true;

                    }
                    catch (Exception)
                    {
                        res = null;
                        return false;
                    }
                }

          
            return true;
        }
    }

    public class MotorCycle : Vehicle
    {
        private ConsoleColor color;
        public MotorCycle(int _rishuy, GazType _gaz_type, ConsoleColor _color)
               : base(_rishuy, _gaz_type)
        {
            color = _color;
        }
        public void Paint(ConsoleColor change)
        {
            color = change;
        }
        public override void Print()
        {
            Console.ForegroundColor = this.color;
            base.Print();
            Console.WriteLine("color:{0}", color);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public class Boat : Vehicle
    {
        private int length;
        private string name;

        public Boat(GazType _gaz_type, int _length, string _name, int _rishuy) : base(-1, _gaz_type)
        {
            length = _length;
            name = _name;
        }
        public int Length
        {
            get { return length; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public override void Print()
        {

            base.Print();
            Console.WriteLine("boat name is {0}", name);
            Console.WriteLine("boat length is:{0}", length);
            for (int i = 0; i < length; i++)
            {
                Console.Write("*");
            }

        }
    }
    class Program
    {
        static bool Predicate (Boat b)
        {
            if (b.Length % 3 == 0)
            {
                return true;
            }

            else return false;
        }
        static void Main(string[] args)
        {
            int counterT = 0;
            int counterF = 0;
            Vehicle vr = new Vehicle(0, GazType.benzin);
            List<Vehicle> VH = new List<Vehicle>();
   
           

           StreamReader sr = new StreamReader(@"C:\Users\ety\Documents\Visual Studio 2015\Projects\homework_3\313234189_data.txt");
            string line = sr.ReadLine();
            while (line != null)
            {
                if (Vehicle.Parse(line, ref vr))
                {
                    counterT++;
                    VH.Add(vr);

                }                
                else
                {
                    counterF++;
                }
                line = sr.ReadLine();
            }
          
            Console.WriteLine("number of true:" + counterT);
            Console.WriteLine("number of false:" + counterF);
            List<Vehicle> vehicle_list = new List<Vehicle>();
            List<Boat> ba = new List<Boat>();
            List<MotorCycle> mc = new List<MotorCycle>();

            foreach (Vehicle v in VH)
            {
               
                if (v is MotorCycle)
                {
                    mc.Add((MotorCycle)v);
                }
                if (v is Boat)
                {
                    ba.Add((Boat)v);
                    
                }
                else if (v is Vehicle)
                {
                   vehicle_list.Add((Vehicle)v);

                }
            }
            Dictionary<int, int> motor = new Dictionary<int, int>();
           
         foreach (MotorCycle mtor in mc)
            {
                if(!motor.ContainsKey(mtor.Rishuy))
                {
                    motor.Add(mtor.Rishuy, 1);
                }
                else
              {
                    motor[mtor.Rishuy]++;
                }
            }
            var list = motor.Keys.ToList();
            list.Sort();
            using (StreamWriter sw = new StreamWriter(@"C:\Users\ety\Documents\Visual Studio 2015\Projects\homework_3\MyOutput.txt", false))
            {
                
                sw.WriteLine("number of true:{0}", counterT);
                sw.WriteLine("number of false:{0}", counterF);
                ba.RemoveAll(Predicate);
                sw.WriteLine("Length of remaining Boats={0}", ba.Count);
                foreach (var item in list)
                {
                    sw.WriteLine(string.Format("{0}-{1}",item,motor[item]));
                }
            }

        }
    }
}