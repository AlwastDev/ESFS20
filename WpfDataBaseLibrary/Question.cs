namespace WpfDataBaseLibrary
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Question")]
    public sealed class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }

        [StringLength(300)]
        public string TitleQuestion { get; set; } = null!;

        public int? TestId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Answer> Answers { get; set; }

        public Test Test { get; set; } = null!;
    }
}
