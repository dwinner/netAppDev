/**
 * SQL CLR ענטדדונ
 */

using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Server;

namespace SqlSamplesUsingAdventureWorks
{
    public class Triggers
    {
        private const string EmailRegExPattern = @"([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$";

        //[SqlTrigger(Name = "InsertContact", Target = "Person.EmailAddress", Event = "FOR INSERT")]
        public static void InsertContact()
        {
            SqlTriggerContext triggerContext = SqlContext.TriggerContext;

            if (triggerContext != null && triggerContext.TriggerAction == TriggerAction.Insert)
            {
                using (var connection = new SqlConnection("Context Connection=true"))
                {
                    using (
                        var command = new SqlCommand
                        {
                            Connection = connection,
                            CommandText = "SELECT EmailAddress FROM INSERTED"
                        })
                    {
                        connection.Open();
                        var email = command.ExecuteScalar() as string;
                        if (email != null && !Regex.IsMatch(email, EmailRegExPattern))
                            throw new FormatException("Invalid email");
                    }
                }
            }
        }
    }
}