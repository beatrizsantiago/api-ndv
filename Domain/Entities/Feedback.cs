namespace Domain.Entities
{
    public class Feedback : BaseEntity
    {
        public string Content { get; set; }
        public long IntegratorId { get; set; }
        public long LifeId { get; set; }

        public virtual User Integrator { get; set; }
        public virtual Life Life { get; set; }
    }
}