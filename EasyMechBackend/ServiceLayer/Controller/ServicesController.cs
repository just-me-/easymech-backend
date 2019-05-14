using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.ServiceLayer.DataTransferObject;
using EasyMechBackend.BusinessLayer;
using System;
using EasyMechBackend.Common.Exceptions;
using EasyMechBackend.ServiceLayer.DataTransferObject.DTOs;
using log4net;
using Microsoft.AspNetCore.Authorization;

namespace EasyMechBackend.ServiceLayer.Controller


{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private const string OKTAG = ResponseObject<object>.OKTAG;

        private static readonly ILog log = LogManager.GetLogger
             (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // GET: /Services
        [HttpGet]
        public async Task<ActionResult<ResponseObject<IEnumerable<GeplanterServiceDto>>>> GetServices()
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ServiceManager();
                    var dtos = manager.GetGeplanteServices().ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<GeplanterServiceDto>>(dtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<GeplanterServiceDto>>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }


        // GET: /Services/2
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<GeplanterServiceDto>>> GetServiceById(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ServiceManager();
                    var dto = manager.GetGeplanterServiceById(id).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return new ResponseObject<GeplanterServiceDto>(dto);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<GeplanterServiceDto>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // POST: /services/geplant
        [HttpPost("geplant")]
        public async Task<ActionResult<ResponseObject<GeplanterServiceDto>>> AddGeplanterService(GeplanterServiceDto toAddDto)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ServiceManager();
                    GeplanterServiceDto addedDto = manager.AddGeplanterService(toAddDto.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: GeplanterService {addedDto.Id} added");
                    return new ResponseObject<GeplanterServiceDto>(addedDto);

                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<GeplanterServiceDto>("DB Update Exception: " + e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<GeplanterServiceDto>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // PUT: services/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<GeplanterServiceDto>>> UpdateService(long id, GeplanterServiceDto toEditDto)        {

            var task = Task.Run(() =>
            {
                try
                {
                    if (id != toEditDto.Id)
                    {
                        return new ResponseObject<GeplanterServiceDto>("ID in URL does not match ID in the request's body data", ErrorCode.IDMismatch);
                    }
                    var manager = new ServiceManager();
                    var editedDto = manager.UpdateGeplanterService(toEditDto.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: GeplanterService {id} updated");
                    return new ResponseObject<GeplanterServiceDto>(editedDto);
                }

                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<GeplanterServiceDto>(e.Message + e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<GeplanterServiceDto>(e.Message, ErrorCode.General);
                }

            });

            return await task;
        }

        // DELETE: services/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<GeplanterServiceDto>>> DeleteGeplanterService(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ServiceManager();
                    var entity = manager.GetGeplanterServiceById(id);
                    manager.DeleteGeplanterService(entity);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Set Kunde {id} to inactive");
                    return new ResponseObject<GeplanterServiceDto>(null, OKTAG, $"Removed GeplanterService {id} from database", 0);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<GeplanterServiceDto>(e.Message + e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<GeplanterServiceDto>(e.Message, ErrorCode.General);
                }
            });
            return await task;
        }



        // POST: services/suchen
        [HttpPost("suchen")]
        public async Task<ActionResult<ResponseObject<IEnumerable<GeplanterServiceDto>>>> GetSearchResult(GeplanterServiceDto dto)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ServiceManager();
                    var dtos = manager.GetSearchResult(dto.ConvertToEntity()).ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<GeplanterServiceDto>>(dtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<GeplanterServiceDto>>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }
    }
}
