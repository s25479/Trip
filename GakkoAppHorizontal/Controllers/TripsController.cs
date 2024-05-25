using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GakkoHorizontalSlice.Context;
using GakkoHorizontalSlice.Models;
using GakkoHorizontalSlice.Services;

namespace GakkoHorizontalSlice.Controllers;

[Route("api/trips")]
[ApiController]
public class TripsController : ControllerBase
{
    private TripsService service;

    public TripsController(TripsService service)
    {
        this.service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripDTO>>> GetTrips()
    {
        return Ok(await service.GetTrips());
    }
}
