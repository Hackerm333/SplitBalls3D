using System;
using UnityEngine;

namespace MirkoZambito
{
    public class Utilities
    {

        /// <summary>
        /// Convert the given float number to meters format.
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static string FloatToMeters(float distance)
        {
            if (distance < 1000f)
            {
                return Mathf.RoundToInt(distance).ToString() + " M";
            }
            else
            {
                float distanceTemp = distance / 1000f;
                return string.Format("{0:0.00}", distanceTemp) + " KM";
            }
        }


        /// <summary>
        /// Covert the given seconds to time format.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string SecondsToTimeFormat(double seconds)
        {
            int hours = (int)seconds / 3600;
            int mins = ((int)seconds % 3600) / 60;
            seconds = Math.Round(seconds % 60, 0);
            return hours + ":" + mins + ":" + seconds;
        }

        /// <summary>
        /// Covert the given seconds to minutes format.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string SecondsToMinutesFormat(double seconds)
        {
            int mins = ((int)seconds % 3600) / 60;
            seconds = Math.Round(seconds % 60, 0);
            return mins + ":" + seconds;
        }
    }
}
