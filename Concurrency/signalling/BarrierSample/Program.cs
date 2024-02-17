var barrier = new Barrier(3);

new Thread(Speak).Start();
new Thread(Speak).Start();
new Thread(Speak).Start();
return;

void Speak()
{
   for (var i = 0; i < 5; i++)
   {
      Console.Write("{0} ", i);
      barrier.SignalAndWait();
   }
}