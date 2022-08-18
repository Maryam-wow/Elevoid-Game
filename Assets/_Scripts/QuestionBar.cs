using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionBar : MonoBehaviour
{

    public Slider slider;


    public void SetQuestionNumber (int question)
    {
        slider.maxValue = question;
        slider.value = question;

    }

    public void SetQuestion(int question)
    {
        slider.value = question;
    }
}

