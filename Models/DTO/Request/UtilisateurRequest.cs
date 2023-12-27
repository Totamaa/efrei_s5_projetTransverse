using System.ComponentModel.DataAnnotations;
using Projet.Models.Entity;

namespace Projet.Models.DTO.Request;

public class UtilisateurRequest
{
    public string? Pseudo { get; set; }

    public string? MotDePasse { get; set; }

    static public UtilisateurEntity ToEntity(UtilisateurRequest utilisateurRequest)
    {
        return new UtilisateurEntity
        {
            Pseudo = utilisateurRequest.Pseudo!,
            MotDePasse = utilisateurRequest.MotDePasse!,
            IsAdmin = false
        };
    }
}
