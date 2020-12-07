using System;
using System.Collections.Generic;
using System.Text;

namespace TimeStructures
{
    public struct TimePeriod:IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        #region properties
        private readonly long _Seconds;
        public long Seconds
        {
            get { return _Seconds; }
        }
        #endregion properties
        #region constructors
        public TimePeriod(long seconds=0)
        {
            _Seconds = seconds;
        }
        public TimePeriod(byte Hour=00,byte Minute=00, byte Second = 00)
        {
            _Seconds =Hour * 3600 + Minute * 60 + Second;
        }
        public TimePeriod(string timeString)
        {
            string[] splittedTimeString = timeString.Split(":");
            _Seconds = byte.Parse(splittedTimeString[0]) * 3600 + byte.Parse(splittedTimeString[1]) * 60 + byte.Parse(splittedTimeString[2]);
        }
        #endregion constructors
        #region methods
        public override string ToString()
        {
            long hours = Seconds % 3600;
            long minutes = (Seconds - hours * 3600) % 60;
            long seconds = Seconds - (hours * 3600 + minutes * 60);
            return String.Format("{0}:{1}:{2}",hours,minutes,seconds);
        }
        public TimePeriod Plus(TimePeriod period)
        {
            return new TimePeriod(Seconds + period.Seconds);
        }
        public static TimePeriod Plus(TimePeriod p1, TimePeriod p2)
        {
            return new TimePeriod(p1.Seconds + p2.Seconds);
        }
        #endregion methods
        #region implementacja IEquatable i IComparable
        public bool Equals(TimePeriod period)
        {
            return ((Seconds == period.Seconds) ? true : false);
        }
        public int CompareTo(TimePeriod period)
        {
            if (Seconds == period.Seconds)
            {
                return 0;
            }
            else if (Seconds > period.Seconds)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        #endregion implementacja IEquatable i IComparable
        #region operatory
        public static bool operator ==(TimePeriod p1, TimePeriod p2)
        {
            return ((p1.CompareTo(p2)==0) ? true : false);
        }
        public static bool operator !=(TimePeriod p1, TimePeriod p2)
        {
            return ((p1.CompareTo(p2)!=0) ? true : false);
        }
        public static bool operator >(TimePeriod p1, TimePeriod p2)
        {
            return ((p1.CompareTo(p2) == -1) ? true : false);
        }
        public static bool operator >=(TimePeriod p1, TimePeriod p2)
        {
            return ((p1.CompareTo(p2) != 1) ? true : false);
        }
        public static bool operator <=(TimePeriod p1, TimePeriod p2)
        {
            return ((p1.CompareTo(p2) != -1) ? true : false);
        }
        public static bool operator <(TimePeriod p1, TimePeriod p2)
        {
            return ((p1.CompareTo(p2) == 1) ? true : false);
        }
        public static TimePeriod operator + (TimePeriod p1, TimePeriod p2)
        {
            return p1.Plus(p2);
        }
        #endregion operatory
    }
}
