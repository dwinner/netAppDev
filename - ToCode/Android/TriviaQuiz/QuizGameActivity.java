public class QuizGameActivity extends QuizActivity
{
   /**
    * Вспомогательный метод для записи ответа и загрузки нового вопроса
    *
    * @param bAnswer true, если пользователь дал положительный ответ, false в противном случае
    */
   private void handleAnswerAndShowNextQuestion(boolean bAnswer)
   {
      int curScore = mGameSettings.getInt(GAME_PREFERENCES_SCORE, 0);
      int nextQuestionNumber = mGameSettings.getInt(GAME_PREFERENCES_CURRENT_QUESTION, 1) + 1;

      Editor editor = mGameSettings.edit();
      editor.putInt(GAME_PREFERENCES_CURRENT_QUESTION, nextQuestionNumber);

      // -- Учитываем ответ в случае Yes
      if (bAnswer)
      {
         editor.putInt(GAME_PREFERENCES_SCORE, curScore + 1);
      }
      editor.commit();

      if (!mQuestions.containsKey(nextQuestionNumber))
      {
         try
         {  // -- Загружаем следующую серию вопросов
            loadQuestionBatch(nextQuestionNumber);
         }
         catch (Exception e)
         {
            Log.e(DEBUG_TAG, "Loading updated question batch failed", e);
         }
      }

      if (mQuestions.containsKey(nextQuestionNumber))
      {
         // -- Обновляем текст вопроса
         TextSwitcher questionTextSwitcher =
           (TextSwitcher) findViewById(R.id.TextSwitcher_QuestionText);
         questionTextSwitcher.setText(getQuestionText(nextQuestionNumber));

         // -- Обновляем изображение вопроса
         ImageSwitcher questionImageSwitcher =
           (ImageSwitcher) findViewById(R.id.ImageSwitcher_QuestionImage);
         Drawable image = getQuestionImageDrawable(nextQuestionNumber);
         questionImageSwitcher.setImageDrawable(image);
      }
      else
      {
         handleNoQuestions();
      }

   }

   /**
    * Вспомогательный метод, отвечающий за ситуацию отсутствия вопросов
    */
   private void handleNoQuestions()
   {
      TextSwitcher questionTextSwitcher =
        (TextSwitcher) findViewById(R.id.TextSwitcher_QuestionText);
      questionTextSwitcher.setText(
        getResources().getText(R.string.no_questions));
      ImageSwitcher questionImageSwitcher =
        (ImageSwitcher) findViewById(R.id.ImageSwitcher_QuestionImage);
      questionImageSwitcher.setImageResource(R.drawable.noquestion);

      // -- Отключаем кнопку Yes
      Button yesButton = (Button) findViewById(R.id.Button_Yes);
      yesButton.setEnabled(false);

      // -- Отключаем кнопку No
      Button noButton = (Button) findViewById(R.id.Button_No);
      noButton.setEnabled(false);
   }

   /**
    * Возвращает строку с текстом вопроса по его номеру
    *
    * @param questionNumber Номер вопроса
    * @return Текст вопроса
    */
   private String getQuestionText(Integer questionNumber)
   {
      String text = null;
      Question curQuestion = mQuestions.get(questionNumber);
      if (curQuestion != null)
      {
         text = curQuestion.getmText();
      }
      return text;
   }

   /**
    * Возвращает строку URL для изображения по номеру вопроса
    *
    * @param questionNumber Номер вопроса
    * @return URL изображения
    */
   private String getQuestionImageUrl(Integer questionNumber)
   {
      String url = null;
      Question curQuestion = mQuestions.get(questionNumber);
      if (curQuestion != null)
      {
         url = curQuestion.getmImageUrl();
      }
      return url;
   }

   /**
    * Извлекает объект Drawable для вопроса
    *
    * @param questionNumber Номер вопроса
    * @return Объект Drawable
    */
   @SuppressWarnings("deprecation")
   private Drawable getQuestionImageDrawable(int questionNumber)
   {
      Drawable image;
      URL imageUrl;

      try
      {
         // -- Создание объекта Drawable через декодирование потока из URL
         imageUrl = new URL(getQuestionImageUrl(questionNumber));
         Bitmap bitmap = BitmapFactory.decodeStream(imageUrl.openStream());
         image = new BitmapDrawable(bitmap);
      }
      catch (Exception e)
      {
         Log.e(DEBUG_TAG, "Decoding Bitmap stream failed.");
         image = getResources().getDrawable(R.drawable.noquestion);
      }
      return image;
   }

   /**
    * Загрузка XML в поле класса
    *
    * @param startQuestionNumber TODO: Пока не используется
    * @throws XmlPullParserException
    * @throws IOException
    */
   private void loadQuestionBatch(int startQuestionNumber) throws XmlPullParserException, IOException
   {
      // Удаляем старую серию вопросов
      mQuestions.clear();

      // TODO: Устанавливаем связь с сервером и извлекаем новую серию вопросов
      XmlResourceParser questionBatch;

      questionBatch = startQuestionNumber < 16
        ? getResources().getXml(R.xml.samplequestions)
        : getResources().getXml(R.xml.samplequestions2);

      int eventType = -1;

      // Ищем записи для score из XML
      while (eventType != XmlResourceParser.END_DOCUMENT)
      {
         if (eventType == XmlResourceParser.START_TAG)
         {
            // Получаем имя тэга
            String strName = questionBatch.getName();

            if (strName.equals(XML_TAG_QUESTION))
            {
               String questionNumber = questionBatch.
                 getAttributeValue(null, XML_TAG_QUESTION_ATTRIBUTE_NUMBER);
               Integer questionNum = Integer.valueOf(questionNumber);
               String questionText =
                 questionBatch.getAttributeValue(null, XML_TAG_QUESTION_ATTRIBUTE_TEXT);
               String questionImageUrl =
                 questionBatch.getAttributeValue(null, XML_TAG_QUESTION_ATTRIBUTE_IMAGEURL);

               // Сохранение данных с карту
               mQuestions.put(
                 questionNum, new Question(questionNum, questionText, questionImageUrl));
            }
         }
         eventType = questionBatch.next();
      }
   }   
}
