using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum LegendLife
    {
        [Display(Name = "Sem legenda")]
        None,
        [Display(Name = "Está nas classes")]
        InClasses,
        [Display(Name = "Uma criança")]
        IsChild,
        [Display(Name = "Vida Perdida")]
        LostLife
    }
}