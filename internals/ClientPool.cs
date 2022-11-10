using System.Collections;
using System.Collections.Concurrent;

namespace Salvini.IoTDB;

internal class ClientPool : IEnumerable<Client>
{
    private bool init = false;
    private readonly ConcurrentQueue<Client> _clients = new ();

    public void Add(Client client)
    {
        init = true;
        Monitor.Enter(_clients);
        _clients.Enqueue(client);
        Monitor.Pulse(_clients);
        Monitor.Exit(_clients);
    }

    public Client Take()
    {
        if (!init) throw new Exception("Please Add some clients before Take!");
        Monitor.Enter(_clients);
        if (_clients.IsEmpty)
        {
            Monitor.Wait(_clients);
        }
        _clients.TryDequeue(out var client);
        Monitor.Exit(_clients);
        return client;
    }

    public async Task Clear()
    { 
        foreach (var client in _clients)
        {
            try
            {
                 await client.ServiceClient.closeSessionAsync(new TSCloseSessionReq(client.SessionId));
            }
            catch
            {

            }
            client.Transport?.Close();
        }
        _clients.Clear();
    }

    public IEnumerator<Client> GetEnumerator()
    {
        return _clients.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _clients.GetEnumerator();
    }
}
