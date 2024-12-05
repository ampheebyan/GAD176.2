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

        [SerializeField] private Transform target;
        
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
            if (_lock) return;
            if (Input.GetMouseButton(0))
            {
                if (target)
                {
                    target.position = eventData.position;
                }
                else
                {
                    transform.position = eventData.position;
                }
            }
        }
    }
}
