//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CheckChanges.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shedule
    {
        public int ScheduleID { get; set; }
        public string Our_case { get; set; }
        public Nullable<System.DateTime> Deadline { get; set; }
        public string Action { get; set; }
        public string Creator { get; set; }
        public Nullable<System.DateTime> Creation_date { get; set; }
        public Nullable<System.DateTime> Reminder { get; set; }
        public string Status { get; set; }
        public Nullable<bool> Executed { get; set; }
        public string User { get; set; }
        public Nullable<System.DateTime> Execution_date { get; set; }
        public Nullable<bool> Show_all { get; set; }
        public Nullable<System.DateTime> Due_date { get; set; }
        public string ChangeReason { get; set; }
        public Nullable<short> Stage { get; set; }
        public Nullable<int> MailID { get; set; }
        public string History { get; set; }
        public Nullable<bool> ToShow { get; set; }
        public byte[] SSMA_TimeStamp { get; set; }
        public string StatusHiddenText { get; set; }
    }
}
