using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;

namespace Projet.Services.Interfaces;

public interface IPreuveBusinessService
{

    #region CREATE
    Task<int> CreatePreuve(CreatePreuveRequest createPreuveRequest);

    #endregion

    #region GET
    Task<PreuveResponse> GetPreuveById(int? id);
    Task<PreuveAndTypeResponse> GetPreuveAndTypeById(int? id);
    Task<IList<PreuveResponse>> GetPreuveByDossierId(int? dossierId);
    Task<IList<PreuveAndTypeResponse>> GetPreuveAndTypeByDossierId(int? dossierId);
    Task<IList<PreuveResponse>> GetPreuveByTypePreuveId(int? typePreuveId);
    Task<IList<PreuveResponse>> GetAllLastPreuves(int from = 0, int nb = 20);
    Task<IList<PreuveAndTypeResponse>> GetAllLastPreuvesAndType(int from = 0, int nb = 20);

    #endregion

    #region UPDATE
    Task<bool> UpdatePreuveContenu(int? id, string? nouveauContenu);

    #endregion

    #region DELETE
    Task<bool> DeletePreuveById(int? id);

    #endregion
}