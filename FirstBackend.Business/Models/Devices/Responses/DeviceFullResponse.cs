﻿namespace FirstBackend.Business.Models.Devices.Responses;

public class DeviceFullResponse : DeviceResponse
{
    public string Address { get; set; }
    public int NumberOrders { get; set; }
}
