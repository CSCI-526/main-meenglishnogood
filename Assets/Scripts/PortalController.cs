// using UnityEngine;
// using static Constants;
//
// public class PortalController : MonoBehaviour {
//
//     public PortalController otherPortal;
//     public float teleportYOffset;
//
//     void Start() {
//         UpdateColor();
//     }
//
//     public void Update() {
//         if (playerInPortal && otherPortal != null && FindObjectOfType<GameManager>().isTransitioning && !FindObjectOfType<GameManager>().isDay && mode == 1) {
//             TeleportPlayer();
//         } else if (playerInPortal && otherPortal != null && FindObjectOfType<GameManager>().isTransitioning && FindObjectOfType<GameManager>().isDay && mode == 2) {
//                 TeleportPlayer();
//         }
//     }
//
//     private void TeleportPlayer() {
//         var teleportPosition = otherPortal.transform.position + Vector3.up * teleportYOffset;
//         FindObjectOfType<PlayerController>().transform.position = teleportPosition;
//     }
// }
