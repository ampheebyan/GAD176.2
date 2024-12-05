using UnityEngine;
using UnityEngine.EventSystems;

// Phoebe Faith (1033478)

/// <summary>
/// Draggable functionality for UI elements.
/// </summary>
namespace PDebug
{
    public class DebugMenu_Draggable : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private bool _lock = true;

        [SerializeField] private Canvas canvas;
        [SerializeField] private RectTransform target;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _lock = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _lock = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Input.GetMouseButton(0))
            {
                if (target)
                {
                    target.anchoredPosition += eventData.delta / canvas.scaleFactor;
                }
                else
                {
                    (transform as RectTransform).anchoredPosition += eventData.delta / canvas.scaleFactor;
                }
            }
        }
    }
}
