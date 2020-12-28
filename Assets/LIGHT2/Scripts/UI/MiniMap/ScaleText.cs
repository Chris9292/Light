using TMPro;
using UnityEngine;

// TODO: Comments

[RequireComponent(typeof(TextMeshPro))]
public class ScaleText : MonoBehaviour
{
    private TextMeshPro textMeshPro;
    private Camera miniMapCamera;

    public Transform line;

    private void Awake()
    {
        textMeshPro = gameObject.GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        miniMapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        float size = miniMapCamera.orthographicSize;
        float aspect = miniMapCamera.aspect;
        float scaleLineRatio = line.localScale.x;

        float scaleLineLength = 2 * scaleLineRatio * size * aspect;

        textMeshPro.text = scaleLineLength.ToString("F3") + "m";
    }
}
