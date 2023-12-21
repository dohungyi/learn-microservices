using Catalog.Application.Mappings;
using Catalog.Application.Properties;
using Catalog.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Catalog.Application.DTOs;

public class CreateSupplierDto : IMapFrom<Supplier>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Alias  { get; set; }
    public string Description { get; set; }
    public string Delegate { get; set; }
    public string Bank { get; set; }
    public string AccountNumber { get; set; }
    public string BankAddress { get; set; }
    public string AddressOne { get; set; }
    public string AddressTwo { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public string TaxCode { get; set; }
    public string NationCode { get; set; }
    public string ProvinceCode { get; set; }
    public string DistrictCode { get; set; }
    public bool Status { get; set; } = true;
}

public class CreateSupplierDtoValidator : AbstractValidator<CreateSupplierDto>
{
    public CreateSupplierDtoValidator(IStringLocalizer<Resources> localizer)
    {
        // Để ý viết validator sau nhớ
    }
}