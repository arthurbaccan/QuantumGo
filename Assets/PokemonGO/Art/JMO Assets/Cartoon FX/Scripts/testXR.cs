using UnityEngine;

using UnityEngine.XR.ARFoundation;


public class testXR : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    var session = FindAnyObjectByType<ARSession>();

    Debug.Log(
        session == null
        ? "ARSession NÃO encontrada"
        : "ARSession encontrada"
    );

    var cameraManager =
    FindAnyObjectByType<ARCameraManager>();

    Debug.Log(
    cameraManager == null
    ? "ARCameraManager NÃO encontrado"
    : "ARCameraManager encontrado"
);
    }
}
