using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.ServiceLayer.DataTransferObject;
using EasyMechBackend.BusinessLayer;
using EasyMechBackend.DataAccessLayer;

namespace EasyMechBackend.ServiceLayer

{
    [Route("[controller]")]
    [ApiController]
    public class KundenController : ControllerBase
    {

        // bRAUCHEN WIR NET ODER? - das wär da die dependency inejction
        //private readonly TodoContext _context;

        // GET: /Kunden/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KundeDto>>> GetKunden()
        {
            return await Task.Run(() => KundeManager.GetKunden().ConvertToDtos());
        }

        // GET: /Kunden/2
        [HttpGet("{id}")]
        public async Task<ActionResult<KundeDto>> GetKunde(long id)
        {
            var kunde = await Task.Run((() => KundeManager.GetKundeById(id).ConvertToDto()));
        
            return kunde;
        }

        // POST: Kunden/
        [HttpPost]
        public async Task<ActionResult<KundeDto>> PostKunde(KundeDto kundeDto)
        {
           Kunde kundeToAdd = kundeDto.ConvertToEntity();

           Task<Kunde> addingTask =  Task.Run(() => KundeManager.AddKunde(kundeToAdd));
           Kunde kundeAdded = await addingTask;

           return CreatedAtAction(nameof(GetKunde), new { id = kundeAdded.Id }, kundeAdded.ConvertToDto());
        }

        //        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        //        {
        //            _context.TodoItems.Add(item);
        //            await _context.SaveChangesAsync();

        //            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        //        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKunde(long id, KundeDto kunde)
        {
            if (id != kunde.Id)
            {
                return BadRequest();
            }

            await Task.Run(() => KundeManager.UpdateKunde(kunde.ConvertToEntity()));  //funzt das ?müssen wir hier die id mitgeben?

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var kunde = await Task.Run(() => KundeManager.GetKundeById(id));

            if (kunde == null)
            {
                return NotFound();
            }

            await Task.Run(() => KundeManager.DeleteKunde(kunde));

            return NoContent();
        }

    }

}
