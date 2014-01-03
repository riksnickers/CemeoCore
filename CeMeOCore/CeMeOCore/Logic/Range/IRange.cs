using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMeOCore.Logic.Range
{
    /// <summary>
    /// This code in found on stackoverflow.
    /// SOURCE:http://stackoverflow.com/questions/4781611/how-to-know-if-a-datetime-is-between-a-daterange-in-c-sharp
    /// </summary>
    /// <typeparam name="T">Generic T</typeparam>
    public interface IRange<T>
    {
        /// <summary>
        /// This is the start value of the range
        /// </summary>
        T Start { get; }
        /// <summary>
        /// This is the end value of the range
        /// </summary>
        T End { get; }

        /// <summary>
        /// Does the range include the value?
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True or False</returns>
        Boolean Includes(T value);
        /// <summary>
        /// Does the include include the range?
        /// </summary>
        /// <param name="range"></param>
        /// <returns>True or False</returns>
        Boolean Includes(IRange<T> range);
    }

    
}
