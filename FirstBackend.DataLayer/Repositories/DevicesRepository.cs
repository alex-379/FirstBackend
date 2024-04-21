using FirstBackend.Core.Dtos;
using FirstBackend.DataLayer.Interfaces;

namespace FirstBackend.DataLayer.Repositories;

public class DevicesRepository : BaseRepository, IDevicesRepository
{
    public DevicesRepository(MainerLxContext context) : base(context)
    {

    }

    public DeviceDto GetDeviceById(Guid id) => _ctx.Devices.FirstOrDefault(d => d.Id == id);

    public DeviceDto GetDeviceByUserId(Guid userId) => _ctx.Devices.FirstOrDefault(d => d.Owner.Id == userId);
}
