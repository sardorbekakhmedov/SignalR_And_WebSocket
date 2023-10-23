namespace DelegateEventExample;

public class SteamBot
{
    private int _productCount;
    private int _balance;

    public int ProductCount => _productCount;
    public int Balance => _balance;

   // public delegate void BalanceHandler(object sender, EventArgs e);

    //public Action<object, BalanceEventArgs>? BalanceHandler;

    //public event Action<object, BalanceEventArgs>? OnEmptyBalance;

    public event EventHandler<BalanceEventArgs>? OnEmptyBalance;

    public SteamBot(int balance, int productCount)
    {
        _balance = balance;
        _productCount = productCount;
    }

    public void Sale(int amount, int price)
    {
        if (_productCount < amount)
        {
            return;
        }

        _productCount -= amount;
        _balance += (price * amount);
    }

    public void Purchase(int amount, int price)
    {
        if (_balance < (price * amount))
        {
            var message = "Pulingiz miqdori yetarli emas!, Operatsiya bekor qilindi!";
            // OnEmptyBalance?.Invoke(message);
            OnEmptyBalance?.Invoke(this, new BalanceEventArgs(message, (price * amount) - _balance));
            return;
        }

        _productCount += amount;
        _balance -= (price * amount);
    }
}