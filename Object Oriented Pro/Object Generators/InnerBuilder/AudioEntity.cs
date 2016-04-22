using System;

namespace InnerBuilder
{
   /// <summary>
   /// Сущность для аудио-записи
   /// </summary>
   public class AudioEntity : IEquatable<AudioEntity>
   {
      #region Поля объекта

      /// <summary>
      /// Название композиции
      /// </summary>
      public string TrackName { get; set; }

      /// <summary>
      /// Время композиции в секундах
      /// </summary>
      public int Duration { get; set; }

      /// <summary>
      /// Качество звучания
      /// </summary>
      public int Bitrate { get; set; }

      /// <summary>
      /// Ссылка на композицию
      /// </summary>
      public string TrackUrl { get; set; }

      /// <summary>
      /// Группа или исполнитель
      /// </summary>
      public string Group { get; set; }

      /// <summary>
      /// Год записи
      /// </summary>
      public int Year { get; set; }

      /// <summary>
      /// Формат записи
      /// </summary>
      public string RecordFormat { get; set; }

      /// <summary>
      /// Название альбома
      /// </summary>
      public string Album { get; set; }

      /// <summary>
      /// Размер файла на диске
      /// </summary>
      public int Size { get; set; }

      /// <summary>
      /// Жанр
      /// </summary>
      public string Genre { get; set; }

      /// <summary>
      /// Оценка исполнения
      /// </summary>
      public int Rate { get; set; }

      private AudioEntity(Builder audioBuilder)
      {
         TrackName = audioBuilder.TrackName;
         Duration = audioBuilder.Duration;
         Bitrate = audioBuilder.Bitrate;
         Size = audioBuilder.Size;
         TrackUrl = audioBuilder.TrackUrl;
         Group = audioBuilder.Group;
         Year = audioBuilder.Year;
         RecordFormat = audioBuilder.RecordFormat;
         Album = audioBuilder.Album;
      }

      #endregion

      #region System.Object

      public bool Equals(AudioEntity other)
      {
         if (ReferenceEquals(null, other)) return false;
         if (ReferenceEquals(this, other)) return true;
         return string.Equals(TrackName, other.TrackName)
                && Duration == other.Duration
                && Bitrate == other.Bitrate
                && string.Equals(TrackUrl, other.TrackUrl)
                && string.Equals(Group, other.Group)
                && Year == other.Year
                && string.Equals(RecordFormat, other.RecordFormat)
                && string.Equals(Album, other.Album)
                && Size == other.Size
                && string.Equals(Genre, other.Genre)
                && Rate == other.Rate;
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((AudioEntity)obj);
      }

      public override int GetHashCode()
      {
         unchecked
         {
            int hashCode = (TrackName != null ? TrackName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ Duration;
            hashCode = (hashCode * 397) ^ Bitrate;
            hashCode = (hashCode * 397) ^ (TrackUrl != null ? TrackUrl.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Group != null ? Group.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ Year;
            hashCode = (hashCode * 397) ^ (RecordFormat != null ? RecordFormat.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Album != null ? Album.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ Size;
            hashCode = (hashCode * 397) ^ (Genre != null ? Genre.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ Rate;
            return hashCode;
         }
      }

      public static bool operator ==(AudioEntity left, AudioEntity right)
      {
         return Equals(left, right);
      }

      public static bool operator !=(AudioEntity left, AudioEntity right)
      {
         return !Equals(left, right);
      }

      public override string ToString()
      {
         return
            string.Format(
               "[AudioEntity TrackName={0}, Duration={1}, Bitrate={2}, Size={3}, TrackUrl={4}, Group={5}, Year={6}, RecordFormat={7}, Album={8}, Genre={9}, Rate={10}]",
               TrackName,
               Duration,
               Bitrate,
               Size,
               TrackUrl,
               Group,
               Year,
               RecordFormat,
               Album,
               Genre,
               Rate);
      }

      #endregion

      #region Строитель объектов внешнего класса

      public class Builder
      {
         #region Обязательные поля

         private readonly string _trackName;
         private readonly int _duration;
         private readonly int _bitrate;
         private readonly int _size;

         public string TrackName
         {
            get { return _trackName; }
         }

         public int Duration
         {
            get { return _duration; }
         }

         public int Bitrate
         {
            get { return _bitrate; }
         }

         public int Size
         {
            get { return _size; }
         }

         #endregion

         #region Необязательные поля

         private string _trackUrl = string.Empty;
         private string _group = string.Empty;
         private int _year = -1;
         private string _recordFormat = string.Empty;
         private string _album = string.Empty;
         private string _genre = string.Empty;
         private int _rate = -1;

         public string TrackUrl
         {
            get { return _trackUrl; }
            set { _trackUrl = value; }
         }

         public string Group
         {
            get { return _group; }
            set { _group = value; }
         }

         public int Year
         {
            get { return _year; }
            set { _year = value; }
         }

         public string RecordFormat
         {
            get { return _recordFormat; }
            set { _recordFormat = value; }
         }

         public string Album
         {
            get { return _album; }
            set { _album = value; }
         }

         public string Genre
         {
            get { return _genre; }
            set { _genre = value; }
         }

         public int Rate
         {
            get { return _rate; }
            set { _rate = value; }
         }

         #endregion

         /// <summary>
         /// Конструктор обязательных полей
         /// </summary>
         /// <param name="trackName">Имя трека</param>
         /// <param name="duration">Время в секундах</param>
         /// <param name="bitrate">Битрэйт</param>
         /// <param name="size">Размер</param>
         public Builder(string trackName, int duration, int bitrate, int size)
         {
            _trackName = trackName;
            _duration = duration;
            _bitrate = bitrate;
            _size = size;
         }

         #region Генераторы необязательных полей.

         public Builder BuildTrackUrl(string aTrackUrl)
         {
            _trackUrl = aTrackUrl;
            return this;
         }

         public Builder BuildGroup(string aGroup)
         {
            _group = aGroup;
            return this;
         }

         public Builder BuildYear(int aRecordYear)
         {
            _year = aRecordYear;
            return this;
         }

         public Builder BuildRecordFormat(string aRecordFormat)
         {
            _recordFormat = aRecordFormat;
            return this;
         }

         public Builder BuildAlbum(string anAlbum)
         {
            _album = anAlbum;
            return this;
         }

         public Builder BuildGenre(string aGenre)
         {
            _genre = aGenre;
            return this;
         }

         public Builder BuildRate(int aRate)
         {
            _rate = aRate;
            return this;
         }

         #endregion

         public AudioEntity Build()
         {
            return new AudioEntity(this);
         }
      }

      #endregion
   }
}
