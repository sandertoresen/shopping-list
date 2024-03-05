using Microsoft.AspNetCore.Mvc;
using ShoppingListAPI.models;

namespace ShoppingListAPI.Controllers;

public class ShoppingListController : ControllerBase
{
    [HttpGet("/shopping-list")]
    public IActionResult GetShoppingList()
    {
        var mockData = new List<Item>
        {
            new Item() { Id = 0, Name = "Milk", Count = 2 },
            new Item() { Id = 1, Name = "Egg", Count = 3 },
            new Item() { Id = 2, Name = "Flour", Count = 12 },
        };

        return Ok(mockData);
    }
    
    [HttpPost("/shopping-list")]
    public IActionResult PostShoppingList([FromBody] Item item)
    {
        //TODO: post

        return Ok();
    }
    
    [HttpDelete("/shopping-list/{id}")]
    public IActionResult DeleteShoppingList(int id)
    {
        //TODO: delete
        return Ok();
    }
}