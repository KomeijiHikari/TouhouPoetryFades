using System;
using System.Collections.Generic;
using System.Linq;

namespace RenaissanceRestart
{
    public interface IWeight
    {
        public float Weight { get; }
    }
    public static class ListExtension
    {
        public static void RemoveAndAdd<T>(this List<T> list, T remove, T add)
        {
            if (list.Contains(remove))
            {
                list.Remove(remove);
            }
            if (!list.Contains(add))
            {
                list.Add(add);
            }
        }
        public static void Add_ifnotExist<T>(this List<T> list, T add)
        {
            if (!list.Contains(add))
            {
                list.Add(add);
            }
        }
        public static void Reset<T>(this List<T> list, params T[] remove)
        {
            list.Clear();
            list.AddRange(remove);
        }
        public static void Remove_ifExist<T>(this List<T> list, T remove)
        {
            if (list.Contains(remove))
            {
                list.Remove(remove);
            }
        }
        public static void Add_ifnotExist<T, T2>(this Dictionary<T, T2> list, T add, T2 value)
        {
            if (!list.ContainsKey(add))
            {
                list.Add(add, value);
            }
        }
        public static void Remove_ifExist<T, T2>(this Dictionary<T, T2> list, T remove)
        {
            if (list.ContainsKey(remove))
            {
                list.Remove(remove);
            }
        }
        public static void Add_orReplace<T, T2>(this Dictionary<T, T2> list, T add, T2 value)
        {
            list[add] = value;
        }
        public static List<T> And<T>(this List<T> list, T obj)
        {
            var l = new List<T>(list);
            l.Add_ifnotExist(obj);
            return l;
        }
        public static List<T> And<T>(this List<T> list, T[] obj)
        {
            var l = new List<T>(list);
            l.AddRange(obj);
            return l;
        }
        public static List<T> And<T>(this List<T> list, List<T> obj)
        {
            var l = new List<T>(list);
            l.AddRange(obj);
            return l;
        }
        public static T[] And<T>(this T[] list, T obj)
        {
            var l = new List<T>(list);
            l.Add_ifnotExist(obj);
            return l.ToArray();
        }
        public static T[] And<T>(this T[] list, T[] obj)
        {
            var l = new List<T>(list);
            l.AddRange(obj);
            return l.ToArray();
        }




        /// <summary>
        /// 均匀随机
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T Random<T>(this List<T> list, Random r)
        {
            var count = list.Count;
            if (count == 0) return default;
            if (count == 1)
            {
                return list[0];
            }
            int idnex = r.Next(0, count);
            return list[idnex];
        }
        public static void ListSetRandom<T>(this List<T> sources, Random rd)
        {
            int index = 0;
            T temp;
            for (int i = 0; i < sources.Count; i++)
            {
                index = rd.Next(0, sources.Count);
                if (index != i)
                {
                    temp = sources[i];
                    sources[i] = sources[index];
                    sources[index] = temp;
                }
            }
        }
        /// <summary>
        /// 均匀随机
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> RandomCount<T>(this List<T> list, int Needcount, Random rd)
        {
            if (Needcount < 0) Needcount = 0;
            var count = list.Count;
            if (count <= Needcount)
            {
                return list.ToArray().ToList();
            }
            list.ListSetRandom(rd);
            return list.GetRange(0, Needcount);
        }

        public static T RandomWithWeight<T>(this List<T> list, Random r) where T : IWeight
        {
            var n = list.Select(e => (e, e.Weight)).ToList();
            return n.RandomWithWeight(r);
        }

        /// <summary>
        /// 带权随机
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RandomWithWeight<T>(this List<(T, float)> list, Random r)
        {
            var count = list.Count;
            if (count == 0) return default;
            if (count == 1)
            {
                return list[0].Item1;
            }
            //计算总权重
            float AllWeight = 0;
            foreach (var item in list)
                AllWeight += item.Item2;
            //创建新的排序列表, 按照升序排序
            var newsortlist = new List<(T, float)>(list);
            newsortlist.Sort((a, b) => a.Item2.CompareTo(b.Item2));

            //计算随机权重, 随机数应成均匀分布
            float randomweight = (float)r.NextDouble() * AllWeight;

            //计算随机数的位置
            for (int i = 0; i < newsortlist.Count; i++)
            {
                randomweight -= newsortlist[i].Item2;
                if (randomweight <= 0)
                    return newsortlist[i].Item1;
            }
            return newsortlist[newsortlist.Count - 1].Item1;
        }


        ///// <summary>
        ///// 求最近的对象
        ///// </summary>
        //public static (Position, T) Nearst<T>(this List<(Position, T)> list, Position pos, T defaultt)
        //{
        //    if (list == null || list.Count <= 0) return (pos, defaultt);
        //    if (list.Count == 1) return list[0];
        //    var select = list[0];
        //    float dis = float.MaxValue;
        //    foreach (var item in list.ToArray())
        //    {
        //        var newdis = item.Item1.Distance(pos);
        //        if (newdis < dis)
        //        {
        //            dis = newdis;
        //            select = item;
        //        }
        //    }
        //    return select;
        //}
        ///// <summary>
        ///// 求最近的对象
        ///// </summary>
        //public static T Nearst<T>(this List<T> list, Position pos) where T : class, IPosition
        //{
        //    if (list == null || list.Count <= 0) return null;
        //    if (list.Count == 1) return list[0];
        //    var select = list[0];
        //    float dis = float.MaxValue;
        //    foreach (var item in list.ToArray())
        //    {
        //        var newdis = item.Position.Distance(pos);
        //        if (newdis < dis)
        //        {
        //            dis = newdis;
        //            select = item;
        //        }
        //    }
        //    return select;
        //}
        //public static Position Nearst(this List<Position> list, Position pos)
        //{
        //    if (list == null || list.Count <= 0) return default;
        //    if (list.Count == 1) return list[0];
        //    var select = list[0];
        //    float dis = float.MaxValue;
        //    foreach (var item in list.ToArray())
        //    {
        //        var newdis = item.Distance(pos);
        //        if (newdis < dis)
        //        {
        //            dis = newdis;
        //            select = item;
        //        }
        //    }
        //    return select;
        //}



        //public static (int x, int y) Nearst(this IEnumerable<(int x, int y)> list, (int x, int y) xy)
        //{
        //    float distance = float.MaxValue;
        //    var select = list.First();
        //    var center = new Vector2(xy.x, xy.y);
        //    foreach (var item in list)
        //    {
        //        var dist = Vector2.Distance(new Vector2(item.x, item.y), center);
        //        if (dist < distance)
        //        {
        //            distance = dist;
        //            select = item;
        //        }
        //    }
        //    return select;
        //}
        public static T Nearst<T>(this IEnumerable<T> list, (float, float) center, Func<(float, float), (float, float), float> getdistance, Func<T, (float, float)> getxy)
        {
            float distance = float.MaxValue;
            var select = list.First();
            foreach (var item in list)
            {
                var get = getxy(item);
                var dist = getdistance((get.Item1, get.Item2), center);
                if (dist < distance)
                {
                    distance = dist;
                    select = item;
                }
            }
            return select;
        }
    }
}

