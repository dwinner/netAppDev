using Microsoft.AspNetCore.Authorization;

namespace CrossCutting;

public class AdminForNamespace : IAuthorizationRequirement
{
   public AdminForNamespace(string @namespace) => Namespace = @namespace;

   public string Namespace { get; }
}