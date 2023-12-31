using System.ComponentModel.DataAnnotations;
using Projet.Models.Entity;

namespace Projet.Models.DTO.Request;

public class ChangeUtilisateurPseudoRequest(
    int utilisateurId,
    string newPseudo
)
{
    public int UtilisateurId { get; set; } = utilisateurId;
    public string NewPseudo { get; set; } = newPseudo;
}
