using UnityEngine;

namespace RenaissanceRestart
{
    public static class VectorExtension
    {




        public static (float, float) ToPair(this Vector2 vector)
        {
            return (vector.x, vector.y);
        }
        public static (int, int) ToPair(this Vector2Int vector)
        {
            return (vector.x, vector.y);
        }
        public static Vector2 ToVector(this (float, float) item)
        {
            return new Vector2(item.Item1, item.Item2);
        }
        public static (int ,int ) RoundToInt(this (float, float) item)
        {
            var xx = Mathf.RoundToInt(item.Item1);
            var yy = Mathf.RoundToInt(item.Item2);
            return (xx, yy);
        }
        public static Vector2 ToVector(this (int, int) item)
        {
            return new Vector2(item.Item1, item.Item2);
        }
        public static Vector2Int ToVectorInt(this (int, int) item)
        {
            return new Vector2Int(item.Item1, item.Item2);
        }
        public static Vector2Int ToVectorInt(this (float, float) item)
        {
            return item.RoundToInt().ToVectorInt();
        }
       
        public static Vector2Int ToRoundInt(this Vector2 pos)
        {
            return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
        }
        public static Vector2Int FlagToVector(VectorFlag flag)
        {
            return flag switch
            {
                VectorFlag.Default => new Vector2Int(),
                VectorFlag.Top => new Vector2Int(0, 1),
                VectorFlag.TopLeft => new Vector2Int(-1, 1),
                VectorFlag.TopRight => new Vector2Int(1, 1),
                VectorFlag.Center => new Vector2Int(0, 0),
                VectorFlag.Left => new Vector2Int(-1, 0),
                VectorFlag.Right => new Vector2Int(1, 0),
                VectorFlag.Bottom => new Vector2Int(0, -1),
                VectorFlag.BottomLeft => new Vector2Int(-1, -1),
                VectorFlag.BottomRight => new Vector2Int(1, -1),
                _ => new Vector2Int()
            };
        }
    }
    public enum VectorFlag : int
    {
        Default = 0, Top = 1, TopLeft = 2, TopRight = 3, Center = 4, Left = 5, Right = 6, Bottom = 7, BottomLeft = 8, BottomRight = 9
    }
}

