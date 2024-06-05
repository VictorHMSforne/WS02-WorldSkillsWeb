using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WS02._1.Data;
using WS02._1.Models;

namespace WS02._1.Controllers
{
    public class PacientesController : Controller
    {

        private readonly WS02_1Context _context;

        public PacientesController(WS02_1Context context)
        {
            _context = context;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            return _context.Paciente != null ?
                        View(await _context.Paciente.ToListAsync()) :
                        Problem("Entity set 'WS02_1Context.Paciente'  is null.");
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null || _context.Paciente == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nome_paciente,nome_doutor,tipo_quarto,quarto")] Paciente paciente)
        {
            SqlConnection con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=WS02._1.Data;Trusted_Connection=True;MultipleActiveResultSets=true");

            if (ModelState.IsValid)
            {
                string titulo = "Cadastro de Pacientes";
                ViewData["Quarto"] = titulo;
                string tipo_quarto, nome_paciente;
                int quarto2;
                tipo_quarto = paciente.tipo_quarto.ToString();
                quarto2 = Convert.ToInt32(paciente.quarto);
                nome_paciente = paciente.nome_paciente;
                con.Open();
                string sql = "SELECT tipo_quarto, count(quarto) as numero from Paciente WHERE  tipo_quarto='" + tipo_quarto + "'  AND quarto='" + quarto2 + "' GROUP BY tipo_quarto";
                string sqlNome = "SELECT * FROM dbo.Paciente WHERE nome_paciente= '" + nome_paciente + "'";
                
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                
                SqlCommand cmdNome = new SqlCommand(sqlNome, con);
                
                var result2 = cmdNome.ExecuteScalar();
                if (result2 != null)
                {
                    string erro = "Paciente já cadastrado";
                    ViewData["Quarto"] = erro;

                }
                else
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        quarto2 = (int)reader["numero"];
                        tipo_quarto = reader["tipo_quarto"].ToString();

                    }
                    if (tipo_quarto == "Solteiro" && quarto2 == 1)
                    {
                        string erro = "Quarto já está cheio, tente outro";
                        ViewData["Quarto"] = erro;
                        return RedirectToAction("Quarto_Cheio");
                    }
                    else if (tipo_quarto == "Duplo" && quarto2 == 2)
                    {
                        string erro = "Quarto já está cheio, tente outro";
                        ViewData["Quarto"] = erro;
                        return RedirectToAction("Quarto_Cheio");
                    }
                    else if (tipo_quarto == "Triplo" && quarto2 == 3)
                    {
                        string erro = "Quarto já está cheio, tente outro";
                        ViewData["Quarto"] = erro;
                        return RedirectToAction("Quarto_Cheio");
                    }
                    con.Close();
                    _context.Add(paciente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Paciente == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nome_paciente,nome_doutor,tipo_quarto,quarto")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Paciente == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Paciente == null)
            {
                return Problem("Entity set 'WS02_1Context.Paciente'  is null.");
            }
            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente != null)
            {
                _context.Paciente.Remove(paciente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return (_context.Paciente?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
