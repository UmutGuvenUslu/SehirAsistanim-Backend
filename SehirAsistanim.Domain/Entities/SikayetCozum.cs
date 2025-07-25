﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SehirAsistanim.Domain.Entities
{
    [Table("sikayetcozumleri")]
    public class SikayetCozum
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("sikayetid")]
        public int SikayetId { get; set; }
        [Column("cozenkullanici")]
        public int CozenKullaniciId { get; set; }
        [Column("cozumaciklamasi")]
        public string? CozumAciklamasi { get; set; }
        [Column("cozumfotourl")]
        public string? CozumFotoUrl { get; set; }
        [Column("cozumetarihi")]
        public DateTime CozumeTarihi { get; set; }
    }
}
