using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool isAnsweringQuestion = false;
    public float imageFillFraction;
    [SerializeField] private float questionTimer = 15;
    [SerializeField] private int answerTimer = 5;
    private float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        var divider = isAnsweringQuestion
            ? questionTimer
            : answerTimer;
        imageFillFraction = timerValue / divider;

        if (timerValue <= 0)
        {
            isAnsweringQuestion = isAnsweringQuestion == false;
            timerValue = isAnsweringQuestion
                ? questionTimer
                : answerTimer;
        }
        Debug.Log($"Answering Question: {isAnsweringQuestion} | Timer Value : {timerValue} | Image Fill Fraction : {imageFillFraction}");
    }
}
