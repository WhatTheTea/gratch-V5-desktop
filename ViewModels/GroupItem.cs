﻿using gratch_core;

using ReactiveUI.Fody.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gratch_desktop.ViewModels
{
    public class GroupItem
    {
        private readonly Group selectedGroup;

        [Reactive]
        public string Name { get; set; }
        [Reactive]
        public string Holidays { get; set; }

        private List<DateTime> holidays
        {
            get
            {
                List<DateTime> result = null;
                var now = DateTime.Now;
                int holidaysCount = selectedGroup.Graph.Weekend.Count;

                for (int i = 1, d = 1; i <= DateTime.Now.DaysInMonth() || d <= holidaysCount; i++)
                {
                    var date = new DateTime(now.Year, now.Month, i);
                    if (selectedGroup.Graph.IsHoliday(date))
                    {
                        result.Add(date);
                    }
                }

                return result;

            }
        }
        public GroupItem()
        {
        }
        public GroupItem(Group grp)
        {
            selectedGroup = grp;

            Name = grp.Name;
            if (holidays != null)
            {
                string holidaysResult = "Holidays: ";
                foreach (var day in holidays)
                {
                    holidaysResult = (day != holidays.Last()) ? $@"{day:ddd}, " : $@"{day:ddd}";
}
                Holidays = holidaysResult;
            }
            else
{
                Holidays = "Holidays: None";
            }

        }

    }
}
