using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Entities
{
    public class Life : BaseEntity
    {
        public Life()
        {
            Steps = new List<ProgressStepsLife>();
            Feedbacks = new List<Feedback>();
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public bool IsLost { get; set; }
        public bool BaptismOtherChurch { get; set; }
        public string BaptismMinister { get; set; }
        public bool FrequentOtherChurch { get; set; }
        public string Companion { get; set; }
        public long IntegratorId { get; set; }

        [NotMapped]
        public ProgressStepsLife LastStep => Steps.OrderByDescending(step => step.DoneDate).FirstOrDefault();

        public virtual User Integrator { get; set; }
        public virtual List<Feedback> Feedbacks { get; set; }
        public virtual List<ProgressStepsLife> Steps { get; set; }
    }
}