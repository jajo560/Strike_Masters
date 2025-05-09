using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public KeyCode abilityKey1 = KeyCode.Q;
    public KeyCode abilityKey2 = KeyCode.E;
    public GameObject panelAbility1;
    public GameObject panelAbility2;

    public GameObject extraPanelToDisable;

    private bool shownAbility1 = false;
    private bool shownAbility2 = false;
    private bool isShowingPanel = false;
    private Queue<IEnumerator> tutorialQueue = new Queue<IEnumerator>();

    void Start()
    {
        if (extraPanelToDisable != null)
            StartCoroutine(DisableExtraPanelAfterDelay(10f));
    }

    void Update()
    {
        if (Input.GetKeyDown(abilityKey1) && !shownAbility1)
        {
            shownAbility1 = true;
            tutorialQueue.Enqueue(ShowPanel(panelAbility1));
        }

        if (Input.GetKeyDown(abilityKey2) && !shownAbility2)
        {
            shownAbility2 = true;
            tutorialQueue.Enqueue(ShowPanel(panelAbility2));
        }

        if (!isShowingPanel && tutorialQueue.Count > 0)
        {
            StartCoroutine(tutorialQueue.Dequeue());
        }
    }

    IEnumerator ShowPanel(GameObject panel)
    {
        isShowingPanel = true;
        panel.SetActive(true);
        yield return new WaitForSeconds(3f);
        panel.SetActive(false);
        isShowingPanel = false;
    }

    IEnumerator DisableExtraPanelAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (extraPanelToDisable != null)
            extraPanelToDisable.SetActive(false);
    }
}