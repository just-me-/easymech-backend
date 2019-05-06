using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EasyMechBackend.BusinessLayer;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace EasyMechBackend.ServiceLayer.Controller

{
    [Route("[controller]")]
    [ApiController]
    public class DevController : ControllerBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
             (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // GET: /Dev/postmanTestCleanup
        [HttpGet("postmanTestCleanup")]
        public async Task<IActionResult> PostManCleanup()
        {
            const long testId = 85496;

            await Task.Run(() =>
            {
                try
                {
                    log.Warn("Test Clean Up Script was Called!");

                    var m1 = new MaschineManager();
                    var e1 = m1.GetMaschineById(testId);
                    m1.DeleteMaschine(e1);

                    var m2 = new MaschinentypManager();
                    var e2 = m2.GetMaschinentypById(testId);
                    m2.DeleteMaschinentyp(e2);

                    var m3 = new KundeManager();
                    var e3 = m3.GetKundeById(testId);
                    m3.DeleteKunde(e3);
                    
                }
                catch (Exception e)
                {
                    log.Error($"Test Clean Up Error {e.Message}");
                    
                }
            });
            return NoContent();

        }


        [HttpGet("log")]
        public void GetLog()
        {

            Response.ContentType = "text/plain";
            Response.StatusCode = 200;
            Response.SendFileAsync("easymech.log");
        }

        [HttpGet("logAlt")]
        public ActionResult GetLogAlt()
        {

            using (var fileStream = new FileStream("easymech.log", FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[1024 * 128];

                fileStream.Read(buffer, 0, 1024 * 32);

                Response.ContentType = "text/plain";
                Response.StatusCode = 200;

                return File(buffer, "text/plain", "log.txt");

            }
        }

    }
}
