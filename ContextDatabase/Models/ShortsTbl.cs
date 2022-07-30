using System;
namespace ContextDatabase.Models
{
    public class ShortsTbl
    {
        public int Id { get; set; }
        public string FullURL { get; set; }
        public string ShortURL { get; set; }
        public int ClickCount { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}