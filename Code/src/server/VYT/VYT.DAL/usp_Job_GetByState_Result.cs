//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VYT.DAL
{
    using System;
    
    public partial class usp_Job_GetByState_Result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime Created { get; set; }
        public int State { get; set; }
        public string Languages { get; set; }
        public Nullable<long> Duration { get; set; }
        public string Notes { get; set; }
        public int DocumentPages { get; set; }
        public Nullable<System.DateTime> Processed { get; set; }
    }
}
