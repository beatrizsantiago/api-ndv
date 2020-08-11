using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Life : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsLost { get; set; }
        public bool IsBaptismOhterChurch { get; set; }
        public string MinisterBaptism { get; set; }
        public long IntegradorId { get; set; }

        public virtual User Integrador { get; set; }
        public virtual List<Feedback> Feedbacks { get; set; }
        public virtual List<ProgressStepsLife> Steps { get; set; }
    }
}