using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AcidTrap : MonoBehaviour
{
    public Tilemap acidTilemap; // ������ �� Tilemap � ��������

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 playerPosition = collision.transform.position;
            Vector3Int gridPosition = acidTilemap.WorldToCell(playerPosition);
            TileBase tile = acidTilemap.GetTile(gridPosition);
            collision.GetComponent<HealthContoller>().SendMessage("Damage", new AttackDetails() { damageAmout = 5, position = transform.position });
        }
    }
}
