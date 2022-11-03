namespace FutsalManager.Models
{
    public partial class Attribute
    {
        public Attribute()
        {
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }
        public int Dribbling { get; set; }
        public int Passing { get; set; }
        public int Juggling { get; set; }
        public int Shooting { get; set; }
        public int Tackling { get; set; }
        public int Reaction { get; set; }
        public int Vision { get; set; }
        public int Positioning { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
