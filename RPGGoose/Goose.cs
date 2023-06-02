using GooseShared;
using SamEngine;
using System;
using System.Threading;

namespace RPGGoose
{
    public class Goose
    {
        public enum SkillsLevel
        {
            Weak,
            Average,
            Strong,
            Imbalance
        }

        public byte Level;
        public SkillsLevel SLevel;
        public int Hp;
        public int MaxHp;
        public int Damage;

        public Goose()
        {
            this.Level = 0;
            this.SLevel = SkillsLevel.Weak;
            this.Hp = 360;
            this.MaxHp = this.Hp;
            this.Damage = 1;
        }

        public Goose(byte Level, SkillsLevel SLevel, int Hp, int MaxHp, int Damage)
        {
            this.Level = Level;
            this.SLevel = SLevel;
            this.Hp = Hp;
            this.MaxHp = MaxHp;
            this.Damage = Damage;
        }

        public void AddLevel(Monster Monster)
        {
            if (this.Level < 255)
            {
                switch (Monster.MType)
                {
                    case Monster.MonsterType.Much_Worse_Than_Weak: this.Level++; break;
                    case Monster.MonsterType.Worse_Weak: this.Level++; break;
                    case Monster.MonsterType.Weak: this.Level++; break;
                    case Monster.MonsterType.Much_Worse_Than_Average: this.Level += 2; break;
                    case Monster.MonsterType.Worse_Than_Average: this.Level += 2; break;
                    case Monster.MonsterType.Average: this.Level += 2; break;
                    case Monster.MonsterType.Much_Worse_Than_Strong: this.Level += 3; break;
                    case Monster.MonsterType.Worse_Than_Strong: this.Level += 3; break;
                    case Monster.MonsterType.Strong: this.Level += 3; break;
                    case Monster.MonsterType.Imbalance: this.Level += 4; break;
                }
                this.CheckLevel();
            }
        }

        private void CheckLevel()
        {
            switch (this.Level)
            {
                case 75:
                    Console.WriteLine("[RPGGoose] Goose Skills Level - Normal");
                    this.SLevel = SkillsLevel.Average;
                    this.Hp *= 2;
                    this.Damage *= 2;
                    break;
                case 150:
                    Console.WriteLine("[RPGGoose] Goose Skills Level - Hard");
                    this.SLevel = SkillsLevel.Strong;
                    this.Hp *= 2;
                    this.Damage *= 2;
                    break;
                case 225:
                    Console.WriteLine("[RPGGoose] Goose Skills Level - Imbalance");
                    this.SLevel = SkillsLevel.Imbalance;
                    this.Hp *= 2;
                    this.Damage *= 2;
                    break;
            }
        }

        public void Respawn (GooseEntity Goose)
        {
            this.Hp = this.MaxHp;
            Goose.position = new Vector2(100, 100);
        }

        public static void Regeneration(object State)
        {
            while (true)
            {
                while (!AttackingTheMonsterTask.InFight)
                {
                    if (ModEntryPoint.Goose.Hp < ModEntryPoint.Goose.MaxHp)
                    {
                        int AddHp = ModEntryPoint.Goose.MaxHp / 100 * 10;
                        if (ModEntryPoint.Goose.Hp + AddHp <= ModEntryPoint.Goose.MaxHp)
                        {
                            ModEntryPoint.Goose.Hp += AddHp;
                        }
                        else
                        {
                            ModEntryPoint.Goose.Hp += ModEntryPoint.Goose.MaxHp - ModEntryPoint.Goose.Hp;
                        }
                        Thread.Sleep(1000);
                    }
                }
            }
        }
    }
}
