using UnityEngine;
using UnityEngine.EventSystems;

// Phoebe Faith (1033478)

/// <summary>
/// Draggable functionality for UI elements.
/// </summary>
namespace PDebug
{
    public class DebugMenu_Draggable : MonoBehaviour, IDragHandler
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private RectTransform target;

        public void OnDrag(PointerEventData eventData)
        {
            if (Input.GetMouseButton(0))
            {
                if (target)
                {
                    target.anchoredPosition += eventData.delta / canvas.scaleFactor; // Move target based on drag delta
                }
                else
                {
                    (transform as RectTransform).anchoredPosition += eventData.delta / canvas.scaleFactor; // Move transform based on drag delta
                }
            }
        }
    }
}
