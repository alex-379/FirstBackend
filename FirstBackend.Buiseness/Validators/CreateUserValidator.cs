using FirstBackend.Buiseness.Models.Users.Requests;
using FirstBackend.DataLayer.Interfaces;
using FluentValidation;

namespace FirstBackend.Buiseness.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    private readonly IUsersRepository _usersRepository;

    public CreateUserValidator(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
        RuleFor(r => r.Name)
           .NotEmpty()
           .WithMessage("Требуется ввести имя");
        RuleFor(r => r.Mail)
            .EmailAddress()
            .WithMessage("Неправильный e-mail")
            .Must(IsMailUnique)
            .WithMessage("Такой e-mail уже существует");
        RuleFor(r => r.Password)
            .MatchPasswordRule();
    }

    public bool IsMailUnique(string mail)
    {
        return _usersRepository.GetUserByMail(mail.ToLower()) is null;
    }

}
