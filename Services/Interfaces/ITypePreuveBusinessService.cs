using Projet.Models.DTO.Response;

namespace Projet.Services.Interfaces;

public interface ITypePreuveBusinessService
{
    Task<IList<TypePreuveResponse>> GetAll();
}
