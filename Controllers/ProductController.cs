﻿using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers;

public class ProductController : Controller
{
    private readonly ProductDbContext _context;
    public ProductController(ProductDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
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

            return RedirectToAction("Index");
        }
        return View(p);
    }
}
