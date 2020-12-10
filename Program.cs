using System;
using System.Reflection;

namespace CustomAttribute
{
    interface IInfoClass
    {
        public double Sum();
        public void Info();
        public void Set(double d1, double d2);
    }
    
    class MyTestClass: IInfoClass
    {
        public double d, f;

        public MyTestClass(double d, double f)
        {
            this.d = d;
            this.f = f;
        }

        public double Sum()
        {
            return d + f;
        }

        public void Info()
        {
            Console.WriteLine(@"d = {0} f = {1}",d,f);
        }

        public void Set(int a, int b)
        {
            d = (double)a;
            f = (double)b;
        }

        public void Set(double a, double b)
        {
            d = a;
            f = b;
        }

        public override string ToString()
        {
            return "MyTestClass";
        }
    }
    
    // В данном классе определены методы использующие рефлексию
    class Reflect
    {
        // Информация о полях и реализуемых интерфейсах 
        public static void FieldInterfaceInfo<T>(T obj) where T : class
        {
            Type t = typeof(T);
            Console.WriteLine("\n*** Реализуемые интерфейсы ***\n");
            var im = t.GetInterfaces();
            foreach (Type tp in im)
                Console.WriteLine("--> "+tp.Name);
            Console.WriteLine("\n*** Поля и свойства ***\n");
            FieldInfo[] fieldNames = t.GetFields();
            foreach (FieldInfo fil in fieldNames)
                Console.Write("--> "+fil.ReflectedType.Name + " " + fil.Name + "\n");
        }
        
        // Данный метод выводит информацию о содержащихся в классе методах
        public static void MethodReflectInfo<T> (T obj) where T: class
        {
            Type t = typeof(T);
            // Получаем коллекцию методов
            MethodInfo[] MArr = t.GetMethods(
                BindingFlags.Instance | 
                BindingFlags.Public);
            Console.WriteLine("*** Список методов класса {0} ***\n",obj.ToString());

            // Вывести методы.
            foreach (MethodInfo m in MArr)
            {
                Console.Write(" --> "+m.ReturnType.Name + " \t" + m.Name + "(");
                // Вывести параметры методов
                ParameterInfo[] p = m.GetParameters();
                for (int i = 0; i < p.Length; i++)
                {
                    Console.Write(p[i].ParameterType.Name + " " + p[i].Name);
                    if (i + 1 < p.Length) Console.Write(", ");
                }
                Console.Write(")\n");
            }
        }
    }
    
    
    /*
     *             Type t = typeof(T);
            // Получаем коллекцию методов
            MethodInfo[] MArr = t.GetMethods(
                BindingFlags.Instance | 
                BindingFlags.Public);
     */
    class Program
    {
        static void Main(string[] args)
        {
            //MyTestClass mtc = new MyTestClass(12.0,3.5);
            //Reflect.MethodReflectInfo<MyTestClass>(mtc);
            //Reflect.FieldInterfaceInfo<MyTestClass>(mtc);
            try
            {
                var assembly = Assembly.LoadFrom("dllsample.dll");
                // Находим типы.
                Type[] alltypes = assembly.GetTypes(); //BindingFlags.Instance | BindingFlags.Public
                Console.WriteLine("*** Найденые типы ***\n");
                foreach (Type t in alltypes)
                {
                    Console.WriteLine("-> "+t.Name);
                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }
    }
}