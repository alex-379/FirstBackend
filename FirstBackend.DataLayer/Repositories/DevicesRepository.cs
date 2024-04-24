using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Interfaces;
using Serilog;

namespace FirstBackend.DataLayer.Repositories;

public class DevicesRepository : BaseRepository, IDevicesRepository
{
    private readonly ILogger _logger = Log.ForContext<DevicesRepository>();

    public DevicesRepository(MainerLxContext context) : base(context)
    {

    }

    public Guid AddDevice(DeviceDto device)
    {
        _ctx.Devices.Add(device);
        _logger.Information("Вносим в базу данных устройство с ID {id}", device.Id);

        return device.Id;
    }

    public DeviceDto GetDeviceById(Guid id)
    {
        _logger.Information("Идём в базу данных и ищем устройство по ID {id}", id);

        return _ctx.Devices.FirstOrDefault(d => d.Id == id);
    }


    public DeviceDto GetDeviceByUserId(Guid userId)
    {
        _logger.Information("Идём в базу данных и ищем устройство по ID пользователя {userId}", userId);

        return _ctx.Devices.FirstOrDefault(d => d.Owner.Id == userId);
    }

    public void DeleteDevice(DeviceDto device)
    {
        _logger.Information("Идём в базу данных и удаляем заказ с ID {id}", device.Id);
        _ctx.Devices.Remove(device);
    }
}
