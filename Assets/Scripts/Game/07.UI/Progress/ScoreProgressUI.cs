using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreProgressUI : MonoBehaviour
{
    [SerializeField]
    private Slider slider_progress;

    public void UpdateProgress(int currentScore, int totalScore)
    {
        float value = (float)currentScore / (float)totalScore;

        slider_progress.value = value;
    }


}
