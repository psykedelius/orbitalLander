using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class gpsPlanetDetector : MonoBehaviour
{
    public Transform target;
    public Transform source;
    public float rotOffset = 0;
    public TMP_Text distanceUI;
    public float distanceToCore = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = target.position - source.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GetComponent<RectTransform>().rotation = Quaternion.AngleAxis(angle + rotOffset, Vector3.forward);
        updateDistance();
    }

    void updateDistance()
    {
        distanceToCore = Mathf.RoundToInt(Vector2.Distance(target.position, source.position));
        if (distanceToCore < 20) { GetComponent<CanvasGroup>().DOFade(0, 1); }
        else
        {
            GetComponent<CanvasGroup>().DOFade(1, 1);
        }
        distanceUI.text = "" + distanceToCore;
    }
}
