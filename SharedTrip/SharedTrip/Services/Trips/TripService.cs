using SharedTrip.Models;
using SharedTrip.Services.Contracts;
using SharedTrip.ViewModels.Trips;
using System;
using System.Globalization;

namespace SharedTrip.Services.Trips
{
    public class TripService : ITripService
    {
        private readonly ApplicationDbContext _db;

        public TripService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public void AddTrip(AddTripInputModel userInput)
        {
            var currTrip = new Trip
            {
                StartingPoint = userInput.StartPoint,
                EndPoint = userInput.EndPoint,
                DepartureTime = DateTime.ParseExact(
                    userInput.DepartureTime
                    , "dd.MM.yyyy HH:mm"
                    , CultureInfo.InvariantCulture
                    , DateTimeStyles.None),
                ImagePath = userInput.ImagePath,
                Seats = userInput.Seats,
                Description = userInput.Description 
            };

            this._db.Trips.Add(currTrip);
            this._db.SaveChanges();
        }

        public void AddUserToTrip(string tripId, string userId) => throw new System.NotImplementedException();
    }
}