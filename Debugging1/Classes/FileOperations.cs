using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace DebuggingFiles.Classes
{
    public class FileOperations
    {
        /*
         *This would normally be passed in from using a pre-defined list in a ListBox/ComboBox or from OpenDialog.
         */
        private readonly string _inputFileName = 
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SacramentocrimeJanuary2006.csv");

        public delegate void OnBadNcis(int sender);
        public event OnBadNcis BadNcisHandler;
        /// <summary>
        /// Load file via VB TextFieldParser
        /// </summary>
        /// <returns></returns>
        public (bool Success, Exception exception, List<DataItem>, List<DataItemInvalid>, int EmptyLineCount) LoadCsvFileTextFieldParser()
        {
            var validRows = new List<DataItem>();
            var invalidRows = new List<DataItemInvalid>();
            var validateBad = 0;

            int index = 0;

            int district = 0;
            int grid = 0;
            int nCode = 0;
            float latitude = 0;
            float longitude = 0;

            var emptyLineCount = 0;
            var line = "";

            try
            {
                /*
                 * If interested in blank line count
                 */
                using (var reader = File.OpenText(_inputFileName))
                {
                    while ((line = reader.ReadLine()) != null) // EOF
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            emptyLineCount++;
                        }
                    }
                }

                using (var parser = new TextFieldParser(_inputFileName))
                {
                    
                    parser.Delimiters = new[] { "," };
                    while (true)
                    {

                        string[] parts = parser.ReadFields();

                        if (parts == null)
                        {
                            break;
                        }

                        index += 1;
                        validateBad = 0;

                        if (parts.Length != 9)
                        {
                            invalidRows.Add(new DataItemInvalid() { Row = index, Line = string.Join(",", parts) });
                            continue;

                        }

                        // Skip first row which in this case is a header with column names
                        if (index <= 1) continue;

                        /*
                         * These columns are checked for proper types
                         */
                        var validRow = 
                            DateTime.TryParse(parts[0], out var d) && 
                            float.TryParse(parts[7].Trim(), out latitude) && 
                            float.TryParse(parts[8].Trim(), out longitude) && 
                            int.TryParse(parts[2], out district) && 
                            int.TryParse(parts[4], out grid) && 
                            !string.IsNullOrWhiteSpace(parts[5]) && 
                            int.TryParse(parts[6], out nCode);

                        /*
                         * Questionable fields
                         */
                        if (string.IsNullOrWhiteSpace(parts[1]))
                        {
                            validateBad += 1;
                        }
                        if (string.IsNullOrWhiteSpace(parts[3]))
                        {
                            validateBad += 1;
                        }

                        // NICI code must be 909 or greater
                        if (nCode < 909)
                        {
                            validateBad += 1;
                            /*
                             * The developer expects tons of data so they use an event to report bad NCIS data
                             * The alternate is to set a breakpoint with a condition to be able to inspect the data
                             */
                            BadNcisHandler?.Invoke(index);
                        }

                        if (validRow)
                        {

                            validRows.Add(new DataItem()
                            {
                                Id = index,
                                Date = d,
                                Address = parts[1],
                                District = district,
                                Beat = parts[3],
                                Grid = grid,
                                Description = parts[5],
                                NcicCode = nCode,
                                Latitude = latitude,
                                Longitude = longitude,
                                Inspect = validateBad > 0
                            });

                        }
                        else
                        {
                            // fields to review in specific rows
                            invalidRows.Add(new DataItemInvalid() { Row = index, Line = string.Join(",", parts) });
                        }

                    }
                }

            }
            catch (Exception exception)
            {

                return (false, exception, null, null, 0);
            }

            return (true, null, validRows, invalidRows,emptyLineCount);

        }

    }
}
