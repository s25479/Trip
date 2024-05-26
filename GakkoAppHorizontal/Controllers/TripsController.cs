using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GakkoHorizontalSlice.Context;
using GakkoHorizontalSlice.Exceptions;
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
        try {
            return Ok(await service.GetTrips());
        } catch (Exception) {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
	
    [HttpPost("{idTrip:int}/clients")]
    public async Task<IActionResult> SignUpClientForTrip([FromBody] SignUpClientForTripDTO signUpClientForTripDTO)
    {
        try {
            await service.SignUpClientForTrip(signUpClientForTripDTO);
            return Ok();
        } catch (ValidationException e) {
            return BadRequest(e.Message);
        } catch (Exception) {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
