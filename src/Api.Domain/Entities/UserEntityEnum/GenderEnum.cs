using System.ComponentModel;

namespace Api.Domain.Entities.UserEntityEnum
{
    public enum GenderEnum
    {
        [Description("Feminino")]
        Femele,
        [Description("Masculino")]
        Male,
        [Description("Não Binário")]
        Nonbinary,
        [Description("Prefiro não Responder")]
        RatherNotSay = 0
    }
}