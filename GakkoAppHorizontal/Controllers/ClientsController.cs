using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GakkoHorizontalSlice.Context;
using GakkoHorizontalSlice.Exceptions;
using GakkoHorizontalSlice.Models;
using GakkoHorizontalSlice.Services;

namespace GakkoHorizontalSlice.Controllers;

[Route("api/clients")]
[ApiController]
public class ClientsController : ControllerBase
{
    private ClientsService service;

    public ClientsController(ClientsService service)
    {
        this.service = service;
    }
    
    [HttpDelete("{idClient:int}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        try {
            await service.DeleteClient(idClient);
            return NoContent();
        } catch (ValidationException e) {
            return BadRequest(e.Message);
        } catch (Exception) {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
