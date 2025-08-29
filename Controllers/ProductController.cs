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
        return View();
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
}
