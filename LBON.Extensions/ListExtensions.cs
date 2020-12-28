using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;

namespace LBON.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IList{T}"/>.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Inserts the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="index">The index.</param>
        /// <param name="items">The items.</param>
        [Description("插入多个")]
        public static void InsertRange<T>(this IList<T> source, int index, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                source.Insert(index++, item);
            }
        }

        /// <summary>
        /// Finds the index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <returns></returns>
        [Description("查找索引")]
        public static int FindIndex<T>(this IList<T> source, Predicate<T> selector)
        {
            for (var i = 0; i < source.Count; ++i)
            {
                if (selector(source[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Adds the first.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="item">The item.</param>
        [Description("在开头插入")]
        public static void AddFirst<T>(this IList<T> source, T item)
        {
            source.Insert(0, item);
        }

        /// <summary>
        /// Adds the last.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="item">The item.</param>
        [Description("在结尾插入")]
        public static void AddLast<T>(this IList<T> source, T item)
        {
            source.Insert(source.Count, item);
        }

        /// <summary>
        /// Inserts the after.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="existingItem">The existing item.</param>
        /// <param name="item">The item.</param>
        [Description("在...之后插入")]
        public static void InsertAfter<T>(this IList<T> source, T existingItem, T item)
        {
            var index = source.IndexOf(existingItem);
            if (index < 0)
            {
                source.AddFirst(item);
                return;
            }

            source.Insert(index + 1, item);
        }

        /// <summary>
        /// Inserts the after.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="item">The item.</param>
        public static void InsertAfter<T>(this IList<T> source, Predicate<T> selector, T item)
        {
            var index = source.FindIndex(selector);
            if (index < 0)
            {
                source.AddFirst(item);
                return;
            }

            source.Insert(index + 1, item);
        }

        /// <summary>
        /// Inserts the before.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="existingItem">The existing item.</param>
        /// <param name="item">The item.</param>
        [Description("在...之前插入")]
        public static void InsertBefore<T>(this IList<T> source, T existingItem, T item)
        {
            var index = source.IndexOf(existingItem);
            if (index < 0)
            {
                source.AddLast(item);
                return;
            }

            source.Insert(index, item);
        }

        /// <summary>
        /// Inserts the before.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="item">The item.</param>
        public static void InsertBefore<T>(this IList<T> source, Predicate<T> selector, T item)
        {
            var index = source.FindIndex(selector);
            if (index < 0)
            {
                source.AddLast(item);
                return;
            }

            source.Insert(index, item);
        }

        /// <summary>
        /// Replaces the while.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="item">The item.</param>
        [Description("替换")]
        public static void ReplaceWhile<T>(this IList<T> source, Predicate<T> selector, T item)
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (selector(source[i]))
                {
                    source[i] = item;
                }
            }
        }

        /// <summary>
        /// Replaces the while.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="itemFactory">The item factory.</param>
        public static void ReplaceWhile<T>(this IList<T> source, Predicate<T> selector, Func<T, T> itemFactory)
        {
            for (int i = 0; i < source.Count; i++)
            {
                var item = source[i];
                if (selector(item))
                {
                    source[i] = itemFactory(item);
                }
            }
        }

        /// <summary>
        /// Replaces the one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="item">The item.</param>
        [Description("替换")]
        public static void ReplaceOne<T>(this IList<T> source, Predicate<T> selector, T item)
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (selector(source[i]))
                {
                    source[i] = item;
                    return;
                }
            }
        }

        /// <summary>
        /// Replaces the one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="itemFactory">The item factory.</param>
        public static void ReplaceOne<T>(this IList<T> source, Predicate<T> selector, Func<T, T> itemFactory)
        {
            for (int i = 0; i < source.Count; i++)
            {
                var item = source[i];
                if (selector(item))
                {
                    source[i] = itemFactory(item);
                    return;
                }
            }
        }

        /// <summary>
        /// Replaces the one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="item">The item.</param>
        /// <param name="replaceWith">The replace with.</param>
        public static void ReplaceOne<T>(this IList<T> source, T item, T replaceWith)
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (Comparer<T>.Default.Compare(source[i], item) == 0)
                {
                    source[i] = replaceWith;
                    return;
                }
            }
        }

        /// <summary>
        /// Moves the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="targetIndex">Index of the target.</param>
        /// <exception cref="IndexOutOfRangeException">targetIndex should be between 0 and " + (source.Count - 1)</exception>
        [Description("移动项")]
        public static void MoveItem<T>(this List<T> source, Predicate<T> selector, int targetIndex)
        {
            if (!targetIndex.IsBetween(0, source.Count - 1))
            {
                throw new IndexOutOfRangeException("targetIndex should be between 0 and " + (source.Count - 1));
            }

            var currentIndex = source.FindIndex(0, selector);
            if (currentIndex == targetIndex)
            {
                return;
            }

            var item = source[currentIndex];
            source.RemoveAt(currentIndex);
            source.Insert(targetIndex, item);
        }

        /// <summary>
        /// Gets the or add.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="factory">The factory.</param>
        /// <returns></returns>
        [Description("获取并添加")]
        public static T GetOrAdd<T>([NotNull] this IList<T> source, Func<T, bool> selector, Func<T> factory)
        {
            var item = source.FirstOrDefault(selector);

            if (item == null)
            {
                item = factory();
                source.Add(item);
            }

            return item;
        }

        /// <summary>
        /// Sort a list by a topological sorting, which consider their dependencies.
        /// </summary>
        /// <typeparam name="T">The type of the members of values.</typeparam>
        /// <param name="source">A list of objects to sort</param>
        /// <param name="getDependencies">Function to resolve the dependencies</param>
        /// <param name="comparer">Equality comparer for dependencies </param>
        /// <returns>
        /// Returns a new list ordered by dependencies.
        /// If A depends on B, then B will come before than A in the resulting list.
        /// </returns>
        [Description("通过topological排序对列表进行排序，topological排序考虑了它们之间的依赖关系")]
        public static List<T> SortByDependencies<T>(
            this IEnumerable<T> source,
            Func<T, IEnumerable<T>> getDependencies,
            IEqualityComparer<T> comparer = null)
        {
            var sorted = new List<T>();
            var visited = new Dictionary<T, bool>(comparer);

            foreach (var item in source)
            {
                SortByDependenciesVisit(item, getDependencies, sorted, visited);
            }

            return sorted;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T">The type of the members of values.</typeparam>
        /// <param name="item">Item to resolve</param>
        /// <param name="getDependencies">Function to resolve the dependencies</param>
        /// <param name="sorted">List with the sortet items</param>
        /// <param name="visited">Dictionary with the visited items</param>
        private static void SortByDependenciesVisit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted,
            Dictionary<T, bool> visited)
        {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(item, out inProcess);

            if (alreadyVisited)
            {
                if (inProcess)
                {
                    throw new ArgumentException("Cyclic dependency found! Item: " + item);
                }
            }
            else
            {
                visited[item] = true;

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        SortByDependenciesVisit(dependency, getDependencies, sorted, visited);
                    }
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }
    }
}
