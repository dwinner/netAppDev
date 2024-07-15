using Microsoft.AspNetCore.Authorization;

namespace CrossCutting;

public class CrossCuttingPoliciesProvider : IAuthorizationPolicyProvider
{
   private const string ValidNs = nameof(CrossCutting);

   private readonly AuthorizationPolicy _policy = new AuthorizationPolicyBuilder()
      .AddRequirements(new AdminForNamespace(ValidNs))
      .Build();

   public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => Task.FromResult(_policy);

   public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy?>(_policy);

   public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName) =>
      Task.FromResult<AuthorizationPolicy?>(_policy);
}