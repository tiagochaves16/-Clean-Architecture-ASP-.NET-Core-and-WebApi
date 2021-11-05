using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CleanArchMvcWebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;
        public ProductsController(IProductService productAppService, ICategoryService categoryService,
            IWebHostEnvironment environment)
        {
            _productService = productAppService;
            _categoryService = categoryService;
            _environment = environment;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetProducts();
            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId =
                new SelectList(await _categoryService.GetCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                await _productService.Add(product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        [HttpGet()]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ProductDto = await _productService.GetById(id);

            if (ProductDto == null) return NotFound();

            var categories = await _categoryService.GetCategories();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", ProductDto.CategoryId);

            return View(ProductDto);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(ProductDTO ProductDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.Update(ProductDto);
                }
                catch (Exception)
                {

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(ProductDto);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet()]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ProductDto = await _productService.GetById(id);

            if (ProductDto == null) return NotFound();

            return View(ProductDto);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.Remove(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var ProductDto = await _productService.GetById(id);

            if (ProductDto == null) return NotFound();
            var wwwwroot = _environment.WebRootPath;
            var image = Path.Combine(wwwwroot, "images\\" + ProductDto.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;

            return View(ProductDto);
        }
    }
}

