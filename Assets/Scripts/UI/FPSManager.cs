using UnityEngine;
using TMPro;

public class FPSManager : MonoBehaviour
{
    [SerializeField] private TMP_Text fpsText;
    [SerializeField] private float pollingTime;

    private float time;
    private int frameCount;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            fpsText.gameObject.SetActive(!fpsText.gameObject.activeSelf);
        }

        time += Time.deltaTime;
        frameCount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = $"{frameRate}";

            time -= pollingTime;
            frameCount = 0;
        }
    }
}
