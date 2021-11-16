using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CsCsLang
{
   public sealed class Variable
   {
      public enum VarType
      {
         None,
         Number,
         String,
         Array,
         Break,
         Continue
      }

      public static readonly Variable EmptyInstance = new Variable();
      private Dictionary<string, int> _mDictionary = new Dictionary<string, int>();
      private string _mString;
      private List<Variable> _mTuple;

      private double _mValue;

      public Variable()
      {
         Reset();
      }

      public Variable(VarType type)
      {
         Type = type;
         if (Type == VarType.Array) SetAsArray();
      }

      public Variable(double d) => Value = d;

      public Variable(bool d) => Value = d ? 1.0 : 0.0;

      public Variable(string s) => String = s;

      public Variable(List<Variable> a) => Tuple = a;

      public double Value
      {
         get => _mValue;
         set
         {
            _mValue = value;
            Type = VarType.Number;
         }
      }

      public string String
      {
         get => _mString;
         set
         {
            _mString = value;
            Type = VarType.String;
         }
      }

      public List<Variable> Tuple
      {
         get => _mTuple;
         set
         {
            _mTuple = value;
            Type = VarType.Array;
         }
      }

      public string Action { get; set; }
      public VarType Type { get; private set; }
      public bool IsReturn { get; set; }

      public Variable Clone()
      {
         var newVar = new Variable();
         newVar.Copy(this);
         return newVar;
      }

      public Variable DeepClone()
      {
         var newVar = new Variable();
         newVar.Copy(this);

         if (_mTuple != null)
         {
            var newTuple = new List<Variable>();
            foreach (var item in _mTuple) newTuple.Add(item.DeepClone());

            newVar.Tuple = newTuple;
         }
         return newVar;
      }

      private void Copy(Variable other)
      {
         Reset();
         Action = other.Action;
         Type = other.Type;
         IsReturn = other.IsReturn;
         _mDictionary = other._mDictionary;

         switch (other.Type)
         {
            case VarType.Number:
               Value = other.Value;
               break;
            case VarType.String:
               String = other.String;
               break;
            case VarType.Array:
               Tuple = other.Tuple;
               break;
         }
      }

      public static Variable NewEmpty() => new Variable();

      public void Reset()
      {
         _mValue = double.NaN;
         _mString = null;
         _mTuple = null;
         Action = null;
         IsReturn = false;
         Type = VarType.None;
         _mDictionary.Clear();
      }

      public bool Equals(Variable other) =>
         Type == other.Type && double.IsNaN(Value) == double.IsNaN(other.Value) &&
         (double.IsNaN(Value) || !(Math.Abs(Value - other.Value) > double.Epsilon)) &&
         string.Equals(String, other.String, StringComparison.Ordinal) &&
         string.Equals(Action, other.Action, StringComparison.Ordinal) && Tuple == null == (other.Tuple == null) &&
         (Tuple == null || Tuple.Equals(other.Tuple));

      public void AddVariableToHash(string hash, Variable newVar)
      {
         Variable listVar;
         if (_mDictionary.TryGetValue(hash, out var retValue))
            listVar = _mTuple[retValue];
         else
         {
            listVar = new Variable(VarType.Array);
            _mTuple.Add(listVar);
            _mDictionary[hash] = _mTuple.Count - 1;
         }

         listVar.AddVariable(newVar);
      }

      public List<Variable> GetAllKeys()
      {
         var keys = _mDictionary.Keys;
         var results = keys.Select(key => new Variable(key)).ToList();
         if (results.Count == 0 && _mTuple != null)
            results = _mTuple;

         return results;
      }

      public int SetHashVariable(string hash, Variable var)
      {
         if (_mDictionary.TryGetValue(hash, out var retValue))
         {
            // already exists, change the value:
            _mTuple[retValue] = var;
            return retValue;
         }

         _mTuple.Add(var);
         _mDictionary[hash] = _mTuple.Count - 1;

         return _mTuple.Count - 1;
      }

      public int GetArrayIndex(Variable indexVar)
      {
         if (Type != VarType.Array)
            return -1;

         if (indexVar.Type == VarType.Number)
         {
            Utils.CheckNonNegativeInt(indexVar);
            return (int) indexVar.Value;
         }

         var hash = indexVar.AsString();
         if (_mDictionary.TryGetValue(hash, out var ptr) && ptr < _mTuple.Count)
            return ptr;

         if (!string.IsNullOrWhiteSpace(indexVar.String) && int.TryParse(indexVar.String, out var result))
            return result;

         return -1;
      }

      public void AddVariable(Variable v)
      {
         SetAsArray();
         _mTuple.Add(v);
      }

      private bool Exists(string hash) => _mDictionary.ContainsKey(hash);

      public bool Exists(Variable indexVar, bool notEmpty = false)
      {
         if (Type != VarType.Array) return false;
         if (indexVar.Type == VarType.Number)
         {
            return !(indexVar.Value < 0) && !(indexVar.Value >= _mTuple.Count)
                   && !(Math.Abs(indexVar.Value - Math.Floor(indexVar.Value)) > double.Epsilon)
                   && (!notEmpty || _mTuple[(int) indexVar.Value].Type != VarType.None);
         }

         var hash = indexVar.AsString();
         return Exists(hash);
      }

      public int AsInt()
      {
         var result = 0;
         if (Type == VarType.Number || Math.Abs(Value) > double.Epsilon)
            return (int) _mValue;

         if (Type == VarType.String)
            int.TryParse(_mString, out result);

         return result;
      }

      public double AsDouble()
      {
         var result = 0.0;
         switch (Type)
         {
            case VarType.Number:
               return _mValue;
            case VarType.String:
               double.TryParse(_mString, out result);
               break;
         }

         return result;
      }

      public string AsString(bool isList = true, bool sameLine = true)
      {
         if (Type == VarType.Number)
            return Value.ToString(CultureInfo.InvariantCulture);

         if (Type == VarType.String)
            return _mString ?? string.Empty;

         if (Type == VarType.None || _mTuple == null)
            return string.Empty;

         var sb = new StringBuilder();
         if (isList)
            sb.AppendFormat("{0}{1}", Constants.StartGroup, sameLine ? string.Empty : Environment.NewLine);

         for (var i = 0; i < _mTuple.Count; i++)
         {
            var arg = _mTuple[i];
            sb.Append(arg.AsString(isList, sameLine));
            if (i != _mTuple.Count - 1)
               sb.Append(sameLine ? " " : Environment.NewLine);
         }

         if (isList)
            sb.AppendFormat("{0}{1}", Constants.EndGroup, (sameLine ? " " : Environment.NewLine));

         return sb.ToString();
      }

      public void SetAsArray()
      {
         Type = VarType.Array;
         if (_mTuple == null)
            _mTuple = new List<Variable>();
      }

      public int TotalElements() => Type == VarType.Array ? _mTuple.Count : 1;

      public Variable GetValue(int index)
      {
         if (index >= TotalElements())
            throw new ArgumentException($"There are only [{TotalElements()}] but {index} requested.");

         return Type == VarType.Array ? _mTuple[index] : this;
      }
   }
}