﻿using FirstBackend.Core.Dtos;

namespace FirstBackend.DataLayer.Interfaces;

public interface ISaltsRepository
{
    void AddSalt(SaltDto salt);
    SaltDto GetSaltByUserId(Guid userId);
    void UpdateSalt(SaltDto salt);
    void DeleteSalt(SaltDto salt);
}