using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quiz Database", menuName = "QuantumGo/Database/Object")]
public class ObjectDatabase : ScriptableObject
{
    public List<QuizData> allQuizzes = new List<QuizData>();
}
