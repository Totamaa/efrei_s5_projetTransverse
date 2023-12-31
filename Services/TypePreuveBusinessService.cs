using Microsoft.EntityFrameworkCore;
using Projet.Models.Context;
using Projet.Models.DTO.Response;
using Projet.Services.Interfaces;

namespace Projet.Services;

public class TypePreuveBusinessService(
    MySqlContext context
) : ITypePreuveBusinessService
{
    private readonly MySqlContext _context = context;

    #region GET

    /// <summary>
    /// Récupère tous les types de preuves
    /// </summary>
    /// <returns>liste de type de preuve</returns>
    public async Task<IList<TypePreuveResponse>> GetAll()
    {
        var typePreuves = await _context.TypePreuves.ToListAsync();
        return TypePreuveResponse.ToListTypePreuveResponse(typePreuves);
    }

    #endregion
}
