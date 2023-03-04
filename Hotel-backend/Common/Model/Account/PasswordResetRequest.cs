// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;

// public class PasswordResetRequestValidator : AbstractValidator<passwrodre>
// {
//     public PasswordResetRequestValidator()
//     {
//         RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(3).MaximumLength(15);
//         RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty().MinimumLength(3).MaximumLength(15);
//         RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Password and Confirm password are'nt same");
//         RuleFor(x => x.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
//         RuleFor(x => x.Token).NotNull().NotEmpty();

//     }
// }
