using FirstBackend.Core.Constants;
using FirstBackend.Core.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstBackend.DataLayer.Configuration.MainerLxDatabase;

public class UserDtoConfiguration : IEntityTypeConfiguration<UserDto>
{
    public void Configure(EntityTypeBuilder<UserDto> builder)
    {
        builder
            .HasKey(u => u.Id);
        builder
            .Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(DatabasesProperties.UserNameLength);
        builder
            .Property(u => u.Mail)
            .IsRequired()
            .HasMaxLength(DatabasesProperties.MailLength);
        builder
            .Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(DatabasesProperties.PasswordLength);
        builder
            .Property(u => u.Role)
            .IsRequired();
        builder
            .Property(u => u.IsDeleted)
            .HasDefaultValue(false);
    }
}
