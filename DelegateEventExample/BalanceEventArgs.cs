namespace DelegateEventExample;

public class BalanceEventArgs : EventArgs
{
    public string Message { get; set; }
    public int Sum { get; set; }

    public BalanceEventArgs(string message, int sum)
    {
        Message = message;
        Sum = sum;
    }
}