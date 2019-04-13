﻿using EasyMechBackend.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    [Table("Kunden", Schema = "public")]
    public class Kunde
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(128)]
        [Required]
        public string Firma { get; set; }

        [MaxLength(128)]
        public string Vorname { get; set; }

        [MaxLength(128)]
        public string Nachname { get; set; }

        [MaxLength(128)]
        public string Adresse { get; set; }

        [MaxLength(128)]
        public string PLZ { get; set; }

        [MaxLength(128)]
        public string Ort { get; set; }

        [MaxLength(128)]
        public string Email { get; set; }

        [MaxLength(128)]
        public string Telefon { get; set; }

        public string Notiz { get; set; }

        [Required]
        public bool? IsActive { get; set; }


        //Upcoming Milestone2:


        //Relationships
        // -------------------------------------------
        //public List<Maschine> Maschinen { get; set; }
        // -------------------------------------------



        public void Validate()
        {
            ClipTo128Chars();
            FillRequiredFields();
        }

        private void FillRequiredFields()
        {
            if (Firma == null) Firma = "";
            if (IsActive == null) IsActive = true;
        }

        private void ClipTo128Chars()
        {
            Firma = Firma.ClipTo128Chars();
            Vorname = Vorname.ClipTo128Chars();
            Nachname = Nachname.ClipTo128Chars();
            Adresse = Adresse.ClipTo128Chars();
            PLZ = PLZ.ClipTo128Chars();
            Ort = Ort.ClipTo128Chars();
            Email = Email.ClipTo128Chars();
            Telefon = Telefon.ClipTo128Chars();

        }
    }
}
