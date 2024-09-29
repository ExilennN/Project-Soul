using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AcidTrap : MonoBehaviour
{
    public Tilemap acidTilemap; // —сылка на Tilemap с кислотой

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 playerPosition = collision.transform.position;
            Vector3Int gridPosition = acidTilemap.WorldToCell(playerPosition);
            TileBase tile = acidTilemap.GetTile(gridPosition);
                Debug.Log("[ACIDTRAP] Player hit! Damage dealt.");
        }
    }
}
