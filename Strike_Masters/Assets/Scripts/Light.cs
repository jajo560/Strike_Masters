using UnityEngine;
using UnityEngine.SceneManagement;

public class LightingFixer : MonoBehaviour
{
    void Start()
    {
        // Forzar la actualizaci�n de la iluminaci�n
        DynamicGI.UpdateEnvironment();
    }
}