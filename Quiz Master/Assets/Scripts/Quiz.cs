using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] QuestionSO currentQuestion;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;


    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Update()
    {
        timerImage.fillAmount = timer.ImageFillFraction;
        if (timer.LoadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.LoadNextQuestion = false;
        }
        else if(hasAnsweredEarly == false && timer.IsAnsweringQuestion == false)
        {
            DisplayAnswer(-1);
        }
    }

    void GetRandomQuestion()
    {
        var index = Random.Range(0, questions.Count-1);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        timer.CancelTimer();
        scoreText.text = $"Score: {scoreKeeper.CalculateScore()}%";
    }

    void DisplayAnswer(int index)
    {
        if (index == correctAnswerIndex)
        {
            questionText.text = "Well done! That is correct!";
            answerButtons[index].GetComponent<Image>().sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            questionText.text = $"Woops! Sorry, the correct answer was {currentQuestion.GetAnswer(correctAnswerIndex)}";
            answerButtons[correctAnswerIndex].GetComponent<Image>().sprite = correctAnswerSprite;
        }
        SetButtonState(false);
    }

    private void DisplayQuestions()
    {
        questionText.text = currentQuestion.GetQuestion();
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            var buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    private void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprite();
        GetRandomQuestion();
        DisplayQuestions();
        scoreKeeper.IncrementQuestionsSeen();
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
