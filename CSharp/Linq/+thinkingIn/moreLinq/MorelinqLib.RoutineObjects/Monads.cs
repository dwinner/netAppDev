﻿// https://raw.github.com/sergun/monads.net/master/Src/Monads/Maybe.Objects.cs

using System;
using System.Linq;

namespace MorelinqLib.RoutineObjects
{
   public static class Monads
   {
      /// <summary>
      ///    Allows to do some <paramref name="action" /> on <paramref name="source" /> if its not null
      /// </summary>
      /// <typeparam name="TSource">Type of source object</typeparam>
      /// <param name="source">Source object for operating</param>
      /// <param name="action">Action which should to do</param>
      /// <returns><paramref name="source" /> object</returns>
      public static TSource Do<TSource>(this TSource source, Action<TSource> action)
         where TSource : class
      {
         if (source != default(TSource))
         {
            action(source);
         }

         return source;
      }

      /// <summary>
      ///    Allows to do some conversion of <paramref name="source" /> if its not null
      /// </summary>
      /// <typeparam name="TSource">Type of source object</typeparam>
      /// <typeparam name="TResult">Type of result</typeparam>
      /// <param name="source">Source object for operating</param>
      /// <param name="action">Conversion action which should to do</param>
      /// <returns>Converted object which returns action</returns>
      public static TResult With<TSource, TResult>(this TSource source, Func<TSource, TResult> action)
         where TSource : class => source != default(TSource) ? action(source) : default(TResult);

      /// <summary>
      ///    Allows to do some conversion of <paramref name="source" /> if its not null or return
      ///    <paramref name="defaultValue" /> otherwise
      /// </summary>
      /// <typeparam name="TSource">Type of source object</typeparam>
      /// <typeparam name="TResult">Type of result</typeparam>
      /// <param name="source">Source object for operating</param>
      /// <param name="action">Conversion action which should to do</param>
      /// <param name="defaultValue">Value which will return if source is null</param>
      /// <returns>Converted object which returns action if source is not null or <paramref name="defaultValue" /> otherwise</returns>
      public static TResult Return<TSource, TResult>(this TSource source, Func<TSource, TResult> action,
         TResult defaultValue)
         where TSource : class => source != default(TSource) ? action(source) : defaultValue;

      /// <summary>
      ///    Retruns the <paramref name="source" /> if both <paramref name="condition" /> is true and <paramref name="source" />
      ///    is not null, or null otherwise
      /// </summary>
      /// <typeparam name="TSource">Type of source object</typeparam>
      /// <param name="source">Source object for operating</param>
      /// <param name="condition">Condition which should be checked</param>
      /// <returns><paramref name="source" /> if <paramref name="condition" /> is true, or null otherwise</returns>
      public static TSource If<TSource>(this TSource source, Func<TSource, bool> condition)
         where TSource : class => (source != default(TSource)) && condition(source) ? source : default(TSource);

      /// <summary>
      ///    Retruns the <paramref name="source" /> if both <paramref name="condition" /> is false and <paramref name="source" />
      ///    is not null, or null otherwise
      /// </summary>
      /// <typeparam name="TSource">Type of source object</typeparam>
      /// <param name="source">Source object for operating</param>
      /// <param name="condition">Condition which should be checked</param>
      /// <returns><paramref name="source" /> if <paramref name="condition" /> is true, or null otherwise</returns>
      public static TSource IfNot<TSource>(this TSource source, Func<TSource, bool> condition)
         where TSource : class => (source != default(TSource)) && (condition(source) == false) ? source : default(TSource);

      /// <summary>
      ///    Allows to construct <paramref name="source" /> if its is null
      /// </summary>
      /// <typeparam name="TInput">Type of source object</typeparam>
      /// <param name="source">Source object for operating</param>
      /// <param name="action">Constructor action</param>
      /// <returns><paramref name="source" /> if it is not null, or result of <paramref name="action" /> otherwise</returns>
      public static TInput Recover<TInput>(this TInput source, Func<TInput> action)
         where TInput : class => source ?? action();

