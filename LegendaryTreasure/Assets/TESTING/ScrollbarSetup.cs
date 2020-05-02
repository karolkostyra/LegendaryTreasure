using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarSetup : MonoBehaviour
{
    [SerializeField] private GameObject scrollbar;

    void Start()
    {
        scrollbar.GetComponent<Scrollbar>().value = 1;
    }

    void Update()
    {
        //gameObject.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);

    }
    public void ShowScrollBar()
    {

        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

    }
}
