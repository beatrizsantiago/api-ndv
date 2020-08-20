using System;

namespace Domain.Entities
{
    public class Visitant : BaseEntity
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public bool FrequentOtherChurch { get; set; }
        public string Companion { get; set; }
    }
}