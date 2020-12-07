using System;
using System.Collections.Generic;
using System.Text;

namespace TimeStructures
{
    public struct Time:IEquatable<Time>, IComparable<Time>
    {
        #region properties
        private readonly byte _Hours;
        public byte Hours
        {
            get { return _Hours; }
        }
        private readonly byte _Minutes;
        public byte Minutes
        {
            get { return _Minutes; }
        }
        private readonly byte _Seconds;
        public byte Seconds
        {
            get { return _Seconds; }
        }
        #endregion properties
        #region constructors
        public Time(byte Hour=00, byte Minute=00, byte Second=00)
        {
            _Hours = Hour;
            _Minutes = Minute;
            _Seconds = Second;
        }
        public Time(string timeString)
        {
            string[] splittedTimeString = timeString.Split(":");
            _Hours = Byte.Parse(splittedTimeString[0]);
            _Minutes = Byte.Parse(splittedTimeString[1]);
            _Seconds = Byte.Parse(splittedTimeString[2]);
        }
        #endregion constructors
        #region methods
        public override string ToString()
        {
            return String.Format("{0}:{1}:{2}", Hours.ToString(), Minutes.ToString(), Seconds.ToString());
        }
        public Time Plus(TimePeriod t)
        {
            int Hours = 0;
            int Minutes = 0;
            long tempPeriodSeconds = t.Seconds;
            while (tempPeriodSeconds % 3600 > 0)
            {
                Hours++;
                tempPeriodSeconds = tempPeriodSeconds - 3600;
            }
            while (tempPeriodSeconds % 60>0)
            {
                Minutes++;
                tempPeriodSeconds = tempPeriodSeconds - 60;
            }
            return new Time(Convert.ToByte(Hours), Convert.ToByte(Minutes), Convert.ToByte(tempPeriodSeconds));
        }
        public static Time Plus(Time t, TimePeriod p)
        {
            long tempPeriodSeconds = t.Seconds+t.Hours*3600+t.Minutes*60+t.Seconds;
            long hours=0;
            long minutes = 0;
            while (tempPeriodSeconds % 3600 > 0)
            {
                hours++;
                tempPeriodSeconds = tempPeriodSeconds - 3600;
            }
            while (tempPeriodSeconds % 60 > 0)
            {
                minutes++;
                tempPeriodSeconds = tempPeriodSeconds - 60;
            }
            return new Time(Convert.ToByte(hours), Convert.ToByte(minutes), Convert.ToByte(tempPeriodSeconds));
        }
        #endregion methods
        #region implementacja IEquatable i IComparable
        public bool Equals(Time time)
        {
            return (Hours==time.Hours && Minutes==time.Minutes && Seconds==time.Seconds ?  true : false);
        }

        public int CompareTo(Time time)
        {
            if (Hours == time.Hours)
            {
                //godziny są takie same-> przeszukiwanie po minutach/sekundach
                if (Minutes == time.Minutes)
                {
                    //minuty takie same -> porównanie po sekundach
                    if (Seconds == time.Seconds)
                    {
                        return 0;
                    }
                    else if (Seconds > time.Seconds)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else if (Minutes > time.Minutes)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }

            }
            else if (Hours > time.Hours)
            {
                // time jest wcześniej niż obiekt na którym wywoływana jest metoda 
                return 1;
            }
            else
            {
                // time jest później niż obiekt na którym wywoływana jest metoda 
                return -1;
            }
        }
        #endregion implementacja IEquatable i IComparable
        #region operatory
        public static bool operator ==(Time time1, Time t2)
        {
            return((time1.CompareTo(t2) == 0) ? true: false);
        }
        public static bool operator !=(Time time1, Time time2)
        {
            return ((time1.CompareTo(time2) != 0) ? true : false);
        }
        public static bool operator < (Time time1, Time time2)
        {
            return ((time1.CompareTo(time2) == 1) ? true : false);
        }
        public static bool operator <=(Time time1, Time time2)
        {
            return ((time1.CompareTo(time2) != -1) ? true : false);
        }
        public static bool operator >(Time time1, Time time2)
        {
            return ((time1.CompareTo(time2) == -1) ? true : false);
        }
        public static bool operator >=(Time time1, Time time2)
        {
            return ((time1.CompareTo(time2) != 1) ? true : false);
        }
        public static Time operator + (Time t1, TimePeriod t2)
        {
            return (t1.Plus(t2));
        }
        #endregion operatory 
    }
}
