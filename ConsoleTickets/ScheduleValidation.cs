﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTickets
{
    public class ScheduleValidation
    {
        public static bool ExecuteValidateSchedule(string ShortNameEvent, List<Schedule> lSchedule, List<ScheduleGlobalValidate> scheduleGlobals)
        {
            bool ValidSchedule = default;

            List<ScheduleValidate> noValidSchedule = new List<ScheduleValidate>();
            int valueMinDays = 60;
            int valueMaxDays = 730;
            int valueMaxHour = 4;

            lSchedule.ForEach(itemSchedule => 
            {
                ScheduleValidate scheduleValidate = new ScheduleValidate();
                scheduleValidate.InfoSchedule = itemSchedule;
                bool IsValidateSchedule = default;

                //[statement] Solo se pueden colocar eventos desde 60 días posteriores hasta 2 años.
                IsValidateSchedule = ValidatePeriodDaySchedule(itemSchedule, valueMinDays, valueMaxDays);
                scheduleValidate.IsValidPeriodDays = IsValidateSchedule;

                //[statement] Cada evento puede durar un máximo de 4 horas
                IsValidateSchedule = ValidateTimeHourSchedule(itemSchedule, valueMaxHour);
                scheduleValidate.IsValidPeriodDays = IsValidateSchedule;
                
                //[statement] Cada evento puede durar un máximo de 4 horas
                IsValidateSchedule = ValidateConcurrenceIntoDay(itemSchedule, lSchedule);
                scheduleValidate.IsValidateConcurrenceIntoDay = IsValidateSchedule;

                
                if(!scheduleValidate.IsValidPeriodDays || !scheduleValidate.IsValidateTimeHour || !scheduleValidate.IsValidateConcurrenceIntoDay)
                {
                    noValidSchedule.Add(scheduleValidate);
                }

            });

            noValidSchedule.ForEach(itemSchedule =>
            {
                Console.WriteLine();
                Console.WriteLine($"{ShortNameEvent}\t{itemSchedule.InfoSchedule.DateEvent}\t{itemSchedule.InfoSchedule.HourInitEvent}\t{itemSchedule.InfoSchedule.HourEndEvent}");
                if (itemSchedule.IsValidPeriodDays)
                {
                    Console.WriteLine($"* Period Day is Invalid. Recomneded Date Event Between {valueMinDays} Days and {valueMaxDays} Days");
                }
                
                if (itemSchedule.IsValidateTimeHour)
                {
                    Console.WriteLine($"* Period Time is Invalid. Recomneded Hour Event Have max {valueMaxHour} Hour");
                }
                
                if (itemSchedule.IsValidateConcurrenceIntoDay)
                {
                    Console.WriteLine($"* Validate Date, Exists Event in this Date. Recomneded change Date Differente {itemSchedule.InfoSchedule.DateEvent.ToShortDateString()} Hour");
                }

            });

            return ValidSchedule;
        }

        
        private static bool ValidatePeriodDaySchedule(Schedule dataSchedule , int minDays, int maxDays)
        {
            bool IsValidatePeriodDaySchedule  = default;

            int TotalDiffDay = int.Parse($"{(dataSchedule.DateEvent - DateTime.Now).TotalDays}");

            if(minDays > TotalDiffDay && TotalDiffDay < maxDays)
            {
                IsValidatePeriodDaySchedule = true;
            }
            else
            {
                IsValidatePeriodDaySchedule = false;
            }

            return IsValidatePeriodDaySchedule;
        }

        
        private static bool ValidateTimeHourSchedule(Schedule dataSchedule, int maxHourSchedule)
        {
            bool IsValidateTimeHour = default;

            int TotalDiffHour = int.Parse($"{(dataSchedule.HourEndEvent - dataSchedule.HourInitEvent).TotalHours}");

            if (maxHourSchedule > TotalDiffHour )
            {
                IsValidateTimeHour = true;
            }
            else
            {
                IsValidateTimeHour = false;
            }

            return IsValidateTimeHour;
        }

        private static bool ValidateConcurrenceIntoDay(Schedule dataSchedule, List<Schedule> ldataSchedule)
        {
            bool IsValidateConcurrenceIntoDay = default;

            int countEventIntoDay = ldataSchedule.Count(aCount => aCount.DateEvent.ToShortDateString() == dataSchedule.DateEvent.ToShortDateString());

            if (countEventIntoDay > 1)
            {
                IsValidateConcurrenceIntoDay = true;
            }
            else
            {
                IsValidateConcurrenceIntoDay = false;
            }

            return IsValidateConcurrenceIntoDay;
        }
    }
}
