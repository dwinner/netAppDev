using Ninject;
using Ninject.Planning.Bindings;

namespace DataMigration.Business
{
   public class IsSourceAttribute : ConstraintAttribute
   {
      private readonly bool _isSource;

      public IsSourceAttribute(bool isSource) => _isSource = isSource;

      public override bool Matches(IBindingMetadata metadata) =>
         metadata.Has("IsSource") && metadata.Get<bool>("IsSource") == _isSource;
   }
}