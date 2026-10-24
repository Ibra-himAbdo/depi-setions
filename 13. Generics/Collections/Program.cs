using System.Collections;

namespace Collections
{
    // ArrayList done
    // List<T> done
    // Dictionary<T1,T2> done
    // SortedList<T1,T2> done
    // HashSet<T> done
    // Stack<T> - Stack
    // Queue<T> - Queue

    internal class Program
    {
        private static void Main(string[] args)
        {
            #region arrayList

            //System.Collections.ArrayList arrayList = new System.Collections.ArrayList();

            //arrayList.Add(1);
            //arrayList.Add("556645");
            //arrayList.Add(true);
            //arrayList.Add(5654l);
            //arrayList.Add(DateTime.Now);
            //arrayList.Add(int.MaxValue);

            //foreach (var item in arrayList)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine("--");

            //Console.WriteLine(arrayList[0]);
            //Console.WriteLine("++");
            //Console.WriteLine("+++");
            //Console.WriteLine("+++");

            //arrayList.RemoveAt(0);
            //arrayList.Remove(true);

            //foreach (var item in arrayList)
            //{
            //    Console.WriteLine(item);
            //}

            #endregion arrayList

            #region List

            //int[] arr = { 1, 4, 5 };

            //List<int> ints = new List<int>(arr);

            //ints.Add(6);
            //ints.Add(7);
            //ints.Add(8);

            //foreach (var item in ints)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine(ints[4]);
            //Console.WriteLine("+++");
            //Console.WriteLine("+++");
            //Console.WriteLine("+++");
            //ints.RemoveAt(0);
            //ints.Remove(8);

            //List<int> list2 = new List<int> { 1, 4, 5 };

            //ints.AddRange(list2);

            //foreach (var item in ints)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine(ints.Contains(8));

            //ints.Sort();
            //foreach (var item in ints)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine("--------------------");

            //List<int> a = new();
            //List<int> b = a;

            //a.Add(1);

            //Console.WriteLine(a.Count);
            //Console.WriteLine(b.Count);
            //Console.WriteLine(a.Capacity);
            //Console.WriteLine(b.Capacity);

            #endregion List

            #region Dictionary
            //Dictionary<int, string> memory = new();

            //memory.Add(500,"ahmed");
            //memory.Add(600,"MOMO");
            //memory.Add(400,"MOMO");
            //memory.Add(300,"MOMO");
            //memory.Add(700,"MOMO");

            //if(memory.TryAdd(500, "55"))
            //    Console.WriteLine("added");

            //foreach (KeyValuePair<int, string> item in memory)
            //{
            //    Console.WriteLine(item.Key + " " + item.Value);
            //}
            //string? input = Console.ReadLine();

            //if (string.IsNullOrWhiteSpace(input))
            //{
            //    Console.WriteLine("Canot get value for it");
            //    return;
            //}

            //if (memory.TryGetValue(int.Parse(input), out string? value))
            //    Console.WriteLine(value); 
            #endregion

            #region SortedDictionary - SotredList // => to do SortedDictionary vs SotredList
            //SortedDictionary<int, string> memory = new();

            //memory.Add(500, "ahmed");
            //memory.Add(600, "MOMO");
            //memory.Add(400, "MOMO");
            //memory.Add(300, "MOMO");
            //memory.Add(700, "MOMO");

            //if (memory.TryAdd(500, "55"))
            //    Console.WriteLine("added");

            //foreach (KeyValuePair<int, string> item in memory)
            //{
            //    Console.WriteLine(item.Key + " " + item.Value);
            //}
            //string? input = Console.ReadLine();

            //if (string.IsNullOrWhiteSpace(input))
            //{
            //    Console.WriteLine("Canot get value for it");
            //    return;
            //}

            //if (memory.TryGetValue(int.Parse(input), out string? value))
            //    Console.WriteLine(value); 
            #endregion


            #region HashSet
            //int[] arr = { 1, 4, 5 };

            //HashSet<int> ints = new HashSet<int>(arr);

            //ints.Add(6);
            //ints.Add(1);
            //ints.Add(1);
            //ints.Add(1);
            //ints.Add(1);
            //ints.Add(1);
            //ints.Add(7);
            //ints.Add(8);

            //foreach (var item in ints)
            //{
            //    Console.WriteLine(item);
            //} 
            #endregion

            int[] arr = { 1, 4, 5 };

            Stack<int> stack1 = new Stack<int>(arr);

            stack1.Push(7);
            stack1.Push(8);
            stack1.Push(100);

            foreach (var item in stack1)
            {
                Console.WriteLine(item);
            }

            int last = stack1.Pop();
            Console.WriteLine("-----");
            Console.WriteLine(last);
            Console.WriteLine("-----");
            foreach (var item in stack1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("-----Peck");
            Console.WriteLine(stack1.Peek());
            Console.WriteLine("-----"); // LIFO
            Console.WriteLine("----------------++++++++++++----------- queue1");
            Queue<int> queue1 = new Queue<int>(arr); // FIFO

            queue1.Enqueue(7);
            queue1.Enqueue(8);
            queue1.Enqueue(100);
            foreach (var item in queue1)
            {
                Console.WriteLine(item);
            }

            int _1st = queue1.Dequeue();
            Console.WriteLine();
            Console.WriteLine(_1st);
            Console.WriteLine();
            foreach (var item in queue1)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(queue1.Count + " " + stack1.Count);
        }
    }
}