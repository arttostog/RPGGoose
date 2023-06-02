using GooseShared;
using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace RPGGoose
{
    public class ModEntryPoint : IMod
    {
        public static Goose Goose;
        
        public static bool DisableMonstress = false;
        public static bool DisableSounds = false;

        void IMod.Init()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            SaveAndLoad.PathToSave = Path.Combine(assemblyFolder, "Save.json");

            Goose = SaveAndLoad.Load();

            string PicFileName = Path.Combine(assemblyFolder, "Monster.png");
            string SoundFileName = Path.Combine(assemblyFolder, "nom.wav");

            Config.PathToConfig = Path.Combine(assemblyFolder, "Config.txt");
            
            Draw.MonsterImage = Image.FromFile(PicFileName);
            Sounds.NomPlayer = new SoundPlayer(SoundFileName);

            Config.ParseConfig();

            if (!DisableMonstress)
            {
                for (int i = 0; i < 5; i++)
                {
                    Draw.Monstress.Add(new Monster());
                }
            }

            InjectionPoints.PreRenderEvent += Draw.PreRender;
            InjectionPoints.PostRenderEvent += Draw.PostRender;
            InjectionPoints.PreTickEvent += PreTick;
            Application.ApplicationExit += OnExit;

            ThreadPool.QueueUserWorkItem(Goose.Regeneration);
        }

        public static void PreTick(GooseEntity g)
        {
            foreach (Monster Monster in Draw.Monstress)
            {
                if (Monster.CheckCollision(g))
                {
                    AttackingTheMonsterTask.InFight = true;
                    Monster.Hp -= Goose.Damage;
                    Goose.Hp -= Monster.Damage;
                    if (Monster.Hp <= 0)
                    {
                        if (!DisableSounds) Sounds.NomPlayer.Play();
                        Goose.AddLevel(Monster);
                        Monster.Respawn();
                        AttackingTheMonsterTask.InFight = false;
                        continue;
                    }
                    if (Goose.Hp <= 0)
                    {
                        Goose.Respawn(g);
                        AttackingTheMonsterTask.InFight = false;
                    }
                    continue;
                }
                AttackingTheMonsterTask.InFight = false;
            }
        }

        public static void OnExit(object Sender, EventArgs EventArgs)
        {
            SaveAndLoad.Save();
        }
    }
}
