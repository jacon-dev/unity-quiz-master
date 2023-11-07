using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;

    public int GetCorrectAnswers {  get { return correctAnswers; } }
    public int GetQuestionsSeen { get {  return questionsSeen; } }
    
    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }
    
    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }
}
