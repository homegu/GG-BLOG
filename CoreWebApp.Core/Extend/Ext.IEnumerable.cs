using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CoreWebApp.Core
{
    public static partial class Ext
    {

        //public static TSource Search<TSource>(IEnumerable<TSource> @this, Func<TSource, bool> predicate, bool includeNull = false)
        //{
        //    if (!includeNull && @this)
        //    {

        //    }
        //}

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex">获取页码（从0开始）</param>
        /// <returns></returns>
        public static IEnumerable<T> TakeMany<T>(this IEnumerable<T> data, int pageSize, int pageIndex)
        {
            return data.Skip(pageIndex * pageSize).Take(pageSize);
        }

        public static List<T> Shrink<T>(this IEnumerable<T> @this)
        {
            return @this.Map(x => x, true);
        }

        public static List<(TOutput Data, int Index)> MapWithIndex<TInput, TOutput>(this IEnumerable<TInput> @this, Func<TInput, TOutput> func, bool shrink = false)
        {
            var list = new List<(TOutput, int)>();
            var index = 0;
            @this.Repeat(item =>
            {
                var result = func(item);
                if (shrink && result == null)
                {
                    return;
                }

                list.Add((result, index));
                ++index;
            });

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="this"></param>
        /// <param name="func"></param>
        /// <param name="shrink">如果是null对象，是否忽悠掉</param>
        /// <returns></returns>
        public static List<TOutput> Map<TInput, TOutput>(this IEnumerable<TInput> @this, Func<TInput, TOutput> func, bool shrink = false)
        {
            var list = new List<TOutput>();
            @this.Repeat(item =>
            {
                var result = func(item);
                if (shrink && result == null)
                {
                    return;
                }

                list.Add(result);
            });

            return list;
        }

        public static string JoinWith<T>(this IEnumerable<T> @this, string separator, Func<T, string> func)
        {
            return @this.Aggregate("", (seed, next) =>
            {
                if (seed.Length > 0)
                {
                    seed += separator;
                }

                return seed + func(next);
            });
        }

        public static bool Exists<T>(this IEnumerable<T> @this, Func<T, bool> predicate)
        {
            if (@this.IsNullOrEmpty())
            {
                return false;
            }

            return @this.Count(predicate) > 0;
        }

        /// <summary>
        /// 返回可枚举对象是否为Null，或空内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> @this)
        {
            return @this == null || @this.Count() == 0;
        }

        public static void Repeat<T>(this IEnumerable<T> @this, Action<T> func)
        {
            if (@this.IsNullOrEmpty())
            {
                return;
            }

            foreach (var item in @this)
            {
                func(item);
            }
        }

        public static void RepeatWithIndex<T>(this IEnumerable<T> @this, Action<T, int> func)
        {
            if (@this.IsNullOrEmpty())
            {
                return;
            }

            foreach (var item in @this.Select((value, index) => new { index, value }))
            {
                func(item.value, item.index);
            }
        }

        public static void Repeat(this DataRowCollection rowCollection, Action<DataRow> func)
        {
            if (rowCollection == null || rowCollection.Count == 0)
            {
                return;
            }

            foreach (DataRow row in rowCollection)
            {
                func(row);
            }
        }

        public static void RepeatWithIndex(this DataRowCollection rowCollection, Action<DataRow, int> func)
        {
            if (rowCollection == null || rowCollection.Count == 0)
            {
                return;
            }

            var index = 0;
            foreach (DataRow row in rowCollection)
            {
                func(row, index);
                ++index;
            }
        }

        //public static List<(T Data, int Index)> DataWithIndex<T>(this IEnumerable<T> @this)
        //{
        //    if (@this == null || @this.Count() == 0)
        //    {
        //        return new List<(T, int)>();
        //    }

        //    var items = new List<(T, int)>();
        //    @this.Map
        //    @this.RepeatWithIndex((item, index) => items.Add((item, index)));
        //    return items;
        //}

        public static List<(DataRow Row, int Index)> RowsWithIndex(this DataRowCollection rowCollection)
        {
            if (rowCollection == null || rowCollection.Count == 0)
            {
                return new List<(DataRow, int)>();
            }

            var items = new List<(DataRow, int)>();

            for (int i = 0; i < rowCollection.Count; i++)
            {
                items.Add((rowCollection[i], i));
            }

            return items;
        }

        public static List<T> UnionWith<T>(this IEnumerable<T> @this, IEnumerable<T> b)
        {
            var all = new List<T>();

            if (@this != null)
            {
                all.AddRange(@this);
            }

            if (b != null)
            {
                all.AddRange(b);
            }

            return all;
        }
    }
}
