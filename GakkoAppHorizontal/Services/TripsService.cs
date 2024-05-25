using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GakkoHorizontalSlice.Context;
using GakkoHorizontalSlice.Models;

namespace GakkoHorizontalSlice.Services;

public class TripsService
{
    private readonly ApbdContext dbContext;
    
    public TripsService()
    {
        this.dbContext = new ApbdContext();
    }

    public async Task<IEnumerable<TripDTO>> GetTrips()
    {
        var trips = await dbContext.Trips
            .OrderByDescending(trip => trip.DateFrom)
            .Select(trip => new TripDTO()
            {
                Name = trip.Name,
                Description = trip.Description,
                DateFrom = trip.DateFrom,
                DateTo = trip.DateTo,
                MaxPeople = trip.MaxPeople,
                Countries = trip.IdCountries.Select(country => new CountryDTO(){ Name = country.Name }),
                Clients = trip.ClientTrips.Select(clientTrip => new ClientDTO()
                {
                    FirstName = clientTrip.IdClientNavigation.FirstName,
                    LastName = clientTrip.IdClientNavigation.LastName
                })
            })
            .ToListAsync();
        return trips;
    }
}
