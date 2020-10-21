using SharedTrip.Models;
using SharedTrip.Services.Contracts;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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

        public void AddUserToTrip(string tripId, string userId)
        {
            var userTrip = new UserTrip
            {
                UserId = userId,
                TripId = tripId
            };

            this._db.UserTrip.Add(userTrip);
            this._db.SaveChanges();
        }

        public ICollection<TripsViewModel> GetAllTrips()
        {
            return this._db.Trips
                .Select(x => new TripsViewModel
                {
                    Id = x.Id,
                    StartPoint = x.StartingPoint,
                    EndPoint = x.EndPoint,
                    Seats = x.Seats - x.UserTrips.Count,
                    DepartureTime = x.DepartureTime
                })
                .ToList();
        }

        public TripDetailViewModel Details(string tripId)
        {
            var currTrip = this._db.Trips.Where(t => t.Id == tripId)
                .Select( x => new TripDetailViewModel { 
                Id = x.Id,
                StartPoint = x.StartingPoint,
                EndPoint = x.EndPoint,
                Seats = x.Seats - x.UserTrips.Count,
                DepartureTime = x.DepartureTime,
                Description = x.Description,
                Image = x.ImagePath
                })
                .FirstOrDefault();

            return currTrip;
        }

        public bool IsUserAddedToTheTrip(string userId, string tripId)
        {
            return this._db.Trips
                .Where(x => x.Id == tripId)
                .Any(t => t.UserTrips
                .Any(x => x.UserId == userId));
                
        }

        public bool HasAvalibleSeats(string tripId)
        {
            var tripInfo = this._db.Trips.Where(t => t.Id == tripId)
                .Select(x => new
                {
                    FreeSeats = x.Seats - x.UserTrips.Count
                })
                .FirstOrDefault();

            return tripInfo.FreeSeats > 0;
        }
    }
}