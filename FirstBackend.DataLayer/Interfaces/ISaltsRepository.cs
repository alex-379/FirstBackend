using FirstBackend.Core.Dtos;
using Microsoft.EntityFrameworkCore.Storage;

namespace FirstBackend.DataLayer.Interfaces;

public interface ISaltsRepository
{
    void AddSalt(SaltDto salt);
    SaltDto GetSaltByUserId(Guid userId);
    void UpdateSalt(SaltDto salt);
    void DeleteSalt(SaltDto salt);
}