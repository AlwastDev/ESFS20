namespace WpfDataBaseLibrary
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Theory")]
    public class Theory
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string? TitleTheory { get; set; }

        [StringLength(8000)]
        public string? TextTheory { get; set; }
    }
}