      /// <summary>
      ///    Allows to cast <paramref name="source" /> to <typeparamref name="TResult" />
      /// </summary>
      /// <typeparam name="TResult">Type of result</typeparam>
      /// <param name="source">Source object for operating</param>
      /// <returns><paramref name="source" /> if it possible to cast to <typeparamref name="TResult" />, or null otherwise</returns>
      public static TResult OfType<TResult>(this object source) => source is TResult ? (TResult) source : default(TResult);

      /// <summary>
      ///    Allows to do <paramref name="action" /> and catch any exceptions
      /// </summary>
      /// <typeparam name="TSource">Type of source object</typeparam>
      /// <param name="source">Source object for operating</param>
      /// <param name="action">Action which should to do</param>
      /// <returns>
      ///    Tuple which contains <paramref name="source" /> and info about exception (if it throws)
      /// </returns>
      public static Tuple<TSource, Exception> TryDo<TSource>(this TSource source, Action<TSource> action)
         where TSource : class
      {
         if (source != default(TSource))
         {
            try
            {
               action(source);
            }
            catch (Exception ex)
            {
               return new Tuple<TSource, Exception>(source, ex);
            }
         }

         return new Tuple<TSource, Exception>(source, null);
      }

      /// <summary>
      ///    Allows to do <paramref name="action" /> and catch exceptions, which handled by
      ///    <param name="exceptionChecker" />
      /// </summary>
      /// <typeparam name="TSource">Type of source object</typeparam>
      /// <param name="source">Source object for operating</param>
      /// <param name="action">Action which should to do</param>
      /// <param name="exceptionChecker">Predicate to determine if exceptions should be handled</param>
      /// <returns>
      ///    Tuple which contains <paramref name="source" /> and info about exception (if it throws)
      /// </returns>
      public static Tuple<TSource, Exception> TryDo<TSource>(this TSource source, Action<TSource> action,
         Func<Exception, bool> exceptionChecker)
         where TSource : class
      {         
         if (source != default(TSource))
         {
            try
            {
               action(source);
            }
            catch (Exception ex)
            {
               if (exceptionChecker(ex))
               {
                  return new Tuple<TSource, Exception>(source, ex);
               }

               throw;
            }
         }

         return new Tuple<TSource, Exception>(source, null);
      }

      /// <summary>
      ///    Allows to do <paramref name="action" /> and catch
      ///    <param name="expectedException" />
      ///    exceptions
      /// </summary>
      /// <typeparam name="TSource">Type of source object</typeparam>
      /// <param name="source">Source object for operating</param>
      /// <param name="action">Action which should to do</param>
      /// <param name="expectedException">Array of exception types, which should be handled</param>
      /// <returns>
      ///    Tuple which contains <paramref name="source" /> and info about exception (if it throws)
      /// </returns>
      public static Tuple<TSource, Exception> TryDo<TSource>(this TSource source, Action<TSource> action,
         params Type[] expectedException)
         where TSource : class
      {
         if (source != default(TSource))
         {
            try
            {
               action(source);
            }
            catch (Exception ex)
            {
               if (expectedException.Any(exp => exp.IsInstanceOfType(ex)))
               {
                  return new Tuple<TSource, Exception>(source, ex);
               }

               throw;
            }
         }

         return new Tuple<TSource, Exception>(source, null);
      }


      /// <summary>
      ///    Allows to do some conversion of <paramref name="source" /> if its not null and catch any exceptions
      /// </summary>
      /// <typeparam name="TSource">Type of source object</typeparam>
      /// <typeparam name="TResult">Type of result</typeparam>
      /// <param name="source">Source object for operating</param>
      /// <param name="action">Action which should to do</param>
      /// <returns>Tuple which contains Converted object and info about exception (if it throws)</returns>
      public static Tuple<TResult, Exception> TryWith<TSource, TResult>(this TSource source,
         Func<TSource, TResult> action)
         where TSource : class
      {
         if (source != default(TSource))
         {
            var result = default(TResult);
            try
            {
               result = action(source);
               return new Tuple<TResult, Exception>(result, null);
            }
            catch (Exception ex)
            {
               return new Tuple<TResult, Exception>(result, ex);
            }
         }

         return new Tuple<TResult, Exception>(default(TResult), null);
      }

