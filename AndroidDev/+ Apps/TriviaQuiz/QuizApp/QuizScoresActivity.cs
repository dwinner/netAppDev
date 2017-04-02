using System.Xml;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using DroidRes = Android.Resource;

#pragma warning disable 618

namespace QuizApp
{
   [Activity(Label = "@string/scores")]
   public class QuizScoresActivity : Activity
   {
      private const string AllTabTag = "allTab";
      private const string FriendsTabTag = "friendsTab";

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         SetContentView(Resource.Layout.Scores);

         // Установка вкладок
         var host = FindViewById<TabHost>(Resource.Id.TabHost1);
         host.Setup();

         // Вкладка "All Scores"
         host.AddTab(
            host.NewTabSpec(AllTabTag)
               .SetIndicator(
                  Resources.GetString(Resource.String.all_scores),
                  Resources.GetDrawable(DroidRes.Drawable.StarOn))
               .SetContent(Resource.Id.ScrollViewAllScores));

         // Вкладка "Friends scores tab"
         host.AddTab(
            host.NewTabSpec(FriendsTabTag)
               .SetIndicator(
                  Resources.GetString(Resource.String.friends_scores),
                  Resources.GetDrawable(DroidRes.Drawable.StarOn))
               .SetContent(Resource.Id.ScrollViewFriendScores));

         // Установка вкладки по умолчанию
         host.SetCurrentTabByTag(AllTabTag);

         // Извлекаем ссылки таблицы TableLayout
         var allScoresTable = FindViewById<TableLayout>(Resource.Id.TableLayout_AllScores);
         var friendsScoresTable = FindViewById<TableLayout>(Resource.Id.TableLayout_FriendScores);

         // Добавим к каждой таблице заголовок желтого цвета с именами столбцов
         InitHeaderRow(allScoresTable);
         InitHeaderRow(friendsScoresTable);

         var mockAllScores = Resources.GetXml(Resource.Xml.AllScores);
         var mockFriendScores = Resources.GetXml(Resource.Xml.FriendScores);

         try
         {
            ProcessScores(allScoresTable, mockAllScores);
            ProcessScores(friendsScoresTable, mockFriendScores);
         }
         catch (XmlException xmlEx)
         {
            Log.Error(GetType().Name, xmlEx.Message, xmlEx);
         }
      }

      /// <summary>
      ///    Заполнение информации таблицы из анализатора XML
      /// </summary>
      /// <param name="tableLayout">Таблица для заполнения</param>
      /// <param name="xmlReader">Стандартный парсер ресурсов XML, содержащий информацию о счетах</param>
      /// <exception cref="XmlException">Если возникли ошибки в ходе работы xml pull-парсера</exception>
      private void ProcessScores(TableLayout tableLayout, XmlReader xmlReader)
      {
         var foundScores = false;

         // Ищем записи счета в XML-файле
         using (xmlReader)
         {
            while (xmlReader.Read())
            {
               if (xmlReader.NodeType != XmlNodeType.Element)
                  continue;

               // Получаем имя тэга (или scores или score)
               var tagName = xmlReader.Name;
               if (tagName == "score")
               {
                  foundScores = true;
                  var scoreValue = xmlReader.MoveToAttribute("score") ? xmlReader.Value : string.Empty;
                  var scoreRank = xmlReader.MoveToAttribute("rank") ? xmlReader.Value : string.Empty;
                  var scoreUserName = xmlReader.MoveToAttribute("username") ? xmlReader.Value : string.Empty;
                  InsertScoreRow(tableLayout, scoreValue, scoreRank, scoreUserName);
               }
            }
         }

         // Обработка ситуации отсутствия счета
         if (!foundScores)
         {
            var newTableRow = new TableRow(this);
            var noResultsTextView = new TextView(this) {Text = Resources.GetString(Resource.String.no_scores)};
            newTableRow.AddView(noResultsTextView);
            tableLayout.AddView(newTableRow);
         }
      }

      /// <summary>
      ///    Добавление новой записи TableRow в таблицу TableLayout
      /// </summary>
      /// <param name="viewGroup">Таблица, к которой добавляется новая запись</param>
      /// <param name="scoreValue">Значение оценки</param>
      /// <param name="scoreRank">Рейтинг</param>
      /// <param name="scoreUserName">Имя (Ник) пользователя</param>
      private void InsertScoreRow(ViewGroup viewGroup, string scoreValue, string scoreRank, string scoreUserName)
      {
         var newTableRow = new TableRow(this);
         var textColor = Resources.GetColor(Resource.Color.title_color);
         var textSize = Resources.GetDimension(Resource.Dimension.help_text_size);
         AddTextRoRowWithValues(newTableRow, scoreUserName, textColor, textSize);
         AddTextRoRowWithValues(newTableRow, scoreValue, textColor, textSize);
         AddTextRoRowWithValues(newTableRow, scoreRank, textColor, textSize);
         viewGroup.AddView(newTableRow);
      }

      /// <summary>
      ///    Добавление заголовка TableRow к таблице TableLayout
      /// </summary>
      /// <param name="viewGroup">Таблица, к которой добавляется заголовок</param>
      private void InitHeaderRow(ViewGroup viewGroup)
      {
         var headeRow = new TableRow(this);
         var textColor = Resources.GetColor(Resource.Color.logo_color);
         var textSize = Resources.GetDimension(Resource.Dimension.help_text_size);
         AddTextRoRowWithValues(headeRow, Resources.GetString(Resource.String.username), textColor, textSize);
         AddTextRoRowWithValues(headeRow, Resources.GetString(Resource.String.score), textColor, textSize);
         AddTextRoRowWithValues(headeRow, Resources.GetString(Resource.String.rank), textColor, textSize);
         viewGroup.AddView(headeRow);
      }

      /// <summary>
      ///    Добавление к строке таблицы TableRow текста
      /// </summary>
      /// <param name="viewGroup">Строка таблицы, в которую нужно добавить текст</param>
      /// <param name="text">Текст для добавления</param>
      /// <param name="textColor">Идентификатор цвета текста</param>
      /// <param name="textSize">Размер текста</param>
      private void AddTextRoRowWithValues(ViewGroup viewGroup, string text, Color textColor, float textSize)
      {
         var newTextView = new TextView(this)
         {
            TextSize = textSize,
            Text = text
         };
         newTextView.SetTextColor(textColor);
         viewGroup.AddView(newTextView);
      }
   }
}