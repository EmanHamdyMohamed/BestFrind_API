//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BestFrind.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class quize
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public quize()
        {
            this.quize_question = new HashSet<quize_question>();
        }
    
        public string name { get; set; }
        public int Id { get; set; }
        public Nullable<int> userId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<quize_question> quize_question { get; set; }
        public virtual user user { get; set; }
    }
}