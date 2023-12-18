namespace SharedKernel.Application;

public class ValidateField
{
    public string FieldName { get; set; }

    public Enum.ValidateCode Code { get; set; }

    public string ErrorMessage { get; set; }
}