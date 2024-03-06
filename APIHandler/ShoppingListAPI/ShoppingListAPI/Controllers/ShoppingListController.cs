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
    
    [HttpDelete("/shopping-list/{id}")]
    public IActionResult DeleteShoppingList(int id)
    {
        return Ok(_databaseCalls.DeleteItem(id));
    }
}