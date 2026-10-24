using System.Collections;

namespace Collections
{
    // ArrayList
    // List<>
    // Dictionary<>
    // SortedList<>
    // Stack<> 
    // Queue<>
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[] arr2 = { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };


            #region ArrayList
            //System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
            //arrayList.Add(1);
            //arrayList.Add('a');
            //arrayList.Add("the");
            //arrayList.Add(true);
            //arrayList.Add(2.5);
            //arrayList.Add("lkasdka");
            //arrayList.Add(new Eneity("asdas"));
            //arrayList.AddRange(arr);

            //foreach (object? item in arrayList)
            //{
            //    Console.WriteLine(item + " - " + item.GetType());
            //}
            //Console.WriteLine();
            //Console.WriteLine(arrayList[0]);
            //arrayList.RemoveAt(0);
            //Console.WriteLine(arrayList[0]);
            //arrayList.Remove(2.5);
            //Console.WriteLine();
            //foreach (object? item in arrayList)
            //{
            //    Console.WriteLine(item + " - " + item.GetType());
            //} 
            #endregion

            #region List
            //List<int> list = new List<int>(arr);

            //list.Add(100);
            //list.AddRange(arr2);

            //list.Remove(11);
            //list.RemoveAt(5);
            ////list.Clear();

            //if(list.Contains(101))
            //    Console.WriteLine("True");

            //list.Sort();

            //foreach (int item in list)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine();
            //Console.WriteLine(list.Count); 
            //Console.WriteLine(list[0]); 
            // 
            #endregion

            #region Dictionary & SortedList 
            //Dictionary<int, string> dictionary1 = new Dictionary<int, string>();

            //dictionary1.Add(1, "Ahmed");
            //dictionary1.Add(800, "Momo");
            //dictionary1.Add(80, "900");


            ////dictionary1.TryAdd(1, "Ah2326");

            //dictionary1[800] = "abc";

            //foreach (KeyValuePair<int, string> item in dictionary1)
            //{
            //    Console.WriteLine(item.Key + " --- " + item.Value);
            //}

            //SortedList<int, string> dictionary1 = new SortedList<int, string>();

            //dictionary1.Add(1, "Ahmed");
            //dictionary1.Add(800, "Momo");
            //dictionary1.Add(80, "900");


            ////dictionary1.TryAdd(1, "Ah2326");

            //dictionary1[800] = "abc";

            //foreach (KeyValuePair<int, string> item in dictionary1)
            //{
            //    Console.WriteLine(item.Key + " --- " + item.Value);
            //} 
            #endregion

            #region stack
            //Stack<int> stack1 = new Stack<int>(arr); // LIFO

            //stack1.Push(10001);

            //foreach (var item in stack1)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine();

            //Console.WriteLine(stack1.Pop());

            //Console.WriteLine();
            //foreach (var item in stack1)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine();

            //Console.WriteLine(stack1.Peek());
            //Console.WriteLine();
            //foreach (var item in stack1)
            //{
            //    Console.WriteLine(item);
            //} 
            #endregion

            Queue<int> queue1 = new Queue<int>(arr2); // FIFO

            queue1.Enqueue(101010);

            foreach (int item in queue1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            Console.WriteLine(queue1.Dequeue());
            Console.WriteLine();
            foreach (int item in queue1)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(queue1.Peek());
        }
    }

    class Eneity
    {
        public string Name { get; set; }
        public Eneity(string name)
        {
            Name = name;
        }
    }
}
