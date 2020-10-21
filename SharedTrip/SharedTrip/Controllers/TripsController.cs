using SharedTrip.Common.Constants;
using SharedTrip.Services.Contracts;
using SharedTrip.ViewModels.Trips;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            this._tripService = tripService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddTripInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(input.StartPoint) )
            {
                return this.Error(ErrorMessages.TripStartPointIsRequired);
            }

            if (string.IsNullOrEmpty(input.EndPoint))
            {
                return this.Error(ErrorMessages.TripEndPointIsRequired);
            }

            if (string.IsNullOrEmpty(input.DepartureTime))
            {
                return this.Error(ErrorMessages.TripDepartureTimeIsRequired);
            }

            if (!DateTime.TryParseExact(input.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return this.Error(ErrorMessages.TripDepartureTimeInvalidFormat);
            }

            if (input.Seats < 2 || input.Seats > 6)
            {
                return this.Error(ErrorMessages.TripSetasCount);
            }

            if (string.IsNullOrEmpty(input.Description))
            {
                return this.Error(ErrorMessages.TripDescriptionIsRequired);
            }

            if (input.Description.Length > TripDataRequirements.DescriptionMaxLength)
            {
                return this.Error($"Description can contain max {TripDataRequirements.DescriptionMaxLength} chars");
            }

            this._tripService.AddTrip(input);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            List<TripsViewModel> tripsData = this._tripService.GetAllTrips().ToList();

            return this.View(tripsData);
        }

        [HttpGet]
        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var currTripDetails = this._tripService.Details(tripId);

            return this.View(currTripDetails);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
               return this.Redirect("/Users/Login");
            }

            if (!this._tripService.HasAvalibleSeats(tripId))
            {
                return this.Error(ErrorMessages.NoAvalibleSeats);
            }

            var currUserId = this.GetUserId();

            if (this._tripService.IsUserAddedToTheTrip(currUserId, tripId))
            {
                return this.Details(tripId);
            }

            this._tripService.AddUserToTrip(tripId, currUserId);

            return this.Redirect("/Trips/All");
        }
    }
}