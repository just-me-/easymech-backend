using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.ServiceLayer.DataTransferObject;
using EasyMechBackend.BusinessLayer;
using System;
using log4net;
using System.IO;

namespace EasyMechBackend.ServiceLayer

{
    [Route("[controller]")]
    [ApiController]
    public class DevController : ControllerBase
    {
        private static readonly string ERRORTAG = ResponseObject<Object>.ERRORTAG;
        private static readonly string OKTAG = ResponseObject<Object>.OKTAG;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
             (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // GET: /Dev/postmanTestCleanup
        [HttpGet("postmanTestCleanup")]
        public async Task<IActionResult> PostManCleanup()
        {
            long testId = 85496;

            await Task.Run(() =>
            {
                try
                {
                    log.Warn($"Test Clean Up Script was Called!");


                    var m1 = new MaschineManager();
                    m1.DeleteMaschine(new DataAccessLayer.Maschine { Id = testId });

                    var m2 = new MaschinentypManager();
                    m2.DeleteMaschinentyp(new DataAccessLayer.Maschinentyp { Id = testId });

                    var m3 = new KundeManager();
                    m3.DeleteKunde(new DataAccessLayer.Kunde { Id = testId });



                }
                catch (Exception e)
                {
                    log.Error($"Test Clean Up Error {e.Message}");
                }
            });


            return NoContent();
        }

        [HttpGet("log")]
        public ActionResult GetLog()
        {

            using (var fileStream = new FileStream("easymech.log", FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[1024*32];

                fileStream.Read(buffer, 0, 1024*32);

                Response.ContentType = "text/plain";
                Response.StatusCode = 200;

                return File(buffer, "text/plain", "log.txt");

            }
        }

    }
}
