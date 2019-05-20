using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.BusinessLayer;
using System;
using EasyMechBackend.Common;
using EasyMechBackend.Common.DataTransferObject;
using EasyMechBackend.Common.DataTransferObject.DTOs;
using EasyMechBackend.Common.Exceptions;
using log4net;

namespace EasyMechBackend.ServiceLayer.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class KundenController : ControllerBase
    {
        private const string OKTAG = ResponseObject<object>.OKTAG;

        private static readonly ILog log = LogManager.GetLogger
             (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // GET: /Kunden
        [HttpGet]
        public async Task<ActionResult<ResponseObject<IEnumerable<KundeDto>>>> GetKunden()
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new KundeManager();
                    var kundenDtos = manager.GetKunden(false).ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<KundeDto>>(kundenDtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<KundeDto>>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // GET: /Kunden/all
        [HttpGet("all")]
        public async Task<ActionResult<ResponseObject<IEnumerable<KundeDto>>>> GetAllKunden()
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new KundeManager();
                    var kundenDtos = manager.GetKunden(true).ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<KundeDto>>(kundenDtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<KundeDto>>(e.Message, ErrorCode.General);
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
                    var manager = new KundeManager();
                    KundeDto kundeDto = manager.GetKundeById(id).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called on Entity {id}");
                    return new ResponseObject<KundeDto>(kundeDto);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<KundeDto>(e.Message, ErrorCode.General);
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
                    var manager = new KundeManager();
                    KundeDto dto = manager.AddKunde(kunde.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: kunde {dto.Id} added");
                    return new ResponseObject<KundeDto>(dto);

                }
                catch (UniquenessException e)
                {
                    log.Warn($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a Uniqueness Exception: {e.Message}");
                    return new ResponseObject<KundeDto>(e.Message, ErrorCode.Uniqueness);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<KundeDto>("DB Update Exception: " + e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<KundeDto>(e.Message, ErrorCode.General);
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
                        return new ResponseObject<KundeDto>("ID in URL does not match ID in the request's body data", ErrorCode.IDMismatch);
                    }
                    var manager = new KundeManager();
                    KundeDto changedKundeDto = manager.UpdateKunde(kunde.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Kunde {id} updated");
                    return new ResponseObject<KundeDto>(changedKundeDto);
                }
                catch (UniquenessException e)
                {
                    log.Warn($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a Uniqueness Exception: {e.Message}");
                    return new ResponseObject<KundeDto>(e.Message, ErrorCode.Uniqueness);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<KundeDto>(e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<KundeDto>(e.Message, ErrorCode.General);
                }

            });

            return await task;
        }

        // DELETE: kunden/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<KundeDto>>> DeleteKunde(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {

                    var manager = new KundeManager();
                    var kunde = manager.GetKundeById(id);
                    manager.SetKundeInactive(kunde);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Set Kunde {id} to inactive");
                    return new ResponseObject<KundeDto>(null, OKTAG, $"Set Kunde {id} to inactive", 0);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<KundeDto>(e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<KundeDto>(e.Message, ErrorCode.General);
                }

            });
            return await task;
        }

        // DELETE: kunden/5/hard
        [HttpDelete("{id}/hard")]
        public async Task<ActionResult<ResponseObject<KundeDto>>> DeleteKundeHard(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new KundeManager();
                    var kunde = manager.GetKundeById(id);
                    manager.DeleteKunde(kunde);
                    log.Warn($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Deleting Kunde {id} from database");
                    return new ResponseObject<KundeDto>(null, OKTAG, $"Deleted Kunde {id} from database", 0);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException?.Message}");
                    if (e.InnerException != null)
                    {
                        return new ResponseObject<KundeDto>(e.InnerException.Message, ErrorCode.DBUpdate);
                    }
                    return new ResponseObject<KundeDto>("Unknown Exception!", ErrorCode.Unknown);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<KundeDto>(e.Message, ErrorCode.General);
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
                    var manager = new KundeManager();
                    var kundenDtos = manager.GetSearchResult(k.ConvertToEntity()).ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<KundeDto>>(kundenDtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<KundeDto>>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }
    }
}
