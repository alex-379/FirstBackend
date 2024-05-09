﻿using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FirstBackend.DataLayer.Repositories;

public class DevicesRepository(MainerLxContext context) : BaseRepository(context), IDevicesRepository
{
    private readonly ILogger _logger = Log.ForContext<DevicesRepository>();

    public Guid AddDevice(DeviceDto device)
    {
        _ctx.Devices.Add(device);
        _logger.Information("Вносим в базу данных устройство с ID {id}", device.Id);
        _ctx.SaveChanges();

        return device.Id;
    }
    public List<DeviceDto> GetAllDevices()
    {
        _logger.Information("Идём в базу данных и ищем все устройства");

        return [.. _ctx.Devices
            .Where(d=>!d.IsDeleted)];
    }

    public DeviceDto GetDeviceById(Guid id)
    {
        _logger.Information("Идём в базу данных и ищем устройство по ID {id}", id);

        return _ctx.Devices
            .FirstOrDefault(d => d.Id == id
                && !d.IsDeleted);
    }

    public List<DeviceDto> GetDevicesByUserId(Guid userId)
    {
        _logger.Information("Идём в базу данных и ищем устройства по ID пользователя {userId}", userId);

        return [.. _ctx.Devices
            .Include(d => d.Orders)
            .Where(d => !d.IsDeleted
                && d.Orders
                .Any(o => o.Customer.Id == userId)
            )];
    }

    public void UpdateDevice(DeviceDto device)
    {
        _logger.Information("Идём в базу данных и обновляем устройство с ID {id}", device.Id);
        _ctx.Devices.Update(device);
        _ctx.SaveChanges();
    }
}
