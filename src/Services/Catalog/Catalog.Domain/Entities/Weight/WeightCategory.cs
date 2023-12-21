﻿using System.ComponentModel.DataAnnotations.Schema;
using Catalog.Domain.Constants;
using SharedKernel.Domain;

namespace Catalog.Domain.Entities;

[Table(TableName.WeightCategory)]
public class WeightCategory : BaseEntity
{
    public string Code { get; set; }
    public string Description { get; set; }

    #region Navigations
    
    public ICollection<ProductWeight> ProductWeights { get; set; }

    #endregion
}
