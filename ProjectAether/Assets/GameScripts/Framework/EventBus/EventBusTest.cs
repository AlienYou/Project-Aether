using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using ProjectAether.Framework;
using UnityEngine;

public class EventBusTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<PlayerDeadEvent>(OnPlayerDead);

        EventBus.Publish(new PlayerDeadEvent(1));
    }

    void OnPlayerDead(PlayerDeadEvent e)
    {
        Log.Info($"Player {e.PlayerId} is dead.");
    }
}
