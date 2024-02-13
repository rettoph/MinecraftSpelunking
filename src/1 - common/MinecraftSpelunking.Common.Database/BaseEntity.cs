using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinecraftSpelunking.Common.Database
{
    public record BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
