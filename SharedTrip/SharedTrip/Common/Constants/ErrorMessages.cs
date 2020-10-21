﻿namespace SharedTrip.Common.Constants
{
    public static class ErrorMessages
    {
        public const string InvalidUsernameOrPassword = "Invalid username or password.";

        public const string InvalidEmail = "Invalid Email address.";

        public const string PasswordsDoNotMatch = "Passwords do not match.";

        public const string EmailNotAvalible = "The Email you try to enter is already used from another user in our system. Please try with another Email.";

        public const string UsernameNotAvalible = "The username you try to enter is already used from another user in our system. Please try with another username.";

        public const string TripStartPointIsRequired = "Start point is required.";

        public const string TripEndPointIsRequired = "End point is required.";

        public const string TripDepartureTimeIsRequired = "Departure time is required.";

        public const string TripDepartureTimeInvalidFormat= "Not valid format for departure time. Please make sure, that you use the folowing Date-Time format: dd.MM.yyyy HH:mm";

        public const string TripSetasCount = "Seats count must be between 2 and 6.";

        public const string TripDescriptionIsRequired = "Description is required.";
    }
}