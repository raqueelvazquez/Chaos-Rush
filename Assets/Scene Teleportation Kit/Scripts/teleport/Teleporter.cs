using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene_Teleportation_Kit.Scripts.teleport
{
    public class Teleporter : MonoBehaviour {
       public string destinationScene;  // Ahora es un string en vez de Object
        public string destSpawnName;

        public void TeleportPlayer(Teleportable teleportable) {
            if (!teleportable.canTeleport) return;

            teleportable.canTeleport = false;
            StartCoroutine(TeleportToNewScene(destinationScene, teleportable));
        }

        private IEnumerator TeleportToNewScene(string sceneName, Teleportable teleportable) {
            // Cargar la nueva escena sin cerrar la actual
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            while (!asyncLoad.isDone) {
                yield return null;
            }

            // Una vez cargada, busca el SpawnPoint
            SpawnPoint spawnPoint = FindSpawnPoint(destSpawnName);
            if (spawnPoint != null) {
                teleportable.transform.position = spawnPoint.transform.position;
            }

            teleportable.canTeleport = true;
        }

        private SpawnPoint FindSpawnPoint(string spawnName) {
    SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
    foreach (SpawnPoint spawn in spawnPoints) {
        Debug.Log("Encontrado SpawnPoint: " + spawn.spawnName); // Agregar para depuración
        if (spawn.spawnName == spawnName) {
            return spawn;
        }
    }
    Debug.Log("No se encontró el SpawnPoint con nombre: " + spawnName);
    return null;
}
    }
}