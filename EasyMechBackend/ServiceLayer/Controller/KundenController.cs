using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.ServiceLayer.DataTransferObject;
using EasyMechBackend.BusinessLayer;
using System;
using EasyMechBackend.DataAccessLayer;
using log4net;

namespace EasyMechBackend.ServiceLayer


{
    [Route("[controller]")]
    [ApiController]
    public class KundenController : ControllerBase
    {
        private static readonly string ERRORTAG = ResponseObject<Object>.ERRORTAG;
        private static readonly string OKTAG = ResponseObject<Object>.OKTAG;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
             (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // GET: /Kunden/
        [HttpGet]
        public async Task<ActionResult<ResponseObject<IEnumerable<KundeDto>>>> GetKunden()
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var kundenDtos = KundeManager.GetKunden().ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<KundeDto>>(kundenDtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<KundeDto>>(e.Message);
                }
            });

            return await task;
        }

        // GET: /Kunden/2
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<KundeDto>>> GetKunde(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    KundeDto kundeDto = KundeManager.GetKundeById(id).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return new ResponseObject<KundeDto>(kundeDto);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<KundeDto>(e.Message);
                }
            });



            return await task;
        }

        // POST: kunden/
        [HttpPost]
        public async Task<ActionResult<ResponseObject<KundeDto>>> PostKunde(KundeDto kunde)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    KundeDto dto = KundeManager.AddKunde(kunde.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: kunde {dto.Id} added");
                    return new ResponseObject<KundeDto>(dto);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<KundeDto>("DB Update Exception: " + e.InnerException.Message);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<KundeDto>(e.Message);
                }
            });

            return await task;
        }

        // PUT: kunden/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<KundeDto>>> PutKunde(long id, KundeDto kunde)
        {

            var task = Task.Run(() =>
            {
                try
                {
                    if (id != kunde.Id)
                    {
                        return new ResponseObject<KundeDto>("ID in URL does not match ID in the request's body data");
                    }
                    KundeDto changedKundeDto = KundeManager.UpdateKunde(kunde.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Kunde {id} updated");
                    return new ResponseObject<KundeDto>(changedKundeDto);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<KundeDto>(e.Message + e.InnerException.Message);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<KundeDto>(e.Message);
                }

            });

            return await task;
        }

        // DELETE: kunden/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<KundeDto>>> DeleteTodoItem(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var kunde = KundeManager.GetKundeById(id);
                    KundeManager.SetKundeInactive(kunde);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Set Kunde {id} to inactive");
                    return new ResponseObject<KundeDto>(null, OKTAG, $"Set Kunde {id} to inactive");
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<KundeDto>(e.Message + e.InnerException.Message);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<KundeDto>(e.Message);
                }

            });
            return await task;
        }

        // POST: kunden/suchen
        [HttpPost("suchen")]
        public async Task<ActionResult<ResponseObject<IEnumerable<KundeDto>>>> GetSearchResult(KundeDto k)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var kundenDtos = KundeManager.GetSearchResult(k.ConvertToEntity()).ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<KundeDto>>(kundenDtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<KundeDto>>(e.Message);
                }
            });

            return await task;
        }
    }
}
