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
                    var dto = manager.GetReseervationById(id).ConvertToDto();
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
                    var entity = manager.GetReseervationById(id);
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
        public async Task<ActionResult<ResponseObject<IEnumerable<ReservationDto>>>> GetSearchResult(ReservationDto dto)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ReservationManager();
                    var dtos = manager.GetSearchResult(dto.ConvertToEntity()).ConvertToDtos();
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


        /*
        #region übergabe und rücknahme separate api - falls wirds doch brauchen

        ////////////////////////////////////
        ////////////Uebergaben//////////////
        ////////////////////////////////////


        // GET: /Reservationen/2/Uebergabe
        [HttpGet("{id}/Uebergabe")]
        public async Task<ActionResult<ResponseObject<MaschinenUebergabeDto>>> GetMaschinenuebergabe(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ReservationManager();
                    var dto = manager.GetMaschinenUebergabe(id).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return new ResponseObject<MaschinenUebergabeDto>(dto);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<MaschinenUebergabeDto>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // POST: Reservationen/2/Uebergabe
        [HttpPost("{id}/Uebergabe")]
        public async Task<ActionResult<ResponseObject<ReservationDto>>> AddMaschinenuergabe(long id, MaschinenUebergabeDto toAddUebergabeDto)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    if (id != toAddUebergabeDto.ReservationsId)
                    {
                        return new ResponseObject<ReservationDto>(" Reservation ID in URL does not match ID in the request's body data", ErrorCode.IDMismatch);
                    }
                    var manager = new ReservationManager();
                    ReservationDto reservationAffected = manager.AddUebergabe(toAddUebergabeDto.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Resrevation {reservationAffected.Id} got Uergabe {reservationAffected.Uebergabe.Id}");
                    return new ResponseObject<ReservationDto>(reservationAffected);

                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<ReservationDto>("DB Update Exception: " + e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<ReservationDto>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // PUT: Reservationen/5/Uergabe
        [HttpPut("{id}/Uebergabe")]
        public async Task<ActionResult<ResponseObject<ReservationDto>>> EditMaschinenuergabe(long id, MaschinenUebergabeDto toEditUergabeDto)
        {

            var task = Task.Run(() =>
            {
                try
                {
                    if (id != toEditUergabeDto.ReservationsId)
                    {
                        return new ResponseObject<ReservationDto>("Reservation ID in URL does not match ID in the request's body data", ErrorCode.IDMismatch);
                    }
                    var manager = new ReservationManager();
                    var reservationAffected = manager.UpdateUebergabe(toEditUergabeDto.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Reservation {id} updated");
                    return new ResponseObject<ReservationDto>(reservationAffected);
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


        ////////////////////////////////////
        ////////////Rueckgaben//////////////
        ////////////////////////////////////


        // GET: /Reservationen/2/Ruecknahme
        [HttpGet("{id}/Ruecknahme")]
        public async Task<ActionResult<ResponseObject<MaschinenRuecknahmeDto>>> GetMaschinenRuecknahmee(long id)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    var manager = new ReservationManager();
                    var dto = manager.GetMaschinenRuecknahme(id).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called");
                    return new ResponseObject<MaschinenRuecknahmeDto>(dto);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<MaschinenRuecknahmeDto>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // POST: Reservationen/2/Ruecknahme
        [HttpPost("{id}/Ruecknahme")]
        public async Task<ActionResult<ResponseObject<ReservationDto>>> AddMaschinenRuecknahme(long id, MaschinenRuecknahmeDto toAddRuecknahmeDto)
        {
            var task = Task.Run(() =>
            {
                try
                {
                    if (id != toAddRuecknahmeDto.ReservationsId)
                    {
                        return new ResponseObject<ReservationDto>("Reservation ID in URL does not match ID in the request's body data", ErrorCode.IDMismatch);
                    }
                    var manager = new ReservationManager();
                    ReservationDto reservationAffected = manager.AddRuecknahme(toAddRuecknahmeDto.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Resrevation {reservationAffected.Id} got Ruecknahme {reservationAffected.Ruecknahme.Id}");
                    return new ResponseObject<ReservationDto>(reservationAffected);

                }
                catch (DbUpdateException e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched a DB Update Exception: {e.InnerException.Message}");
                    return new ResponseObject<ReservationDto>("DB Update Exception: " + e.InnerException.Message, ErrorCode.DBUpdate);
                }
                catch (Exception e)
                {
                    log.Error($"{System.Reflection.MethodBase.GetCurrentMethod().Name} catched Exception: {e.Message}");
                    return new ResponseObject<ReservationDto>(e.Message, ErrorCode.General);
                }
            });

            return await task;
        }

        // PUT: Reservationen/5/Ruecknahme
        [HttpPut("{id}/Ruecknahme")]
        public async Task<ActionResult<ResponseObject<ReservationDto>>> EditMaschinenruecknahme(long id, MaschinenRuecknahmeDto toEditRuecknahmeDto)
        {

            var task = Task.Run(() =>
            {
                try
                {
                    if (id != toEditRuecknahmeDto.ReservationsId)
                    {
                        return new ResponseObject<ReservationDto>("Reservation ID in URL does not match ID in the request's body data", ErrorCode.IDMismatch);
                    }
                    var manager = new ReservationManager();
                    var reservationAffected = manager.UpdateRuecknahme(toEditRuecknahmeDto.ConvertToEntity()).ConvertToDto();
                    log.Debug($"{System.Reflection.MethodBase.GetCurrentMethod().Name} was called: Reservation {id} updated");
                    return new ResponseObject<ReservationDto>(reservationAffected);
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


#endregion
*/
    }
}
