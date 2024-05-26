using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GakkoHorizontalSlice.Context;
using GakkoHorizontalSlice.Exceptions;
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
                Countries = trip.Countries.Select(country => new CountryDTO(){ Name = country.Name }),
                Clients = trip.ClientTrips.Select(clientTrip => new ClientDTO()
                {
                    FirstName = clientTrip.Client.FirstName,
                    LastName = clientTrip.Client.LastName
                })
            })
            .ToListAsync();
        return trips;
    }
	
	public async Task SignUpClientForTrip(int idTrip, SignUpClientForTripDTO signUpClientForTripDTO)
	{
        var client = await dbContext.Clients
            .SingleOrDefaultAsync(client => client.Pesel == signUpClientForTripDTO.Pesel);

        if (client == null) {
            client = new Client()
            {
                FirstName = signUpClientForTripDTO.FirstName,
                LastName = signUpClientForTripDTO.LastName,
                Email = signUpClientForTripDTO.Email,
                Telephone = signUpClientForTripDTO.Telephone,
                Pesel = signUpClientForTripDTO.Pesel
            };

            dbContext.Clients.Add(client);
        } else {
            var clientTrip = await dbContext.ClientTrips
                .SingleOrDefaultAsync(clientTrip => clientTrip.Trip.IdTrip == idTrip && clientTrip.Client.IdClient == client.IdClient);
            if (clientTrip != null)
                throw new ValidationException("Client already signed up for this trip");
        }

        var trip = await dbContext.Trips.SingleOrDefaultAsync(trip => trip.IdTrip == idTrip);
        if (trip == null)
            throw new ValidationException("Trip does not exist");

        dbContext.ClientTrips.Add(new ClientTrip()
        {
            IdClient = client.IdClient,
            IdTrip = idTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = signUpClientForTripDTO.PaymentDate
        });

        await dbContext.SaveChangesAsync();
	}
}
