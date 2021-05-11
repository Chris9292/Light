using UnityEngine;

/// <summary>
/// Copies the line from the Path Script
/// </summary>

[RequireComponent(typeof(LineRenderer))]
public class LineCopier : MonoBehaviour
{
    public Path path;
    LineRenderer thisLine;

    public enum LineType { Static, Dynamic}
    [Tooltip("Choose which line to copy")]
    public LineType lineType;

    void Awake()
    {
        thisLine = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        if (lineType == LineType.Static)
            path.OnStaticLineUpdated += CopyLine;
        else if (lineType == LineType.Dynamic)
            path.OnDynamicLineUpdated += CopyLine;
    }

    public void CopyLine(LineRenderer lineToCopy)
    {
        Vector3[] linePositions = new Vector3[lineToCopy.positionCount];
        lineToCopy.GetPositions(linePositions);
        thisLine.positionCount = linePositions.Length;
        thisLine.SetPositions(linePositions);
    }
}
