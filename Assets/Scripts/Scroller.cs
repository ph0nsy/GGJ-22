using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage background_img;
    [SerializeField] private float x, y;

    void Update()
    {
        background_img.uvRect = new Rect(background_img.uvRect.position + new Vector2(x, y) * Time.deltaTime, background_img.uvRect.size);
    }
}
