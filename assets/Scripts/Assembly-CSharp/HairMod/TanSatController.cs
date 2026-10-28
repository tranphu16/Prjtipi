
using System;
using System.Collections.Generic;
using System.Linq;
    // Token: 0x020000C7 RID: 199
    public class TanSatController
    {
        // Token: 0x06000A23 RID: 2595 RVA: 0x000A2324 File Offset: 0x000A0524
        public static void Update()
        {
            bool flag = TanSatController.IsWaiting();
            if (!flag)
            {
                global::Char @char = global::Char.myCharz();
                bool flag2 = @char.statusMe == 14 || @char.cHP <= 0;
                if (!flag2)
                {
                    bool flag3 = GameScr.hpPotion >= 1 && (@char.cHP <= @char.cHPFull * TanSatHandler.HpBuff / 100 || @char.cMP <= @char.cMPFull * TanSatHandler.MpBuff / 100);
                    if (flag3)
                    {
                        GameScr.gI().doUseHP();
                    }
                    bool flag4 = ItemTime.isExistItem(4387);
                    bool flag5 = GameCanvas.panel.indexMod[4] && flag4;
                    bool flag6 = TanSatHandler.IsAutoPickItems && !flag5;
                    if (flag6)
                    {
                        bool isPickingItems = TanSatController.IsPickingItems;
                        if (isPickingItems)
                        {
                            bool flag7 = TanSatController.IndexItemPick >= TanSatController.ItemPicks.Count;
                            if (flag7)
                            {
                                TanSatController.IsPickingItems = false;
                                return;
                            }
                            ItemMap itemMap = TanSatController.ItemPicks[TanSatController.IndexItemPick];
                            switch (TanSatController.GetTpyePickItem(itemMap))
                            {
                                case TanSatController.TpyePickItem.CanNotPickItem:
                                    TanSatController.IndexItemPick++;
                                    return;
                                case TanSatController.TpyePickItem.PickItemNormal:
                                    {
                                        Service.gI().pickItem(itemMap.itemMapID);
                                        bool isTanSat = GameCanvas.panel.indexMod[4];
                                        if (isTanSat)
                                        {
                                            itemMap.countAutoPick++;
                                            TanSatController.IndexItemPick++;
                                        }
                                        TanSatController.Wait((TanSatHandler.TimesAutoPickItemMax <= 1) ? 1 : 500);
                                        return;
                                    }
                                case TanSatController.TpyePickItem.PickItemTDLT:
                                    {
                                        Service.gI().pickItem(itemMap.itemMapID);
                                        bool isTanSat2 = GameCanvas.panel.indexMod[4];
                                        if (isTanSat2)
                                        {
                                            itemMap.countAutoPick++;
                                            TanSatController.IndexItemPick++;
                                        }
                                        TanSatController.Wait((TanSatHandler.TimesAutoPickItemMax <= 1) ? 1 : 500);
                                        return;
                                    }
                                case TanSatController.TpyePickItem.PickItemTanSat:
                                    Service.gI().pickItem(itemMap.itemMapID);
                                    TanSatController.Wait((TanSatHandler.TimesAutoPickItemMax <= 1) ? 1 : 500);
                                    return;
                            }
                        }
                        TanSatController.ItemPicks.Clear();
                        TanSatController.IndexItemPick = 0;
                        for (int i = 0; i < GameScr.vItemMap.size(); i++)
                        {
                            ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(i);
                            bool flag8 = TanSatController.GetTpyePickItem(itemMap2) > TanSatController.TpyePickItem.CanNotPickItem;
                            if (flag8)
                            {
                                TanSatController.ItemPicks.Add(itemMap2);
                            }
                        }
                        bool flag9 = TanSatController.ItemPicks.Count > 0;
                        if (flag9)
                        {
                            TanSatController.IsPickingItems = true;
                            return;
                        }
                    }
                    bool isTanSat3 = GameCanvas.panel.indexMod[4];
                    if (isTanSat3)
                    {
                        bool flag10 = global::Char.myCharz().cHP <= global::Char.myCharz().cHPFull * 10 / 100 || global::Char.myCharz().cMP <= global::Char.myCharz().cMPFull * 10 / 100;
                        if (flag10)
                        {
                            GameScr.gI().doUseHP();
                        }
                        bool isCharge = @char.isCharge;
                        if (isCharge)
                        {
                            TanSatController.Wait(GameCanvas.panel.indexMod[4] ? 1 : 500);
                        }
                        else
                        {
                            @char.clearFocus(0);
                            bool flag11 = @char.mobFocus != null && !TanSatController.IsMobTanSat(@char.mobFocus);
                            if (flag11)
                            {
                                @char.mobFocus = null;
                            }
                            bool flag12 = @char.mobFocus == null;
                            if (flag12)
                            {
                                @char.mobFocus = TanSatController.GetMobTanSat();
                                bool flag13 = flag4 && @char.mobFocus != null;
                                if (flag13)
                                {
                                   
                                        bool flag14 = global::Math.abs(@char.mobFocus.xFirst - @char.cx) >= 20 || global::Math.abs(@char.mobFocus.yFirst - @char.cy) >= 20;
                                        if (flag14)
                                        {
                                           GotoXY(@char.mobFocus.xFirst, @char.mobFocus.yFirst);
                                        }
                                    @char.cx = @char.mobFocus.xFirst;
                                    @char.cy = @char.mobFocus.yFirst;
                                    Service.gI().charMove();
                                }
                            }
                            bool flag15 = @char.mobFocus != null;
                            if (flag15)
                            {
                                bool flag16 = @char.skillInfoPaint() == null;
                                if (flag16)
                                {
                                    Skill skillAttack = TanSatController.GetSkillAttack();
                                    bool flag17 = skillAttack != null && !skillAttack.paintCanNotUseSkill;
                                    if (flag17)
                                    {
                                        Mob mobFocus = @char.mobFocus;
                                        mobFocus.x = mobFocus.xFirst;
                                        mobFocus.y = mobFocus.yFirst;
                                        bool flag18 = global::Char.myCharz().myskill != skillAttack;
                                        if (flag18)
                                        {
                                            GameScr.gI().doSelectSkill(skillAttack, true);
                                        }
                                        bool flag19 = Res.distance(mobFocus.xFirst, mobFocus.yFirst, @char.cx, @char.cy) <= 48;
                                        if (flag19)
                                        {
                                            bool flag20 = GameCanvas.gameTick % 50 == 0 && Mob.arrMobTemplate[global::Char.myCharz().mobFocus.templateId].type == 4;
                                            if (flag20)
                                            {
                                               GotoXY(mobFocus.xFirst, mobFocus.yFirst + 1);
                                            }
                                            bool flag21 = global::Char.myCharz().myskill.template.iconId == 539 || global::Char.myCharz().myskill.template.name.ToLower().Contains("liên hoàn") || global::Char.myCharz().myskill.template.iconId == 540;
                                            if (flag21)
                                            {
                                               Ak();
                                                return;
                                            }
                                            GameScr.gI().doDoubleClickToObj(mobFocus);
                                        }
                                        else
                                        {
                                                bool flag22 = global::Math.abs(mobFocus.xFirst - @char.cx) >= 20 || global::Math.abs(mobFocus.yFirst - @char.cy) >= 20;
                                                if (flag22)
                                                {
                                                   GotoXY(mobFocus.xFirst, mobFocus.yFirst);
                                                }
                                            
                                            TanSatController.Move(mobFocus.xFirst, mobFocus.yFirst);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                bool flag23 = !flag4;
                                if (flag23)
                                {
                                    Mob mobNext = TanSatController.GetMobNext();
                                    bool flag24 = mobNext != null;
                                    if (flag24)
                                    {
                                        global::Char.myCharz().focusManualTo(mobNext);
                                       {
                                            bool flag25 = global::Math.abs(mobNext.xFirst - @char.cx) >= 20 || global::Math.abs(mobNext.yFirst - @char.cy) >= 20;
                                            if (flag25)
                                            {
                                               GotoXY(mobNext.xFirst, mobNext.yFirst);
                                            }
                                            
                                        }
                                        TanSatController.Move(mobNext.xFirst, mobNext.yFirst);
                                    }
                                }
                            }
                            TanSatController.Wait(GameCanvas.panel.indexMod[4] ? 1 : 500);
                        }
                    }
                }
            }
        }

        // Token: 0x06000A24 RID: 2596 RVA: 0x000A2A14 File Offset: 0x000A0C14
        public static void Move(int x, int y)
        {
            global::Char @char = global::Char.myCharz();
            bool flag = !GameCanvas.panel.indexMod[6];
            if (flag)
            {
                @char.currentMovePoint = new MovePoint(x, y);
            }
            else
            {
                int[] pointYsdMax = TanSatController.GetPointYsdMax(@char.cx, x);
                bool flag2 = pointYsdMax[1] >= y || (pointYsdMax[1] >= @char.cy && (@char.statusMe == 2 || @char.statusMe == 1));
                if (flag2)
                {
                    pointYsdMax[0] = x;
                    pointYsdMax[1] = y;
                }
                @char.currentMovePoint = new MovePoint(pointYsdMax[0], pointYsdMax[1]);
            }
        }

        // Token: 0x06000A25 RID: 2597 RVA: 0x000A2A9C File Offset: 0x000A0C9C
        private static TanSatController.TpyePickItem GetTpyePickItem(ItemMap itemMap)
        {
            global::Char @char = global::Char.myCharz();
            bool flag = itemMap.playerId == @char.charID || itemMap.playerId == -1;
            bool flag2 = TanSatHandler.IsItemMe && !flag;
            TanSatController.TpyePickItem result;
            if (flag2)
            {
                result = TanSatController.TpyePickItem.CanNotPickItem;
            }
            else
            {
                bool flag3 = TanSatHandler.IsLimitTimesPickItem && itemMap.countAutoPick > TanSatHandler.TimesAutoPickItemMax;
                if (flag3)
                {
                    result = TanSatController.TpyePickItem.CanNotPickItem;
                }
                else
                {
                    bool flag4 = !TanSatController.FilterItemPick(itemMap);
                    if (flag4)
                    {
                        result = TanSatController.TpyePickItem.CanNotPickItem;
                    }
                    else
                    {
                        bool flag5 = Res.abs(@char.cx - itemMap.xEnd) < 60 && Res.abs(@char.cy - itemMap.yEnd) < 60;
                        if (flag5)
                        {
                            result = TanSatController.TpyePickItem.PickItemNormal;
                        }
                        else
                        {
                            bool flag6 = ItemTime.isExistItem(4387);
                            if (flag6)
                            {
                                result = TanSatController.TpyePickItem.PickItemTDLT;
                            }
                            else
                            {
                                bool isTanSat = GameCanvas.panel.indexMod[4];
                                if (isTanSat)
                                {
                                    result = TanSatController.TpyePickItem.PickItemTanSat;
                                }
                                else
                                {
                                    result = TanSatController.TpyePickItem.CanNotPickItem;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        // Token: 0x06000A26 RID: 2598 RVA: 0x000A2B80 File Offset: 0x000A0D80
        private static bool FilterItemPick(ItemMap itemMap)
        {
            return (TanSatHandler.IdItemPicks.Count == 0 || TanSatHandler.IdItemPicks.Contains(itemMap.template.id)) && (TanSatHandler.IdItemBlocks.Count == 0 || !TanSatHandler.IdItemBlocks.Contains(itemMap.template.id)) && (TanSatHandler.TypeItemPicks.Count == 0 || TanSatHandler.TypeItemPicks.Contains(itemMap.template.type)) && (TanSatHandler.TypeItemBlock.Count == 0 || !TanSatHandler.TypeItemBlock.Contains(itemMap.template.type));
        }

        // Token: 0x06000A27 RID: 2599 RVA: 0x000A2C28 File Offset: 0x000A0E28
        private static Mob GetMobTanSat()
        {
            Mob result = null;
            int num = int.MaxValue;
            global::Char @char = global::Char.myCharz();
            for (int i = 0; i < GameScr.vMob.size(); i++)
            {
                Mob mob = (Mob)GameScr.vMob.elementAt(i);
                int num2 = (mob.xFirst - @char.cx) * (mob.xFirst - @char.cx) + (mob.yFirst - @char.cy) * (mob.yFirst - @char.cy);
                bool flag = TanSatController.IsMobTanSat(mob) && num2 < num;
                if (flag)
                {
                    result = mob;
                    num = num2;
                }
            }
            return result;
        }

        // Token: 0x06000A28 RID: 2600 RVA: 0x000A2CDC File Offset: 0x000A0EDC
        private static Mob GetMobNext()
        {
            Mob result = null;
            long num = mSystem.currentTimeMillis();
            for (int i = 0; i < GameScr.vMob.size(); i++)
            {
                Mob mob = (Mob)GameScr.vMob.elementAt(i);
                bool flag = TanSatController.IsMobNext(mob) && mob.timeLastDie < num;
                if (flag)
                {
                    result = mob;
                    num = mob.timeLastDie;
                }
            }
            return result;
        }

        // Token: 0x06000A29 RID: 2601 RVA: 0x000A2D4C File Offset: 0x000A0F4C
        private static bool IsMobTanSat(Mob mob)
        {
            bool flag = mob.status == 0 || mob.status == 1 || mob.hp <= 0 || mob.isMobMe;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = GameCanvas.panel.indexMod[5] && !ItemTime.isExistItem(4387);
                result = ((mob.levelBoss == 0 || !flag2) && TanSatController.FilterMobTanSat(mob));
            }
            return result;
        }

        // Token: 0x06000A2A RID: 2602 RVA: 0x000A2DB8 File Offset: 0x000A0FB8
        private static bool IsMobNext(Mob mob)
        {
            bool isMobMe = mob.isMobMe;
            bool result;
            if (isMobMe)
            {
                result = false;
            }
            else
            {
                bool flag = !TanSatController.FilterMobTanSat(mob);
                if (flag)
                {
                    result = false;
                }
                else
                {
                    bool flag2 = GameCanvas.panel.indexMod[5] && !ItemTime.isExistItem(4387) && mob.getTemplate().hp >= 3000;
                    if (flag2)
                    {
                        bool flag3 = mob.levelBoss != 0;
                        if (flag3)
                        {
                            Mob mob2 = null;
                            bool flag4 = false;
                            for (int i = 0; i < GameScr.vMob.size(); i++)
                            {
                                mob2 = (Mob)GameScr.vMob.elementAt(i);
                                bool flag5 = mob2.countDie == 10 && (mob2.status == 0 || mob2.status == 1);
                                if (flag5)
                                {
                                    flag4 = true;
                                    break;
                                }
                            }
                            bool flag6 = !flag4;
                            if (flag6)
                            {
                                return false;
                            }
                            mob.timeLastDie = mob2.timeLastDie;
                        }
                        else
                        {
                            bool flag7 = mob.countDie == 10 && (mob.status == 0 || mob.status == 1);
                            if (flag7)
                            {
                                return false;
                            }
                        }
                    }
                    result = true;
                }
            }
            return result;
        }

        // Token: 0x06000A2B RID: 2603 RVA: 0x000A2EF0 File Offset: 0x000A10F0
        private static bool FilterMobTanSat(Mob mob)
        {
            return (TanSatHandler.IdMobsTanSat.Count == 0 || TanSatHandler.IdMobsTanSat.Contains(mob.mobId)) && (TanSatHandler.TypeMobsTanSat.Count == 0 || TanSatHandler.TypeMobsTanSat.Contains(mob.templateId));
        }

        public static long getTimeSkill(Skill s)
        {
            long result;
            if (s.template.id == 29 || s.template.id == 22 || s.template.id == 7 || s.template.id == 18 || s.template.id == 23)
            {
                result = (long)s.coolDown + 500L;
            }
            else
            {
                long num = (long)((double)s.coolDown * 1.2);
                if (num < 406L)
                {
                    result = 406L;
                }
                else
                {
                    result = num;
                }
            }
            return result;
        }

        // Token: 0x06000A2C RID: 2604 RVA: 0x000A2F44 File Offset: 0x000A1144
        private static Skill GetSkillAttack()
        {
            Skill skill = null;
            SkillTemplate skillTemplate = new SkillTemplate();
            foreach (sbyte id in TanSatHandler.IdSkillsTanSat)
            {
                skillTemplate.id = id;
                Skill skill2 = global::Char.myCharz().getSkill(skillTemplate);
                bool flag = TanSatController.IsSkillBetter(skill2, skill);
                if (flag)
                {
                    skill = skill2;
                }
            }
            return skill;
        }

        public static void Ak()
        {
            if (!global::Char.myCharz().stone && !global::Char.isLoadingMap && !global::Char.myCharz().meDead && global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5 && global::Char.myCharz().myskill.template.type != 3 && global::Char.myCharz().myskill.template.id != 10 && global::Char.myCharz().myskill.template.id != 11 && !global::Char.myCharz().myskill.paintCanNotUseSkill)
            {
                int skill =getSkill();
                if (mSystem.currentTimeMillis() -currTimeAK[skill] >getTimeSkill(global::Char.myCharz().myskill))
                {
                    if (GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus) && (double)Res.abs(global::Char.myCharz().mobFocus.xFirst - global::Char.myCharz().cx) < (double)global::Char.myCharz().myskill.dx * 1.5)
                    {
                        global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
                       AkMob();
                       currTimeAK[skill] = mSystem.currentTimeMillis();
                        return;
                    }
                    if (global::Char.myCharz().charFocus != null && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus) && (double)Res.abs(global::Char.myCharz().charFocus.cx - global::Char.myCharz().cx) < (double)global::Char.myCharz().myskill.dx * 1.5)
                    {
                        global::Char.myCharz().myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
                       AkChar();
                       currTimeAK[skill] = mSystem.currentTimeMillis();
                    }
                }
            }
        }
        public static long[] currTimeAK = new long[8];
        private static int getSkill()
        {
            for (int i = 0; i < GameScr.keySkill.Length; i++)
            {
                if (GameScr.keySkill[i] == global::Char.myCharz().myskill)
                {
                    return i;
                }
            }
            return 0;
        }
        public static void AkMob()
        {
            try
            {
                MyVector myVector = new MyVector();
                myVector.addElement(global::Char.myCharz().mobFocus);
                Service.gI().sendPlayerAttack(myVector, new MyVector(), 1);
                global::Char.myCharz().cMP -= global::Char.myCharz().myskill.manaUse;
            }
            catch
            {
            }
        }
        public static void AkChar()
        {
            try
            {
                MyVector myVector = new MyVector();
                myVector.addElement(global::Char.myCharz().charFocus);
                Service.gI().sendPlayerAttack(new MyVector(), myVector, 2);
                global::Char.myCharz().cMP -= global::Char.myCharz().myskill.manaUse;
            }
            catch
            {
            }
        }
        private static bool IsSkillBetter(Skill SkillBetter, Skill skill)
        {
            bool flag = SkillBetter == null;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = !TanSatController.CanUseSkill(SkillBetter);
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    bool flag3 = (SkillBetter.template.id == 17 && skill.template.id == 2) || (SkillBetter.template.id == 9 && skill.template.id == 0);
                    result = (skill == null || skill.coolDown < SkillBetter.coolDown || flag3);
                }
            }
            return result;
        }

        // Token: 0x06000A2E RID: 2606 RVA: 0x000A3054 File Offset: 0x000A1254
        private static bool CanUseSkill(Skill skill)
        {
            bool flag = mSystem.currentTimeMillis() - skill.lastTimeUseThisSkill > (long)skill.coolDown + 50L;
            if (flag)
            {
                skill.paintCanNotUseSkill = false;
            }
            return (!skill.paintCanNotUseSkill || TanSatController.IdSkillsMelee.Contains(skill.template.id)) && !TanSatController.IdSkillsCanNotAttack.Contains(skill.template.id) && global::Char.myCharz().cMP >= TanSatController.GetManaUseSkill(skill);
        }

        // Token: 0x06000A2F RID: 2607 RVA: 0x000A30DC File Offset: 0x000A12DC
        private static long GetManaUseSkill(Skill skill)
        {
            bool flag = skill.template.manaUseType == 2;
            long result;
            if (flag)
            {
                result = 1;
            }
            else
            {
                bool flag2 = skill.template.manaUseType == 1;
                if (flag2)
                {
                    result = skill.manaUse * global::Char.myCharz().cMPFull / 100;
                }
                else
                {
                    result = skill.manaUse;
                }
            }
            return result;
        }

        // Token: 0x06000A30 RID: 2608 RVA: 0x000A3138 File Offset: 0x000A1338
        public static int GetYsd(int xsd)
        {
            global::Char @char = global::Char.myCharz();
            int num = TileMap.pxh;
            int result = -1;
            for (int i = 24; i < TileMap.pxh; i += 24)
            {
                bool flag = TileMap.tileTypeAt(xsd, i, 2);
                if (flag)
                {
                    int num2 = Res.abs(i - @char.cy);
                    bool flag2 = num2 < num;
                    if (flag2)
                    {
                        num = num2;
                        result = i;
                    }
                }
            }
            return result;
        }

        // Token: 0x06000A31 RID: 2609 RVA: 0x000A31A8 File Offset: 0x000A13A8
        private static int[] GetPointYsdMax(int xStart, int xEnd)
        {
            int num = TileMap.pxh;
            int num2 = -1;
            bool flag = xStart > xEnd;
            if (flag)
            {
                for (int i = xEnd; i < xStart; i += 24)
                {
                    int ysd = TanSatController.GetYsd(i);
                    bool flag2 = ysd < num;
                    if (flag2)
                    {
                        num = ysd;
                        num2 = i;
                    }
                }
            }
            else
            {
                for (int j = xEnd; j > xStart; j -= 24)
                {
                    int ysd2 = TanSatController.GetYsd(j);
                    bool flag3 = ysd2 < num;
                    if (flag3)
                    {
                        num = ysd2;
                        num2 = j;
                    }
                }
            }
            return new int[]
            {
                num2,
                num
            };
        }

        // Token: 0x06000A32 RID: 2610 RVA: 0x000065D6 File Offset: 0x000047D6
        public static void Wait(int time)
        {
            TanSatController.IsWait = true;
            TanSatController.TimeStartWait = mSystem.currentTimeMillis();
            TanSatController.TimeWait = (long)time;
        }

        // Token: 0x06000A33 RID: 2611 RVA: 0x000A3244 File Offset: 0x000A1444
        public static bool IsWaiting()
        {
            bool flag = TanSatController.IsWait && mSystem.currentTimeMillis() - TanSatController.TimeStartWait >= TanSatController.TimeWait;
            if (flag)
            {
                TanSatController.IsWait = false;
            }
            return TanSatController.IsWait;
        }

        // Token: 0x04001314 RID: 4884
        private const int TIME_REPICKITEM = 500;

        // Token: 0x04001315 RID: 4885
        private const int TIME_DELAY_TANSAT = 0;

        // Token: 0x04001316 RID: 4886
        private const int ID_ICON_ITEM_TDLT = 4387;

        // Token: 0x04001317 RID: 4887
        private static readonly sbyte[] IdSkillsMelee = new sbyte[]
        {
            0,
            9,
            2,
            17,
            4
        };

        // Token: 0x04001318 RID: 4888
        private static readonly sbyte[] IdSkillsCanNotAttack = new sbyte[]
        {
            10,
            11,
            14,
            23,
            7
        };

        // Token: 0x04001319 RID: 4889
        private static readonly TanSatController _Instance = new TanSatController();

        // Token: 0x0400131A RID: 4890
        public static bool IsPickingItems;

        // Token: 0x0400131B RID: 4891
        private static bool IsWait;

        // Token: 0x0400131C RID: 4892
        private static long TimeStartWait;

        // Token: 0x0400131D RID: 4893
        private static long TimeWait;

        // Token: 0x0400131E RID: 4894
        public static List<ItemMap> ItemPicks = new List<ItemMap>();

        // Token: 0x0400131F RID: 4895
        private static int IndexItemPick = 0;
        public static void GotoXY(int x, int y)
        {
            global::Char.myCharz().cx = x;
            global::Char.myCharz().cy = y;
            Service.gI().charMove();
        }
        // Token: 0x020000C8 RID: 200
        private enum TpyePickItem
        {
            // Token: 0x04001321 RID: 4897
            CanNotPickItem,
            // Token: 0x04001322 RID: 4898
            PickItemNormal,
            // Token: 0x04001323 RID: 4899
            PickItemTDLT,
            // Token: 0x04001324 RID: 4900
            PickItemTanSat
        }
    }

