using System.ComponentModel.DataAnnotations;

namespace FutsalManager.Models
{
    public partial class Player
    {
        public Player()
        {
            Transfers = new HashSet<Transfer>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        [Display(Name="Active? (yes/no)")]
        public bool IsActive { get; set; }
        public string Nationality { get; set; }
        public int? AttributeId { get; set; }
        public int? TeamId { get; set; }
        public int? PositionId { get; set; }

        public virtual Attribute? Attribute { get; set; }
        public virtual Position? Position { get; set; }
        public virtual Team? Team { get; set; }
        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
