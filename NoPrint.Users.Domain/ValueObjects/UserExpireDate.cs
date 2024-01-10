﻿using NoPrint.Framework.Validation;

namespace NoPrint.Users.Domain.ValueObjects;

public class UserExpireDate
{
    public UserExpireDate(DateTime? expireDate)
    {
        expireDate.ValidationCheck("ExpireDate", x => x is null | x > DateTime.Now, "E1024");
        ExpireDate = expireDate;
    }

    public UserExpireDate()
    {
        ExpireDate = null;
    }

    public DateTime? ExpireDate { get; private set; }

    public bool IsExpire() => ExpireDate != null && ExpireDate <= DateTime.Now;
}