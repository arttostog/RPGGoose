using GooseShared;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RPGGoose
{
    class Draw
    {
        public static SolidBrush TextBrush = new SolidBrush(ColorTranslator.FromHtml("#4AF626"));
        public static SolidBrush FoneBrush = new SolidBrush(ColorTranslator.FromHtml("#000000"));

        public static int ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
        public static int ScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;

        public static List<Monster> Monstress = new List<Monster>();

        public static Image MonsterImage;

        public static void PreRender(GooseEntity Goose, Graphics Graph)
        {
            DrawMonstress(Goose, Graph);
        }

        public static void DrawMonstress(GooseEntity Goose, Graphics Graph)
        {
            foreach (Monster Monster in Monstress)
            {
                Point MonsterImagePoint = new Point((int)Monster.Position.X - MonsterImage.Width / 2, (int)Monster.Position.Y - MonsterImage.Height / 2);
                Graph.DrawImage(MonsterImage, MonsterImagePoint.X, MonsterImagePoint.Y, MonsterImage.Width, MonsterImage.Height);
            }
        }

        public static void PostRender(GooseEntity Goose, Graphics Graph)
        {
            DrawStats(Goose, Graph);
            DrawMonstressHpBars(Goose, Graph);
        }

        private static void DrawStats(GooseEntity Goose, Graphics Graph)
        {
            string Text = ModEntryPoint.Goose.SLevel + " - " + ModEntryPoint.Goose.Level + " lvl\n" +
                "HP: " + ModEntryPoint.Goose.Hp + "/" + ModEntryPoint.Goose.MaxHp + "\n" +
                "Damage:" + ModEntryPoint.Goose.Damage;
            Size TextLength = Graph.MeasureString(Text, SystemFonts.DefaultFont).ToSize();
            PointF TextPosition = new PointF(Goose.position.x - TextLength.Width - TextLength.Width / 2, Goose.position.y + 25);

            Graph.FillRectangle(FoneBrush, new Rectangle(Point.Round(TextPosition), TextLength));
            Graph.DrawString(Text, SystemFonts.DefaultFont, TextBrush, TextPosition);
        }

        public static void DrawMonstressHpBars(GooseEntity Goose, Graphics Graph)
        {
            foreach (Monster Monster in Monstress)
            {
                string HpBar = Monster.Hp + " HP";
                Size HpBarLength = Graph.MeasureString(HpBar, SystemFonts.DefaultFont).ToSize();
                PointF HpBarPosition = new PointF(Monster.Position.X - HpBarLength.Width / 2, Monster.Position.Y - MonsterImage.Height - 10);

                Graph.FillRectangle(FoneBrush, new Rectangle(Point.Round(HpBarPosition), HpBarLength));
                Graph.DrawString(HpBar, SystemFonts.DefaultFont, TextBrush, HpBarPosition);
            }
        }
    }
}
