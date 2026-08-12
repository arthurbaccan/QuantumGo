using UnityEngine;

[CreateAssetMenu(fileName = "QuizMultipleChoiceOption", menuName = "Scriptable Objects/QuizMultipleChoiceOption")]
public class QuizMultipleChoiceOption : ScriptableObject
{
    public string text;
    public bool isCorrect;
}
