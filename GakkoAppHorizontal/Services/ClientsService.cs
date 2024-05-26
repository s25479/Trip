using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GakkoHorizontalSlice.Context;
using GakkoHorizontalSlice.Exceptions;
using GakkoHorizontalSlice.Models;

namespace GakkoHorizontalSlice.Services;

public class ClientsService
{
    private readonly ApbdContext dbContext;
    
    public ClientsService()
    {
        this.dbContext = new ApbdContext();
    }

    public async Task DeleteClient(int idClient)
    {
        var client = await dbContext.Clients.SingleOrDefaultAsync(client => client.IdClient == idClient);
        
        if (client == null)
			throw new ValidationException("Client does not exist");

        if (client.ClientTrips.Count > 0)
            throw new ValidationException("Client has assigned trips");
        
        dbContext.Clients.Remove(client);
        await dbContext.SaveChangesAsync();
    }
}
