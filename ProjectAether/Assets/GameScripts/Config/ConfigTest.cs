using System.Collections;
using System.Collections.Generic;
using ProjectAether.Core;
using UnityEngine;

namespace ProjectAether.Config
{
    public class ConfigTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            ConfigManager.Initialize();

            List<MonsterConfig> monsterConfigs = new List<MonsterConfig>
            {
                new MonsterConfig { Id = 1, Name = "Goblin", Hp = 100 },
                new MonsterConfig { Id = 2, Name = "Orc", Hp = 200 },
                new MonsterConfig { Id = 3, Name = "Dragon", Hp = 1000 }
            };

            ConfigManager.Load(monsterConfigs);

            MonsterConfig monster = ConfigManager.Get<MonsterConfig>(2);

            Log.Info($"Monster ID: {monster.Id}, Name: {monster.Name}, HP: {monster.Hp}");
        
            SkillConfig skillConfig = new SkillConfig { Id = 1, Name = "Fireball", Damage = 50, Cooldown = 2.5f };
            ConfigManager.Load(new List<SkillConfig> { skillConfig });
        
            Log.Info($"Skill ID: {skillConfig.Id}, Name: {skillConfig.Name}, Damage: {skillConfig.Damage}, Cooldown: {skillConfig.Cooldown}");
        }
    }
}