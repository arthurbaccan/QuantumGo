using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuizActivity", menuName = "Scriptable Objects/QuizActivity")]
public class QuizActivity : ScriptableObject
{
    public QuizActivityType type;
    public GameObject objetoCapturar;
    [TextArea]
    public string textPergunta;
    public List<QuizMultipleChoiceOption> respostasPergunta;
}

public enum QuizActivityType
{
    PERGUNTA_MULTIPLA_ESCOLHA,
    LABIRINTO,
    THROW_ATTOMBALL
}
