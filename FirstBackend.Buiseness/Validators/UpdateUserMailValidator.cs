using FirstBackend.Buiseness.Models.Users.Requests;
using FirstBackend.DataLayer.Interfaces;
using FluentValidation;

namespace FirstBackend.Buiseness.Validators;

public class UpdateUserMailValidator : AbstractValidator<UpdateUserMailRequest>
{
    private readonly IUsersRepository _usersRepository;

    public UpdateUserMailValidator(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
        RuleFor(r => r.Mail)
            .EmailAddress()
            .WithMessage("Неправильный e-mail")
            .Must(IsMailUnique)
            .WithMessage("Такой e-mail уже существует");
    }

    public bool IsMailUnique(string mail)
    {
        return _usersRepository.GetUserByMail(mail.ToLower()) is null;
    }
}