using Business.Abstract;
using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TextUploadController : ControllerBase
    {
        private readonly IChunkHolderService _chunkHolderService;

        public TextUploadController(IChunkHolderService chunkHolderService)
        {
            _chunkHolderService = chunkHolderService;
        }

        [HttpPost("upload")]
        public IActionResult UploadChunkString([FromBody] ChunkRequestDto chunkRequestDto)
        {
            if (chunkRequestDto is null) return BadRequest("Request body cannot be null");

            var addResult=_chunkHolderService.AddChunkDictionary(chunkRequestDto);

            if (addResult) return Accepted(new { status = "Accepted" });

            return Ok(new { status = "Chunk received" });
        }
    }
}
