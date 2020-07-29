namespace Domain.Entities
{
    public class Feedback : BaseEntity
    {
        public string Content { get; set; }
        public long IntegradorId { get; set; }
        public long LifeId { get; set; }

        public virtual User Integrador { get; set; }
        public virtual Life Life { get; set; }
    }
}