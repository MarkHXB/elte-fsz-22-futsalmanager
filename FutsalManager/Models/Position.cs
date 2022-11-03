namespace FutsalManager.Models
{
    public partial class Position
    {
        public Position()
        {
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }
        public string Pos { get; set; } = null!;

        public virtual ICollection<Player> Players { get; set; }
    }
}
