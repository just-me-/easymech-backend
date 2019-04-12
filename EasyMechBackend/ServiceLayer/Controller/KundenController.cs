﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyMechBackend.ServiceLayer.DataTransferObject;
using EasyMechBackend.BusinessLayer;
using System;
using EasyMechBackend.DataAccessLayer;

namespace EasyMechBackend.ServiceLayer

{
    [Route("[controller]")]
    [ApiController]
    public class KundenController : ControllerBase
    {
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
                    return response;
                }
                catch (Exception e)
                {
                    return new ResponseObject<IEnumerable<KundeDto>>("error", e.Message);
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
                    return new ResponseObject<KundeDto>(kundeDto);
                }
                catch (Exception e)
                {
                    return new ResponseObject<KundeDto>("error", e.Message);
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
                    return new ResponseObject<KundeDto>(dto);
                }
                catch (DbUpdateException e)
                {
                    return new ResponseObject<KundeDto>("error", "DB Update Exception: " + e.InnerException.Message);
                }
                catch (Exception e)
                {
                    return new ResponseObject<KundeDto>("error", e.Message);
                }
            });

            return await task;
        }

        // PUT: kunden/
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<KundeDto>>> PutKunde(long id, KundeDto kunde)
        {

            var task = Task.Run(() =>
            {
                try
                {
                    if (id != kunde.Id)
                    {
                        return new ResponseObject<KundeDto>("error", "ID in URL does not match ID in the request's body data");
                    }
                    KundeDto changedKundeDto = KundeManager.UpdateKunde(kunde.ConvertToEntity()).ConvertToDto();
                    return new ResponseObject<KundeDto>(changedKundeDto);
                }
                catch (DbUpdateException e)
                {
                    return new ResponseObject<KundeDto>("error", e.Message + e.InnerException.Message);
                }
                catch (Exception e)
                {
                    return new ResponseObject<KundeDto>("error", e.Message);
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
                    return new ResponseObject<KundeDto>("ok", $"Set Kunde {id} to inactive");
                }
                catch (DbUpdateException e)
                {
                    return new ResponseObject<KundeDto>("error", e.Message + e.InnerException.Message);
                }
                catch (Exception e)
                {
                    return new ResponseObject<KundeDto>("error", e.Message);
                }

            });
            return await task;
        }
    }
}
