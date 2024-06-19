using System.Runtime.Serialization;
using System.Text;

namespace AdapterDecorator;

public class MyStringBuilder
{
   private readonly StringBuilder _stringBuilder = new();

   public int Capacity
   {
      get => _stringBuilder.Capacity;
      set => _stringBuilder.Capacity = value;
   }

   public int MaxCapacity => _stringBuilder.MaxCapacity;

   public int Length
   {
      get => _stringBuilder.Length;
      set => _stringBuilder.Length = value;
   }

   public char this[int index]
   {
      get => _stringBuilder[index];
      set => _stringBuilder[index] = value;
   }

   //=============================================

   public static implicit operator MyStringBuilder(string s)
   {
      var msb = new MyStringBuilder();
      msb._stringBuilder.Append(s);
      return msb;
   }

   public static MyStringBuilder operator +(MyStringBuilder msb, string s)
   {
      msb.Append(s);
      return msb;
   }

   public override string ToString() => _stringBuilder.ToString();

   //=============================================

   public void GetObjectData(SerializationInfo info, StreamingContext context)
   {
      ((ISerializable)_stringBuilder).GetObjectData(info, context);
   }

   public int EnsureCapacity(int capacity) => _stringBuilder.EnsureCapacity(capacity);

   public string ToString(int startIndex, int length) => _stringBuilder.ToString(startIndex, length);

   public StringBuilder Clear() => _stringBuilder.Clear();

   public StringBuilder Append(char value, int repeatCount) => _stringBuilder.Append(value, repeatCount);

   public StringBuilder Append(char[] value, int startIndex, int charCount) =>
      _stringBuilder.Append(value, startIndex, charCount);

   public StringBuilder Append(string value) => _stringBuilder.Append(value);

   public StringBuilder Append(string value, int startIndex, int count) =>
      _stringBuilder.Append(value, startIndex, count);

   public StringBuilder AppendLine() => _stringBuilder.AppendLine();

   public StringBuilder AppendLine(string value) => _stringBuilder.AppendLine(value);

   public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
   {
      _stringBuilder.CopyTo(sourceIndex, destination, destinationIndex, count);
   }

   public StringBuilder Insert(int index, string value, int count) => _stringBuilder.Insert(index, value, count);

   public StringBuilder Remove(int startIndex, int length) => _stringBuilder.Remove(startIndex, length);

   public StringBuilder Append(bool value) => _stringBuilder.Append(value);

   public StringBuilder Append(sbyte value) => _stringBuilder.Append(value);

   public StringBuilder Append(byte value) => _stringBuilder.Append(value);

   public StringBuilder Append(char value) => _stringBuilder.Append(value);

   public StringBuilder Append(short value) => _stringBuilder.Append(value);

   public StringBuilder Append(int value) => _stringBuilder.Append(value);

   public StringBuilder Append(long value) => _stringBuilder.Append(value);

   public StringBuilder Append(float value) => _stringBuilder.Append(value);

   public StringBuilder Append(double value) => _stringBuilder.Append(value);

   public StringBuilder Append(decimal value) => _stringBuilder.Append(value);

   public StringBuilder Append(ushort value) => _stringBuilder.Append(value);

   public StringBuilder Append(uint value) => _stringBuilder.Append(value);

   public StringBuilder Append(ulong value) => _stringBuilder.Append(value);

   public StringBuilder Append(object value) => _stringBuilder.Append(value);

   public StringBuilder Append(char[] value) => _stringBuilder.Append(value);

   public StringBuilder Insert(int index, string value) => _stringBuilder.Insert(index, value);

   public StringBuilder Insert(int index, bool value) => _stringBuilder.Insert(index, value);

   public StringBuilder Insert(int index, sbyte value) => _stringBuilder.Insert(index, value);

   public StringBuilder Insert(int index, byte value) => _stringBuilder.Insert(index, value);

   public StringBuilder Insert(int index, short value) => _stringBuilder.Insert(index, value);

   public StringBuilder Insert(int index, char value) => _stringBuilder.Insert(index, value);

   public StringBuilder Insert(int index, char[] value) => _stringBuilder.Insert(index, value);

   public StringBuilder Insert(int index, char[] value, int startIndex, int charCount) =>
      _stringBuilder.Insert(index, value, startIndex, charCount);

   public StringBuilder Insert(int index, int value) => _stringBuilder.Insert(index, value);

   public StringBuilder Insert(int index, long value) => _stringBuilder.Insert(index, value);

   public StringBuilder Insert(int index, float value) => _stringBuilder.Insert(index, value);

   public StringBuilder Insert(int index, double value) => _stringBuilder.Insert(index, value);

   public StringBuilder Insert(int index, decimal value) => _stringBuilder.Insert(index, value);

   public StringBuilder Insert(int index, ushort value) => _stringBuilder.Insert(index, value);

   public StringBuilder Insert(int index, uint value) => _stringBuilder.Insert(index, value);

   public StringBuilder Insert(int index, ulong value) => _stringBuilder.Insert(index, value);

   public StringBuilder Insert(int index, object value) => _stringBuilder.Insert(index, value);

   public StringBuilder AppendFormat(string format, object arg0) => _stringBuilder.AppendFormat(format, arg0);

   public StringBuilder AppendFormat(string format, object arg0, object arg1) =>
      _stringBuilder.AppendFormat(format, arg0, arg1);

   public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2) =>
      _stringBuilder.AppendFormat(format, arg0, arg1, arg2);

   public StringBuilder AppendFormat(string format, params object[] args) => _stringBuilder.AppendFormat(format, args);

   public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0) =>
      _stringBuilder.AppendFormat(provider, format, arg0);

   public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0,
      object arg1) =>
      _stringBuilder.AppendFormat(provider, format, arg0, arg1);

   public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0,
      object arg1, object arg2) =>
      _stringBuilder.AppendFormat(provider, format, arg0, arg1, arg2);

   public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args) =>
      _stringBuilder.AppendFormat(provider, format, args);

   public StringBuilder Replace(string oldValue, string newValue) => _stringBuilder.Replace(oldValue, newValue);

   public bool Equals(StringBuilder sb) => _stringBuilder.Equals(sb);

   public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count) =>
      _stringBuilder.Replace(oldValue, newValue, startIndex, count);

   public StringBuilder Replace(char oldChar, char newChar) => _stringBuilder.Replace(oldChar, newChar);

   public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count) =>
      _stringBuilder.Replace(oldChar, newChar, startIndex, count);
}