<getServiceA () PowerShell>
get-service A*
<return(serviceAList)>

<printServiceA (serviceAList) Python>
for item in serviceAList:
  print item.Members["ServiceName"].Value
<return()>

