using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.BusinessLayer;
using System;
using log4net;
using static EasyMechBackend.Common.EnumHelper;
using EasyMechBackend.Common.DataTransferObject;
using EasyMechBackend.Common.DataTransferObject.DTOs;
using EasyMechBackend.Common;
using EasyMechBackend.Common.Exceptions;

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


        // GET: /Services/0
        [HttpGet("{status}")]
        public async Task<ActionResult<ResponseObject<IEnumerable<ServiceDto>>>> GetServices(ServiceState status)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ServiceManager();
                    var dtos = manager.GetServices(status).ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<ServiceDto>>(dtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<ServiceDto>>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // GET: /Services
        [HttpGet]
        public async Task<ActionResult<ResponseObject<IEnumerable<ServiceDto>>>> GetAllServices()
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ServiceManager();
                    var dtos = manager.GetServices(0).ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<ServiceDto>>(dtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<ServiceDto>>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }


        // GET: /Services/id/2
        [HttpGet("id/{id}")]
        public async Task<ActionResult<ResponseObject<ServiceDto>>> GetServiceById(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ServiceManager();
                    var dto = manager.GetServiceById(id).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return new ResponseObject<ServiceDto>(dto);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<ServiceDto>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // POST: /services
        [HttpPost]
        public async Task<ActionResult<ResponseObject<ServiceDto>>> AddService(ServiceDto toAddDto)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ServiceManager();
                    ServiceDto addedDto = manager.AddService(toAddDto.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Service {addedDto.Id} added");
                    return new ResponseObject<ServiceDto>(addedDto);

                }
                catch (MaintenanceException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a Maintenance Exception: {e.Message}");
                    return new ResponseObject<ServiceDto>(e.Message, ErrorCode.MaintenanceException);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<ServiceDto>("DB Update Exception: " + e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<ServiceDto>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // PUT: services/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<ServiceDto>>> UpdateService(long id, ServiceDto toEditDto)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    if (id != toEditDto.Id)
                    {
                        return new ResponseObject<ServiceDto>("ID in URL does not match ID in the request's body data", ErrorCode.IDMismatch);
                    }
                    var manager = new ServiceManager();
                    var editedDto = manager.UpdateService(toEditDto.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Service {id} updated");
                    return new ResponseObject<ServiceDto>(editedDto);
                }
                catch (MaintenanceException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a Maintenance Exception: {e.Message}");
                    return new ResponseObject<ServiceDto>(e.Message, ErrorCode.MaintenanceException);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<ServiceDto>(e.Message + e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<ServiceDto>(e.Message, ErrorCode.General);
                }

            });

            return await task;
        }

        // DELETE: services/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<ServiceDto>>> DeleteService(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ServiceManager();
                    var entity = manager.GetServiceById(id);
                    manager.DeleteService(entity);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Delete Service with id {id} ");
                    return new ResponseObject<ServiceDto>(null, OKTAG, $"Removed Service {id} from database", 0);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<ServiceDto>(e.Message + e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<ServiceDto>(e.Message, ErrorCode.General);
                }
            });
            return await task;
        }

        // POST: services/suchen
        [HttpPost("suchen")]
        public async Task<ActionResult<ResponseObject<IEnumerable<ServiceDto>>>> GetSearchResult(ServiceSearchDto dto)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ServiceManager();
                    var dtos = manager.GetServiceSearchResult(dto).ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<ServiceDto>>(dtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<ServiceDto>>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }
    }
}
