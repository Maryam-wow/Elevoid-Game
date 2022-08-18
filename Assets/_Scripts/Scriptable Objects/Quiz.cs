using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Quiz")]
public class Quiz : ScriptableObject
{
    public List<Question> questions = new List<Question>();
}

[Serializable]
public class Question
{
    public string question;
    public List<Answer> answers = new List<Answer>();
}

[Serializable]
public class Answer
{
    public string answerText;
    public CharacterType charactersValue;
}
