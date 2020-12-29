using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class ScaleText : MonoBehaviour
{
    private TextMeshPro textMeshPro;
    private Camera miniMapCamera;

    // The object used to show MiniMap real-world scale
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
        // Math here: https://answers.unity.com/questions/174002/what-is-the-relationship-between-camera-size-units.html
        
        float size = miniMapCamera.orthographicSize;
        float aspect = miniMapCamera.aspect;
        float scaleLineRatio = line.localScale.x;

        float scaleLineLength = 2 * scaleLineRatio * size * aspect;

        textMeshPro.text = scaleLineLength.ToString("F2") + "m";
    }
}