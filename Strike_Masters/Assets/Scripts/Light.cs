using UnityEngine;
using UnityEngine.SceneManagement;

public class LightingFixer : MonoBehaviour
{
    void Start()
    {
        // Forzar la actualización de la iluminación
        DynamicGI.UpdateEnvironment();
    }
}