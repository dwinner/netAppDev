//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Formula1Demo
{
    using System;
    using System.Collections.Generic;
    
    public partial class TeamsAndMotor
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string FullTeamName { get; set; }
        public string Motor { get; set; }
    
        public virtual Team Team { get; set; }
    }
}
