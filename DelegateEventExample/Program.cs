namespace DelegateEventExample;

public class Program
{
    // public delegate int MathOperation(int a, int b);

/*    public static Func<int, int, int> mathOperation;

    public static int Add(int a, int b) => a + b;
    public static int Minus(int a, int b) => a - b;
    public static double Divide(int a, int b) => a / b;
*/
    public static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green;

        /* Console.WriteLine("Hello, World!");


     // MathOperation sumPlyus = new MathOperation(Add);
     // Console.WriteLine(sumPlyus.Invoke(2, 6));
     // Console.WriteLine(sumPlyus(2, 8));
     // MathOperation sumMinus = new MathOperation(Minus);

    // Func<int, int, int> mathOperation = Add;

     mathOperation = Add;

     Console.WriteLine(mathOperation(10, 5));*/

        /*
                var steamBot = new SteamBot2(balance: 100, productCount: 10);

                steamBot._balanceHandler += GetMessage;
                steamBot._balanceHandler += GetMessageForTelegram;

                foreach (var @delegate in steamBot._balanceHandler.GetInvocationList())
                {
                    Console.WriteLine(@delegate.Target);
                    Console.WriteLine(@delegate.Method);
                }

                //steamBot.Register(GetMessage);
                //steamBot.Register(GetMessageForTelegram);
        */

        var steamBot = new SteamBot(balance: 100, productCount: 10);
        //steamBot.OnEmptyBalance += GetMessage;
        //steamBot.OnEmptyBalance += GetMessageForTelegram;

        steamBot.OnEmptyBalance += (sender, e) =>
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(e.Message);
            Console.WriteLine($"Sizga yetmayotgan summa: {e.Sum}");
        };

        steamBot.OnEmptyBalance += (sender, e) =>
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Jo'natuvchi:  {sender}");
            Console.WriteLine(e.Message);
            Console.WriteLine($"Sizga yetmayotgan summa: {e.Sum}");
            Console.ForegroundColor = ConsoleColor.White;
        };

        Console.WriteLine($" 1 - Bot yaratildi! hozirgi balance: {steamBot.Balance},  Maxsulotlar soni:  {steamBot.ProductCount}");
        Console.ReadKey();

        steamBot.Sale(amount: 11, price: 150);
        Console.WriteLine($" 2 - Bot yaratildi! hozirgi balance: {steamBot.Balance},  Maxsulotlar soni:  {steamBot.ProductCount}");

        Console.ReadKey();
        steamBot.Purchase(amount: 6, price: 10);
        Console.WriteLine($" 3 - Bot yaratildi! hozirgi balance: {steamBot.Balance},  Maxsulotlar soni:  {steamBot.ProductCount}");

        Console.ReadKey();
        steamBot.Purchase(amount: 6, price: 10);
        Console.WriteLine($" 4 - Bot yaratildi! hozirgi balance: {steamBot.Balance},  Maxsulotlar soni:  {steamBot.ProductCount}");

    }

/*    private static void GetMessageForTelegram(object sender, BalanceEventArgs e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine($"Sizga yetmayotgan summa: {e.Sum}");
    }

    private static void GetMessage(object sender, BalanceEventArgs e)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Jo'natuvchi:  {sender}");
        Console.WriteLine(e.Message);
        Console.WriteLine($"Sizga yetmayotgan summa: {e.Sum}");
        Console.ForegroundColor = ConsoleColor.White;
    }
*/
    /* public static void GetMessage(string message)
{
    Console.WriteLine(message);
}

public static void GetMessageForTelegram(string message)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"Warning!: {message}");
    Console.ForegroundColor = ConsoleColor.White;
}*/
}