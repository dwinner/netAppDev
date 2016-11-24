using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealTimeOrderTrackingApplication
{
    public class CallBackImplementation : RealTimeOrderTrackingCallBackContract.IOrderTracking
    {
        Dictionary<string, int> NumberOfOrderEntries;

        public CallBackImplementation()
        {
            NumberOfOrderEntries = new Dictionary<string, int>();

        }
        public System.Windows.Forms.Form formToReportTo;

        public void NewOrderForCountry(string countryID)
        {
            if (!NumberOfOrderEntries.ContainsKey(countryID))
            {
                NumberOfOrderEntries.Add(countryID, 1);
            }
            else
            {
                NumberOfOrderEntries[countryID] = NumberOfOrderEntries[countryID] + 1;
            }


            ((Form1)formToReportTo).listBox1.Items.Clear();
            foreach (var item in NumberOfOrderEntries)
            {
                ((Form1)formToReportTo).listBox1.Items.Add(string.Format("processed {0} orders for {1}", item.Value.ToString(), item.Key));
            }
        }

    }
}
