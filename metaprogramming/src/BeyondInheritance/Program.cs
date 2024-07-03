using BeyondInheritance;
using BeyondInheritance.EventSourcing;
using Fundamentals;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
   .ConfigureServices((_, services) =>
   {
      var types = new Types();
      services.AddSingleton<ITypes>(types);
      services.AddBindingsByConvention(types);
      services.AddSelfBinding(types);
   })
   .Build();

var eventLog = host.Services.GetRequiredService<IEventLog>();

var bankAccountId = EventSourceId.New();
await eventLog.Append(bankAccountId, new BankAccountOpened("Jane Doe"));
await eventLog.Append(bankAccountId, new DepositPerformed(100));
await eventLog.Append(bankAccountId, new WithdrawalPerformed(32));
await eventLog.Append(bankAccountId, new BankAccountClosed());