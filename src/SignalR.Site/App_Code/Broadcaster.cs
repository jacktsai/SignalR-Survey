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
public class Broadcaster : Hub
{
    private readonly CancellationToken cancellationToken = new CancellationToken();

    public Broadcaster()
    {
        Task.Factory.StartNew(() =>
        {
        }, cancellationToken);
    }

    protected override void Dispose(bool disposing)
    {
        cancellationToken.CanBeCanceled
        base.Dispose(disposing);
    }
}