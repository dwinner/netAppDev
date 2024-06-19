using BuilderParameter;

var ms = new MailService();
ms.SendEmail(email => email.From("foo@bar.com")
   .To("bar@baz.com")
   .Body("Hello, how are you?"));