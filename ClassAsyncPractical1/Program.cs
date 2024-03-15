using System.Numerics;
using System.Threading.Tasks;

namespace ClassAsyncPractical1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("1 - Task1, 2 - Task2, 3 - Task3, 4 - Task4: ");

            int input;
            int.TryParse(Console.ReadLine(), out input);

            Console.Clear();

            switch (input)
            {
                case 1: await Task1(); break;

                case 2: await Task2(); break;

                case 3: await Task3(); break;

                case 4: await Task4(); break;
            }
        }

        public static async Task Task1()
        {
            Console.Write("1 - Factorial, 2 - Number of digits, 3 - Sum of digits: ");

            int input;
            int.TryParse(Console.ReadLine(), out input);

            Console.Write("Enter number: ");

            long number;
            long.TryParse(Console.ReadLine(), out number);

            long result = default;

            if (input == 1)
            {
                result = await CalcFactorial(number);
            }
            else if(input == 2)
            {
                result = await CountDigits(number);
            }
            else if(input == 3)
            {
                result = await SumDigits(number);
            }

            Console.WriteLine(result);
        }

        public static async Task Task2()
        {
            int start;
            Console.Write("Enter start range: ");
            int.TryParse(Console.ReadLine(), out start);

            int end;
            Console.Write("Enter end range: ");
            int.TryParse(Console.ReadLine(), out end);

            using (StreamWriter writer = new StreamWriter("multitable.txt"))
            {
                Parallel.For(start, end + 1, (i) =>
                {
                    for (int j = 1; j <= 10; j++)
                    {
                        writer.WriteLine($"{i} * {j} = {i * j}");
                    }
                });
            }
        }

        public static async Task Task3()
        {
            List<int> arr = new List<int>();

            LoadTheNumbersFile(arr);

            Parallel.ForEach(arr, (number) =>
            {
                Console.WriteLine($"!{number} = {CalcFactorial(number).Result}");
            });
        }

        public static async Task Task4()
        {
            List<int> arr = new List<int>();

            LoadTheNumbersFile(arr);

            Console.WriteLine($"Sum - {arr.AsParallel().Sum()}, Max - {arr.AsParallel().Max()}, Min - {arr.AsParallel().Min()}");
        }

        public static void LoadTheNumbersFile(List<int> arr)
        {
            using (StreamReader reader = new StreamReader("numbers.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (int.TryParse(line, out int number))
                    {
                        arr.Add(number);
                    }
                }
            }
        }

        public static async Task<long> CalcFactorial(long n)
        {
            long fact = 1;
            Parallel.For(1, n + 1, i =>
            {
                fact *= i;
            });
            return fact;
        }

        public static async Task<long> CountDigits(long value)
        {
            long count = 0;
            Parallel.For(0, (int)Math.Floor(Math.Log10(value)) + 1, (i) =>
            {
                count++;
            });
            return count;
        }

        public static async Task<long> SumDigits(long value)
        {
            long sum = 0;
            string num = value.ToString();
            Parallel.ForEach(num, c =>
            {
                sum += long.Parse(c.ToString());
            });
            return sum;
        }
    }
}
