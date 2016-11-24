<task1 () Python>
import clr
from System.Threading import Thread
print "Task1 runs on thread id " + Thread.CurrentThread.ManagedThreadId.ToString()
Thread.SpinWait(1000000000)
<return()>

<task2 () Python>
import clr
from System.Threading import Thread
print "Task2 runs on thread id " + Thread.CurrentThread.ManagedThreadId.ToString()
Thread.SpinWait(1000000000)
<return()>

<task3 () Python>
import clr
from System.Threading import Thread
print "Task3 runs on thread id " + Thread.CurrentThread.ManagedThreadId.ToString()
Thread.SpinWait(1000000000)
<return()>


