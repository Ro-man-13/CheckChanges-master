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
    
    public partial class DocumentFolders
    {
        public int id { get; set; }
        public Nullable<int> client_id { get; set; }
        public string DocumentFolder { get; set; }
    
        public virtual Users Users { get; set; }
    }
}
