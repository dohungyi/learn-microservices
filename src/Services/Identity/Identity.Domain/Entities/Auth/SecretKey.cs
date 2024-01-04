using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel.Domain;

namespace Identity.Domain.Entities;

[Table("auth_secret_key")]
public class SecretKey : PersonalizedEntity
{
    public string Key { get; set; }
}