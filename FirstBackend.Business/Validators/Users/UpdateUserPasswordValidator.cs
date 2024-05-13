﻿using FirstBackend.Business.Models.Users.Requests;
using FluentValidation;

namespace FirstBackend.Business.Validators.Users;

public class UpdateUserPasswordValidator : AbstractValidator<UpdateUserPasswordRequest>
{
    public UpdateUserPasswordValidator()
    {
        RuleFor(r => r.Password)
            .MatchPasswordRule();
    }
}