      /// <summary>
      ///    Allows to do some conversion of <paramref name="source" /> if its not null and catch exceptions, which handled by
      ///    <param name="exceptionChecker" />
      /// </summary>
      /// <typeparam name="TSource">Type of source object</typeparam>
      /// <typeparam name="TResult">Type of result</typeparam>
      /// <param name="source">Source object for operating</param>
      /// <param name="action">Action which should to do</param>
      /// <param name="exceptionChecker">Predicate to determine if exceptions should be handled</param>
      /// <returns>Tuple which contains Converted object and info about exception (if it throws)</returns>
      public static Tuple<TResult, Exception> TryWith<TSource, TResult>(this TSource source,
         Func<TSource, TResult> action, Func<Exception, bool> exceptionChecker)
         where TSource : class
      {
         if (source != default(TSource))
         {
            var result = default(TResult);
            try
            {
               result = action(source);
               return new Tuple<TResult, Exception>(result, null);
            }
            catch (Exception ex)
            {
               if (exceptionChecker(ex))
               {
                  return new Tuple<TResult, Exception>(result, ex);
               }
               throw;
            }
         }

         return new Tuple<TResult, Exception>(default(TResult), null);
      }

      /// <summary>
      ///    Allows to do some conversion of <paramref name="source" /> if its not null and catch
      ///    <param name="expectedException" />
      ///    exceptions
      /// </summary>
      /// <typeparam name="TSource">Type of source object</typeparam>
      /// <typeparam name="TResult">Type of result</typeparam>
      /// <param name="source">Source object for operating</param>
      /// <param name="action">Action which should to do</param>
      /// <param name="expectedException">Array of exception types, which should be handled</param>
      /// <returns>Tuple which contains Converted object and info about exception (if it throws)</returns>
      public static Tuple<TResult, Exception> TryWith<TSource, TResult>(this TSource source,
         Func<TSource, TResult> action, params Type[] expectedException)
         where TSource : class
      {
         if (source != default(TSource))
         {
            var result = default(TResult);
            try
            {
               result = action(source);
               return new Tuple<TResult, Exception>(result, null);
            }
            catch (Exception ex)
            {
               if (expectedException.Any(exp => exp.IsInstanceOfType(ex)))
               {
                  return new Tuple<TResult, Exception>(result, ex);
               }

               throw;
            }
         }

         return new Tuple<TResult, Exception>(default(TResult), null);
      }

      /// <summary>
      ///    Handle exception with no actions
      /// </summary>
      /// <typeparam name="TSource">Type of source object</typeparam>
      /// <param name="source">Source object for operating</param>
      /// <returns><paramref name="source" /> object</returns>
      public static TSource Catch<TSource>(this Tuple<TSource, Exception> source) => source.Item1;

      /// <summary>
      ///    Handle exception with
      ///    <param name="handler" />
      ///    action
      /// </summary>
      /// <typeparam name="TSource">Type of source object</typeparam>
      /// <param name="source">Source object for operating</param>
      /// <param name="handler">Обработчик</param>
      /// <returns><paramref name="source" /> object</returns>
      public static TSource Catch<TSource>(this Tuple<TSource, Exception> source, Action<Exception> handler)
      {         
         if (source.Item2 != null)
         {
            handler(source.Item2);
         }

         return source.Item1;
      }

      /// <summary>
      ///    Allows to check whether <paramref name="source" /> is null
      /// </summary>
      /// <typeparam name="TSource">Type of source object</typeparam>
      /// <param name="source">Source object for checking</param>
      /// <returns>true if <paramref name="source" /> is null, or false otherwise</returns>
      public static bool IsNull<TSource>(this TSource source)
         where TSource : class => source == default(TSource);

      /// <summary>
      ///    Allows to check whether <paramref name="source" /> is not null
      /// </summary>
      /// <typeparam name="TSource">Type of source object</typeparam>
      /// <param name="source">Source object for checking</param>
      /// <returns>true if <paramref name="source" /> is not null, or false otherwise</returns>
      public static bool IsNotNull<TSource>(this TSource source)
         where TSource : class => source != default(TSource);
   }
}