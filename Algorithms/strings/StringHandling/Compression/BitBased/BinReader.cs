using System.Collections;

namespace StringHandling.Compression.BitBased;

public sealed class BinReader : BinaryReader
{
   private const int LgR = 8;
   private readonly bool[] _currentByte = new bool[LgR];
   private BitArray? _bitArray;
   private byte _currentBitIdx;

   public BinReader(Stream stream)
      : base(stream)
   {
      _bitArray = new BitArray(new[] { base.ReadByte() });
      _bitArray.CopyTo(_currentByte, 0);
      _bitArray = null;
   }

   public override bool ReadBoolean()
   {
      if (_currentBitIdx == LgR)
      {
         _bitArray = new BitArray(new[] { base.ReadByte() });
         _bitArray.CopyTo(_currentByte, 0);
         _bitArray = null;
         _currentBitIdx = 0;
      }

      var boolVal = _currentByte[_currentBitIdx];
      _currentBitIdx++;

      return boolVal;
   }

   public override byte ReadByte()
   {
      var boolArray = new bool[LgR];
      byte i;
      for (i = 0; i < LgR; i++)
      {
         boolArray[i] = ReadBoolean();
      }

      byte byteVal = 0;
      byte bitIndex = 0;
      for (i = 0; i < LgR; i++)
      {
         if (boolArray[i])
         {
            byteVal |= (byte)(1 << bitIndex);
         }

         bitIndex++;
      }

      return byteVal;
   }

   public override byte[] ReadBytes(int count)
   {
      var bytes = new byte[count];
      for (var i = 0; i < count; i++)
      {
         bytes[i] = ReadByte();
      }

      return bytes;
   }

   public override ushort ReadUInt16()
   {
      var bytes = ReadBytes(2);
      return BitConverter.ToUInt16(bytes, 0);
   }

   public override uint ReadUInt32()
   {
      var bytes = ReadBytes(4);
      return BitConverter.ToUInt32(bytes, 0);
   }

   public override ulong ReadUInt64()
   {
      var bytes = ReadBytes(LgR);
      return BitConverter.ToUInt64(bytes, 0);
   }
}