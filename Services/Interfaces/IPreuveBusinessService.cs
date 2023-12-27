using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Projet.Models.DTO.Request;
using Projet.Models.DTO.Response;

namespace Projet.Services.Interfaces;

public interface IPreuveBusinessService
{

    /***** CREATE *****/
    Task<int> CreatePreuve(CreatePreuveRequest createPreuveRequest);

    /***** GET *****/
    Task<PreuveResponse> GetPreuveById(int? id);
    Task<PreuveAndTypeResponse> GetPreuveAndTypeById(int? id);
    Task<IList<PreuveResponse>> GetPreuveByDossierId(int? dossierId);
    Task<IList<PreuveAndTypeResponse>> GetPreuveAndTypeByDossierId(int? dossierId);
    Task<IList<PreuveResponse>> GetPreuveByTypePreuveId(int? typePreuveId);
    Task<IList<PreuveResponse>> GetAllLastPreuves(int from = 0, int nb = 20);
    Task<IList<PreuveAndTypeResponse>> GetAllLastPreuvesAndType(int from = 0, int nb = 20);

    /***** UPDATE *****/
    Task<bool> UpdatePreuveContenu(int? id, string? nouveauContenu);

    /***** DELETE *****/
    Task<bool> DeletePreuveById(int? id);
}