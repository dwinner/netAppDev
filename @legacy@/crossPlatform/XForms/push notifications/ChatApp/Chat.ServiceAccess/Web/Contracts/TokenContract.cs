﻿using Newtonsoft.Json;

namespace Chat.ServiceAccess.Web.Contracts
{
   /// <summary>
   ///    Token contract.
   /// </summary>
   public class TokenContract
   {
      /// <summary>
      ///    Gets or sets the access token.
      /// </summary>
      /// <value>The access token.</value>
      [JsonProperty("access_token")]
      public string AccessToken { get; set; }

      /// <summary>
      ///    Gets or sets the type of the token.
      /// </summary>
      /// <value>The type of the token.</value>
      [JsonProperty("token_type")]
      public string TokenType { get; set; }

      /// <summary>
      ///    Gets or sets the expires in.
      /// </summary>
      /// <value>The expires in.</value>
      [JsonProperty("expires_in")]
      public int ExpiresIn { get; set; }

      /// <summary>
      ///    Gets or sets the username.
      /// </summary>
      /// <value>The username.</value>
      [JsonProperty("userName")]
      public string Username { get; set; }

      /// <summary>
      ///    Gets or sets the issued at.
      /// </summary>
      /// <value>The issued at.</value>
      [JsonProperty(".issued")]
      public string IssuedAt { get; set; }

      /// <summary>
      ///    Gets or sets the expires at.
      /// </summary>
      /// <value>The expires at.</value>
      [JsonProperty(".expires")]
      public string ExpiresAt { get; set; }
   }
}