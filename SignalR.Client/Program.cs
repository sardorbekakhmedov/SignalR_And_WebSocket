using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("SignalR client is working!\n");

var hubConnection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7240/hub")
    .Build();


hubConnection.On<string>("Send",
    message => Console.WriteLine($"\nMessage from server: {message}"));

await hubConnection.StartAsync();

var isExit = true;

while (isExit)
{
    Console.Write("Enter your message: ");
    var message = Console.ReadLine();

    if (message != "exit")
    {
        await hubConnection.SendAsync("SendMessage", message);
    }
    else
    {
        isExit = false;
    }
}

Console.WriteLine("Press any key to end the program");
Console.ReadKey();
Console.WriteLine("The program has completed its work");


