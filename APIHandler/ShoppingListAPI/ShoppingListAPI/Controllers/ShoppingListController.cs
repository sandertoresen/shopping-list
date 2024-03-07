using Microsoft.AspNetCore.Mvc;
using ShoppingListAPI.Database;
using ShoppingListAPI.models;

namespace ShoppingListAPI.Controllers;
public class ShoppingListController : ControllerBase
{
    private readonly DatabaseCalls _databaseCalls;

    public ShoppingListController(DatabaseCalls databaseCalls)
    {
        _databaseCalls = databaseCalls;
    }
    [HttpGet("/shopping-list")]
    public IActionResult GetShoppingList()
    {
        return Ok(_databaseCalls.GetItems());
    }

    [HttpPost("/shopping-list")]
    public IActionResult PostShoppingList([FromBody] Item item)
    {
        return Ok(_databaseCalls.PostItem(item.Name, item.Count));
    }

    [HttpPut("/shopping-list")]
    public IActionResult PutShoppingList([FromBody] Dictionary<string, int> updateItem)
    {
        return Ok(_databaseCalls.PutItem(updateItem["id"], updateItem["added"]));
    }

    [HttpDelete("/shopping-list")]
    public IActionResult DeleteShoppingList([FromQuery] int id)
    {
        return Ok(_databaseCalls.DeleteItem(id));
    }
}