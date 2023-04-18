using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dominio.Models
{
	public class RefreshToken
	{
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }

        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created { get; set; }

        public DateTime getCreatedPlusDays(int days)
        {

            return this.Created.AddDays(days);

        }

        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
        public bool IsRevoked => Revoked != null;
        public int UsuarioId { get; set; }
    }
}

