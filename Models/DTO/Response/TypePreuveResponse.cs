using Projet.Models.Entity;

namespace Projet.Models.DTO.Response;

public class TypePreuveResponse(TypePreuveEntity t)
{
    public int Id { get; set; } = t.Id;
    public string? Nom { get; set; } = t.Nom;

    public static IList<TypePreuveResponse> ToListTypePreuveResponse(List<TypePreuveEntity> typePreuves)
    {
        return typePreuves.Select(ToTypePreuveResponse).ToList();
    }

    public static TypePreuveResponse ToTypePreuveResponse(TypePreuveEntity typePreuve)
    {
        return new TypePreuveResponse(typePreuve);
    }
}
