using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.ServiceLayer.DataTransferObject;
using EasyMechBackend.BusinessLayer;

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
            return BadRequest("blablablabla");
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
        public async Task<ActionResult<KundeDto>> PostKunde(KundeDto kunde)
        {
            await Task.Run(() => KundeManager.AddKunde(kunde.ConvertToEntity()));
            return CreatedAtAction(nameof(GetKunde), new { id = kunde.Id }, kunde);
        }

<<<<<<< HEAD
        // PUT: kunden/5
=======
        // PUT: api/Todo/5
>>>>>>> parent of cfb01e3... Merge branch 'master' of ssh://gitlab.dev.ifs.hsr.ch:45022/epj-2019-fs/easymech/easymech-backend
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

        // DELETE: kunden/5
        [HttpDelete("{id}")]
<<<<<<< HEAD
        public async Task<IActionResult> DeleteKunde(long id)
            

=======
        public async Task<IActionResult> DeleteTodoItem(long id)
>>>>>>> parent of cfb01e3... Merge branch 'master' of ssh://gitlab.dev.ifs.hsr.ch:45022/epj-2019-fs/easymech/easymech-backend
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
