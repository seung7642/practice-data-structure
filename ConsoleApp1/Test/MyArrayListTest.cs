using System;
using MyDataStructure;

namespace Test
{
    public class MyArrayListTest
    {
        public static void Test()
        {
            MyArrayList list = new MyArrayList();
            Init(ref list);
            
            ForeachTest(list);
        }

        private static void ForeachTest(in MyArrayList list)
        {
            // IEnumerable 인터페이스를 구현해야 foreach 구문을 사용할 수 있게 된다.
            // IEnumerable#GetEnumerator 메서드는 IEnumerator를 반환한다. 
            foreach (var item in list)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
        }

        private static void Init(ref MyArrayList list)
        {
            list.Add("Hello");
            list.Add("C#");
            list.Add("World");
            list.Add(100);
        }
    }
}