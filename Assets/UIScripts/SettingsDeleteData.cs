using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using TMPro;
using UnityEngine;
using System.Threading.Tasks; // Adicionado para usar o TaskScheduler


public class SettingsDeleteData : MonoBehaviour
{
    public TextMeshProUGUI confirmText;
    string originalText;
    bool confirmed = false;
    public float secsDeConfirm = 1.5f;
    private static Thread workerThread;

    // Guarda a referência da Main Thread para a UI
    private TaskScheduler mainThreadScheduler;

    public void Start()
    {
        originalText = confirmText.text;
        // Captura o contexto da Main Thread logo no início
        mainThreadScheduler = TaskScheduler.FromCurrentSynchronizationContext();
    }

    private void timerDeConfirm()
    {
        Thread.Sleep((int)(secsDeConfirm * 1000));

        // Executa a alteração de UI de forma segura na Main Thread, sem usar Update
        Task.Factory.StartNew(() =>
        {
            confirmText.text = originalText;
        }, CancellationToken.None, TaskCreationOptions.None, mainThreadScheduler);

        confirmed = false;
    }

    public void eraseSaveDataWithConfirm()
    {

        if (!confirmed)
        {
            if (workerThread?.IsAlive == true && workerThread != null) return;

            confirmText.text = "Confirmar?";
            confirmed = true;
            workerThread = new Thread(timerDeConfirm);
            workerThread.IsBackground = true;
            workerThread.Start();
            return;
        }

        SaveSystem.DeleteSave();
        Application.Quit();
    }
}

