using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingGuide
{
    public class TravellCard
    {
        /// <summary>
        /// Departure point
        /// </summary>
        public PlaceEnum PlaceFrom { get; set; }
        
        /// <summary>
        /// Destination
        /// </summary>
        public PlaceEnum PlaceTo { get; set; }

        public TravellCard(PlaceEnum placeFrom)
        {
            this.PlaceFrom = placeFrom;
        }
    }
}
