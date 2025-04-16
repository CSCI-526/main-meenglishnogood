using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Winning : MonoBehaviour
{
    public GameObject winningPage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Destination"))
        {
            winningPage.SetActive(true); // 显示胜利页面
            //Time.timeScale = 0f; // 停止游戏时间
        }
    }
}
