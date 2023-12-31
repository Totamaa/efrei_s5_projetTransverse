using System.ComponentModel.DataAnnotations;
using Projet.Models.Entity;

namespace Projet.Models.DTO.Request;

public class UtilisateurRequest(
    string pseudo,
    string motDePasse
)
{
    public string Pseudo { get; set; } = pseudo;

    public string MotDePasse { get; set; } = motDePasse;

    static public UtilisateurEntity ToEntity(UtilisateurRequest utilisateurRequest)
    {
        return new UtilisateurEntity(
            utilisateurRequest.Pseudo,
            utilisateurRequest.MotDePasse
        );
    }
}
