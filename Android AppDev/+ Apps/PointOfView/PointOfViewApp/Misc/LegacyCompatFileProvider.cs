using System;
using Android.Database;
using Android.Support.V4.Content;
using static Android.Provider.MediaStore.MediaColumns;
using Uri = Android.Net.Uri;

namespace PointOfViewApp.Misc
{
   public class LegacyCompatFileProvider : FileProvider
   {
      public override ICursor Query(Uri uri, string[] projection, string selection, string[] selectionArgs,
         string sortOrder) =>
         new LegacyCompatCursorWrapper(base.Query(uri, projection, selection, selectionArgs, sortOrder));

      private sealed class LegacyCompatCursorWrapper : CursorWrapper
      {
         private readonly int _fakeDataColumn;
         private readonly int _fakeMimeTypeColumn;
         private readonly string _mimeType;
         private readonly Uri _uriForDataColumn;

         public LegacyCompatCursorWrapper(ICursor cursor, string mimeType = null)
            : this(cursor, mimeType, null)
         {
         }

         private LegacyCompatCursorWrapper(ICursor cursor, string mimeType, Uri uriForDataColumn)
            : base(cursor)
         {
            _uriForDataColumn = uriForDataColumn;
            _fakeDataColumn = cursor.GetColumnIndex(Data) >= 0 ? -1 : cursor.ColumnCount;
            _fakeMimeTypeColumn = cursor.GetColumnIndex(MimeType) >= 0 ? -1 :
               _fakeDataColumn == -1 ? cursor.ColumnCount : _fakeDataColumn + 1;
            _mimeType = mimeType;
         }

         public override int ColumnCount
         {
            get
            {
               var count = base.ColumnCount;

               if (!CursorHasDataColumn())
                  count += 1;

               if (!CursorHasMimeTypeColumn())
                  count += 1;

               return count;
            }
         }

         public override int GetColumnIndex(string columnName)
         {
            if (!CursorHasDataColumn() && Data.Equals(columnName, StringComparison.CurrentCultureIgnoreCase))
               return _fakeDataColumn;

            if (!CursorHasMimeTypeColumn() && MimeType.Equals(columnName, StringComparison.CurrentCultureIgnoreCase))
               return _fakeMimeTypeColumn;

            return base.GetColumnIndex(columnName);
         }

         public override string GetColumnName(int columnIndex) => columnIndex == _fakeDataColumn ? Data :
            columnIndex == _fakeMimeTypeColumn ? MimeType : base.GetColumnName(columnIndex);

         public override string[] GetColumnNames()
         {
            if (CursorHasDataColumn() && CursorHasMimeTypeColumn())
               return base.GetColumnNames();

            var orig = base.GetColumnNames();
            var result = Array.Empty<string>();
            Array.Copy(orig, result, ColumnCount);

            if (!CursorHasDataColumn())
               result[_fakeDataColumn] = Data;

            if (!CursorHasMimeTypeColumn())
               result[_fakeMimeTypeColumn] = MimeType;

            return result;
         }

         public override string GetString(int columnIndex) =>
            !CursorHasDataColumn() && columnIndex == _fakeDataColumn
               ? _uriForDataColumn?.ToString()
               : !CursorHasMimeTypeColumn() && columnIndex == _fakeMimeTypeColumn
                  ? _mimeType
                  : base.GetString(columnIndex);


         public override FieldType GetType(int columnIndex) =>
            !CursorHasDataColumn() && columnIndex == _fakeDataColumn
               ? FieldType.String
               : !CursorHasMimeTypeColumn() && columnIndex == _fakeMimeTypeColumn
                  ? FieldType.String
                  : base.GetType(columnIndex);

         private bool CursorHasDataColumn() => _fakeDataColumn == -1;

         private bool CursorHasMimeTypeColumn() => _fakeMimeTypeColumn == -1;
      }
   }
}