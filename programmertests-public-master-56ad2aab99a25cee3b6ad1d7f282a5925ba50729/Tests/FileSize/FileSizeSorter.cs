using System.Collections.Generic;
using Framework;
using System.Linq;
using System;

namespace Tests.FileSize
{
    public class FileSizeSorter
    {
        /// <summary>
        /// Sort a list of File Size strings by the value they represent
        /// </summary>
        /// <remarks>
        /// We'd like this method like to sort a given list of strings that represent file sizes (eg. "23.5 kB")
        /// 
        /// File sizes may come with decimals or without, will have whitespace seperating the number and unit
        /// and will use the following units (In Descending order):
        ///     - TB  (TeraByte) - 1024^4 - 1,099,511,627,776 Bytes
        ///     - GB  (GigaByte) - 1024^3 - 1,073,741,824     Bytes
        ///     - MB  (MegaByte) - 1024^2 - 1,048,576         Bytes
        ///     - kB  (KiloByte) - 1024   - 1,024             Bytes
        ///     - B   (Byte)     - 1      - 1                 Bytes
        /// 
        /// See Unit Tests for more details.
        /// </remarks>
        /// <param name="fileSizes">An enumerable of strings containing a value that represents a filesize as described above.</param>
        /// <param name="descending">A boolean determining if the results should be in descending order.</param>
        /// <returns>
        /// An enumerable of strings (same values provided) sorted by the file-sizes that they represent.
        /// </returns>
        class Size
        {
            public string fileSize { get; set; }
        }


        public virtual IEnumerable<string> Sort(IEnumerable<string> fileSizes, bool descending = false)
        {
            throw new TestUnfinishedException();

            List<decimal> decSizes;
            foreach(string size in fileSizes)
            {
                decSizes.Add(substring(size));
            }
            decSizes.OrderByDescending(i => i);
            List<string> strSizes = decSizes.Select(x => x.ToString()).ToList();
            for (int i = 0; i < strSizes.Count; i++)
            {
                switch(Convert.ToDecimal(strSizes[i]))
                {
                    case decimal s when (s > 1024 && s < (decimal)Math.Pow(1024, (1/2))):
                        strSizes[i] = Convert.ToString(Convert.ToDecimal(strSizes[i]) * 1024) + " kB" ;
                        break;
                    case decimal s when (s > (decimal)Math.Pow(1024, (1 / 2)) && s < (decimal)Math.Pow(1024, (1 / 3))):
                        strSizes[i] = Convert.ToString(Convert.ToDecimal(strSizes[i]) * (decimal)Math.Pow(1024, (1 / 2))) + " MB";
                        break;
                    case decimal s when (s > (decimal)Math.Pow(1024, (1 / 3)) && s < (decimal)Math.Pow(1024, (1 / 4))):
                        strSizes[i] = Convert.ToString(Convert.ToDecimal(strSizes[i]) * (decimal)Math.Pow(1024, (1 / 3))) + " GB";
                        break;
                    case decimal s when (s > (decimal)Math.Pow(1024, (1 / 4))):
                        strSizes[i] = Convert.ToString(Convert.ToDecimal(strSizes[i]) * (decimal)Math.Pow(1024, (1 / 2))) + " TB";
                        break;
                    default:
                        strSizes[i] = strSizes[i] + " B";
                        break;
                }
            }
            return strSizes.AsEnumerable();
        }

        private decimal substring(string size)
        {
            throw new NotImplementedException();

            string[] sizeSplit = size.Split(' ');
            decimal sizeOnly;
            switch (sizeSplit[1])
            {
                case "B":
                    sizeOnly = Convert.ToDecimal(sizeSplit[0]);
                    break;
                case "kB":
                    sizeOnly = Convert.ToDecimal(sizeSplit[0]) * 1024;
                    break;
                case "MB":
                    sizeOnly = Convert.ToDecimal(sizeSplit[0]) * (decimal)Math.Pow(1024, 2);
                    break;
                case "GB":
                    sizeOnly = Convert.ToDecimal(sizeSplit[0]) * (decimal)Math.Pow(1024, 3);
                    break;
                case "TB":
                    sizeOnly = Convert.ToDecimal(sizeSplit[0]) * (decimal)Math.Pow(1024, 4);
                    break;
            }
            return sizeOnly;
        }
    }
}
