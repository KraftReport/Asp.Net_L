using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TableProjectComponentServiceTestWebAPI.Ticket
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketRepository ticketRepository;
        public TicketController(TicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        [HttpPost]
        public async Task<IActionResult> bulkInsert([FromBody] List<TicketDto> ticketDtos)
        {
            var ticketTable = ticketRepository.GetDataTable();
            foreach(var ticket in ticketDtos)
            {
                var qrCode = ticketRepository.generateQrCodeString(ticket.EventName);
                ticketTable.Rows.Add(ticket.UserId,ticket.EventName,DateTime.Now);
                ticketRepository.InsertPhoto(ticket.UserId, ticketRepository.generateQrCodeByte(ticket.EventName));
            }
            await ticketRepository.BulkTicketInsert(ticketTable);
            return Ok("tickets are inserted successfully");

        }

        [HttpGet]
        public async Task<IActionResult> GenerateQrCodePng()
        {
            try
            {
                var imageData = await ticketRepository.GetImageData(1);
                if (imageData != null)
                {
                    return File(imageData, "image/png");
                }
                else
                {
                    return NotFound("Image data not found.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{pagination}")]
        public async Task<IActionResult> GetTickets([FromQuery]int pageSize, [FromQuery]int pageNum)
        {
            return Ok(await ticketRepository.GetPaginatedTickets(pageSize, pageNum));
        }


    }
}
