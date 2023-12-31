﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BussinessTickets
{
    public class Events 
    {
        public string ShortNameEvent { get; set; }
        public string FullNameEvent { get; set; }
        public StateEvent StateEvent { get; set; }
        public List<Schedule> lSchedule { get; set; } = new List<Schedule>();//[statement] Un evento puede llevarse a cabo una o varias veces
        public List<PriceByTicket> lPriceByTickets { get; set; } = new List<PriceByTicket>();
    }

    public class Schedule : ScheduleValidateProcess
    {
        public int Secuential { get; set; }
        public DateTime DateEvent { get; set; }
        public TimeSpan HourInitEvent { get; set; }
        public TimeSpan HourEndEvent { get; set; }
    }

    public class PriceByTicket
    {
        public int Secuential { get; set; }
        public TypeTicket ETypeTicket { get; set; }
        public decimal PriceTicket { get; set; }
    }

    public enum TypeTicket
    {
        General,
        Tribuna,
        Piso,
        VIP
    }

    public enum StateEvent
    {
        Pending,
        Confirmed,
        Canceled,
        Closed

    }

    public class ScheduleValidate
    {
        public Schedule InfoSchedule { get; set; }
        public bool IsValidPeriodDays { get; set; }
        public bool IsValidateTimeHour { get; set; }
        public bool IsValidateConcurrenceIntoDay { get; set; }
        public bool IsValidateConcurrenceIntoDayGlobal { get; set; }
        public bool IsValidateConcurrenceIntoHour { get; set; }
        public bool IsValidateConcurrenceIntoHourGlobal { get; set; }
    }

    public class ScheduleGlobalValidate : Schedule
    {
        public string ShortNameEvent { get; set; }
    }

}
