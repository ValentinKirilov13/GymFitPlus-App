using GymFitPlus.Infrastructure.Constants;

namespace GymFitPlus.Core.ErrorMessages
{
    public static class ErrorMessages
    {
        public const string RequiredErrorMessage = "The field {0} is required!";

        public const string LengthErrorMessage = "The field {0} must be between {2} and {1} characters long!";

        public const string EmailErrorMessage = "Invalid email adress!";

        public const string ConfirmPassErrorMessage = "The password and confirmation password do not match.";

        public const string DateFormatErrorMessage = 
            $"The date format is incorect! Allow format is : {DataConstants.ApplicationUserConstants.DateFormat}";

        public const string PhotoErrorMessage = "File is too big. Max alloed is 2 GB";

        public const string ExercisePropertiesErrorMessages = "The value is not valid! Must be between {1} and {2}.";

        public const string DurationErrorMessages = $"The value is not valid!";
    }
}
