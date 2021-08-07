using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Reflection;
namespace LateBindingApp
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("fun with late Binding");
            // try to load a local copy of CarLibrary
            Assembly a = null;
            try
            {
                a = Assembly.Load("CarLibrary");

            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            if (a != null)
                CreateUsingLateBinding(a);
            InvokeMethodWithArgsUsingLateBinding(a);

        }

        private static void CreateUsingLateBinding(Assembly asm)
        {
            try
            {
                //get metadata for the MiniVan tye
                Type minivan = asm.GetType("CarLibrary.MiniVan");

                //creat a minivan instance on the fly
                object obj = Activator.CreateInstance(minivan);
                Console.WriteLine("Create a {0} using late binding", obj);
                //get info for TurboBoost
                MethodInfo mi = minivan.GetMethod("TurboBoost");

                //invoke the method. (null for no parameters)
                mi.Invoke(obj, null);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void InvokeMethodWithArgsUsingLateBinding(Assembly asm)
        {
            try
            {
                //get metadata for the MiniVan tye
                Type sports = asm.GetType("CarLibrary.SportCar");

                //creat a minivan instance on the fly
                object obj = Activator.CreateInstance(sports);
                Console.WriteLine("Create a {0} using late binding", obj);
                //get info for TurboBoost
                MethodInfo mi = sports.GetMethod("TurnOnRadio");

                //invoke the method. (null for no parameters)
                mi.Invoke(obj, new object[] { true,2});

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
