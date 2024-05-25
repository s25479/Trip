using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GakkoHorizontalSlice.Context;
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
        await service.DeleteClient(idClient);
        return NoContent();
    }
}
