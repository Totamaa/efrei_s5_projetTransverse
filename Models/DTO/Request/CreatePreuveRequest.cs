using Projet.Models.Entity;

namespace Projet.Models.DTO.Request;

public class CreatePreuveRequest(
    int dossierId,
    int typePreuveId,
    string? contenu = null
)
{
    public int DossierId { get; set; } = dossierId;
    public int TypePreuveId { get; set; } = typePreuveId;
    public string? Contenu { get; set; } = contenu;

    public PreuveEntity ToEntity()
    {
        return new PreuveEntity(
            DossierId,
            TypePreuveId,
            Contenu
        );
    }
}