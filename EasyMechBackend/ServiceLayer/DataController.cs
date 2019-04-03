using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.ServiceLayer.DataTransferObject;
using EasyMechBackend.BusinessLayer;

namespace EasyMechBackend.ServiceLayer

    //TODO Asynchronität.... :/
{
        [Route("[controller]")]
        [ApiController]
        public class DataController : ControllerBase
        {

        // bRAUCHEN WIR NET ODER?
            //private readonly TodoContext _context;

            //public TodoController(TodoContext context)
            //{
            //    _context = context;

            //    if (_context.TodoItems.Count() == 0)
            //    {
            //        // Create a new TodoItem if collection is empty,
            //        // which means you can't delete all TodoItems.
            //        _context.TodoItems.Add(new TodoItem { Name = "Item1" });
            //        _context.SaveChanges();
            //    }
            //}

            // GET: Kunden
            [HttpGet]
            public IEnumerable<KundeDto> GetKunden()
            {
            return KundeManager.GetKunden().ConvertToDtos();
            }

            // GET: api/Todo/5
            [HttpGet("{id}")]
            public KundeDto GetKunde(long id)
            {
            var kunde = KundeManager.GetKundeById(id).ConvertToDto();
            return kunde;
            }

            // POST: api/Todo
            [HttpPost]
            public async Task<ActionResult<KundeDto>> PostKunde(KundeDto kunde)
            {
            KundeManager.AddKunde(kunde.ConvertToEntity()); //await??
            return CreatedAtAction(nameof(GetKunde), new { id = kunde.Id }, kunde);
            }

            // PUT: api/Todo/5
            [HttpPut("{id}")]
            public async Task<IActionResult> PutKunde(long id, KundeDto kunde)
            {
                if (id != kunde.Id)
                {
                    return BadRequest();
                }

            KundeManager.UpdateKunde(kunde.ConvertToEntity()); //ID mitgeben?? await???

                return NoContent();
            }

            // DELETE: api/Todo/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteTodoItem(long id)
            {
            var kunde = KundeManager.GetKundeById(id);

                if (kunde == null)
                {
                    return NotFound();
                }

            KundeManager.DeleteKunde(kunde);

                return NoContent();
            }

        }
    
}
