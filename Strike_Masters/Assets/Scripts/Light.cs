using UnityEngine;
using UnityEngine.SceneManagement;

public class LightingFixer : MonoBehaviour
{
    void Start()
    {
        DynamicGI.UpdateEnvironment();
    }
}