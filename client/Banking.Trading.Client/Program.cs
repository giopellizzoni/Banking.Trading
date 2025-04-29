using Banking.Trading.Client.App;

using Microsoft.Extensions.Hosting;

var host = TradeMessageClient.StartClient();

await host.RunAsync();
Console.WriteLine("Aguardando mensagens. Pressione [enter] para sair.");

Console.ReadKey();
