using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.ServiceLayer.DataTransferObject;
using EasyMechBackend.BusinessLayer;
using System;
using EasyMechBackend.ServiceLayer.DataTransferObject.DTOs;

namespace EasyMechBackend.ServiceLayer.Controller


{
    [Route("[controller]")]
    [ApiController]
    public class MaschinenController : ControllerBase
    {
        private const string OKTAG = ResponseObject<object>.OKTAG;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
             (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // GET: /Maschinen/
        [HttpGet]
        public async Task<ActionResult<ResponseObject<IEnumerable<MaschineDto>>>> GetMaschinen()
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new MaschineManager();
                    var maschinenDtos = manager.GetMaschinen().ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<MaschineDto>>(maschinenDtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<MaschineDto>>(e.Message);
                }
            });

            return await task;
        }

        // GET: /Maschinen/2
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<MaschineDto>>> GetMaschine(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new MaschineManager();
                    MaschineDto maschineDto = manager.GetMaschineById(id).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return new ResponseObject<MaschineDto>(maschineDto);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<MaschineDto>(e.Message);
                }
            });

            return await task;
        }

        // POST: maschinen/
        [HttpPost]
        public async Task<ActionResult<ResponseObject<MaschineDto>>> PostMaschine(MaschineDto maschine)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new MaschineManager();
                    MaschineDto dto = manager.AddMaschine(maschine.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: maschine {dto.Id} added");
                    return new ResponseObject<MaschineDto>(dto);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<MaschineDto>("DB Update Exception: " + e.InnerException.Message);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<MaschineDto>(e.Message);
                }
            });

            return await task;
        }

        // PUT: maschinen/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<MaschineDto>>> PutMaschine(long id, MaschineDto maschine)
        {

            var task = Task.Run(() =>
            {
                try
                {
                    if (id != maschine.Id)
                    {
                        return new ResponseObject<MaschineDto>("ID in URL does not match ID in the request's body data");
                    }
                    var manager = new MaschineManager();
                    MaschineDto changedMaschineDto = manager.UpdateMaschine(maschine.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Maschine {id} updated");
                    return new ResponseObject<MaschineDto>(changedMaschineDto);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<MaschineDto>(e.Message + e.InnerException.Message);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<MaschineDto>(e.Message);
                }

            });

            return await task;
        }

        // DELETE: maschinen/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<MaschineDto>>> DeleteMaschine(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new MaschineManager();
                    var maschine = manager.GetMaschineById(id);
                    manager.SetMaschineInactive(maschine);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Set Maschine {id} to inactive");
                    return new ResponseObject<MaschineDto>(null, OKTAG, $"Set Maschine {id} to inactive");
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<MaschineDto>(e.Message + e.InnerException.Message);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<MaschineDto>(e.Message);
                }

            });
            return await task;
        }

        // DELETE: maschinen/5
        [HttpDelete("{id}/hard")]
        public async Task<ActionResult<ResponseObject<MaschineDto>>> DeleteMaschineHard(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new MaschineManager();
                    var maschine = manager.GetMaschineById(id);
                    manager.DeleteMaschine(maschine);
                    log.Warn($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Delete Maschine {id} from database");
                    return new ResponseObject<MaschineDto>(null, OKTAG, $"Delete Maschine {id} from database");
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<MaschineDto>(e.Message + e.InnerException.Message);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<MaschineDto>(e.Message);
                }

            });
            return await task;
        }

        // POST: maschinen/suchen
        [HttpPost("suchen")]
        public async Task<ActionResult<ResponseObject<IEnumerable<MaschineDto>>>> GetSearchResult(MaschineDto m)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new MaschineManager();
                    var maschinenDtos = manager.GetSearchResult(m.ConvertToEntity()).ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<MaschineDto>>(maschinenDtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<MaschineDto>>(e.Message);
                }
            });

            return await task;
        }
    }
}
