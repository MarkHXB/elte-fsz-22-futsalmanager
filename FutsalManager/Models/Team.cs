using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FutsalManager.Models
{
    public partial class Team
    {
        public Team()
        {
            Players = new HashSet<Player>();
            Transfers = new HashSet<Transfer>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Team name is required")]
        [Display(Name = "Team name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "The email address is required")]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "You must provide a phone number.")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;

        public string LogoPath { get; set; } = string.Empty;

        [NotMapped]
        [AllowedExtensionsAttribute(new[] { ".jpg", ".jpeg", ".jfif", ".png" })]
        [MaxFileSize(5 * 1024 * 1024)]
        [Display(Name = "Upload logo image")]
        [BindProperty]
        public IFormFile? FileModel { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
