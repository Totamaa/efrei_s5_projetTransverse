using Projet.Models.Entity;

namespace Projet.Models.DTO.Response;

public class DossierResponse
{
    public int Id { get; set; }
    
    public DossierResponse(DossierEntity d)
    {
        d.Id = Id;
    }

    public static IList<DossierResponse> ToListDossierResponse(List<DossierEntity> dossiers)
    {
        return dossiers.Select(d => new DossierResponse(d)).ToList();
    }

    public static DossierResponse ToDossierResponse(DossierEntity dossier)
    {
        return new DossierResponse(dossier);
    }
}
