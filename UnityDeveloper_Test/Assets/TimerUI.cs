using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timer;

    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        timer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            timer.gameObject.SetActive(true);
            isActive = true;
        }
    }

}
