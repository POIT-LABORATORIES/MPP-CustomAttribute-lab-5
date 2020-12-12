using System;
using System.Reflection;
using ExportClassAttr;

namespace CustomAttribute
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var assembly = Assembly.LoadFrom("dllsample.dll");
                // Находим типы.
                Type[] alltypes = assembly.GetTypes(); 
                Console.WriteLine("*** Найденые типы, соответствующие атрибуту ExportClass ***\n");
                foreach (Type t in alltypes)
                {
                    object[] attrs = t.GetCustomAttributes(false);
                    foreach (Attribute attr in attrs)
                    {
                        if (attr is ExportClass)
                        {
                            Console.WriteLine("-> "+t.Name);
                            // Получаем коллекцию методов
                            MethodInfo[] methodArray = t.GetMethods(
                                BindingFlags.Instance | BindingFlags.Public);
                            Console.WriteLine($"*** Список методов класса ${t.Name} ***\n");
                            // Вывести методы.
                            foreach (MethodInfo m in methodArray)
                            {
                                Console.Write(" --> "+m.ReturnType.Name + " \t" + m.Name + "(");
                                // Вывести параметры методов
                                ParameterInfo[] p = m.GetParameters();
                                for (int i = 0; i < p.Length; i++)
                                {
                                    Console.Write(p[i].ParameterType.Name + " " + p[i].Name);
                                    if (i + 1 < p.Length) Console.Write(", ");
                                }
                                Console.WriteLine(")");
                            }
                            
                            Console.WriteLine($"\n*** Поля и свойства класса ${t.Name} ***\n");
                            FieldInfo[] fieldNames = t.GetFields(
                                BindingFlags.Instance | BindingFlags.Public);
                            foreach (FieldInfo fil in fieldNames)
                                Console.Write("--> "+fil.ReflectedType.Name + " " + fil.Name + "\n");

                            Console.WriteLine($"\n*** Конструкторы класса ${t.Name} ***\n");
                            var constructors = t.GetConstructors(
                                BindingFlags.Instance | BindingFlags.Public);
                            foreach (var constructorInfо in constructors)
                            {
                                Console.Write(" --> " + constructorInfо.Name + "(");
                                var parameters = constructorInfо.GetParameters();
                                for (int i = 0; i < parameters.Length; i++)
                                {
                                    Console.Write(parameters[i].ParameterType.Name + " " + parameters[i].Name);
                                    if (i + 1 < parameters.Length) Console.Write(", ");
                                }
                                Console.Write(")\n");
                            }
                        }
                    }
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