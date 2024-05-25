using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GakkoHorizontalSlice.Context;
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
        var client = await dbContext.Clients.FirstOrDefaultAsync(client => client.IdClient == idClient);
        
        if (client == null)
            return;

        if (client.ClientTrips.Count == 0)
            return;
        
        dbContext.Clients.Remove(client);
        dbContext.SaveChangesAsync();
    }
}
