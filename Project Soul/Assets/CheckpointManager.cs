using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private void Start()
    {
        string[] checkpointNames = {
            "lamppole_checkpoint_1",
            "lamppole_checkpoint_2",
            "lamppole_checkpoint_3",
            "lamppole_checkpoint_4",
            "lamppole_checkpoint_5",
            "lamppole_checkpoint_6",
            "lamppole_checkpoint_7",
            "lamppole_checkpoint_8",
            "lamppole_checkpoint_9"
        };

        foreach (string checkpointName in checkpointNames)
        {
            string checkpointKey = checkpointName + "_Activated";
            if (PlayerPrefs.HasKey(checkpointKey))
            {
                PlayerPrefs.DeleteKey(checkpointKey);
                Debug.Log("Deleted checkpoint key: " + checkpointKey);
            }
        }

        PlayerPrefs.Save(); // Сохраняем изменения
        Debug.Log("All checkpoint data deleted in the new scene.");
    }
}
