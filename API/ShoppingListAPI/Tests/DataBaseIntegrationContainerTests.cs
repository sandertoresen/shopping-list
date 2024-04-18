using Microsoft.Extensions.Configuration;
using ShoppingListAPI.Database;
using ShoppingListAPI.models;
using Testcontainers.MySql;
using Xunit.Abstractions;

namespace Tests;

public class DataBaseIntegrationContainerTests: IAsyncLifetime
{
    private readonly ITestOutputHelper _testOutputHelper;
    private DatabaseCalls _databaseCalls;
    private readonly MySqlContainer _databaseContainer;

    public DataBaseIntegrationContainerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _databaseContainer = new MySqlBuilder()
            .WithImage("mysql:8.0")
            .WithDatabase("uniqueName" + Guid.NewGuid().ToString())
            .WithUsername("supersecretadminusername")
            .WithPassword("supersecretadminpassword")
            .WithPortBinding(new Random().Next(1024, 49152))
            .Build();
    }

    [Theory]
    [InlineData("TestItem", 1000)]
    public void DataBaseAddItem(string name, int count)
    {
        Item item = new Item { Name = name, Count = count };
        IList<Item> result = _databaseCalls.PostItem(item.Name, item.Count);
        
        Assert.Equal(1, result.Count);
        Assert.Equal(name, result[0].Name);
        Assert.Equal(1000, result[0].Count);
    }
    
    [Theory]
    [InlineData(1, "TestItem", 1000, 100)]
    public void DataBasePutItem(int id, string name, int count, int addToCount)
    {
        Item item = new Item { Name = name, Count = count };
        _databaseCalls.PostItem(item.Name, item.Count);
        IList<Item> result = _databaseCalls.PutItem(id, addToCount);
        
        Assert.Equal(1, result.Count);
        Assert.Equal(name, result[0].Name);
        Assert.Equal(count + addToCount, result[0].Count);
    }
    
    [Theory]
    [InlineData(1, "TestItem", 1000)]
    public void DataBaseDeleteItem(int id, string name, int count)
    {
        Item item = new Item { Name = name, Count = count };
        IList<Item> allItems = _databaseCalls.PostItem(item.Name, item.Count);
        
        _testOutputHelper.WriteLine($"Got item:{allItems[0].Id}");
        IList<Item> allItemsAfterDelete = _databaseCalls.DeleteItem(id);
        
        Assert.Equal(allItems.Count - 1, allItemsAfterDelete.Count);
        Assert.Empty(allItemsAfterDelete);
    }
    
    [Fact]
    public void DatabaseIsEmpty()
    {
        Assert.Empty(_databaseCalls.GetItems());
    }
    
    public async Task InitializeAsync()
    {
        
        await _databaseContainer.StartAsync();
        _databaseCalls = new DatabaseCalls(_databaseContainer.GetConnectionString());
    }

    public async Task DisposeAsync()
    {
        await _databaseContainer.StopAsync();
    }
}