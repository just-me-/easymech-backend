using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.ServiceLayer.DataTransferObject;
using EasyMechBackend.BusinessLayer;
using System;
using EasyMechBackend.Common.Exceptions;
using EasyMechBackend.ServiceLayer.DataTransferObject.DTOs;

namespace EasyMechBackend.ServiceLayer.Controller


{
    [Route("[controller]")]
    [ApiController]
    public class MaschinentypenController : ControllerBase
    {
        private const string OKTAG = ResponseObject<object>.OKTAG;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
             (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // GET: /Maschinentypen/
        [HttpGet]
        public async Task<ActionResult<ResponseObject<IEnumerable<MaschinentypDto>>>> GetMaschinentypen()
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new MaschinentypManager();
                    var maschinentypDtos = manager.GetMaschinentypen().ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<MaschinentypDto>>(maschinentypDtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<MaschinentypDto>>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // GET: /Maschinentypen/2
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<MaschinentypDto>>> GetMaschinentyp(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new MaschinentypManager();
                    MaschinentypDto maschinentypDto = manager.GetMaschinentypById(id).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return new ResponseObject<MaschinentypDto>(maschinentypDto);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<MaschinentypDto>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // POST: maschinentypen/
        [HttpPost]
        public async Task<ActionResult<ResponseObject<MaschinentypDto>>> PostMaschinentyp(MaschinentypDto maschinentyp)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new MaschinentypManager();
                    MaschinentypDto dto = manager.AddMaschinentyp(maschinentyp.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Maschinentyp {dto.Id} added");
                    return new ResponseObject<MaschinentypDto>(dto);
                }
                catch (UniquenessException e)
                {
                    log.Warn($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a Uniqueness Exception: {e.Message}");
                    return new ResponseObject<MaschinentypDto>(e.Message, ErrorCode.Uniqueness);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<MaschinentypDto>("DB Update Exception: " + e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<MaschinentypDto>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // PUT: maschinentypen/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<MaschinentypDto>>> PutMaschinentyp(long id, MaschinentypDto maschinentyp)
        {

            var task = Task.Run(() =>
            {
                try
                {
                    if (id != maschinentyp.Id)
                    {
                        return new ResponseObject<MaschinentypDto>("ID in URL does not match ID in the request's body data", ErrorCode.DBUpdate);
                    }
                    var manager = new MaschinentypManager();
                    MaschinentypDto changedMaschinentypDto = manager.UpdateMaschinentyp(maschinentyp.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Maschinentyp {id} updated");
                    return new ResponseObject<MaschinentypDto>(changedMaschinentypDto);
                }

                catch (UniquenessException e)
                {
                    log.Warn($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a Uniqueness Exception: {e.Message}");
                    return new ResponseObject<MaschinentypDto>(e.Message, ErrorCode.Uniqueness);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<MaschinentypDto>(e.Message + e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<MaschinentypDto>(e.Message, ErrorCode.General);
                }

            });

            return await task;
        }

        // DELETE: maschinentypen/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<MaschinentypDto>>> DeleteMaschinentyp(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new MaschinentypManager();
                    var maschinentyp = manager.GetMaschinentypById(id);
                    manager.DeleteMaschinentyp(maschinentyp);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Delete Maschinentyp {id}");
                    return new ResponseObject<MaschinentypDto>(null, OKTAG, $"Delete Maschinentyp {id}", 0);
                }
                catch (ForeignKeyRestrictionException e)
                {
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a Foreign Key Restriction Exception: {e.Message}");
                    return new ResponseObject<MaschinentypDto>(e.Message, ErrorCode.ForeignKey);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<MaschinentypDto>(e.Message + e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<MaschinentypDto>(e.Message, ErrorCode.General);
                }

            });
            return await task;
        }

        // POST: Maschinentypen/suchen
        [HttpPost("suchen")]
        public async Task<ActionResult<ResponseObject<IEnumerable<MaschinentypDto>>>> GetSearchResult(MaschinentypDto f)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new MaschinentypManager();
                    var maschinentypDtos = manager.GetSearchResult(f.ConvertToEntity()).ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<MaschinentypDto>>(maschinentypDtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<MaschinentypDto>>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }


    }
}
