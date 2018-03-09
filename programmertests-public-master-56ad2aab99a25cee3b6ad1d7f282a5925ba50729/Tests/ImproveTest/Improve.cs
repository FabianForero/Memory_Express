using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Framework;

namespace Tests.ImproveTest
{
    /// <summary>
    /// This case/test will test your ability to recognize & improve poor / legacy code. This implementation
    /// is messy / poor, please improve it in any way you see fit.
    /// </summary>
    public class Improve
    {
        private readonly IAuctionService _auctionService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// Here you are passed the neccesary dependencies for the class
        /// </remarks>
        /// <param name="auctionService"></param>
        public Improve(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        /// <summary>
        /// Get the selling price of a car based on the advertisement information
        /// </summary>
        /// <remarks>
        /// Given a CarAdvertisement:
        ///  - Type      - The car type, determines the base price of the vehicle
        ///  - Age       - Determines the depreciation of the vehicle
        ///  - Accidents - Determines the depreciation rate of a vehicle
        ///  - Market    - Determines the additional pricing rules based on the market (see below)
        /// Type:
        ///  - Sedan       - Base Price: $15,000
        ///  - Coupe       - Base Price: $13,000
        ///  - Hatchback   - Base Price: $18,000
        ///  - PickupTruck - Base Price: $25,000
        /// Age:
        ///  - See accident to determine rate
        /// Accidents:
        ///  - 0 Accidents: 5% per year (compounding)
        ///  - 1 Accident:  15% per year (compounding)
        ///  - 2 or More:   25% per year (compounding)
        /// Market:
        ///  - Lot:     No changes
        ///  - Online:  Price reduced by $1000 if car's total price is over $5000
        ///  - Auction: Base price halved + surcharge from the auction service.
        /// Notes:
        ///  - Values are rounded to the nearest cent on the way out
        /// </remarks>
        /// <param name="advertisement"></param>
        /// <returns></returns>
        public virtual decimal GetSellingPrice(CarAdvertisement advertisement)
        {
            throw new TestUnfinishedException();

            if (advertisement != null)
            {
                decimal price = 0m;

                switch (advertisement.Type)
                {
                    case CarType.Sedan:
                        price = 15000m;
                        break;
                    case CarType.Coupe:
                        price = 1000m;
                        break;
                    case CarType.Hatchback:
                        price = 18000m;
                        break;
                    case CarType.PickupTruck:
                        price = 25000m;
                        break;
                }

                switch (advertisement.SellingMarket)
                {
                    case CarMarket.Lot:
                        price = Accidents(advertisement.Accidents, advertisement.Age, price);
                        break;
                    case CarMarket.Online:
                        price = Accidents(advertisement.Accidents, advertisement.Age, price);
                        if (price > 5000m)
                            price = price - 1000m;
                        break;
                    default:
                        price = price / 2m;
                        var surcharge = _auctionService.GetSurcharge(advertisement.Type, advertisement.Accidents);
                        price = price + surcharge;
                        price = Accidents(advertisement.Accidents, advertisement.Age, price);
                        break;
                }
                return Math.Round(price, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public static decimal Accidents(int Accident, int age, decimal price)
        {
            decimal compo;
            switch (Accident)
            {
                case 0:
                    compo = 0.05m;
                    break;
                case 1:
                    compo = 0.15m;
                    break;
                default:
                    compo = 0.25m;
                    break;
            }
            return GetFinalPrice(age, compo);
        }

        public static decimal GetFinalPrice(int age, decimal ReducPer)
        {
            decimal price = 0m;
            for (int i = 0; i < age; i++)
            {
                var reduction = price * ReducPer;
                price = price - reduction;
            }
            return price;
        }

    }
}
