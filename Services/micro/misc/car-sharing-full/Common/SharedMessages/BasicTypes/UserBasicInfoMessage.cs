using System;
using System.Collections.Generic;
using System.Text;

namespace SharedMessages.BasicTypes
{
    public class UserBasicInfoMessage : LoginBasicInfoMessage
    {
        public Guid Id { get; set; }
        public string? DisplayName { get; set; }
    }
}
