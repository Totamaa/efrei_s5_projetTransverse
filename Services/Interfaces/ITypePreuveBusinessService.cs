using Projet.Models.DTO.Response;

namespace Projet.Services.Interfaces;

public interface ITypePreuveBusinessService
{
    #region GET
    Task<IList<TypePreuveResponse>> GetAll();

    #endregion
}
