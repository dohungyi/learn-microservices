using Catalog.Application.DTOs;
using Catalog.Application.Properties;
using FluentValidation;
using Microsoft.Extensions.Localization;
using SharedKernel.Application;
using SharedKernel.Libraries;

namespace Catalog.Application.Features.VersionOne;

[AuthorizationRequest(new ActionExponent[] { ActionExponent.Supplier })]
public class CreateSupplierCommand : BaseInsertCommand<SupplierDto>
{
    public string Code { get; init; }
    public string Name { get; init; }
    public string Alias { get; set; }
    public string Description { get; init; }
    public string Delegate { get; init; }
    public string Bank { get; init; }
    public string AccountNumber { get; init; }
    public string BankAddress { get; init; }
    public string AddressOne { get; init; }
    public string AddressTwo { get; init; }
    public string Phone { get; init; }
    public string Fax { get; init; }
    public string TaxCode { get; init; }
    public string NationCode { get; init; }
    public string ProvinceCode { get; init; }
    public string DistrictCode { get; init; }
    public bool Status { get; init; } = true;
}

public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
{
    public CreateSupplierCommandValidator(IStringLocalizer<Resources> localizer)
    {
        // Mã nhà cung cấp
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage(localizer["supplier_code_required"].Value)
            .MaximumLength(50).WithMessage(localizer["supplier_code_max_length_50"].Value);

        // Tên nhà cung cấp
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(localizer["supplier_name_required"].Value)
            .MaximumLength(255).WithMessage(localizer["supplier_name_max_length_255"].Value);

        // Người đại diện
        RuleFor(x => x.Delegate)
            .NotEmpty().WithMessage(localizer["supplier_delegate_required"].Value)
            .MaximumLength(500).WithMessage(localizer["supplier_delegate_max_length_50"].Value);

        // Ngân hàng
        RuleFor(x => x.Bank)
            .MaximumLength(50).WithMessage(localizer["bank_max_length_50"].Value);

        // Số tài khoản ngân hàng
        RuleFor(x => x.AccountNumber)
            .MaximumLength(50).WithMessage(localizer["account_number_max_length_50"].Value);

        // Địa chỉ ngân hàng
        RuleFor(x => x.BankAddress)
            .MaximumLength(255).WithMessage(localizer["bank_address_max_length_255"].Value);

        // Địa chỉ một
        RuleFor(x => x.AddressOne)
            .NotEmpty().WithMessage(localizer["supplier_address_required"].Value)
            .MaximumLength(255).WithMessage(localizer["supplier_address_max_length_255"].Value);

        // Địa chỉ hai
        RuleFor(x => x.AddressTwo)
            .MaximumLength(255).WithMessage(localizer["supplier_address_max_length_255"].Value);

        // Điện thoại
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(localizer["phone_required"])
            .MaximumLength(20).WithMessage(localizer["phone_max_length_20"].Value);

        // Fax
        RuleFor(x => x.Fax)
            .MaximumLength(50).WithMessage(localizer["fax_max_length_50"].Value);

        // Mã quốc gia
        RuleFor(x => x.NationCode)
            .MaximumLength(10).WithMessage(localizer["nation_code_max_length_10"].Value);

        // Mã tỉnh/thành phố
        RuleFor(x => x.ProvinceCode)
            .MaximumLength(10).WithMessage(localizer["province_code_max_length_10"].Value);

        // Mã quận/huyện
        RuleFor(x => x.DistrictCode)
            .MaximumLength(10).WithMessage(localizer["district_code_max_length_10"].Value);
    }
}