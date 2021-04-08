using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FilledImageLoader : MonoBehaviour
{
    Image image;

    // Total divisions of the circle. Set this to 0 to have continuous loading.
    public int segments;
    // Total time taken to fill
    public float fillTime;
    // Counter used if there exist segments
    float counter;

    [System.Serializable]
    public struct ColorChanges
    {
        public Color color;
        [Range(0,1)]
        public float percentage;
    }
    
    public ColorChanges[] colors;
    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    private void OnEnable()
    {
        // Reset counter and fill amount on enable
        counter = 0;
        image.fillAmount = 0;
    }

    void Update()
    {
        // Error handling
        if (fillTime == 0)
        {
            Debug.Log("Fill Time cannot be 0");
            return;
        }

        // Add fill depending on time
        if (segments != 0)
        {
            counter += Time.deltaTime;
            if (counter >= fillTime / segments)
            {
                image.fillAmount += 1f / segments;
                counter -= fillTime / segments;
            }
        }
        else
            image.fillAmount += Time.deltaTime / fillTime;

        // Itterate through colors from last to change them
        for (int i = colors.Length - 1; i >= 0; i--)
        {
            if (image.fillAmount >= colors[i].percentage)
            {
                image.color = colors[i].color;
                break;
            }
        }
    }
}
