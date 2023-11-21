using System.ComponentModel;

namespace Api.Domain.Entities.UserEntityEnum
{
    public enum GenderEnum
    {
        [Description("Prefiro não Responder")]
        RatherNotSay = 0,
        [Description("Feminino")]
        Femele,
        [Description("Masculino")]
        Male,
        [Description("Não Binário")]
        Nonbinary        
    }
}