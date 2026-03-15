using Backend_ЛР4_Воробьева_В.Д._241_333.Api.Product.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private static List<(int Id, string Name, string Category, string Description, DateTime ProductionDate, Guid Guid, int Amount)> _product = new();

    [HttpPost]
    public ActionResult Create([FromBody] ProductUpsertRequestContract contract)
    {
        var nextId = _product.Count == 0 ? 1 : _product.Max(x => x.Id) + 1;
        var product = (Id: nextId, Name: contract.Name, Category: contract.Category, Description: contract.Description, ProductionDate: contract.ProductionDate, Guid:contract.Guid, Amount: contract.Amount);
        _product.Add(product);

        return CreatedAtAction(
            nameof(ProductGen),
            new { id = product.Id },
            product
        );
    }

    [Route("all")]
    [HttpGet]
    public ActionResult<ProductResponseContract[]> GetAllProducts()
    {
        var products = _product
            .Select(x => new ProductResponseContract { Id = x.Id, Name = x.Name, Category = x.Category, Description = x.Description, ProductionDate  = x.ProductionDate, Guid = x.Guid, Amount = x.Amount})
            .ToArray();
        return Ok(products);
    }

    [HttpGet]
    [Route("{id:int:min(1)?}")]
    public ActionResult ProductGen(int? id)
    {
        if (id == null)
        {
            return Ok(_product.Select(x => new ProductResponseContract 
            { 
                Id = x.Id, 
                Name = x.Name, 
                Category = x.Category, 
                Description = x.Description, 
                ProductionDate = x.ProductionDate, 
                Guid = x.Guid,
            Amount = x.Amount}));
        }
        var index = _product.FindIndex(x => x.Id == id);
        if (index < 0) return NotFound();

        var product = _product[index];
        return Ok(new ProductResponseContract { Id = product.Id, Name = product.Name, Category = product.Category, Description = product.Description, ProductionDate = product.ProductionDate, Guid = product.Guid, Amount = product.Amount});
    }

    [HttpGet]
    [Route("{name?}/description")]
    public ActionResult ProductGenName(string? name)
    {
        var index = _product.FindIndex(x => x.Name == name);
        if (index < 0) return NotFound();
        var product = _product[index];

        return Ok(product.Description);
    }

    [HttpGet]
    [Route("{date:datetime}")]
    public ActionResult ProductGenDate(DateTime date)
    {
        var index = _product.FindIndex(x => x.ProductionDate == date);
        if (index < 0) return NotFound();

        var product = _product[index];
        return Ok(new ProductResponseContract { Id = product.Id, Name = product.Name, Category = product.Category, Description = product.Description, ProductionDate = product.ProductionDate, Guid = product.Guid, Amount = product.Amount });

    }
    [HttpGet]
    [Route("Filter")]
    public ActionResult ProductFilter([FromQuery] int amount = 1, [FromQuery] string? categoty = null)
    {
        var index = _product.FindIndex(x => x.Amount == amount);
        if (index < 0) return NotFound();

        var product = _product[index];
        return Ok(new ProductResponseContract { Id = product.Id, Name = product.Name, Category = product.Category, Description = product.Description, ProductionDate = product.ProductionDate, Guid = product.Guid, Amount = product.Amount });

    }
    [HttpGet]
    [Route("{guid:guid}")]
    public ActionResult ProductGenGuid(Guid guid)
    {
        var index = _product.FindIndex(x => x.Guid == guid);
        if (index < 0) return NotFound();

        var product = _product[index];
        return Ok(new ProductResponseContract { Id = product.Id, Name = product.Name, Category = product.Category, Description = product.Description, ProductionDate = product.ProductionDate, Guid = product.Guid, Amount = product.Amount });
    }

   
    
    [HttpPut]
    [Route("{id:int:min(1)}")]
    public ActionResult Update(int id, [FromBody] ProductUpsertRequestContract contract)
    {
        var index = _product.FindIndex(x => x.Id == id);
        if (index < 0) return NotFound();

        var updated = (id, contract.Name, contract.Category, contract.Description, contract.ProductionDate, contract.Guid, contract.Amount);
        _product[index] = updated;
        return Ok();

    }
    

    [HttpDelete]
    [Route("{id:int:min(1)}")]

    public ActionResult Delete(int id)
    {
        var index = _product.FindIndex(x => x.Id == id);
        if (index < 0) return NotFound();
        _product.RemoveAt(index);
        return Ok();
    }


    [HttpDelete]
    [Route("{name:minlength(3)}")]
    public ActionResult Delete(string name)
    {
        var index = _product.FindIndex(x => x.Name == name);
        if (index < 0) return NotFound();
        _product.RemoveAt(index);
        return Ok();
    }

    

}