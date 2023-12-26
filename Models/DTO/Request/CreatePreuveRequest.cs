using Projet.Models.Entity;

namespace Projet.Models.DTO.Request;

public class CreatePreuveRequest
{
    public int? DossierId;
    public int? TypePreuveId;
    public string? Contenu;

    static public CreatePreuveRequest ToEntity(PreuveEntity preuveEntity)
    {
        return new CreatePreuveRequest
        {
            DossierId = preuveEntity.DossierId,
            TypePreuveId = preuveEntity.TypePreuveId,
            Contenu = preuveEntity.Contenu
        };
    }
}