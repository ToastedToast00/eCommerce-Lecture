using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers;

public class ProductController : Controller
{
    private readonly ProductDbContext _context;
    public ProductController(ProductDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        List<Product> allProducts = await _context.Products.ToListAsync();
        return View(allProducts); //code not working, found issue: this bit was missing
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product p)
    {
        if(ModelState.IsValid)
        {
            _context.Products.Add(p);           //Add to database
            await _context.SaveChangesAsync();  //Save changes

            //TempData is used to pass data between actions and will persist over redirect
            TempData["Message"] = $"{p.Title} created successfully!";

            return RedirectToAction("Index");
        }
        return View(p);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Product? product = _context.Products
            .Where(p => p.ProductId == id)
            .FirstOrDefault();

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Product product)
    {
        if (ModelState.IsValid)
        { 
            _context.Update(product);
            await _context.SaveChangesAsync();

            TempData["Message"] = $"{product.Title} updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    { 
        if (id <= 0)
        {
            return BadRequest();
        }

        Product? product= _context.Products
            .Where(p => p.ProductId == id)
            .FirstOrDefault();
        
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        Product? product = _context.Products
            .Where(p => p.ProductId == id)
            .FirstOrDefault();

        if (product == null) 
        { 
            return RedirectToAction(nameof(Index));
        }

        _context.Remove(product);
        await _context.SaveChangesAsync();

        TempData["Message"] = $"{product.Title} deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
