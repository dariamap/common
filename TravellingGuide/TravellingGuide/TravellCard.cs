using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingGuide
{
    public class TravellCard
    {
        private PlaceEnum _placeFrom;
        private PlaceEnum _placeTo;
        /// <summary>
        /// Departure point
        /// </summary>
        public PlaceEnum PlaceFrom { get { return _placeFrom; } set { _placeFrom = value; } }
        
        /// <summary>
        /// Destination
        /// </summary>
        public PlaceEnum PlaceTo { get { return _placeTo; } set { _placeTo = value; } }

        public TravellCard(PlaceEnum placeFrom)
        {
            _placeFrom = placeFrom;
        }
        public TravellCard(PlaceEnum placeFrom, PlaceEnum placeTo)
        {
            _placeFrom = placeFrom;
            _placeTo = placeTo;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            return PlaceTo == ((TravellCard)obj).PlaceTo && 
                   PlaceFrom == ((TravellCard)obj).PlaceFrom; 
        }

        public override int GetHashCode()
        {
            return (int)_placeTo ^ (int)_placeFrom;
        }
    }
}
