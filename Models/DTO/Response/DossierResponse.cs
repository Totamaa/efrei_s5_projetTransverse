using Projet.Models.Entity;

namespace Projet.Models.DTO.Response;

public class DossierResponse(DossierEntity d)
{
    public int Id { get; set; } = d.Id;
    public DateTime CreatedAt { get; set; } = d.CreatedAt;


    public static IList<DossierResponse> ToListDossierResponse(List<DossierEntity> dossiers)
    {
        return dossiers.Select(d => new DossierResponse(d)).ToList();
    }

    public static DossierResponse ToDossierResponse(DossierEntity dossier)
    {
        return new DossierResponse(dossier);
    }
}
