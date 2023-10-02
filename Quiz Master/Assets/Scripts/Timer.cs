using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool IsAnsweringQuestion = false;
    public float ImageFillFraction;
    public bool LoadNextQuestion = false;
    [SerializeField] private float questionTimer = 15;
    [SerializeField] private int answerTimer = 5;
    private float timerValue;
    private bool isFirstRun = true;

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if (IsAnsweringQuestion)
        {
            if(timerValue > 0)
            {
                ImageFillFraction = timerValue / questionTimer;
            }
            else
            {
                IsAnsweringQuestion = false;
                timerValue = answerTimer;
            }
        }
        else
        {
            if(timerValue > 0)
            {
                ImageFillFraction = timerValue / answerTimer;
            }
            else if (isFirstRun)
            {
                IsAnsweringQuestion = true;
                timerValue = questionTimer;
                isFirstRun = false;
            }
            else
            {
                IsAnsweringQuestion = true;
                timerValue = questionTimer;
                LoadNextQuestion = true;
            }
        }
        Debug.Log($"Answering Question: {IsAnsweringQuestion} | Timer Value : {timerValue} | Image Fill Fraction : {ImageFillFraction} | Load Next Question : {LoadNextQuestion}");
    }
}
