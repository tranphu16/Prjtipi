using System;
using System.Collections.Generic;

    // Token: 0x020000C7 RID: 199
    public class SuicideRange
    {
        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000B17 RID: 2839 RVA: 0x0009E315 File Offset: 0x0009C515
        // (set) Token: 0x06000B18 RID: 2840 RVA: 0x0009E31C File Offset: 0x0009C51C
        public static List<IMapObject> mapObjsInRange { get; private set; } = new List<IMapObject>();

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000B19 RID: 2841 RVA: 0x0009E324 File Offset: 0x0009C524
        // (set) Token: 0x06000B1A RID: 2842 RVA: 0x0009E32B File Offset: 0x0009C52B
        public static List<IMapObject> mapObjsInMyRange { get; private set; } = new List<IMapObject>();

        // Token: 0x06000B1B RID: 2843 RVA: 0x0009E334 File Offset: 0x0009C534
        public static void update()
        {
            SuicideRange.mapObjsInRange.Clear();
            SuicideRange.mapObjsInMyRange.Clear();
            if (!Char.myCharz().isDie && Char.myCharz().myskill == Char.myCharz().getSkill(Char.myCharz().nClass.skillTemplates[4]) && Char.myCharz().cgender == 2)
            {
                SuicideRange.FindMapObjInRange(Char.myCharz());
            }
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char @char = GameScr.vCharInMap.elementAt(i) as Char;
                if (@char.isStandAndCharge)
                {
                    SuicideRange.FindMapObjInRange(@char);
                    if (SuicideRange.getDistance(Char.myCharz(), @char) <= @char.getSuicideRange() && !SuicideRange.mapObjsInRange.Contains(Char.myCharz()))
                    {
                        SuicideRange.mapObjsInRange.Add(Char.myCharz());
                    }
                }
            }
        }

        // Token: 0x06000B1C RID: 2844 RVA: 0x0009E410 File Offset: 0x0009C610
      

        // Token: 0x06000B1D RID: 2845 RVA: 0x000037D7 File Offset: 0x000019D7
        private static void paintMapObjInRange(mGraphics g, IMapObject mapObject)
        {
        }

        // Token: 0x06000B1E RID: 2846 RVA: 0x0009E44C File Offset: 0x0009C64C
        private static void FindMapObjInRange(Char suicidingChar)
        {
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char @char = GameScr.vCharInMap.elementAt(i) as Char;
                if (SuicideRange.getDistance(@char, suicidingChar) <= suicidingChar.getSuicideRange() && ((suicidingChar.cFlag != 0 && @char.cFlag != 0 && (@char.cFlag != suicidingChar.cFlag || (@char.cFlag == 8 && suicidingChar.cFlag == 8))) || suicidingChar.cTypePk == 5 || @char.cTypePk == 5))
                {
                    if (suicidingChar.me)
                    {
                        if (!SuicideRange.mapObjsInMyRange.Contains(@char))
                        {
                            SuicideRange.mapObjsInMyRange.Add(@char);
                        }
                    }
                    else if (!SuicideRange.mapObjsInRange.Contains(@char))
                    {
                        SuicideRange.mapObjsInRange.Add(@char);
                    }
                }
            }
            for (int j = 0; j < GameScr.vMob.size(); j++)
            {
                Mob mob = GameScr.vMob.elementAt(j) as Mob;
                if (SuicideRange.getDistance(mob, suicidingChar) <= suicidingChar.getSuicideRange() && !SuicideRange.mapObjsInRange.Contains(mob) && !mob.isMobMe)
                {
                    if (suicidingChar.me)
                    {
                        if (!SuicideRange.mapObjsInMyRange.Contains(mob))
                        {
                            SuicideRange.mapObjsInMyRange.Add(mob);
                        }
                    }
                    else if (!SuicideRange.mapObjsInRange.Contains(mob))
                    {
                        SuicideRange.mapObjsInRange.Add(mob);
                    }
                }
            }
        }

        // Token: 0x06000B1F RID: 2847 RVA: 0x0009E594 File Offset: 0x0009C794
        public static int getDistance(IMapObject mapObject1, IMapObject mapObject2)
        {
            return Res.distance(mapObject1.getX(), mapObject1.getY(), mapObject2.getX(), mapObject2.getY());
        }
    }
