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

            var monsterConfigContainer = new ConfigContainer<MonsterConfig>();
            monsterConfigContainer.Add(new MonsterConfig { Id = 1, Name = "Goblin", Hp = 100 });
            monsterConfigContainer.Add(new MonsterConfig { Id = 2, Name = "Orc", Hp = 200 });
            ConfigManager.RegisterTable(monsterConfigContainer);

            MonsterConfig monster = ConfigManager.Get<MonsterConfig>(2);

            Log.Info($"Monster ID: {monster.Id}, Name: {monster.Name}, HP: {monster.Hp}");

            var skillConfigContainer = new ConfigContainer<SkillConfig>();
            skillConfigContainer.Add(new SkillConfig { Id = 1, Name = "Fireball", Damage = 50, Cooldown = 2.5f });
            ConfigManager.RegisterTable(skillConfigContainer);
            var skill = ConfigManager.Get<SkillConfig>(1);
            Log.Info($"Skill ID: {skill.Id}, Name: {skill.Name}, Damage: {skill.Damage}, Cooldown: {skill.Cooldown}");
        }
    }
}