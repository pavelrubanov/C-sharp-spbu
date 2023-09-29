using System.Linq;

class Program
{
    public static bool IsPerfect(int n)
    {
        int sum = 0;
        for (int i = 1; i < n; i++)
        {
            if (n % i == 0)
                sum += i;
        }
        if (sum == n) 
            return true;
        else 
            return false;
    }
    static void Main()
    {
        int[] numbers = new int[10000000];
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = i + 1;
        }

        (from n in numbers.AsParallel()
                               .Where(n => IsPerfect(n))
                              select n).ForAll(Console.WriteLine);

    }
}

