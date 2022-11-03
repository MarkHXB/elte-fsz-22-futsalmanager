namespace FutsalManager.Models
{
    public partial class Transfer
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public DateTime History { get; set; }

        public virtual Player Player { get; set; } = null!;
        public virtual Team Team { get; set; } = null!;
    }
}
