﻿namespace Time;

public interface IDateTimeServer
{
   DateTime Now { get; }
}

public class DateTimeServer2 : IDateTimeServer
{
   public DateTime Now => DateTime.Now;
}

public class InquiryController
{
   private readonly DateTimeServer2 _dateTimeServer;

   public InquiryController(DateTimeServer2 dateTimeServer) => _dateTimeServer = dateTimeServer;

   public void ApproveInquiry(int id)
   {
      var inquiry = GetById(id);
      inquiry.Approve(_dateTimeServer.Now);
      SaveInquiry(inquiry);
   }

   private void SaveInquiry(Inquiry inquiry)
   {
   }

   private Inquiry GetById(int id) => null;
}

public class Inquiry
{
   private Inquiry(bool isApproved, DateTime? timeApproved)
   {
      if (isApproved && !timeApproved.HasValue)
      {
         throw new Exception();
      }

      IsApproved = isApproved;
      TimeApproved = timeApproved;
   }

   public bool IsApproved { get; private set; }
   public DateTime? TimeApproved { get; private set; }

   public void Approve(DateTime now)
   {
      if (IsApproved)
      {
         return;
      }

      IsApproved = true;
      TimeApproved = now;
   }
}