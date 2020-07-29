using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum NumberNets
    {
        [Display(Name = "Rede 01")]
        NetOne = 1,
        [Display(Name = "Rede 02")]
        NetTwo,
        [Display(Name = "Rede 03")]
        NetThree,
        [Display(Name = "Rede 04")]
        NetFour
    }
}