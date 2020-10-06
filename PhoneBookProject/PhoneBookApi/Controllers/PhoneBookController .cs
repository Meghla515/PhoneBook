using Confluent.Kafka;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PhoneBookApi;
using PhoneBookApi.Repository;
using System.Collections.Generic;

namespace ASPCoreSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneBookController : ControllerBase
    {
        private readonly PhoneBookRepository phonebookRepository;

        public PhoneBookController(IConfiguration configuration, ProducerConfig config)
        {
            phonebookRepository = new PhoneBookRepository(configuration, config);
        }

        [AllowAnonymous]
        [Route("save-entry"), HttpPost]
        [ProducesResponseType(typeof(PhoneBook), 200)]
        public IActionResult SaveEntry(PhoneBook dto)
        {

            var data = phonebookRepository.Add(dto);

            return Ok(data);
        }

        [Route("/delete-entry/{pbid:Int}"), HttpDelete]
        [ProducesResponseType(202)]
        [AllowAnonymous]
        public IActionResult RemoveEntry(int pbid)
        {

            phonebookRepository.Remove(pbid);
            return Accepted();
        }

        [AllowAnonymous]
        [Route("get-entries"), HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PhoneBook>), 200)]
        public IActionResult GetPhoneBookEntries()
        {

            var data = phonebookRepository.FindAll();
            return Ok(data);
        }

        [AllowAnonymous]
        [Route("get-entry"), HttpGet]
        [ProducesResponseType(typeof(PhoneBook), 200)]
        public IActionResult GetPhoneBookEntry(int pbid)
        {

            var data = phonebookRepository.FindByID(pbid);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPut("")]
        [ProducesResponseTypeAttribute(typeof(PhoneBook), 200)]
        public IActionResult UpdateEntry(PhoneBook dto)
        {
            var result = phonebookRepository.Update(dto);
            if (result == null)
            {
                return BadRequest("Couldn't update entry. Please check your data or try again later.");

            }
            return Ok(result);
        }
    }
}