using System;
using Domain.Enums;

namespace Domain.Entities
{
    public class ProgressStepsLife : BaseEntity
    {
        public string LifeId { get; set; }
        public StepsPropheticWay Step { get; set; }

        public virtual Life Life { get; set; }
    }
}