using System.Collections;

namespace StringHandling.Compression.BitBased;

public sealed class BinWriter : BinaryWriter
{
   private const int LgR = 8;
   private BitArray? _bitArray;
   private byte _currentBitIdx;
   private bool[] _currentByte = new bool[LgR];

   public BinWriter(Stream stream)
      : base(stream)
   {
   }

   public override void Flush()
   {
      base.Write(ConvertToByte(_currentByte));
      base.Flush();
   }

   public override void Write(bool value)
   {
      _currentByte[_currentBitIdx] = value;
      _currentBitIdx++;

      if (_currentBitIdx == LgR)
      {
         base.Write(ConvertToByte(_currentByte));
         _currentBitIdx = 0;
         _currentByte = new bool[LgR];
      }
   }

   public override void Write(byte value)
   {
      _bitArray = new BitArray(new[] { value });
      for (byte i = 0; i < LgR; i++)
      {
         Write(_bitArray[i]);
      }

      _bitArray = null;
   }

   public override void Write(byte[] buffer)
   {
      foreach (var @byte in buffer)
      {
         Write(@byte);
      }
   }

   public override void Write(uint value)
   {
      _bitArray = new BitArray(BitConverter.GetBytes(value));
      for (byte i = 0; i < 32; i++)
      {
         Write(_bitArray[i]);
      }

      _bitArray = null;
   }

   public override void Write(ulong value)
   {
      _bitArray = new BitArray(BitConverter.GetBytes(value));
      for (byte i = 0; i < 64; i++)
      {
         Write(_bitArray[i]);
      }

      _bitArray = null;
   }

   public override void Write(ushort value)
   {
      _bitArray = new BitArray(BitConverter.GetBytes(value));
      for (byte i = 0; i < 16; i++)
      {
         Write(_bitArray[i]);
      }

      _bitArray = null;
   }

   private static byte ConvertToByte(IReadOnlyList<bool> bools)
   {
      byte boolVal = 0;
      byte bitIndex = 0;
      for (var i = 0; i < LgR; i++)
      {
         if (bools[i])
         {
            boolVal |= (byte)(1 << bitIndex);
         }

         bitIndex++;
      }

      return boolVal;
   }
}