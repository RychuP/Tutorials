namespace AttributeTest
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [Obsolete("ThisClass is obsolete. Use ThisClass2 instead.")]
    [MyPrecious("Lord of the Rings")]
    class Program
    {
        static void Main()
        {
            TypeInfo typeInfo = typeof(Program).GetTypeInfo();
            Console.WriteLine("The assembly qualified name of the class Program is {0}\n", typeInfo.AssemblyQualifiedName);

            var attrs = typeInfo.GetCustomAttributes();
            foreach (var attr in attrs)
            {
                Console.WriteLine("Attribute on Program class: {0}", attr);
            }
            Console.WriteLine();

            MyUIClass mc = new();
            mc.PropertyChanged += McPropChangedInfo;

            mc.Name = "Adam";
            Console.WriteLine(mc.Name);
            mc.Name = "Lambert";
            Console.WriteLine(mc.Name);

            mc.PropertyChanged -= McPropChangedInfo;

            mc.Name = "Lisa";
            Console.WriteLine(mc.Name);
        }

        static void McPropChangedInfo(object sender, PropertyChangedEventArgs args)
        {
            Console.WriteLine(sender.GetType().Name + " is notifying of a change of " + args.PropertyName + " property.");
        }
    }

    public class MyUIClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    RaisePropertyChanged();   // notice that "Name" is not needed here explicitly
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    class MyPrecious : Attribute
    {
        public MyPrecious(string s)
        {
            Console.WriteLine("MyPrecious is being created with a msg {0}", s);
        }
    }
}