﻿using GymFitPlus.Infrastructure.Enums;

namespace GymFitPlus.Infrastructure.Constants
{
    public static class DataConstants
    {
        public static class ApplicationUserConstants 
        {
            public const int NameMaxLenght = 50;
            public const int NameMinLenght = 5;

            public const string DateFormat = "mm/dd/yyyy";

            public const int UrlMaxLenght = 200;
            public const int UrlMinLenght = 50;

            public const double GenderTypeEnumMaxValue = 3;
            public const double GenderTypeEnumMinValue = 1;
        }

        public static class ExcerciseConstants
        {
            public const int NameMaxLenght = 50;
            public const int NameMinLenght = 5;

            public const int DescriptionMaxLenght = 400;
            public const int DescriptionMinLenght = 50;

            public const int ImgUrlMaxLenght = 200;
            public const int ImgUrlMinLenght = 50;
        }



    }
}
