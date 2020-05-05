using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarSetup : MonoBehaviour
{
    [SerializeField] private GameObject scrollbar;
    [SerializeField] private GameObject scrollrect;

    void Start()
    {
        SetPosition(1);
    }

    void Update()
    {
        AutoAcitvateScrollbar(scrollrect);
    }

    private void AutoAcitvateScrollbar(GameObject obj)
    {
        scrollbar.SetActive(obj.activeSelf);
    }

    public bool EndOfScroll()
    {
        if (scrollrect.GetComponent<ScrollRect>().verticalNormalizedPosition <= 0.05f)
        {
            return true;
        }
        return false;
    }

    public void SetPosition(int value)
    {
        scrollbar.GetComponent<Scrollbar>().value = value;
    }
}
