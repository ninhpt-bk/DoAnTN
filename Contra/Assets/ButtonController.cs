using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int type = 0;
    GameController controller;

    private void Awake()
    {
        controller =GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        controller.PlayerWithIOS(type);
        // Xử lý khi con chuột đi vào đối tượng nút
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        controller.PlayerWithIOS(-type);
        // Xử lý khi con chuột rời khỏi đối tượng nút
    }
}
