using Projet.Models.Entity;

namespace Projet.Models.DTO.Response;

public class PreuveResponse(PreuveEntity p)
{
    public int Id { get; set; } = p.Id;
    public string Contenu { get; set; } = p.Contenu;
    public DateTime CreatedAt { get; set; } = p.CreatedAt;
    public DateTime ModifiedAt { get; set; } = p.ModifiedAt;

    public static IList<PreuveResponse> ToListPreuveResponse(List<PreuveEntity> preuves)
    {
        return preuves.Select(ToPreuveResponse).ToList();
    }

    public static PreuveResponse ToPreuveResponse(PreuveEntity preuve)
    {
        return new PreuveResponse(preuve);
    }
}
