using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private AudioSource audioSource;
    private Vector3 originalSize; 
    [SerializeField] float imageSizeIncrease;
    [SerializeField] AudioClip audioClip;
    void Start()
    {
        image = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        originalSize = image.transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image == null) return;
        image.transform.localScale += Vector3.one * imageSizeIncrease;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (image == null) return;
        image.transform.localScale = originalSize;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        audioSource.PlayOneShot(audioClip);
        if (image == null) return;
        image.transform.localScale = originalSize;
    }
}
