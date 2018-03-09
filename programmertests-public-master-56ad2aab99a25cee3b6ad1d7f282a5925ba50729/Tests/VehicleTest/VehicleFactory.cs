using System;
using System.Collections.Generic;
using Framework;

namespace Tests.VehicleTest
{
    /// <summary>
    /// Creates vehicles that adhere to the <c>IVehicle</c> interface
    /// </summary>
    public class VehicleFactory
    {
        public IEnumerable<IVehicle> CreateAllVehicles()
        {
            foreach (VehicleType vehicleType in Enum.GetValues(typeof(VehicleType)))
            {
                yield return CreateVehicle(vehicleType);
            }
        }

        /// <summary>
        /// Creates a vehcle of the specified type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Class that impliments </returns>
        public virtual IVehicle CreateVehicle(VehicleType type)
        {
            throw new TestUnfinishedException();
        }
    }
}