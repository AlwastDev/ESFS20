namespace WpfDataBaseLibrary
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Answer")]
    public class Answer
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string? TextAnswer { get; set; }

        public int? QuestionId { get; set; }

        public bool? Right { get; set; }

        public virtual Question? Question { get; set; }
    }
}
