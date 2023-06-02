using GooseShared;
using SamEngine;

namespace RPGGoose
{
    class AttackingTheMonsterTask : GooseTaskInfo
    {
        public static bool InFight = false;

        public AttackingTheMonsterTask()
        {
            canBePickedRandomly = true;
            shortName = "Attacking the monsters";
            description = "";
            taskID = "AttackingTheMonster";
        }

        public class AttackingTheMonsterTaskData : GooseTaskData
        {
            public float timeStarted;
        }

        public override GooseTaskData GetNewTaskData(GooseEntity Goose)
        {
            AttackingTheMonsterTaskData taskData = new AttackingTheMonsterTaskData();
            taskData.timeStarted = Time.time;
            return taskData;
        }

        public override void RunTask(GooseEntity Goose)
        {
            if (!InFight)
            {
                AttackingTheMonsterTaskData data = (AttackingTheMonsterTaskData)Goose.currentTaskData;

                if (Time.time - data.timeStarted > 30 || Draw.Monstress[0] == null)
                {
                    API.Goose.setSpeed(Goose, GooseEntity.SpeedTiers.Walk);
                    API.Goose.setCurrentTaskByID(Goose, "Wander");
                    return;
                }

                Monster FinalTarget = Draw.Monstress[0];
                float FinalTargetDistance = float.MaxValue;

                foreach (Monster Monster in Draw.Monstress)
                {
                    float Distance = Vector2.Distance(Goose.position, new Vector2(Monster.Position.X, Monster.Position.Y));
                    if (Distance < FinalTargetDistance)
                    {
                        FinalTarget = Monster;
                        FinalTargetDistance = Distance;
                    }
                }

                API.Goose.setSpeed(Goose, GooseEntity.SpeedTiers.Charge);
                Goose.targetPos = new Vector2(FinalTarget.Position.X, FinalTarget.Position.Y);
            }
        }
    }
}
