﻿using SharedTrip.Models;
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

        public void AddUserToTrip(string tripId, string userId) => throw new System.NotImplementedException();

        public ICollection<TripsViewModel> GetAllTrips()
        {
            return this._db.Trips
                .Select(x => new TripsViewModel
                {
                    Id = x.Id,
                    StartPoint = x.StartingPoint,
                    EndPoint = x.EndPoint,
                    Steats = x.Seats - x.UserTrips.Count,
                    DepartureTime = x.DepartureTime.ToString("dd.MM.yyyy hh:mm:ss")
                })
                .ToList();
        }
    }
}