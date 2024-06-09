using System.ComponentModel;
using System.Reflection;

namespace Api.CrossCutting.Communication.Enums
{
    public enum TemplateEmailEnum
    {
        [Description("ForgotPasswordModel.html")]
        ForgetPassword,

        [Description("EmailValidationModel.html")]
        EmailValidation
    }

    public static class TemplateEmailExtensions
    {
        public static string ToFileName(this TemplateEmailEnum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString())!;
            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))!;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}