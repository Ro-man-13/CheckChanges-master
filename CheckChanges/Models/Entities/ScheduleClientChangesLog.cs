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
    
    public partial class ScheduleClientChangesLog
    {
        public int id_change { get; set; }
        public Nullable<System.DateTime> ExactTime { get; set; }
        public Nullable<int> schedule_id { get; set; }
        public string OurCase { get; set; }
        public string WhatChanged { get; set; }
        public string ChangedFrom { get; set; }
        public string ChangedTo { get; set; }
        public string ChangedBy { get; set; }
        public Nullable<bool> Sent { get; set; }
        public byte[] SSMA_TimeStamp { get; set; }
    }
}
