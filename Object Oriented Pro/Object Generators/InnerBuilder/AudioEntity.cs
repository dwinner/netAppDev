using System;

namespace InnerBuilder
{
   /// <summary>
   ///    Сущность для аудио-записи
   /// </summary>
   public class AudioEntity : IEquatable<AudioEntity>
   {
      #region Строитель объектов внешнего класса

      public sealed class Builder
      {
         /// <summary>
         ///    Конструктор обязательных полей
         /// </summary>
         /// <param name="trackName">Имя трека</param>
         /// <param name="duration">Время в секундах</param>
         /// <param name="bitrate">Битрэйт</param>
         /// <param name="size">Размер</param>
         public Builder(
            string trackName, int duration, int bitrate, int size)
         {
            TrackName = trackName;
            Duration = duration;
            Bitrate = bitrate;
            Size = size;
         }

         public AudioEntity Build() => new AudioEntity(this);

         #region Обязательные поля

         public string TrackName { get; }

         public int Duration { get; }

         public int Bitrate { get; }

         public int Size { get; }

         #endregion

         #region Необязательные поля

         public string TrackUrl { get; private set; }
            = string.Empty;

         public string Group { get; private set; }
            = string.Empty;

         public int Year { get; private set; }
            = -1;

         public string RecordFormat { get; private set; }
            = string.Empty;

         public string Album { get; private set; }
            = string.Empty;

         public string Genre { get; private set; }
            = string.Empty;

         public int Rate { get; private set; }
            = -1;

         #endregion

         #region Генераторы необязательных полей.

         public Builder BuildTrackUrl(string aTrackUrl)
         {
            TrackUrl = aTrackUrl;
            return this;
         }

         public Builder BuildGroup(string aGroup)
         {
            Group = aGroup;
            return this;
         }

         public Builder BuildYear(int aRecordYear)
         {
            Year = aRecordYear;
            return this;
         }

         public Builder BuildRecordFormat(string aRecordFormat)
         {
            RecordFormat = aRecordFormat;
            return this;
         }

         public Builder BuildAlbum(string anAlbum)
         {
            Album = anAlbum;
            return this;
         }

         public Builder BuildGenre(string aGenre)
         {
            Genre = aGenre;
            return this;
         }

         public Builder BuildRate(int aRate)
         {
            Rate = aRate;
            return this;
         }

         #endregion
      }

      #endregion

      #region Поля объекта

      /// <summary>
      ///    Название композиции
      /// </summary>
      public string TrackName { get; set; }

      /// <summary>
      ///    Время композиции в секундах
      /// </summary>
      public int Duration { get; set; }

      /// <summary>
      ///    Качество звучания
      /// </summary>
      public int Bitrate { get; set; }

      /// <summary>
      ///    Ссылка на композицию
      /// </summary>
      public string TrackUrl { get; set; }

      /// <summary>
      ///    Группа или исполнитель
      /// </summary>
      public string Group { get; set; }

      /// <summary>
      ///    Год записи
      /// </summary>
      public int Year { get; set; }

      /// <summary>
      ///    Формат записи
      /// </summary>
      public string RecordFormat { get; set; }

      /// <summary>
      ///    Название альбома
      /// </summary>
      public string Album { get; set; }

      /// <summary>
      ///    Размер файла на диске
      /// </summary>
      public int Size { get; }

      /// <summary>
      ///    Жанр
      /// </summary>
      public string Genre { get; }

      /// <summary>
      ///    Оценка исполнения
      /// </summary>
      public int Rate { get; }

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
         Genre = audioBuilder.Genre;
         Rate = audioBuilder.Rate;
      }

      #endregion

      #region System.Object

      public bool Equals(AudioEntity other)
         => !ReferenceEquals(null, other) &&
            (ReferenceEquals(this, other) || string.Equals(TrackName, other.TrackName)
             && Duration == other.Duration
             && Bitrate == other.Bitrate
             && string.Equals(TrackUrl, other.TrackUrl)
             && string.Equals(Group, other.Group)
             && Year == other.Year
             && string.Equals(RecordFormat, other.RecordFormat)
             && string.Equals(Album, other.Album)
             && Size == other.Size
             && string.Equals(Genre, other.Genre)
             && Rate == other.Rate);

      public override bool Equals(object obj)
         => !ReferenceEquals(null, obj) &&
            (ReferenceEquals(this, obj) ||
             obj.GetType() == GetType() && Equals((AudioEntity) obj));

      public override int GetHashCode()
      {
         unchecked
         {
            var hashCode = TrackName != null ? TrackName.GetHashCode() : 0;
            hashCode = (hashCode*397) ^ Duration;
            hashCode = (hashCode*397) ^ Bitrate;
            hashCode = (hashCode*397) ^ (TrackUrl != null ? TrackUrl.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (Group != null ? Group.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ Year;
            hashCode = (hashCode*397) ^ (RecordFormat != null ? RecordFormat.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (Album != null ? Album.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ Size;
            hashCode = (hashCode*397) ^ (Genre != null ? Genre.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ Rate;
            return hashCode;
         }
      }

      public static bool operator ==(AudioEntity left, AudioEntity right)
         => Equals(left, right);

      public static bool operator !=(AudioEntity left, AudioEntity right)
         => !Equals(left, right);

      public override string ToString()
         =>
            $"[AudioEntity TrackName={TrackName}, Duration={Duration}, Bitrate={Bitrate}, Size={Size}, TrackUrl={TrackUrl}, Group={Group}, Year={Year}, RecordFormat={RecordFormat}, Album={Album}, Genre={Genre}, Rate={Rate}]";

      #endregion
   }
}