using System;

namespace SharedMessages.BasicTypes;

public class UserBasicInfoMessage
{
   public Guid Id { get; set; }
   public string? DisplayName { get; set; }
}