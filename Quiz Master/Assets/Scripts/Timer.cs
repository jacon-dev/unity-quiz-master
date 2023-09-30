using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool IsAnsweringQuestion = false;
    public float ImageFillFraction;
    public bool LoadNextQuestion = true;
    [SerializeField] private float questionTimer = 15;
    [SerializeField] private int answerTimer = 5;
    private float timerValue;

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

        var divider = IsAnsweringQuestion
            ? questionTimer
            : answerTimer;
        ImageFillFraction = timerValue / divider;

        if (timerValue <= 0)
        {
            IsAnsweringQuestion = IsAnsweringQuestion == false;
            timerValue = IsAnsweringQuestion
                ? questionTimer
                : answerTimer;
            LoadNextQuestion = false;
        }
        Debug.Log($"Answering Question: {IsAnsweringQuestion} | Timer Value : {timerValue} | Image Fill Fraction : {ImageFillFraction} | Load Next Question : {LoadNextQuestion}");
    }
}
