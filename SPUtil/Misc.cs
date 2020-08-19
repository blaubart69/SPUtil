using System;

namespace SPUtil
{
    public class Misc
    {
        public static string NiceDuration2(in TimeSpan duration)
        {
            string nice = duration.Milliseconds + "ms"; ;

            if (duration.Ticks >= TimeSpan.TicksPerSecond)
            {
                nice = duration.Seconds + "s " + nice;

                if (duration.Ticks >= TimeSpan.TicksPerMinute)
                {
                    nice = duration.Minutes + "m " + nice;

                    if (duration.Ticks >= TimeSpan.TicksPerHour)
                    {
                        nice = duration.Hours + "h " + nice;

                        if (duration.Ticks >= TimeSpan.TicksPerDay)
                        {
                            nice += duration.Days + "d ";
                        }
                    }
                }
            }

            return nice;
        }
    }
}
