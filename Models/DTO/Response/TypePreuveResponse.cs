using Projet.Models.Entity;

namespace Projet.Models.DTO.Response;

public class TypePreuveResponse
{
    public int Id { get; set; }
    public string Nom { get; set; }
    
    public TypePreuveResponse(TypePreuveEntity t)
    {
        t.Id = Id;
        t.Nom = Nom;
    }

    public static IList<TypePreuveResponse> ToListTypePreuveResponse(List<TypePreuveEntity> typePreuves)
    {
        return typePreuves.Select(t => new TypePreuveResponse(t)).ToList();
    }

    public static TypePreuveResponse ToTypePreuveResponse(TypePreuveEntity typePreuve)
    {
        return new TypePreuveResponse(typePreuve);
    }
}