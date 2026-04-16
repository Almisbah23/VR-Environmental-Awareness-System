using UnityEngine;

public class SubtitleTester : MonoBehaviour
{
    void Start()
    {
        // This will show a test line for 3 seconds
        SubtitleManager.Instance?.ShowLine("Testing subtitles…", 3f);
    }
}
