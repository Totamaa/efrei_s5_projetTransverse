using Projet.Models.Entity;

namespace Projet.Models.DTO.Response;

public class PreuveAndTypeResponse(PreuveEntity p)
{
    public int Id { get; set; } = p.Id;
    public string? Contenu { get; set; } = p.Contenu;
    public DateTime CreatedAt { get; set; } = p.CreatedAt;
    public DateTime ModifiedAt { get; set; } = p.ModifiedAt;
    public int TypePreuveId { get; set; } = p.TypePreuveEntity!.Id;
    public string? TypePreuveNom { get; set; } = p.TypePreuveEntity!.Nom;

    public static IList<PreuveAndTypeResponse> ToListPreuveAndTypeResponse(List<PreuveEntity> preuves)
    {
        return preuves.Select(ToPreuveAndTypeResponse).ToList();
    }

    public static PreuveAndTypeResponse ToPreuveAndTypeResponse(PreuveEntity preuve)
    {
        return new PreuveAndTypeResponse(preuve);
    }
}