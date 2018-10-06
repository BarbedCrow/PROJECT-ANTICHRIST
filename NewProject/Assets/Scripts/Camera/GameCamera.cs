using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{

    public void SetPlayer(Transform player)
    {
        this.player = player;
    }

    #region private

    Transform player;

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
    }

    #endregion
}
