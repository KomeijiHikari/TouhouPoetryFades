using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

[AddComponentMenu("UI/Text_Extend", 0)]
public class UIT_TextExtend : Text
{
    public int m_characterSpacing;
    //正则表达式参数
    private const string m_RichTextRegexPatterns = @"<b>|</b>|<i>|</i>|<size=.*?>|</size>|<Size=.*?>|</Size>|<color=.*?>|</color>|<Color=.*?>|</Color>|<material=.*?>|</material>";

    //ContentSizeFitter的PreferdWidth获取
    public override float preferredWidth
    {
        get
        {
            //获取初始宽度
            float preferredWidth = cachedTextGenerator.GetPreferredWidth(text, GetGenerationSettings(Vector2.zero));

            //根据生成的有效初始顶点数据获得最大行数的初始顶点数量
            List<List<int>> linesVertexStartIndexes = GetLinesVertexStartIndexes();
            int maxLineCount = 0;
            for (int i = 0; i < linesVertexStartIndexes.Count; i++)
                maxLineCount = Mathf.Max(maxLineCount, linesVertexStartIndexes[i].Count);

            return preferredWidth + m_characterSpacing * (maxLineCount - 1);
        }
    }

    //定点生成覆写
    //当前使用的字数像素偏移方案为(字数*偏移值) 请根据需求自行配置
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        base.OnPopulateMesh(toFill);
        if (m_characterSpacing == 0)
            return;
        List<UIVertex> vertexes = new List<UIVertex>();
        toFill.GetUIVertexStream(vertexes);

        List<List<int>> linesVertexStartIndexes = GetLinesVertexStartIndexes();
        float alignmentFactor = GetAlignmentFactor();

        for (int i = 0; i < linesVertexStartIndexes.Count; i++)
        {
            //行偏移(左中右) 
            float lineOffset = (linesVertexStartIndexes[i].Count - 1) * m_characterSpacing * alignmentFactor;
            for (int j = 0; j < linesVertexStartIndexes[i].Count; j++)
            {
                int vertexStartIndex = linesVertexStartIndexes[i][j];
                Vector3 offset = Vector3.right * ((m_characterSpacing * j) - lineOffset);
                //对每个有效字体的六个顶点偏移
                AddVertexOffset(vertexes, vertexStartIndex + 0, offset);
                AddVertexOffset(vertexes, vertexStartIndex + 1, offset);
                AddVertexOffset(vertexes, vertexStartIndex + 2, offset);
                AddVertexOffset(vertexes, vertexStartIndex + 3, offset);
                AddVertexOffset(vertexes, vertexStartIndex + 4, offset);
                AddVertexOffset(vertexes, vertexStartIndex + 5, offset);
            }
        }

        toFill.Clear();
        toFill.AddUIVertexTriangleStream(vertexes);
    }
    //对顶点进行偏移
    void AddVertexOffset(List<UIVertex> vertexes, int index, Vector3 offset)
    {
        UIVertex vertex = vertexes[index];
        vertex.position += offset;
        vertexes[index] = vertex;
    }
    //获取有效的初始顶点
    List<List<int>> GetLinesVertexStartIndexes()
    {
        List<List<int>> linesVertexIndexes = new List<List<int>>();
        IList<UILineInfo> lineInfos = cachedTextGenerator.lines;
        for (int i = 0; i < lineInfos.Count; i++)
        {
            List<int> lineVertexStartIndex = new List<int>();
            int lineStart = lineInfos[i].startCharIdx;
            int lineLength = (i < lineInfos.Count - 1) ? lineInfos[i + 1].startCharIdx - lineInfos[i].startCharIdx : text.Length - lineInfos[i].startCharIdx;

            //Rich Text根据正则表达式获取需要忽略的初始顶点
            List<int> ignoreIndexes = new List<int>();
            if (supportRichText)
            {
                string line = text.Substring(lineStart, lineLength);
                foreach (Match matchTag in Regex.Matches(line, m_RichTextRegexPatterns))
                {
                    for (int j = 0; j < matchTag.Length; j++)
                        ignoreIndexes.Add(matchTag.Index + j);
                }
            }

            for (int j = 0; j < lineLength; j++)
                if (!ignoreIndexes.Contains(j))
                    lineVertexStartIndex.Add((lineStart + j) * 6);

            linesVertexIndexes.Add(lineVertexStartIndex);
        }
        return linesVertexIndexes;
    }

    //获取行偏移比例参数
    float GetAlignmentFactor()
    {
        switch (alignment)
        {
            default:
                Debug.LogError("Invalid Convertions Here!");
                return 0;
            case TextAnchor.UpperLeft:
            case TextAnchor.MiddleLeft:
            case TextAnchor.LowerLeft:
                return 0;
            case TextAnchor.UpperCenter:
            case TextAnchor.MiddleCenter:
            case TextAnchor.LowerCenter:
                return .5f;
            case TextAnchor.UpperRight:
            case TextAnchor.MiddleRight:
            case TextAnchor.LowerRight:
                return 1f;
        }

    }
}