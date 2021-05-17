using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class StoryTeller : MonoBehaviour
{
    TMP_Text tmp;

    // Used to discard the first delta timer addition as it causes a bug where the first line doesn't get loaded correctly
    // More here: https://gamedev.stackexchange.com/questions/63972/first-frame-has-a-much-longer-delta-time-than-other-frames
    bool firstFrame;

    [Tooltip("Active time for each line")]
    public float defaultActiveTime = 4f;
    public float fadeInTime;
    public float fadeOutTime;

    public bool useUnscaledTime;
    [Tooltip("Overrides the active time completely")]
    public bool useManualTransitions;

    public delegate void StoryEndHandler();
    public event StoryEndHandler OnStoryEnded;

    [System.Serializable]
    public struct StoryLine
    {
        public string line;
        [Tooltip("Check this to override the default active time")]
        public bool overrideActiveTime;
        [Tooltip("Only relevant if OverrideActiveTime is set to true")]
        public float activeTime;
    }

    public StoryLine[] storyLines;
    void Awake()
    {
        tmp = GetComponent<TMP_Text>();
    }

    // Transitions for the state machine behaviour (more in Update)
    enum Transition { FadeIn, FadeOut, NoTransition }
    Transition transition = Transition.NoTransition;

    // Timer and timeout period to reset timer
    float timer;
    float timeout;

    // Current line
    int lineIndex;
    public int CurrentLine { get { return lineIndex; } }

    private void OnEnable()
    {
        if (storyLines != null)
        {
            timer = 0f;
            firstFrame = true;

            lineIndex = 0;
            LoadLine();
        }
        else
            throw new UnityException("StoryLines cannot be null");
    }

    void Update()
    {
        float timeSinceLastFrame;

        if (useUnscaledTime)
            timeSinceLastFrame = Time.unscaledDeltaTime;
        else
            timeSinceLastFrame = Time.deltaTime;

        // Avoids Awake bug where first frame's deltaTime is much bigger than usual
        if (firstFrame)
        {
            timeSinceLastFrame = 0f;
            firstFrame = false;
        }

        timer += timeSinceLastFrame;

        /// <summary>
        /// Enter -> FadeIn -> NoTransition -> FadeOut -> Exit or Loop
        /// Cleaner State Machine implementation: https://stackoverflow.com/questions/5923767/simple-state-machine-example-in-c
        /// </summary>

        if (timer > timeout)
        {
            timer = 0f;

            if (transition == Transition.FadeOut)
            {
                lineIndex++;
                LoadLine();
            }

            else if (transition == Transition.FadeIn)
            {
                transition = Transition.NoTransition;
                timeout = GetLineTimeout();
            }

            else if (transition == Transition.NoTransition && !useManualTransitions)
            {
                NextLine();
            }

        }

        // FadeIn/Out logic
        if (transition == Transition.FadeIn)
            tmp.color += new Color(0f, 0f, 0f, timeSinceLastFrame / fadeInTime);
        else if (transition == Transition.FadeOut)
            tmp.color -= new Color(0f, 0f, 0f, timeSinceLastFrame / fadeOutTime);
    }

    // Get the current line's active time
    float GetLineTimeout()
    {
        float lineTimeout;

        if (storyLines[lineIndex].overrideActiveTime)
            lineTimeout = storyLines[lineIndex].activeTime;
        else
            lineTimeout = defaultActiveTime;

        return lineTimeout;
    }

    void LoadLine()
    {
        if (lineIndex < storyLines.Length)
        {
            tmp.text = storyLines[lineIndex].line;

            SetTmpApha(0f);
            timeout = fadeInTime;
            transition = Transition.FadeIn;
        }
        else
        {
            gameObject.SetActive(false);
            OnStoryEnded?.Invoke();
        }
    }

    void NextLine()
    {
        timer = 0f;
        SetTmpApha(1f);
        timeout = fadeOutTime;
        transition = Transition.FadeOut;
    }

    void SetTmpApha(float alpha)
    {
        tmp.color += new Color(0f, 0f, 0f, alpha - tmp.color.a);
    }

    public void TryNextLine()
    {
        if (transition == Transition.NoTransition)
            NextLine();
    }
}