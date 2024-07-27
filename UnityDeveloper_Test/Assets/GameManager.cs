using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float gameTimer = 200f;
    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Press E to Start Timer";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isActive)
        {
            isActive = true;
        }

        if (isActive)
        {
            gameTimer -= Time.deltaTime;
            text.text = "Timer : " + Mathf.Ceil(gameTimer).ToString();
            if(gameTimer < 0f)
            {
                text.text = "Game Over";
            }
        }
    }

}
