using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using SidesMpcLibrary.Models;

namespace SidesMpcLibrary.Classes
{

    public class SettingsOperations
    {
        private const string _configurationFileName = "SidesConnector.exe.config";

        #region Configuration file locations 
        private static readonly string LocalFileNameDevelopment =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Development", _configurationFileName);

        private static readonly string LocalFileNameTest =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test", _configurationFileName);

        private static readonly string LocalFileNameProduction =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Production", _configurationFileName);
        #endregion

        /// <summary>
        /// Set current <see cref="SidesEnvironment"/>
        /// </summary>
        /// <param name="environment">Selected environment</param>
        /// <returns>folder location and file name</returns>
        public static (string folder, string fileName) GetLocation(SidesEnvironment environment)
        {
            switch (environment)
            {
                case SidesEnvironment.Development:
                    return new ValueTuple<string, string>(Path.GetDirectoryName(LocalFileNameDevelopment), 
                        Path.GetFileName(LocalFileNameDevelopment));

                case SidesEnvironment.Testing:
                    return new ValueTuple<string, string>(Path.GetDirectoryName(LocalFileNameTest), 
                        Path.GetFileName(LocalFileNameTest));

                case SidesEnvironment.Production:
                    return new ValueTuple<string, string>(Path.GetDirectoryName(LocalFileNameProduction), 
                        Path.GetFileName(LocalFileNameProduction));

                default:
                    throw new ArgumentOutOfRangeException(nameof(environment), environment, null);
            }

        }

        /// <summary>
        /// Set value for specified key to current selected configuration file
        /// </summary>
        /// <param name="environment"><see cref="SidesEnvironment"/></param>
        /// <param name="key">AppSettings key</param>
        /// <param name="value">AppSettings value</param>
        /// <returns>success and a exception on failure</returns>
        public static (bool success, Exception exception) SetValue(SidesEnvironment environment, string key, string value)
        {

            try
            {
                var (folder, fileName) = GetLocation(environment);

                var fileMap = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = Path.Combine(Path.GetFullPath(folder + "\\" + fileName))
                };

                var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

                config.AppSettings.Settings[key].Value = value;
                config.Save();

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }

        }

        public static (bool success, Exception exception, List<SettingItem> items)  ReadSettings(SidesEnvironment environment = SidesEnvironment.Development)
        {

            try
            {
                var (folder, fileName) = GetLocation(environment);

                var fileMap = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = Path.Combine(Path.GetFullPath(folder + "\\" + fileName))
                };


                var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                KeyValueConfigurationCollection collection = config.AppSettings.Settings;

                List<SettingItem> list =
                    (
                        from KeyValueConfigurationElement element in collection
                        select new SettingItem()
                        {
                            Name = element.Key,
                            Value = element.Value,
                            Type = element.Key.GetType()
                        }).OrderBy(setting => setting.Name).ToList();


                for (int index = 0; index < list.Count; index++)
                {

                    if (int.TryParse(list[index].Value, out _))
                    {
                        list[index].Type = typeof(int);
                    }

                    if (!string.IsNullOrWhiteSpace(list[index].Value))
                    {
                        if (list[index].Value.ToLower() == "false" || list[index].Value.ToLower() == "true")
                        {
                            list[index].Type = typeof(bool);
                        }

                        if (list[index].Value.Contains("\\"))
                        {
                            list[index].IsPath = true;
                        }
                    }
                }

                return (true, null, list);
            }
            catch (Exception ex)
            {
                return (false, ex, null);
            }

            
        }

    }
}
