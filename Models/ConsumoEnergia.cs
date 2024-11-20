using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GsDotNet.Models
{
    [Table("CONSUMO_ENERGIA")]
    public class ConsumoEnergia
    {
        [Key]
        [Column("ID_CONSUMO")]
        public int IdConsumo { get; set; }

        [Required]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("DATA_REGISTRO", TypeName = "DATE")]
        public DateTime DataRegistro { get; set; } = DateTime.Now; // Preenche automaticamente com a data atual

        [Column("CONSUMO_KWH", TypeName = "NUMBER(10,2)")]
        public decimal ConsumoKwh { get; set; }

        [ForeignKey("IdUsuario")]
        [JsonIgnore]
        public virtual UsuarioEnergia? Usuario { get; set; }
    }
}
