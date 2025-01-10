namespace BPP.Person.DALAPI.Tests;

/// <summary>
/// Tests "online" endpoint
/// </summary>
[TestClass]
public class OnlineEndpointTest
{
    /// <summary>
    /// Transceives test data
    /// </summary>
    /// <returns>Task instance</returns>
    [TestMethod]
    public async Task TransceiveTestData()
    {
        HttpClient httpClient = new HttpClient();
        string endpointResponse = await httpClient.GetStringAsync("https://localhost:7288/online");
        Assert.AreEqual<string>(endpointResponse, "OK");
    }
}
