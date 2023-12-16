using System.ComponentModel.DataAnnotations.Schema;
using Catalog.Domain.Constants;
using SharedKernel.Domain;

namespace Catalog.Domain.Entities;

[Table(TableName.Attribute)]
public class Attribute : BaseEntity
{
    public string Key { get; set; }
    public string Value { get; set; }
}