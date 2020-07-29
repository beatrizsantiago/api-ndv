using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum StepsPropheticWay
    {
        [Display(Name = "Visitante")]
        Visitor,
        [Display(Name = "Conversão")]
        Converted,
        [Display(Name = "Reconciliação")]
        Reconsiliated,
        [Display(Name = "Batismo")]
        Baptism,
        [Display(Name = "Experiência com Deus")]
        GodExperience,
        [Display(Name = "Ativação da Paternidade")]
        ActivationPaternity,
        [Display(Name = "Classe Vida Cristã")]
        ClassChristianLife,
        [Display(Name = "Classe Ministros Que Transformam")]
        ClassLeaderCap,
    }
}