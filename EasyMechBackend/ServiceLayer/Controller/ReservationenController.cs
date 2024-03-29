﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.BusinessLayer;
using System;
using EasyMechBackend.Common.DataTransferObject;
using EasyMechBackend.Common.DataTransferObject.DTOs;
using EasyMechBackend.Common.Exceptions;
using log4net;
using EasyMechBackend.Common;

namespace EasyMechBackend.ServiceLayer.Controller
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ReservationenController : ControllerBase
    {
        private const string OKTAG = ResponseObject<object>.OKTAG;

        private static readonly ILog log = LogManager.GetLogger
             (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        // GET: /Reservationen
        [HttpGet]
        public async Task<ActionResult<ResponseObject<IEnumerable<ReservationDto>>>> GetReservationen()
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ReservationManager();
                    var dtos = manager.GetReservationen().ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<ReservationDto>>(dtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<ReservationDto>>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // GET: /Reservationen/2
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<ReservationDto>>> GetReservation(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ReservationManager();
                    var dto = manager.GetReservationById(id).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called on Entity {id}");
                    return new ResponseObject<ReservationDto>(dto);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<ReservationDto>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // POST: Reservationen/
        [HttpPost]
        public async Task<ActionResult<ResponseObject<ReservationDto>>> AddReservation(ReservationDto toAddDto)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ReservationManager();
                    ReservationDto addedDto = manager.AddReservation(toAddDto.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Reserevation {addedDto.Id} added with Übergabe {addedDto.Uebergabe} and Rücknahme {addedDto.Ruecknahme}");
                    return new ResponseObject<ReservationDto>(addedDto);

                }
                catch (ReservationException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a Reservation Exception: {e.Message}");
                    return new ResponseObject<ReservationDto>(e.Message, ErrorCode.ReservationException);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<ReservationDto>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // PUT: Reservationen/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<ReservationDto>>> PutReservation(long id, ReservationDto toEditDto)
        {

            var task = Task.Run(() =>
            {
                try
                {
                    if (id != toEditDto.Id)
                    {
                        return new ResponseObject<ReservationDto>("ID in URL does not match ID in the request's body data", ErrorCode.IDMismatch);
                    }
                    var manager = new ReservationManager();
                    var editedDto = manager.UpdateReservation(toEditDto.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Reservation {id} updated");
                    return new ResponseObject<ReservationDto>(editedDto);
                }
                catch (ReservationException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a Reservation Exception: {e.Message}");
                    return new ResponseObject<ReservationDto>(e.Message, ErrorCode.ReservationException);
                }

                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.Message} - {e.InnerException.Message}");
                    return new ResponseObject<ReservationDto>(e.Message + e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<ReservationDto>(e.Message, ErrorCode.General);
                }

            });

            return await task;
        }

        // DELETE: Reservationen/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<ReservationDto>>> DeleteReservation(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ReservationManager();
                    var entity = manager.GetReservationById(id);
                    manager.DeleteReservation(entity);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Set Kunde {id} to inactive");
                    return new ResponseObject<ReservationDto>(null, OKTAG, $"Removed Reservation {id} from database", 0);
                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<ReservationDto>(e.Message + e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<ReservationDto>(e.Message, ErrorCode.General);
                }

            });
            return await task;
        }

        // POST: reservationen/suchen
        [HttpPost("suchen")]
        public async Task<ActionResult<ResponseObject<IEnumerable<ReservationDto>>>> GetSearchResult(ServiceSearchDto s)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ReservationManager();
                    var dtos = manager.GetServiceSearchResult(s).ConvertToDtos();
                    var response = new ResponseObject<IEnumerable<ReservationDto>>(dtos);
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return response;
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<IEnumerable<ReservationDto>>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }
    }
}
