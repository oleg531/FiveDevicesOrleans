# FiveDevicesOrleans
A simple example of using .net Orleans with unit tests.

Imagine that you have a set of devices, sending Tuples of (DeviceId, Temperature) every 1 to 5 seconds (randomly).

Implemented the following logic:

1. Every Second, emit average temperature of all devices.
2. Send Alert (to console) if a single device hits temperature threshold.
3. All emitted events send to Console.
4. For average temperature was taken only values not older than 3 seconds.
5. Created some Unit Tests
