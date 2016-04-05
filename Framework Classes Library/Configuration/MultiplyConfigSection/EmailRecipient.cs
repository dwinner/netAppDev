namespace CustomConfigSectionSample
{
   /// <summary>
   ///    Получатель письма
   /// </summary>
   public struct EmailRecipient
   {
      public string ToDisplayName { get; set; }

      public string ToAddress { get; set; }

      public override string ToString() => $"ToDisplayName: {ToDisplayName}, ToAddress: {ToAddress}";
   }
}