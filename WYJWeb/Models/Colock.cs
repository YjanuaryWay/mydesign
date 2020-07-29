using Hangfire;
using WYJWeb.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using WYJCore;
using WYJCore.Extension;

namespace WYJWeb.Models
{
    public class Clock : ModelBase<Clock>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 响铃时间
        /// </summary>
        public DateTime AlertTime { get; set; }

        /// <summary>
        /// 频率设置
        /// </summary>
        [SugarColumn(IsIgnore =true)]
        public IntervalType IntervalType { get; set; }

        /// <summary>
        /// 重复时间
        /// </summary>     
        public string IntervalDateStr { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<int> IntervalDate => IntervalDateStr?.SplitToInt();

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? StopTime { get; set; }

        public static bool AlertAtNow(Clock clock)
        {
            var today = DateTime.Today;
            switch (clock.IntervalType)
            {
                case IntervalType.天:
                    var n = clock.IntervalDate.First();// int.Parse(IntervalDateStr);
                    return (today - clock.AlertTime).Days % n == 0;
                case IntervalType.周:
                    var now = (int)today.DayOfWeek;
                    return clock.IntervalDate.Exists( x => x == now);
                case IntervalType.月:
                    now = today.Day;
                    return clock.IntervalDate.Exists( x => x == now);
                case IntervalType.季度:
                    return today.Day == clock.AlertTime.Day && (today.Month - clock.AlertTime.Month) % 3 == 0;
                case IntervalType.年:
                    return today.DayOfYear == clock.AlertTime.DayOfYear;

            }
            return false;
        }

        /// <summary>
        /// 重复方式
        /// </summary>
        public string Repeation
        {
            get
            {
                switch (IntervalType)
                {
                    case IntervalType.天:
                        return $"每{IntervalDate[0]}天";
                    case IntervalType.周:
                        var days = IntervalDate.Select(x => (Weekday)x).JoinToString(",");
                        return $"每{days}";
                    case IntervalType.月:
                        days = IntervalDate.Select(x => x + "号").JoinToString(",");
                        return $"每月{days}";
                    case IntervalType.仅一次:
                        return "仅一次";
                    default:
                        return "每" + IntervalType;
                }
            }
        }


        /// <summary>
        /// 响铃行为
        /// </summary>
        public static void Alert()
        {
            
        }
        public virtual void Update(DbContext<Clock> db)
        {
            if (IntervalType != IntervalType.仅一次)
            {
                AlertTime = NextDate(AlertTime);
                db.Save(this);
            }
        }

        public DateTime NextDate(DateTime thisDate)
        {
            switch (IntervalType)
            {
                case IntervalType.天:
                    return thisDate.AddDays(IntervalDate[0]);
                case IntervalType.周:
                    var day = IntervalDate[0];
                    try
                    {
                        day = IntervalDate.First(x => x > (int)thisDate.DayOfWeek);
                    }
                    catch
                    {
                    }
                    return GetNextWeekday((DayOfWeek)day, thisDate);
                case IntervalType.月:
                    day = GetNextMonthday(thisDate.Year, thisDate.Month, thisDate.Day, IntervalDate);
                    return thisDate.AddDays(day - thisDate.Day);
                case IntervalType.季度:
                    return thisDate.AddMonths(3);
                case IntervalType.年:
                    return thisDate.AddYears(1);
                default:
                    return thisDate;
            }
        }

        private DateTime GetNextWeekday(DayOfWeek day, DateTime thisDate)
        {
            var x = day - thisDate.DayOfWeek;
            if (x <= 0) x += 7;
            return thisDate.AddDays(x);
        }

        /// <summary>
        /// 重复时间
        /// </summary>
        public int GetNextMonthday(int Year, int Month, int Today, List<int> intervalDates)
        {
            int lastDay = DateTime.DaysInMonth(Year, Month);
            if (lastDay > Today)
            {
                for (int i = 0; i < intervalDates.Count; i++)
                {
                    if (intervalDates[i] > Today)
                    {
                        if (intervalDates[i] > lastDay)
                        {
                            return lastDay;
                        }
                        return intervalDates[i];
                    }
                }
            }
            return GetNextMonthday(Year, Month + 1, 0, intervalDates) + lastDay;
        }
    }

}
