﻿
using ConfigurationLibrary.Classes;

namespace BasicPatternMatching.Classes
{
    public class Operations
    {
        public static string ConnectionString() => ConfigurationHelper.ConnectionString();

        public static Settings ApplicationSettings() => Configuration.ApplicationSettings();

    }
}
