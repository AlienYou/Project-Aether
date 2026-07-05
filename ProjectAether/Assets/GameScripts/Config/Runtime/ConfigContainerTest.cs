using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using UnityEngine;

namespace ProjectAether.Config
{
    public class ConfigContainerTest : MonoBehaviour
    {
        void Start()
        {
            ConfigContainer<MonsterConfig> monsterConfigContainer = new ConfigContainer<MonsterConfig>();
            monsterConfigContainer.Add(new MonsterConfig { Id = 1, Name = "Goblin" });
            monsterConfigContainer.Add(new MonsterConfig { Id = 2, Name = "Orc" });
        
            MonsterConfig goblin = monsterConfigContainer.Get(1);
            MonsterConfig orc = monsterConfigContainer.Get(2);

            Log.Info($"Goblin: Id={goblin.Id}, Name={goblin.Name}");
            Log.Info($"Orc: Id={orc.Id}, Name={orc.Name}");
        }
    }
}