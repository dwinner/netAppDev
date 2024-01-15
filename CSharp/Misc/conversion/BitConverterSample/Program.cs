foreach (var @byte in BitConverter.GetBytes(3.5))
{
   Console.Write(@byte + " "); // 0 0 0 0 0 0 12 64
}