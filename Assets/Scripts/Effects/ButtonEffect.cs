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
    [SerializeField] float imageSizeIncrease;
    [Tooltip("0 = Mouse Over SFX, 1 = Mouse Down SFX")]
    [SerializeField] AudioClip[] audioClips;
    void Start()
    {
        image = GetComponent<Image>();
        //buttonText = GetComponentInChildren<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image == null) return;
        image.transform.localScale += Vector3.one * imageSizeIncrease;
        //audioSource.PlayOneShot(audioClips[0]);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (image == null) return;
        image.transform.localScale -= Vector3.one * imageSizeIncrease;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        audioSource.PlayOneShot(audioClips[1]);
        if (image == null) return;
        image.transform.localScale -= Vector3.one * imageSizeIncrease;
    }
}
