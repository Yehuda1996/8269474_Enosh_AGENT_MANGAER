using System.ComponentModel.DataAnnotations;

namespace AgentsRest.Models
{
    public enum TargetStatus
    {
        Alive,
        Eliminated
    }
    public class TargetModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        [Required]
        [StringLength(100)]
        public required string Position { get; set; }
        [Required]
        public required string Image { get; set; }
        public int Coordinate_x { get; set; } = -1;
        public int Coordinate_y { get; set; } = -1;
        public TargetStatus Status { get; set; } = TargetStatus.Alive;
    }
}
