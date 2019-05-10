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
    public class TransaktionenController : ControllerBase
    {
        private const string OKTAG = ResponseObject<object>.OKTAG;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
             (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // GET: /Transaktionen/
        [HttpGet]
        public async Task<ActionResult<ResponseObject<IEnumerable<TransaktionDto>>>> GetTransaktionen()
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new TransaktionManager();
                    var transaktionDtos = manager.GetTransaktionen().ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<TransaktionDto>>(transaktionDtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<TransaktionDto>>(e.Message, ErrorCode.General);
                }
            });
            return await task;
        }

        // GET: /Transaktionen/2
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<TransaktionDto>>> GetTransaktion(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new TransaktionManager();
                    TransaktionDto transaktionDto = manager.GetTransaktionById(id).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return new ResponseObject<TransaktionDto>(transaktionDto);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<TransaktionDto>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // POST: Transaktionen/
        [HttpPost]
        public async Task<ActionResult<ResponseObject<TransaktionDto>>> PostTransaktion(TransaktionDto transaktion)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new TransaktionManager();
                    TransaktionDto dto = manager.AddTransaktion(transaktion.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Transaktion {dto.Id} added");
                    return new ResponseObject<TransaktionDto>(dto);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<TransaktionDto>(e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<TransaktionDto>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // PUT: Transaktionen/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<TransaktionDto>>> PutTransaktion(long id, TransaktionDto transaktion)
        {

            var task = Task.Run(() =>
            {
                try
                {
                    if (id != transaktion.Id)
                    {
                        return new ResponseObject<TransaktionDto>("ID in URL does not match ID in the request's body data", ErrorCode.IDMismatch);
                    }
                    var manager = new TransaktionManager();
                    TransaktionDto changedTransaktionDto = manager.UpdateTransaktion(transaktion.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Transaktion {id} updated");
                    return new ResponseObject<TransaktionDto>(changedTransaktionDto);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<TransaktionDto>(e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<TransaktionDto>(e.Message, ErrorCode.General);
                }

            });

            return await task;
        }


        // POST: transaktionen/suchen
        [HttpPost("suchen")]
        public async Task<ActionResult<ResponseObject<IEnumerable<TransaktionDto>>>> GetSearchResult(TransaktionDto k)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new TransaktionManager();
                    var transaktionDtos = manager.GetSearchResult(k.ConvertToEntity()).ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<TransaktionDto>>(transaktionDtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<TransaktionDto>>(e.Message, ErrorCode.General);
                }
            });
            return await task;
        }
    }
}
