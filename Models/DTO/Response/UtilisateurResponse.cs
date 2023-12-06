using System.ComponentModel.DataAnnotations;
using Projet.Models.Entity;

namespace Projet.Models.DTO.Response;

public class UtilisateurResponse
{
    public int Id { get; set; }

    public string Pseudo { get; set; }

    public bool IsAdmin { get; set; }

    public UtilisateurResponse(UtilisateurEntity utilisateurEntity)
    {
        Id = utilisateurEntity.Id;
        Pseudo = utilisateurEntity.Pseudo;
        IsAdmin = utilisateurEntity.IsAdmin;
    }

    public UtilisateurResponse(int id, string pseudo, bool isAdmin)
    {
        Id = id;
        Pseudo = pseudo;
        IsAdmin = isAdmin;
    }
}
