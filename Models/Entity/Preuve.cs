﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet.Models.Entity
{
    public class Preuve : BaseEntity
    {
        [ForeignKey("DossierEntity")]
        [Required]
        public int DossierId { get; set; }

        [ForeignKey("TypePreuveEntity")]
        [Required]
        public int TypePreuveId { get; set; }

        [Required]
        public string Contenu { get; set; } // URL ou texte

        // Navigation property
        public virtual DossierEntity? DossierEntity { get; set; }

        public virtual TypePreuveEntity? TypePreuveEntity { get; set; }
    }
}