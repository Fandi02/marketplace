#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using marketplace.Datas;
using marketplace.Datas.Entities;

namespace marketplace.Controllers
{
    public class KategoriProduksController : Controller
    {
        private readonly marketplaceContext _context;

        public KategoriProduksController(marketplaceContext context)
        {
            _context = context;
        }

        // GET: KategoriProduks
        public async Task<IActionResult> Index()
        {
            return View(await _context.KategoriProduks.ToListAsync());
        }

        // GET: KategoriProduks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoriProduk = await _context.KategoriProduks
                .FirstOrDefaultAsync(m => m.IdKategori == id);
            if (kategoriProduk == null)
            {
                return NotFound();
            }

            return View(kategoriProduk);
        }

        // GET: KategoriProduks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KategoriProduks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKategori,Nama,Deskripsi,Icon")] KategoriProduk kategoriProduk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategoriProduk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategoriProduk);
        }

        // GET: KategoriProduks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoriProduk = await _context.KategoriProduks.FindAsync(id);
            if (kategoriProduk == null)
            {
                return NotFound();
            }
            return View(kategoriProduk);
        }

        // POST: KategoriProduks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKategori,Nama,Deskripsi,Icon")] KategoriProduk kategoriProduk)
        {
            if (id != kategoriProduk.IdKategori)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategoriProduk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriProdukExists(kategoriProduk.IdKategori))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kategoriProduk);
        }

        // GET: KategoriProduks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategoriProduk = await _context.KategoriProduks
                .FirstOrDefaultAsync(m => m.IdKategori == id);
            if (kategoriProduk == null)
            {
                return NotFound();
            }

            return View(kategoriProduk);
        }

        // POST: KategoriProduks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategoriProduk = await _context.KategoriProduks.FindAsync(id);
            _context.KategoriProduks.Remove(kategoriProduk);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriProdukExists(int id)
        {
            return _context.KategoriProduks.Any(e => e.IdKategori == id);
        }
    }
}
