using Projet.Models.Entity;

namespace Projet.Models.DTO.Request;

public class CreatePreuveRequest
{
    public int? DossierId;
    public int? TypePreuveId;
    public string? Contenu;

    public PreuveEntity ToEntity()
    {
        return new PreuveEntity
        {
            DossierId = (int)DossierId!,
            TypePreuveId = (int)TypePreuveId!,
            Contenu = Contenu
        };
    }
}