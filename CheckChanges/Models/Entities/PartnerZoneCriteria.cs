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
    
    public partial class PartnerZoneCriteria
    {
        public int id_record { get; set; }
        public Nullable<int> user_id { get; set; }
        public string WhatCriteria { get; set; }
        public Nullable<bool> IsNumeric { get; set; }
        public string ID { get; set; }
        public byte[] SSMA_TimeStamp { get; set; }
    
        public virtual Users Users { get; set; }
    }
}
