namespace WpfDataBaseLibrary
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Account")]
    public class Account
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string? Login { get; set; }

        [StringLength(50)]
        public string? Password { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? MiddleName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        public bool? IsAdmin { get; set; }

        public int? GeneralMark { get; set; }
    }
}
