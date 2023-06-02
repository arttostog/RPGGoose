using GooseShared;
using SamEngine;
using System.Drawing;

namespace RPGGoose
{
    public class Monster
    {
        public enum MonsterType
        {
            Much_Worse_Than_Weak,
            Worse_Weak,
            Weak,
            Much_Worse_Than_Average,
            Worse_Than_Average,
            Average,
            Much_Worse_Than_Strong,
            Worse_Than_Strong,
            Strong,
            Imbalance
        }

        public MonsterType MType;
        public int Hp;
        public int Damage;
        public SolidBrush Brush;
        public PointF Position;

        public Monster()
        {
            this.GetNewMonster();
        }

        private void GetNewMonster()
        {
            this.MType = (MonsterType)SamMath.RandomRange(0, 9);

            switch (this.MType)
            {
                case MonsterType.Much_Worse_Than_Weak: this.Damage = 1; this.Hp = 120; this.Brush = new SolidBrush(ColorTranslator.FromHtml("#FF0000")); break;
                case MonsterType.Worse_Weak: this.Damage = 1; this.Hp = 240; this.Brush = new SolidBrush(ColorTranslator.FromHtml("#E60000")); break;
                case MonsterType.Weak: this.Damage = 1; this.Hp = 360; this.Brush = new SolidBrush(ColorTranslator.FromHtml("#CC0000")); break;
                case MonsterType.Much_Worse_Than_Average: this.Damage = 2; this.Hp = 480; this.Brush = new SolidBrush(ColorTranslator.FromHtml("#1A0000")); break;
                case MonsterType.Worse_Than_Average: this.Damage = 2; this.Hp = 600; this.Brush = new SolidBrush(ColorTranslator.FromHtml("#990000")); break;
                case MonsterType.Average: this.Hp = 720; this.Damage = 3; this.Brush = new SolidBrush(ColorTranslator.FromHtml("#800000")); break;
                case MonsterType.Much_Worse_Than_Strong: this.Damage = 3; this.Hp = 840; this.Brush = new SolidBrush(ColorTranslator.FromHtml("#660000")); break;
                case MonsterType.Worse_Than_Strong: this.Damage = 4; this.Hp = 960; this.Brush = new SolidBrush(ColorTranslator.FromHtml("#4D0000")); break;
                case MonsterType.Strong: this.Damage = 4; this.Hp = 1080; this.Brush = new SolidBrush(ColorTranslator.FromHtml("#330000")); break;
                case MonsterType.Imbalance: this.Damage = 5; this.Hp = 1200; this.Brush = new SolidBrush(ColorTranslator.FromHtml("#1A0000")); break;
            }
            this.Position = new PointF(SamMath.RandomRange(64, Draw.ScreenWidth - 64), SamMath.RandomRange(32, Draw.ScreenHeight -32));
        }

        public bool CheckCollision(GooseEntity Goose)
        {
            if (Goose.position.x >= this.Position.X - Draw.MonsterImage.Width && Goose.position.x <= this.Position.X + Draw.MonsterImage.Width &&
                Goose.position.y >= this.Position.Y - Draw.MonsterImage.Height && Goose.position.y <= this.Position.Y + Draw.MonsterImage.Height) return true;

            return false;
        }

        public void Respawn()
        {
            this.GetNewMonster();
        }
    }
}
