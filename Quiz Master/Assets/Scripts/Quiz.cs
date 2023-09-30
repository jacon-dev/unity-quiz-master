using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    int correctAnswerIndex;

    void Start()
    {
        GetNextQuestion();
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
