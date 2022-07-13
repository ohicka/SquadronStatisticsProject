using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace SquadronStatistics.WebApp.Helpers
{
    public static class FileValidationHelper
    {
        private readonly static Regex _regex;
        static FileValidationHelper()
        {
            _regex = new Regex(Consts.StatisticLineRegex);
        }
        public static async Task<List<Row>> ValidateFileContent(Stream fileStream)
        {
            try
            {
                List<Row> rows = new List<Row>();
                using (var reader = new StreamReader(fileStream))
                {
                    while (!reader.EndOfStream)
                    {
                        var row = await reader.ReadLineAsync();

                        if (row == null || !_regex.IsMatch(row))
                            continue;

                        var splitedRow = row.Remove(0, 2).Split(", ");

                        if (!IsValidColor(splitedRow[0]))
                            continue;

                        rows.Add(new Row()
                        {
                            Color = splitedRow[0],
                            Value = Int32.Parse(splitedRow[1]),
                            Label = splitedRow[2]
                        });
                        //-----------------------------------
                    }
                }
                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception("Problem appeared in file validation process", ex.InnerException);
            }
        }

        private static bool IsValidColor(string color)
        {
            var colorsArray = Enum.GetValues(typeof(KnownColor));

            KnownColor[] allColors = new KnownColor[colorsArray.Length];
            Array.Copy(colorsArray, allColors, colorsArray.Length);


            return allColors.Any(item => item.ToString().ToLowerInvariant() == color.ToLowerInvariant());

        }
    }
}
