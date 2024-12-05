using UnityEngine;
using TMPro;

// Phoebe Faith (1033478)

/// <summary>
/// Single purpose script to serve as "debug menu tiles"
/// </summary>

namespace PDebug
{
    public class DebugMenuTile : MonoBehaviour
    {
        [SerializeField] private TMP_Text header;
        [SerializeField] private TMP_Text body;

        public void SetHeader(string header)
        {
            this.header.SetText(header);
        }

        public void SetBody(string body)
        {
            this.body.SetText(body);
        }
    }
}    
