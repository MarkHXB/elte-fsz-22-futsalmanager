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
        
        [Display(Name = "First name")]
        public string FirstName { get; set; } = null!;
        
        [Display(Name = "Last name")]
        public string LastName { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy.MM.dd}")]
        [DataType(DataType.DateTime,ErrorMessage = "Please enter valid date time format")]
        public DateTime Birthday { get; set; }
        
        [Range(0,135,ErrorMessage = "Please enter valid integer Number")]
        public int Age { get; set; }
        [Display(Name="Active? (yes/no)")]
        public bool IsActive { get; set; }
        public string Nationality { get; set; }
        
        [Required(ErrorMessage = "You should set the attributes for the player")]
        public int AttributeId { get; set; }
        
        [Required(ErrorMessage = "You should choose a team")]
        public int TeamId { get; set; }
        
        [Required(ErrorMessage = "You should choose a position")]
        public int PositionId { get; set; }

        public virtual Attribute? Attribute { get; set; }
        public virtual Position? Position { get; set; }
        public virtual Team? Team { get; set; }
        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
