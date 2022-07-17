using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortStoryNetwork.Core
{
    [Table("StatVowel")]
    public class StatVowel
    {
        [Key]
        public string Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int SingleVowelCount { get; set; }
        public int PairVowelCount { get; set; }
        public int TotalWordCount { get; set; }

    }
}
