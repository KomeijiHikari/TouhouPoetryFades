using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RenaissanceRestart
{
    public static class CellExtension
    {
        public static List<Vector2Int> GridSize( this Vector2Int center, int width, int height)
        {
            List<Vector2Int> output = new List<Vector2Int>();
            int mid = width / 2;
            for (int x = 0-mid; x < width-mid; x++)
            {
                for (int y = 0-mid; y < height-mid; y++)
                {
                    output.Add(center + new Vector2Int(x, y));
                }
            }
            return output;
        }
    }
    public class GridExtension
    {
        public static List<Vector2> GetAllIntPointInFloatRange(Vector2 center, float max_range, Vector2Int center_int, int max_range_int)
        {
            List<Vector2> pointspos = new List<Vector2>();
            for (int x = -max_range_int; x <= max_range_int; x++)
            {
                for (int y = -max_range_int; y <= max_range_int; y++)
                {
                    var point = new Vector2(x, y);
                    if (Vector2.Distance(point + center_int, center) <= max_range)
                        pointspos.Add(point);
                }
            }
            return pointspos;
        }
        public static void GetRectAllEdge(ref List<LineAB> lines, Vector2 item, float halfxsize, float halfysize)
        {
            var tl = new Vector2Pointer(item.x - halfxsize, item.y + halfysize);
            var tr = new Vector2Pointer(item.x + halfxsize, item.y + halfysize);
            var bl = new Vector2Pointer(item.x - halfxsize, item.y - halfysize);
            var br = new Vector2Pointer(item.x + halfxsize, item.y - halfysize);
            lines.Add(new LineAB(tl, tr));
            lines.Add(new LineAB(tr, br));
            lines.Add(new LineAB(bl, br));
            lines.Add(new LineAB(tl, bl));
        }
        public static void GetRectAllEdge_Split(ref List<LineAB> lines, Vector2 item, float halfxsize, float halfysize, int splitcount = 1)
        {
            var tl = new Vector2Pointer(item.x - halfxsize, item.y + halfysize);
            var tr = new Vector2Pointer(item.x + halfxsize, item.y + halfysize);
            var bl = new Vector2Pointer(item.x - halfxsize, item.y - halfysize);
            var br = new Vector2Pointer(item.x + halfxsize, item.y - halfysize);
            for (int i = 0; i < splitcount; i++)
            {
                lines.Add(new LineAB(tl.Lerp(tr, (float)(i) / splitcount), tl.Lerp(tr, (i + 1f) / splitcount)));
                lines.Add(new LineAB(tr.Lerp(br, (float)(i) / splitcount), tr.Lerp(br, (i + 1f) / splitcount)));
                lines.Add(new LineAB(bl.Lerp(br, (float)(i) / splitcount), bl.Lerp(br, (i + 1f) / splitcount)));
                lines.Add(new LineAB(tl.Lerp(bl, (float)(i) / splitcount), tl.Lerp(bl, (i + 1f) / splitcount)));
            }
        }
        public static void GetRectAllEdge_ConstSplit_FromBottom(ref List<LineAB> lines, Vector2 item, float halfBlocky, float halfxsize, float halfysize, float mini, int iteator = 5)
        {
            var tl = new Vector2Pointer(item.x - halfxsize, item.y - halfBlocky + halfysize * 2);
            var tr = new Vector2Pointer(item.x + halfxsize, item.y - halfBlocky + halfysize * 2);
            var bl = new Vector2Pointer(item.x - halfxsize, item.y - halfBlocky);
            var br = new Vector2Pointer(item.x + halfxsize, item.y - halfBlocky);
            for (int i = 0; i < iteator; i++)
            {
                var a = new LineAB(tl.MoveTo(tr, mini, i), tl.MoveTo(tr, mini, i + 1));
                var b = new LineAB(br.MoveTo(tr, mini, i), br.MoveTo(tr, mini, i + 1));
                var c = new LineAB(bl.MoveTo(br, mini, i), bl.MoveTo(br, mini, i + 1));
                var d = new LineAB(bl.MoveTo(tl, mini, i), bl.MoveTo(tl, mini, i + 1));
                lines.Add(a);
                lines.Add(b);
                lines.Add(c);
                lines.Add(d);
            }
        }
        public static List<LineAB> GetAllEdgeInGrids(List<Vector2> grids)
        {
            const int gridsize = 1;
            float halfgridsize = 0.5f * gridsize;

            List<LineAB> getalledges = new List<LineAB>();
            foreach (var item in grids)
            {
                GetRectAllEdge(ref getalledges, item, halfgridsize, halfgridsize);
            }
            return getalledges;
        }
        public static List<LineAB> GetSideEdgeInAllEdge(List<LineAB> allsides)
        {
            Dictionary<LineAB, int> edges = new Dictionary<LineAB, int>();
            foreach (var item in allsides)
            {
                //出现一次的
                var a = new LineAB(item.a, item.b);
                if (edges.ContainsKey(a)) //已经加入过了
                {
                    edges[a]++;
                }
                else
                {
                    edges.Add(a, 1);
                }
            }
            List<LineAB> list = new List<LineAB>();
            foreach (var item in edges)
            {
                if (item.Value == 1)
                {
                    list.Add(item.Key);
                }
            }
            return list;
        }
        public static List<Vector2> GetSidePointsBySides(List<LineAB> side)
        {
            Dictionary<Vector2, bool> points = new Dictionary<Vector2, bool>();
            foreach (var item in side)
            {
                points[new Vector2(item.a.x, item.a.y)] = true;
                points[new Vector2(item.b.x, item.b.y)] = true;
            }
            return points.Keys.ToList();
        }
    }

    /// <summary>
    /// 一条线段 从a到b
    /// </summary>
    public class LineAB
    {
        public Vector2Pointer a;
        public Vector2Pointer b;
        private readonly int hash;

        public LineAB(Vector2Pointer a, Vector2Pointer b)
        {
            this.a = a;
            this.b = b;
            this.hash = HashCode.Combine(a, b);
        }
        public override int GetHashCode()
        {
            return this.hash;
        }
        public override bool Equals(object obj)
        {
            return ((LineAB)obj).hash == this.hash;
        }
    }
    public class Vector2Pointer
    {
        public float x;
        public float y;
        private readonly int hash;

        public Vector2Pointer(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.hash = HashCode.Combine(x, y);
        }
        public override int GetHashCode()
        {
            return this.hash;
        }
        public override bool Equals(object obj)
        {
            return ((Vector2Pointer)obj).hash == this.hash;
        }
        public Vector2Pointer Lerp(Vector2Pointer b, float percent)
        {
            return new Vector2Pointer(x = this.x + (b.x - this.x) * percent, y = this.y + (b.y - this.y) * percent);
        }
        public Vector2Pointer MoveTo(Vector2Pointer b, float mini, float iteator)
        {
            float nx = this.x;
            if (b.x > nx)
            {
                nx = this.x + mini * iteator;
                if (nx > b.x) nx = b.x;
            }
            else
            {
                nx = this.x - mini * iteator;
                if (nx < b.x) nx = b.x;
            }
            float ny = this.y;
            if (b.y > ny)
            {
                ny = this.y + mini * iteator;
                if (ny > b.y) ny = b.y;
            }
            else
            {
                ny = this.y - mini * iteator;
                if (ny < b.y) ny = b.y;
            }

            return new Vector2Pointer(nx, ny);
        }

        public Vector2 ToVector()
        {
            return new Vector2(x, y);
        }
    }
}

