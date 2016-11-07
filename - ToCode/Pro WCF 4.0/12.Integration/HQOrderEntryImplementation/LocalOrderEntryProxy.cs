using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;

namespace HQOrderEntryImplementation
{
    public class LocalOrderEntryProxy : ClientBase<LocalOrderEntryInterface.IReceiveOrderEntryLocalBranch>, LocalOrderEntryInterface.IReceiveOrderEntryLocalBranch
    {

        public LocalOrderEntryProxy()
            : base("LocalOrderEntryEndpoint")
        {

        }
        public int SendLocalOrderEntry(LocalOrderEntryInterface.LocalOrderEntry localOrderEntry)
        {
            return Channel.SendLocalOrderEntry(localOrderEntry);
        }
    }
}
