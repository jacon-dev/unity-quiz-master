using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;

    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    void Start()
    {
        GetNextQuestion();
        timer = FindObjectOfType<Timer>();
    }

    void Update()
    {
        timerImage.fillAmount = timer.ImageFillFraction;
    }

    public void OnAnswerSelected(int index)
    {
        if(index == correctAnswerIndex)
        {
            questionText.text = "Well done! That is correct!";
            answerButtons[index].GetComponent<Image>().sprite = correctAnswerSprite;
        }
        else
        {
            questionText.text = $"Woops! Sorry, the correct answer was {question.GetAnswer(correctAnswerIndex)}";
            answerButtons[correctAnswerIndex].GetComponent<Image>().sprite = correctAnswerSprite;
        }
        SetButtonState(false);
    }

    private void DisplayQuestions()
    {
        questionText.text = question.GetQuestion();
        correctAnswerIndex = question.GetCorrectAnswerIndex();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            var buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    private void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprite();
        DisplayQuestions();
    }

    private void SetButtonState(bool state)
    {
        foreach(var button in answerButtons)
        {
            button.GetComponent<Button>().interactable = state;
        }
    }

    private void SetDefaultButtonSprite()
    {
        foreach (var button in answerButtons)
        {
            button.GetComponent<Image>().sprite = defaultAnswerSprite;
        }
    }
}
