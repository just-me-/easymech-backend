using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.ServiceLayer.DataTransferObject;
using EasyMechBackend.BusinessLayer;
using System;
using log4net;

namespace EasyMechBackend.ServiceLayer


{
    [Route("[controller]")]
    [ApiController]
    public class FahrzeugtypController : ControllerBase
    {
        private static readonly string ERRORTAG = ResponseObject<Object>.ERRORTAG;
        private static readonly string OKTAG = ResponseObject<Object>.OKTAG;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
             (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // GET: /Fahrzeugtypen/
        [HttpGet]
        public async Task<ActionResult<ResponseObject<IEnumerable<FahrzeugtypDto>>>> GetFahrzeugtypen()
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new FahrzeugtypManager(new DataAccessLayer.EMContext());
                    var fahrzeugtypDtos = manager.GetFahrzeugtypen().ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<FahrzeugtypDto>>(fahrzeugtypDtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<FahrzeugtypDto>>(e.Message);
                }
            });

            return await task;
        }

        // GET: /Fahrzeugtypen/2
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<FahrzeugtypDto>>> GetFahrzeugtyp(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new FahrzeugtypManager(new DataAccessLayer.EMContext());
                    FahrzeugtypDto fahrzeugtypDto = manager.GetFahrzeugtypById(id).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return new ResponseObject<FahrzeugtypDto>(fahrzeugtypDto);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<FahrzeugtypDto>(e.Message);
                }
            });

            return await task;
        }

        // POST: fahrzeugtypen/
        [HttpPost]
        public async Task<ActionResult<ResponseObject<FahrzeugtypDto>>> PostFahrzeugtyp(FahrzeugtypDto fahrzeugtyp)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new FahrzeugtypManager(new DataAccessLayer.EMContext());
                    FahrzeugtypDto dto = manager.AddFahrzeugtyp(fahrzeugtyp.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Fahrzeugtyp {dto.Id} added");
                    return new ResponseObject<FahrzeugtypDto>(dto);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<FahrzeugtypDto>("DB Update Exception: " + e.InnerException.Message);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<FahrzeugtypDto>(e.Message);
                }
            });

            return await task;
        }

        // PUT: fahrzeugtypen/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<FahrzeugtypDto>>> PutFahrzeugtyp(long id, FahrzeugtypDto fahrzeugtyp)
        {

            var task = Task.Run(() =>
            {
                try
                {
                    if (id != fahrzeugtyp.Id)
                    {
                        return new ResponseObject<FahrzeugtypDto>("ID in URL does not match ID in the request's body data");
                    }
                    var manager = new FahrzeugtypManager(new DataAccessLayer.EMContext());
                    FahrzeugtypDto changedFahrzeugtypDto = manager.UpdateFahrzeugtyp(fahrzeugtyp.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Fahrzeugtyp {id} updated");
                    return new ResponseObject<FahrzeugtypDto>(changedFahrzeugtypDto);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<FahrzeugtypDto>(e.Message + e.InnerException.Message);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<FahrzeugtypDto>(e.Message);
                }

            });

            return await task;
        }

        // DELETE: fahrzeugtypen/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<FahrzeugtypDto>>> DeleteFahrzeugtyp(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new FahrzeugtypManager(new DataAccessLayer.EMContext());
                    var fahrzeugtyp = manager.GetFahrzeugtypById(id);
                    manager.DeleteFahrzeugtyp(fahrzeugtyp);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Delete Fahrzeugtyp {id}");
                    return new ResponseObject<FahrzeugtypDto>(null, OKTAG, $"Delete Fahrzeugtyp {id}");
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<FahrzeugtypDto>(e.Message + e.InnerException.Message);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<FahrzeugtypDto>(e.Message);
                }

            });
            return await task;
        }

        // POST: fahrzeugtypen/suchen
        [HttpPost("suchen")]
        public async Task<ActionResult<ResponseObject<IEnumerable<FahrzeugtypDto>>>> GetSearchResult(FahrzeugtypDto f)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new FahrzeugtypManager(new DataAccessLayer.EMContext());
                    var fahrzeugtypDtos = manager.GetSearchResult(f.ConvertToEntity()).ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<FahrzeugtypDto>>(fahrzeugtypDtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<FahrzeugtypDto>>(e.Message);
                }
            });

            return await task;
        }
    }
}
