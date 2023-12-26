using Projet.Models.Context;
using Projet.Services.Interfaces;

namespace Projet.Services;

public class PreuveBusinessService : IPreuveBusinessService
{
    private readonly MySqlContext _context;

    public PreuveBusinessService(
        MySqlContext context
    )
    {
        _context = context;
    }
}