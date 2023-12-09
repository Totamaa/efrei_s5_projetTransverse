using System.ComponentModel.DataAnnotations;
using Projet.Models.Entity;

namespace Projet.Models.DTO.Request;

public class ChangeUtilisateurPseudoRequest
{
    public int? UtilisateurId { get; set; }
    public string? NewPseudo { get; set; }
}
