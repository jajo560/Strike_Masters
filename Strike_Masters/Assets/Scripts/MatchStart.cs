using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.isMatchStarted = true;
    }

}
