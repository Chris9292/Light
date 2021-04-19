using UnityEngine;

public class MinimapIconScaler : MonoBehaviour
{
    MiniMapCamera miniMapCamera;
    Vector3 startingScale;

    private void Awake()
    {
        startingScale = transform.localScale;
    }
    void Start()
    {
        miniMapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera").GetComponent<MiniMapCamera>();
    }

    void Update()
    {
        transform.localScale = startingScale * (miniMapCamera.CurrentOrthographicSize / miniMapCamera.DefaultOrthographicSize);
    }
}
