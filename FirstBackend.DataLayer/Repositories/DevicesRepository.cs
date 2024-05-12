using FirstBackend.Core.Constants.Logs.DataLayer;
using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Contexts;
using FirstBackend.DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FirstBackend.DataLayer.Repositories;

public class DevicesRepository(MainerLxContext context) : BaseRepository<MainerLxContext>(context), IDevicesRepository
{
    private readonly ILogger _logger = Log.ForContext<DevicesRepository>();

    public Guid AddDevice(DeviceDto device)
    {
        _ctx.Devices.Add(device);
        _logger.Information(DevicesRepositoryLogs.AddDevice, device.Id);
        _ctx.SaveChanges();

        return device.Id;
    }
    public IEnumerable<DeviceDto> GetAllDevices()
    {
        _logger.Information(DevicesRepositoryLogs.GetAllDevices);

        return _ctx.Devices
            .Where(d => !d.IsDeleted);
    }

    public DeviceDto GetDeviceById(Guid id)
    {
        _logger.Information(DevicesRepositoryLogs.GetDeviceById, id);

        return _ctx.Devices
            .FirstOrDefault(d => d.Id == id
                && !d.IsDeleted);
    }

    public IEnumerable<DeviceDto> GetDevicesByUserId(Guid userId)
    {
        _logger.Information(DevicesRepositoryLogs.GetDevicesByUserId, userId);

        return _ctx.Devices
            .Include(d => d.Orders)
            .Where(d => !d.IsDeleted
                && d.Orders
                .Any(o => o.Customer.Id == userId)
            );
    }

    public void UpdateDevice(DeviceDto device)
    {
        _logger.Information(DevicesRepositoryLogs.UpdateDevice, device.Id);
        _ctx.Devices.Update(device);
        _ctx.SaveChanges();
    }
}
