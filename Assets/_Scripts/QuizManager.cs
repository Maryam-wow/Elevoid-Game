using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{

    public Quiz quiz;

    public TextMeshProUGUI questionsNumberText;

    public TextMeshProUGUI questionText;

    public List<Button> answersButtons;

    public int currentQuestion;

    public QuestionBar questionBar;
    
    public int maxQuestions = 10;

    public List<int> scores = new List<int> { 0, 0, 0, 0 };

    void Start()
    {
        Initializ();
    }

    public void Initializ()
    {

        for (int i = 0; i < answersButtons.Count; i++)
        {
            int v = i;
            answersButtons[i].onClick.AddListener(delegate { AnswerButtonClicked(v); });
        }


        currentQuestion = 1;
        questionBar.SetQuestion(currentQuestion);

        questionsNumberText.text = currentQuestion + " / " + quiz.questions.Count;

        ShowQuestion();
    }

    public void AnswerButtonClicked(int number)
    {
        // register answer 
        if (quiz.questions[currentQuestion - 1].answers[number].charactersValue == CharacterType.Water)
        {
            scores[0]++;
        }
        else if (quiz.questions[currentQuestion - 1].answers[number].charactersValue == CharacterType.Earth)
        {
            scores[1]++;
        }
        else if (quiz.questions[currentQuestion - 1].answers[number].charactersValue == CharacterType.Air)
        {
            scores[2]++;
        }
        else if (quiz.questions[currentQuestion - 1].answers[number].charactersValue == CharacterType.Fire)
        {
            scores[3]++;
        }

        NextQuestion();
        questionBar.SetQuestion(currentQuestion);
    }

    public void NextQuestion()
    {
        //check for last question
        if (currentQuestion == quiz.questions.Count)
        {
            EndQuiz();
        }
        else
        {
            currentQuestion++;
            questionsNumberText.text = currentQuestion + " / " + quiz.questions.Count;
            ShowQuestion();
            //questionBar.SetQuestion(currentQuestion);
        }
    }

    public void ShowQuestion()
    {
        questionText.text = quiz.questions[currentQuestion - 1].question;

        for (int i = 0; i < answersButtons.Count; i++)
        {
            answersButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = quiz.questions[currentQuestion - 1].answers[i].answerText;
        }
    }

    public void EndQuiz()
    {
        //select character based on quiz
        // 0>water 1>earth 2>air 3>fire
        questionBar.SetQuestionNumber(maxQuestions);
        int max = scores[0];
        int selected = 0;

        for (int i = 1; i < scores.Count; i++)
        {
            if (scores[i] > max)
            {
                max = scores[i];
                selected = i;
            }
        }

        GameManager.Instance.SetCharacter((CharacterType)selected);

        UIManager.Instance.EndQuiz();
    }

}
