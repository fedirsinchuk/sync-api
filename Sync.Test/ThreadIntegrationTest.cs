using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sync.Test;

public class ThreadIntegrationTest
{
    private List<HttpClient> httpClients;

    private int dbRecordId = 4;
    
    [SetUp]
    public void Setup()
    {
        httpClients = new List<HttpClient>(50);
        for (int idx = 0; idx < 50; idx++)
        {
            httpClients.Add(new HttpClient());
        }
    }

    [Test]
    public async Task Test1()
    {
        var tasks = Enumerable.Range(0, 50)
            .Select( idx => 
                httpClients[idx].PostAsync($"http://localhost:8600/api/data/threadsafe/add?DbId={dbRecordId}&index={idx}", null));

        await Task.WhenAll(tasks);
    }
}