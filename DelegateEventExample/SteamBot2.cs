namespace DelegateEventExample;

public class SteamBot2
{
    private int _productCount;
    private int _balance;

    public int ProductCount => _productCount;
    public int Balance => _balance;

    // public delegate void BalanceHandler(string message);

    //private BalanceHandler? _balanceHandler;

    public Action<string>? _balanceHandler;

    public SteamBot2(int balance, int productCount)
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
            _balanceHandler?.Invoke("Pulingiz miqdori yetarli emas!, Operatsiya bekor qilindi!");
            return;
        }

        _productCount += amount;
        _balance -= (price * amount);
    }

    public void Register(Action<string>? action)
    {
        _balanceHandler += action;

        /*      if (action is null)
              {
                  _balanceHandler = action;
              }
              else
              {
                  Delegate.Combine(_balanceHandler, action);
              }*/

    }

    public void UnRegister(Action<string>? action)
    {
        _balanceHandler -= action;

        /*        if (action is null)
                {
                    _balanceHandler = action;
                }
                else
                {
                    Delegate.Remove(_balanceHandler, action);
                }*/
    }

}