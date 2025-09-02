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
    public async Task<IActionResult> Index(int page = 1)
    {
        const int productsPerPage = 3;

        int totalItems = await _context.Products.CountAsync();
        int totalPagesNeeded = (int)Math.Ceiling((double)totalItems / productsPerPage);

        if (page < 1) 
            page = 1;

        //if user tries to navigate to a page greater than total pages, set to last page
        if (totalPagesNeeded > 0 && page > totalPagesNeeded) page = totalPagesNeeded;

        List<Product> products = await _context.Products
            .OrderBy(p => p.Title)
            .Skip((page - 1) * productsPerPage)
            .Take(productsPerPage)
            .ToListAsync();

        ProductListViewModel productListViewModel = new ProductListViewModel
        {
            Products = products,
            CurrentPage = page,
            TotalPages = totalPagesNeeded,
            PageSize = productsPerPage,
            TotalItems = totalItems
        };

        return View(productListViewModel);
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
    public async Task<IActionResult> Edit(int id)
    {
        Product? product = await _context.Products.FindAsync(id);

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
    public async Task<IActionResult> Delete(int id)
    { 
        Product? product= await _context.Products.FindAsync(id);

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
