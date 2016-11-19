using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class a
{
    int i = 0;
    int j = 100;
}

public class test_openo
{
    bool isTrue = true;
    bool isFalse = false;


    public void Start()
    {
        isTrue = this.os("isTsdfsdfrue", "Test").o("test", 50).As<bool>();
        Console.WriteLine(this.os("isFalse", "isTrue").As<bool>());
    }


    void Update()
    {

    }

    bool Test(string name, int size)
    {
        isTrue = false;
        Console.WriteLine(isTrue);
        Console.WriteLine(size);
        return false;
    }
}


namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new test_openo();
            c.os("Start", "Test").o();

            var a = new a();
            var b = a.o("j").As<int>();
            var i = 0;
        }
    }
}
