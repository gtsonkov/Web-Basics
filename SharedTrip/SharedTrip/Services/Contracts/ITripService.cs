﻿using SharedTrip.ViewModels.Trips;
using System.Collections.Generic;

namespace SharedTrip.Services.Contracts
{
    public interface ITripService
    {
        void AddTrip(AddTripInputModel userInput);

        void AddUserToTrip(string tripId, string userId);

        bool IsUserAddedToTheTrip(string userId, string tripId);

        bool HasAvalibleSeats(string tripId);

        ICollection<TripsViewModel> GetAllTrips();

        TripDetailViewModel Details(string id);
    }
}