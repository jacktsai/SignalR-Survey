using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Threading;

/// <summary>
/// Broadcaster 的摘要描述
/// </summary>
public class BroadcastingHub : Hub
{
    private static Dictionary<string, CancellationTokenSource> clientMap = new Dictionary<string, CancellationTokenSource>();

    public void Start()
    {
        var id = base.Context.ConnectionId;

        CancellationTokenSource tokenSource;
        if (!clientMap.TryGetValue(id, out tokenSource))
        {
            tokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                while (!tokenSource.Token.IsCancellationRequested)
                {
                    Clients.Caller.broadcastMessage(string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                    Thread.Sleep(1000);
                }
            }, tokenSource.Token);

            clientMap.Add(id, tokenSource);
        }
    }

    public void Stop()
    {
        var id = base.Context.ConnectionId;

        CancellationTokenSource tokenSource;
        if (clientMap.TryGetValue(id, out tokenSource))
        {
            if (tokenSource.Token.CanBeCanceled)
            {
                tokenSource.Cancel();
            }

            clientMap.Remove(id);
        }
    }
}