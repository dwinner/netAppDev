using System.Runtime.Serialization;
using System.Text;

namespace CodeBuilder;

public class CodeBuilder
{
   public readonly StringBuilder Builder = new();
   private int _indentLevel;

   public int Capacity
   {
      get => Builder.Capacity;
      set => Builder.Capacity = value;
   }

   public int MaxCapacity => Builder.MaxCapacity;

   public int Length
   {
      get => Builder.Length;
      set => Builder.Length = value;
   }

   public char this[int index]
   {
      get => Builder[index];
      set => Builder[index] = value;
   }

   public CodeBuilder Indent()
   {
      _indentLevel++;
      return this;
   }

   public override string ToString() => Builder.ToString();

   public void GetObjectData(SerializationInfo info, StreamingContext context)
   {
      ((ISerializable)Builder).GetObjectData(info, context);
   }

   public int EnsureCapacity(int capacity) => Builder.EnsureCapacity(capacity);

   public string ToString(int startIndex, int length) => Builder.ToString(startIndex, length);

   public CodeBuilder Clear()
   {
      Builder.Clear();
      return this;
   }

   public CodeBuilder Append(char value, int repeatCount)
   {
      Builder.Append(value, repeatCount);
      return this;
   }

   public CodeBuilder Append(char[] value, int startIndex, int charCount)
   {
      Builder.Append(value, startIndex, charCount);
      return this;
   }

   public CodeBuilder Append(string value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Append(string value, int startIndex, int count)
   {
      Builder.Append(value, startIndex, count);
      return this;
   }

   public CodeBuilder AppendLine()
   {
      Builder.AppendLine();
      return this;
   }

   public CodeBuilder AppendLine(string value)
   {
      Builder.AppendLine(value);
      return this;
   }

   public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
   {
      Builder.CopyTo(sourceIndex, destination, destinationIndex, count);
   }

   public CodeBuilder Insert(int index, string value, int count)
   {
      Builder.Insert(index, value, count);
      return this;
   }

   public CodeBuilder Remove(int startIndex, int length)
   {
      Builder.Remove(startIndex, length);
      return this;
   }

   public CodeBuilder Append(bool value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Append(sbyte value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Append(byte value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Append(char value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Append(short value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Append(int value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Append(long value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Append(float value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Append(double value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Append(decimal value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Append(ushort value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Append(uint value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Append(ulong value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Append(object value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Append(char[] value)
   {
      Builder.Append(value);
      return this;
   }

   public CodeBuilder Insert(int index, string value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder Insert(int index, bool value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder Insert(int index, sbyte value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder Insert(int index, byte value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder Insert(int index, short value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder Insert(int index, char value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder Insert(int index, char[] value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder Insert(int index, char[] value, int startIndex, int charCount)
   {
      Builder.Insert(index, value, startIndex, charCount);
      return this;
   }

   public CodeBuilder Insert(int index, int value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder Insert(int index, long value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder Insert(int index, float value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder Insert(int index, double value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder Insert(int index, decimal value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder Insert(int index, ushort value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder Insert(int index, uint value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder Insert(int index, ulong value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder Insert(int index, object value)
   {
      Builder.Insert(index, value);
      return this;
   }

   public CodeBuilder AppendFormat(string format, object arg0)
   {
      Builder.AppendFormat(format, arg0);
      return this;
   }

   public CodeBuilder AppendFormat(string format, object arg0, object arg1)
   {
      Builder.AppendFormat(format, arg0, arg1);
      return this;
   }

   public CodeBuilder AppendFormat(string format, object arg0, object arg1, object arg2)
   {
      Builder.AppendFormat(format, arg0, arg1, arg2);
      return this;
   }

   public CodeBuilder AppendFormat(string format, params object[] args)
   {
      Builder.AppendFormat(format, args);
      return this;
   }

   public CodeBuilder AppendFormat(IFormatProvider provider, string format, params object[] args)
   {
      Builder.AppendFormat(provider, format, args);
      return this;
   }

   public CodeBuilder Replace(string oldValue, string newValue)
   {
      Builder.Replace(oldValue, newValue);
      return this;
   }

   public bool Equals(CodeBuilder sb) => Builder.Equals(sb);

   public CodeBuilder Replace(string oldValue, string newValue, int startIndex, int count)
   {
      Builder.Replace(oldValue, newValue, startIndex, count);
      return this;
   }

   public CodeBuilder Replace(char oldChar, char newChar)
   {
      Builder.Replace(oldChar, newChar);
      return this;
   }

   public CodeBuilder Replace(char oldChar, char newChar, int startIndex, int count)
   {
      Builder.Replace(oldChar, newChar, startIndex, count);
      return this;
   }
}