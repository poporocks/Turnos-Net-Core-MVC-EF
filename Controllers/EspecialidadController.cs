using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Turnos.Models;
using Microsoft.EntityFrameworkCore;

namespace Turnos.Controllers
{
    public class EspecialidadController : Controller
    {
        private readonly TurnosContext _context;
        public EspecialidadController(TurnosContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Especialidad.ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null) return NotFound();
            var especialidad = await _context.Especialidad.FindAsync(id);
            if(especialidad == null) return NotFound();
            return View(especialidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEspecialidad,Descripcion")] Especialidad especialidad)
        {
            if(id != especialidad.IdEspecialidad) return NotFound();
            if(ModelState.IsValid)
            {
                _context.Update(especialidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(especialidad);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null) return NotFound();
            var especialidad = await _context.Especialidad.FirstOrDefaultAsync(e => e.IdEspecialidad == id);
            if(especialidad == null) return NotFound();
            return View(especialidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var especialidad = await _context.Especialidad.FindAsync(id);
            _context.Especialidad.Remove(especialidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEspecialidad, Descripcion")] Especialidad especialidad)
        {
            if(ModelState.IsValid)
            {
                _context.Add(especialidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(especialidad);
        }
    }
}