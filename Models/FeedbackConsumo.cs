using GsDotNet.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("FEEDBACK_CONSUMO")]
public class FeedbackConsumo
{
    [Key]
    [Column("ID_FEEDBACK")]
    public int IdFeedback { get; set; }

    [Required]
    [Column("ID_USUARIO")]
    public int IdUsuario { get; set; }

    [Column("MENSAGEM_FEEDBACK", TypeName = "NVARCHAR2(255)")]
    public string MensagemFeedback { get; set; }

    [ForeignKey("IdUsuario")]
    [JsonIgnore]
    public virtual UsuarioEnergia? Usuario { get; set; }
}
