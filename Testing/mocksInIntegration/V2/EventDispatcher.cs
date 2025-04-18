﻿namespace V2;

public class EventDispatcher
{
   private readonly IDomainLogger _domainLogger;
   private readonly MessageBus _messageBus;

   public EventDispatcher(
      MessageBus messageBus,
      IDomainLogger domainLogger)
   {
      _domainLogger = domainLogger;
      _messageBus = messageBus;
   }

   public void Dispatch(List<IDomainEvent> events)
   {
      foreach (var ev in events)
      {
         Dispatch(ev);
      }
   }

   private void Dispatch(IDomainEvent ev)
   {
      switch (ev)
      {
         case EmailChangedEvent emailChangedEvent:
            _messageBus.SendEmailChangedMessage(
               emailChangedEvent.UserId,
               emailChangedEvent.NewEmail);
            break;

         case UserTypeChangedEvent userTypeChangedEvent:
            _domainLogger.UserTypeHasChanged(
               userTypeChangedEvent.UserId,
               userTypeChangedEvent.OldType,
               userTypeChangedEvent.NewType);
            break;
      }
   }
}