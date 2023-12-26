using Microsoft.EntityFrameworkCore;
using Projet.Models.Context;
using Projet.Models.DTO.Response;
using Projet.Services.Interfaces;

namespace Projet.Services;

public class TypePreuveBusinessService : ITypePreuveBusinessService
{
    private readonly MySqlContext _context;

    public TypePreuveBusinessService(
        MySqlContext context
    )
    {
        _context = context;
    }

    public async Task<IList<TypePreuveResponse>> GetAll()
    {
        var typePreuves = await _context.TypePreuves.ToListAsync();
        return TypePreuveResponse.ToListTypePreuveResponse(typePreuves);
    }
}
