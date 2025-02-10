using UnityEngine;

namespace Scene_Teleportation_Kit.Scripts.teleport
{
    public class PortTrigger : MonoBehaviour {
  public string destinationScene;
        public string destinationSpawnName;

        private void OnTriggerEnter(Collider collider) {
    Debug.Log("Jugador ha entrado al portal"); // Agregar esta línea para depuración

    var teleportable = collider.GetComponent<Teleportable>();
    if (teleportable != null) {
        Debug.Log("Se detectó un objeto teleportable");
        Teleporter teleporter = new Teleporter {
            destinationScene = destinationScene,
            destSpawnName = destinationSpawnName
        };
        teleporter.TeleportPlayer(teleportable);
    }
}
    }
}
