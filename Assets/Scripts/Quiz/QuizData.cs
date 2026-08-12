using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[CreateAssetMenu(fileName = "New Quiz", menuName = "QuantumGo/Quiz")]
public class QuizData : ScriptableObject
{
    public int id;

    // parametros antigos:
    public string name;
    [TextArea(3, 10)]
    public string description;
    
    public Sprite icon;                              // icone para a enciclopédia

    public int waitRecaptureSecs;

    [NonSerialized]
    public int foundTimes = 0;                       // Indica se o objeto foi encontrado ou não

    [NonSerialized]
    // CHECAR NULO ANTES DE USAR PARAMETROS DA CLASSE CaptureInfo
    public List<CaptureInfo> objectCaptureInfo;   // Imagens capturáveis do objeto e seus correspondentes modelos e datas de captura

    // novos:
    public List<QuizActivity> quizActivities;
}
