using GakkoHorizontalSlice.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GakkoHorizontalSlice.Controllers;

[Route("api/")]
[ApiController]
public class TripsController : ControllerBase
{
    public TripsController()
    {
    }
    
    [HttpGet]
	[Route("trips")]
    public IActionResult GetTrips()
    {
        return Ok();
    }
}