using Confluent.Kafka;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PhoneBookPersistense.Repository.PhoneBookRepository;
using PhoneBookService.DataTransfer.Model;
using PhoneBookService.Services.PBService;
using System.Collections.Generic;

namespace ASPCoreSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPBService pbService;

        public PhoneBookController(IPBService service)
        {
            this.pbService = service;
        }

        [AllowAnonymous]
        [Route("save-entry"), HttpPost]
        [ProducesResponseType(typeof(PhoneBookDTO), 200)]
        public IActionResult SaveEntry(PhoneBookDTO dto)
        {

            var data = pbService.SaveEntry(dto);

            return Ok(data);
        }

        [Route("/delete-entry/{pbid:Int}"), HttpDelete]
        [ProducesResponseType(202)]
        [AllowAnonymous]
        public IActionResult RemoveEntry(int pbid)
        {

            pbService.RemoveEntry(pbid);
            return Accepted();
        }

        [AllowAnonymous]
        [Route("get-entries"), HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PhoneBookDTO>), 200)]
        public IActionResult GetPhoneBookEntries()
        {

            var data = pbService.GetEntries();
            return Ok(data);
        }

        [AllowAnonymous]
        [Route("get-entry"), HttpGet]
        [ProducesResponseType(typeof(PhoneBookDTO), 200)]
        public IActionResult GetPhoneBookEntry(int pbid)
        {

            var data = pbService.GetEntryById(pbid);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPut("")]
        [ProducesResponseTypeAttribute(typeof(PhoneBookDTO), 200)]
        public IActionResult UpdateEntry(PhoneBookDTO dto)
        {
            pbService.UpdateEntry(dto);          
            return Ok();
        }
    }
}