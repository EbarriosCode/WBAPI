using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WBAPI.Application.Features.Albums.Commands.CreateAlbum;
using WBAPI.Application.Features.Albums.Commands.DeleteAlbum;
using WBAPI.Application.Features.Albums.Commands.UpdateAlbum;
using WBAPI.Application.Features.Albums.DTOs;
using WBAPI.Application.Features.Albums.Queries.GetAlbumById;
using WBAPI.Application.Features.Albums.Queries.GetAllAlbums;

namespace WBAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AlbumsController(ISender sender) : ControllerBase
    {        
        /// <summary>Get all of the active albums</summary>
        [HttpGet]
        [AllowAnonymous] // Public read
        [ProducesResponseType(typeof(IReadOnlyList<AlbumDto>), 200)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var result = await sender.Send(new GetAllAlbumsQuery(), ct);
            return Ok(result);
        }
        
        /// <summary>Get one album by Id</summary>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AlbumDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new GetAlbumByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        
        /// <summary>Create a new album</summary>
        [HttpPost]
        [ProducesResponseType(typeof(AlbumDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateAlbumDto dto, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreateAlbumCommand(dto.Name, dto.Artist, dto.GenreId, dto.Year), cancellationToken);

            if (!result.Success) return BadRequest(result);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Data!.Id },
                result);
        }

       
        /// <summary>Update some existing album</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(AlbumDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAlbumDto dto, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateAlbumCommand(id, dto.Name, dto.Artist, dto.GenreId, dto.Year), cancellationToken);

            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>Delete an album (soft-delete)</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await sender.Send(new DeleteAlbumCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
