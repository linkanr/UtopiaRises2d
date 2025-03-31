using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldDebugger : MonoBehaviour
{
    public static OldDebugger Instance;
    public Transform card;
    public Transform cardParent;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine("DelayedInstance");
        }
    }
    IEnumerator DelayedInstance()
    {
        yield return new WaitForSeconds(.2f);
        Transform newCard = Instantiate(card, cardParent);
        newCard.SetAsFirstSibling();
    }
}
