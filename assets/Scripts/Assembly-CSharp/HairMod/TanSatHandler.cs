
using System;
using System.Collections.Generic;
using System.Text;

    // Token: 0x020000C9 RID: 201
    public class TanSatHandler
    {
        // Token: 0x06000A36 RID: 2614 RVA: 0x000A32DC File Offset: 0x000A14DC
        public static bool Chat(string text)
        {
            bool flag = text == "add";
            if (flag)
            {
                Mob mobFocus = global::Char.myCharz().mobFocus;
                ItemMap itemFocus = global::Char.myCharz().itemFocus;
                bool flag2 = mobFocus != null;
                if (flag2)
                {
                    bool flag3 = TanSatHandler.IdMobsTanSat.Contains(mobFocus.mobId);
                    if (flag3)
                    {
                        TanSatHandler.IdMobsTanSat.Remove(mobFocus.mobId);
                        GameScr.info1.addInfo("Đã xoá mob: " + mobFocus.mobId.ToString(), 0);
                    }
                    else
                    {
                        TanSatHandler.IdMobsTanSat.Add(mobFocus.mobId);
                        GameScr.info1.addInfo("Đã thêm mob: " + mobFocus.mobId.ToString(), 0);
                    }
                }
                else
                {
                    bool flag4 = itemFocus != null;
                    if (flag4)
                    {
                        bool flag5 = TanSatHandler.IdItemPicks.Contains(itemFocus.template.id);
                        if (flag5)
                        {
                            TanSatHandler.IdItemPicks.Remove(itemFocus.template.id);
                            GameScr.info1.addInfo(string.Format("Đã xoá khỏi danh sách chỉ tự động nhặt item: {0}[{1}]", itemFocus.template.name, itemFocus.template.id), 0);
                        }
                        else
                        {
                            TanSatHandler.IdItemPicks.Add(itemFocus.template.id);
                            GameScr.info1.addInfo(string.Format("Đã thêm vào danh sách chỉ tự động nhặt item: {0}[{1}]", itemFocus.template.name, itemFocus.template.id), 0);
                        }
                    }
                    else
                    {
                        GameScr.info1.addInfo("Cần trỏ vào quái hay vật phẩm cần thêm vào danh sách", 0);
                    }
                }
            }
            else
            {
                bool flag6 = text == "addd";
                if (flag6)
                {
                    Mob mobFocus2 = global::Char.myCharz().mobFocus;
                    ItemMap itemFocus2 = global::Char.myCharz().itemFocus;
                    bool flag7 = mobFocus2 != null;
                    if (flag7)
                    {
                        bool flag8 = TanSatHandler.TypeMobsTanSat.Contains(mobFocus2.templateId);
                        if (flag8)
                        {
                            TanSatHandler.TypeMobsTanSat.Remove(mobFocus2.templateId);
                            GameScr.info1.addInfo(string.Format("Đã xoá loại mob: {0}[{1}]", mobFocus2.getTemplate().name, mobFocus2.templateId), 0);
                        }
                        else
                        {
                            TanSatHandler.TypeMobsTanSat.Add(mobFocus2.templateId);
                            GameScr.info1.addInfo(string.Format("Đã thêm loại mob: {0}[{1}]", mobFocus2.getTemplate().name, mobFocus2.templateId), 0);
                        }
                    }
                    else
                    {
                        bool flag9 = itemFocus2 != null;
                        if (flag9)
                        {
                            bool flag10 = TanSatHandler.TypeItemPicks.Contains(itemFocus2.template.type);
                            if (flag10)
                            {
                                TanSatHandler.TypeItemPicks.Remove(itemFocus2.template.type);
                                GameScr.info1.addInfo("Đã xoá khỏi danh sách chỉ tự động nhặt loại item:" + itemFocus2.template.type.ToString(), 0);
                            }
                            else
                            {
                                TanSatHandler.TypeItemPicks.Add(itemFocus2.template.type);
                                GameScr.info1.addInfo("Đã thêm vào danh sách chỉ tự động nhặt loại item:" + itemFocus2.template.type.ToString(), 0);
                            }
                        }
                        else
                        {
                            GameScr.info1.addInfo("Cần trỏ vào quái hay vật phẩm cần thêm vào danh sách", 0);
                        }
                    }
                }
                else
                {
                    bool flag11 = text == "anhat";
                    if (flag11)
                    {
                        TanSatHandler.IsAutoPickItems = !TanSatHandler.IsAutoPickItems;
                        GameScr.info1.addInfo("Auto nhặt của tàn sát: " + (TanSatHandler.IsAutoPickItems ? "Bật" : "Tắt"), 0);
                    }
                    else
                    {
                        bool flag12 = text == "itm";
                        if (flag12)
                        {
                            TanSatHandler.IsItemMe = !TanSatHandler.IsItemMe;
                            GameScr.info1.addInfo("Lọc không nhặt vật phẩm của người khác: " + (TanSatHandler.IsItemMe ? "Bật" : "Tắt"), 0);
                        }
                        else
                        {
                            bool flag13 = text == "sln";
                            if (flag13)
                            {
                                TanSatHandler.IsLimitTimesPickItem = !TanSatHandler.IsLimitTimesPickItem;
                                StringBuilder stringBuilder = new StringBuilder();
                                stringBuilder.Append("Giới hạn số lần nhặt là ");
                                stringBuilder.Append(TanSatHandler.TimesAutoPickItemMax);
                                stringBuilder.Append(TanSatHandler.IsLimitTimesPickItem ? ": Bật" : ": Tắt");
                                GameScr.info1.addInfo(stringBuilder.ToString(), 0);
                            }
                            else
                            {
                                bool flag14 = TanSatHandler.IsGetInfoChat<int>(text, "sln");
                                if (flag14)
                                {
                                    TanSatHandler.TimesAutoPickItemMax = TanSatHandler.GetInfoChat<int>(text, "sln");
                                    GameScr.info1.addInfo("Số lần nhặt giới hạn là: " + TanSatHandler.TimesAutoPickItemMax.ToString(), 0);
                                }
                                else
                                {
                                    bool flag15 = TanSatHandler.IsGetInfoChat<short>(text, "addi");
                                    if (flag15)
                                    {
                                        short infoChat = TanSatHandler.GetInfoChat<short>(text, "addi");
                                        bool flag16 = TanSatHandler.IdItemPicks.Contains(infoChat);
                                        if (flag16)
                                        {
                                            TanSatHandler.IdItemPicks.Remove(infoChat);
                                            GameScr.info1.addInfo(string.Format("Đã xoá khỏi danh sách chỉ tự động nhặt item: {0}[{1}]", ItemTemplates.get(infoChat).name, infoChat), 0);
                                        }
                                        else
                                        {
                                            TanSatHandler.IdItemPicks.Add(infoChat);
                                            GameScr.info1.addInfo(string.Format("Đã thêm vào danh sách chỉ tự động nhặt item: {0}[{1}]", ItemTemplates.get(infoChat).name, infoChat), 0);
                                        }
                                    }
                                    else
                                    {
                                        bool flag17 = text == "blocki";
                                        if (flag17)
                                        {
                                            ItemMap itemFocus3 = global::Char.myCharz().itemFocus;
                                            bool flag18 = itemFocus3 != null;
                                            if (flag18)
                                            {
                                                bool flag19 = TanSatHandler.IdItemBlocks.Contains(itemFocus3.template.id);
                                                if (flag19)
                                                {
                                                    TanSatHandler.IdItemBlocks.Remove(itemFocus3.template.id);
                                                    GameScr.info1.addInfo(string.Format("Đã xoá khỏi danh sách không tự động nhặt item: {0}[{1}]", itemFocus3.template.name, itemFocus3.template.id), 0);
                                                }
                                                else
                                                {
                                                    TanSatHandler.IdItemBlocks.Add(itemFocus3.template.id);
                                                    GameScr.info1.addInfo(string.Format("Đã thêm vào danh sách không tự động nhặt item: {0}[{1}]", itemFocus3.template.name, itemFocus3.template.id), 0);
                                                }
                                            }
                                            else
                                            {
                                                GameScr.info1.addInfo("Cần trỏ vào vật phẩm cần chặn khi auto nhặt", 0);
                                            }
                                        }
                                        else
                                        {
                                            bool flag20 = TanSatHandler.IsGetInfoChat<short>(text, "blocki");
                                            if (flag20)
                                            {
                                                short infoChat2 = TanSatHandler.GetInfoChat<short>(text, "blocki");
                                                bool flag21 = TanSatHandler.IdItemBlocks.Contains(infoChat2);
                                                if (flag21)
                                                {
                                                    TanSatHandler.IdItemBlocks.Remove(infoChat2);
                                                    GameScr.info1.addInfo(string.Format("Đã thêm vào danh sách không tự động nhặt item: {0}[{1}]", ItemTemplates.get(infoChat2).name, infoChat2), 0);
                                                }
                                                else
                                                {
                                                    TanSatHandler.IdItemBlocks.Add(infoChat2);
                                                    GameScr.info1.addInfo(string.Format("Đã xoá khỏi danh sách không tự động nhặt item: {0}[{1}]", ItemTemplates.get(infoChat2).name, infoChat2), 0);
                                                }
                                            }
                                            else
                                            {
                                                bool flag22 = TanSatHandler.IsGetInfoChat<sbyte>(text, "addti");
                                                if (flag22)
                                                {
                                                    sbyte infoChat3 = TanSatHandler.GetInfoChat<sbyte>(text, "addti");
                                                    bool flag23 = TanSatHandler.TypeItemPicks.Contains(infoChat3);
                                                    if (flag23)
                                                    {
                                                        TanSatHandler.TypeItemPicks.Remove(infoChat3);
                                                        GameScr.info1.addInfo("Đã xoá khỏi danh sách chỉ tự động nhặt loại item: " + infoChat3.ToString(), 0);
                                                    }
                                                    else
                                                    {
                                                        TanSatHandler.TypeItemPicks.Add(infoChat3);
                                                        GameScr.info1.addInfo("Đã thêm vào danh sách chỉ tự động nhặt loại item: " + infoChat3.ToString(), 0);
                                                    }
                                                }
                                                else
                                                {
                                                    bool flag24 = TanSatHandler.IsGetInfoChat<sbyte>(text, "blockti");
                                                    if (flag24)
                                                    {
                                                        sbyte infoChat4 = TanSatHandler.GetInfoChat<sbyte>(text, "blockti");
                                                        bool flag25 = TanSatHandler.TypeItemBlock.Contains(infoChat4);
                                                        if (flag25)
                                                        {
                                                            TanSatHandler.TypeItemBlock.Remove(infoChat4);
                                                            GameScr.info1.addInfo("Đã xoá khỏi danh sách không tự động nhặt loại item: " + infoChat4.ToString(), 0);
                                                        }
                                                        else
                                                        {
                                                            TanSatHandler.TypeItemBlock.Add(infoChat4);
                                                            GameScr.info1.addInfo("Đã thêm vào danh sách không tự động nhặt loại item: " + infoChat4.ToString(), 0);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bool flag26 = text == "clri";
                                                        if (flag26)
                                                        {
                                                            TanSatHandler.IdItemPicks.Clear();
                                                            TanSatHandler.TypeItemPicks.Clear();
                                                            TanSatHandler.TypeItemBlock.Clear();
                                                            TanSatHandler.IdItemBlocks.Clear();
                                                            TanSatHandler.IdItemBlocks.AddRange(TanSatHandler.IdItemBlockBase);
                                                            GameScr.info1.addInfo("Danh sách lọc item đã được đặt lại mặc định", 0);
                                                        }
                                                        else
                                                        {
                                                            bool flag27 = text == "cnn";
                                                            if (flag27)
                                                            {
                                                                TanSatHandler.IdItemPicks.Clear();
                                                                TanSatHandler.TypeItemPicks.Clear();
                                                                TanSatHandler.TypeItemBlock.Clear();
                                                                TanSatHandler.IdItemBlocks.Clear();
                                                                TanSatHandler.IdItemBlocks.AddRange(TanSatHandler.IdItemBlockBase);
                                                                TanSatHandler.IdItemPicks.Add(77);
                                                                TanSatHandler.IdItemPicks.Add(861);
                                                                GameScr.info1.addInfo("Đã cài đặt chỉ nhặt ngọc (của tàn sát)", 0);
                                                            }
                                                            else
                                                            {
                                                                bool flag28 = text == "ts";
                                                                if (flag28)
                                                                {
                                                                GameCanvas.panel.indexMod[4] = !GameCanvas.panel.indexMod[4];
                                                                    GameScr.info1.addInfo("Tự động đánh quái: " + (GameCanvas.panel.indexMod[4] ? "Bật" : "Tắt"), 0);
                                                                }
                                                                else
                                                                {
                                                                    bool flag29 = text == "nsq";
                                                                    if (flag29)
                                                                    {
                                                                    GameCanvas.panel.indexMod[5] = !GameCanvas.panel.indexMod[5];
                                                                        GameScr.info1.addInfo("Tàn sát né siêu quái: " + (GameCanvas.panel.indexMod[5] ? "Bật" : "Tắt"), 0);
                                                                    }
                                                                    else
                                                                    {
                                                                        bool flag30 = TanSatHandler.IsGetInfoChat<int>(text, "addm");
                                                                        if (flag30)
                                                                        {
                                                                            int infoChat5 = TanSatHandler.GetInfoChat<int>(text, "addm");
                                                                            bool flag31 = TanSatHandler.IdMobsTanSat.Contains(infoChat5);
                                                                            if (flag31)
                                                                            {
                                                                                TanSatHandler.IdMobsTanSat.Remove(infoChat5);
                                                                                GameScr.info1.addInfo("Đã xoá mob: " + infoChat5.ToString(), 0);
                                                                            }
                                                                            else
                                                                            {
                                                                                TanSatHandler.IdMobsTanSat.Add(infoChat5);
                                                                                GameScr.info1.addInfo("Đã thêm mob: " + infoChat5.ToString(), 0);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            bool flag32 = TanSatHandler.IsGetInfoChat<int>(text, "addtm");
                                                                            if (flag32)
                                                                            {
                                                                                int infoChat6 = TanSatHandler.GetInfoChat<int>(text, "addtm");
                                                                                bool flag33 = TanSatHandler.TypeMobsTanSat.Contains(infoChat6);
                                                                                if (flag33)
                                                                                {
                                                                                    TanSatHandler.TypeMobsTanSat.Remove(infoChat6);
                                                                                    GameScr.info1.addInfo(string.Format("Đã xoá loại mob: {0}[{1}]", Mob.arrMobTemplate[infoChat6].name, infoChat6), 0);
                                                                                }
                                                                                else
                                                                                {
                                                                                    TanSatHandler.TypeMobsTanSat.Add(infoChat6);
                                                                                    GameScr.info1.addInfo(string.Format("Đã thêm loại mob: {0}[{1}]", Mob.arrMobTemplate[infoChat6].name, infoChat6), 0);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                bool flag34 = text == "clrm";
                                                                                if (flag34)
                                                                                {
                                                                                    TanSatHandler.IdMobsTanSat.Clear();
                                                                                    TanSatHandler.TypeMobsTanSat.Clear();
                                                                                    GameScr.info1.addInfo("Đã xoá danh sách đánh quái", 0);
                                                                                }
                                                                                else
                                                                                {
                                                                                    bool flag35 = text == "skill";
                                                                                    if (flag35)
                                                                                    {
                                                                                        SkillTemplate template = global::Char.myCharz().myskill.template;
                                                                                        bool flag36 = TanSatHandler.IdSkillsTanSat.Contains(template.id);
                                                                                        if (flag36)
                                                                                        {
                                                                                            TanSatHandler.IdSkillsTanSat.Remove(template.id);
                                                                                            GameScr.info1.addInfo(string.Format("Đã xoá khỏi danh sách skill sử dụng tự động đánh quái skill: {0}[{1}]", template.name, template.id), 0);
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            TanSatHandler.IdSkillsTanSat.Add(template.id);
                                                                                            GameScr.info1.addInfo(string.Format("Đã thêm vào danh sách skill sử dụng tự động đánh quái skill: {0}[{1}]", template.name, template.id), 0);
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        bool flag37 = TanSatHandler.IsGetInfoChat<int>(text, "skill");
                                                                                        if (flag37)
                                                                                        {
                                                                                            int num = TanSatHandler.GetInfoChat<int>(text, "skill") - 1;
                                                                                            SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[num];
                                                                                            bool flag38 = TanSatHandler.IdSkillsTanSat.Contains(skillTemplate.id);
                                                                                            if (flag38)
                                                                                            {
                                                                                                TanSatHandler.IdSkillsTanSat.Remove(skillTemplate.id);
                                                                                                GameScr.info1.addInfo(string.Format("Đã xoá khỏi danh sách skill sử dụng tự động đánh quái skill: {0}[{1}]", skillTemplate.name, skillTemplate.id), 0);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                TanSatHandler.IdSkillsTanSat.Add(skillTemplate.id);
                                                                                                GameScr.info1.addInfo(string.Format("Đã thêm vào danh sách skill sử dụng tự động đánh quái skill: {0}[{1}]", skillTemplate.name, skillTemplate.id), 0);
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            bool flag39 = TanSatHandler.IsGetInfoChat<sbyte>(text, "skillid");
                                                                                            if (flag39)
                                                                                            {
                                                                                                sbyte infoChat7 = TanSatHandler.GetInfoChat<sbyte>(text, "skillid");
                                                                                                bool flag40 = TanSatHandler.IdSkillsTanSat.Contains(infoChat7);
                                                                                                if (flag40)
                                                                                                {
                                                                                                    TanSatHandler.IdSkillsTanSat.Remove(infoChat7);
                                                                                                    GameScr.info1.addInfo("Đã xoá khỏi danh sách skill sử dụng tự động đánh quái skill: " + infoChat7.ToString(), 0);
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    TanSatHandler.IdSkillsTanSat.Add(infoChat7);
                                                                                                    GameScr.info1.addInfo("Đã thêm vào danh sách skill sử dụng tự động đánh quái skill: " + infoChat7.ToString(), 0);
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                bool flag41 = text == "clrs";
                                                                                                if (flag41)
                                                                                                {
                                                                                                    TanSatHandler.IdSkillsTanSat.Clear();
                                                                                                    TanSatHandler.IdSkillsTanSat.AddRange(TanSatHandler.IdSkillsBase);
                                                                                                    GameScr.info1.addInfo("Đã đặt danh sách skill sử dụng tự động đánh quái về mặc định", 0);
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    bool flag42 = text == "abf";
                                                                                                    if (flag42)
                                                                                                    {
                                                                                                        bool flag43 = TanSatHandler.HpBuff == 0 && TanSatHandler.MpBuff == 0;
                                                                                                        if (flag43)
                                                                                                        {
                                                                                                            GameScr.info1.addInfo("Tự động sử dụng đậu thần: Tắt", 0);
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            TanSatHandler.HpBuff = 20;
                                                                                                            TanSatHandler.MpBuff = 20;
                                                                                                            GameScr.info1.addInfo(string.Format("Tự động sử dụng đậu thần khi HP dưới {0}%, MP dưới {1}%", TanSatHandler.HpBuff, TanSatHandler.MpBuff), 0);
                                                                                                        }
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        bool flag44 = TanSatHandler.IsGetInfoChat<int>(text, "abf");
                                                                                                        if (flag44)
                                                                                                        {
                                                                                                            TanSatHandler.HpBuff = TanSatHandler.GetInfoChat<int>(text, "abf");
                                                                                                            TanSatHandler.MpBuff = 0;
                                                                                                            GameScr.info1.addInfo(string.Format("Tự động sử dụng đậu thần khi HP dưới {0}%", TanSatHandler.HpBuff), 0);
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            bool flag45 = TanSatHandler.IsGetInfoChat<int>(text, "abf", 2);
                                                                                                            if (flag45)
                                                                                                            {
                                                                                                                int[] infoChat8 = TanSatHandler.GetInfoChat<int>(text, "abf", 2);
                                                                                                                TanSatHandler.HpBuff = 10;
                                                                                                                TanSatHandler.MpBuff = 10;
                                                                                                                GameScr.info1.addInfo("Tự động sử dụng đậu thần khi HP dưới 10%, MP dưới 10%", 0);
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                bool flag46 = !(text == "vdh");
                                                                                                                if (flag46)
                                                                                                                {
                                                                                                                    return false;
                                                                                                                }
                                                                                                            GameCanvas.panel.indexMod[6] = !GameCanvas.panel.indexMod[6];
                                                                                                                GameScr.info1.addInfo("Tự động đánh quái vượt địa hình: " + (GameCanvas.panel.indexMod[6] ? "Bật" : "Tắt"), 0);
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        // Token: 0x06000A37 RID: 2615 RVA: 0x000A41F4 File Offset: 0x000A23F4
        public static bool HotKeys()
        {
            int keyAsciiPress = GameCanvas.keyAsciiPress;
            bool flag = keyAsciiPress <= 98;
            if (flag)
            {
                bool flag2 = keyAsciiPress == 97;
                if (flag2)
                {
                    TanSatHandler.Chat("add");
                    return true;
                }
                bool flag3 = keyAsciiPress == 98;
                if (flag3)
                {
                    TanSatHandler.Chat("abf");
                    return true;
                }
            }
            else
            {
                bool flag4 = keyAsciiPress == 110;
                if (flag4)
                {
                    TanSatHandler.Chat("anhat");
                    return true;
                }
                bool flag5 = keyAsciiPress == 116;
                if (flag5)
                {
                    TanSatHandler.Chat("ts");
                    return true;
                }
            }
            return false;
        }

        // Token: 0x06000A38 RID: 2616 RVA: 0x000065F0 File Offset: 0x000047F0
        public static void Update()
        {
            TanSatController.Update();
        }

        // Token: 0x06000A39 RID: 2617 RVA: 0x000A428C File Offset: 0x000A248C
        public static void MobStartDie(object obj)
        {
            Mob mob = (Mob)obj;
            bool flag = mob.status != 1 && mob.status != 0;
            if (flag)
            {
                mob.timeLastDie = mSystem.currentTimeMillis();
                mob.countDie++;
                bool flag2 = mob.countDie > 10;
                if (flag2)
                {
                    mob.countDie = 0;
                }
            }
        }

        // Token: 0x06000A3A RID: 2618 RVA: 0x000A42EC File Offset: 0x000A24EC
        public static void UpdateCountDieMob(Mob mob)
        {
            bool flag = mob.levelBoss != 0;
            if (flag)
            {
                mob.countDie = 0;
            }
        }

        // Token: 0x06000A3B RID: 2619 RVA: 0x000A4310 File Offset: 0x000A2510
        private static bool IsGetInfoChat<T>(string text, string s)
        {
            bool flag = !text.StartsWith(s);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                try
                {
                    Convert.ChangeType(text.Substring(s.Length), typeof(T));
                }
                catch
                {
                    return false;
                }
                result = true;
            }
            return result;
        }

        // Token: 0x06000A3C RID: 2620 RVA: 0x000A4370 File Offset: 0x000A2570
        private static T GetInfoChat<T>(string text, string s)
        {
            return (T)((object)Convert.ChangeType(text.Substring(s.Length), typeof(T)));
        }

        // Token: 0x06000A3D RID: 2621 RVA: 0x000A43A4 File Offset: 0x000A25A4
        private static bool IsGetInfoChat<T>(string text, string s, int n)
        {
            bool flag = !text.StartsWith(s);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                try
                {
                    string[] array = text.Substring(s.Length).Split(new char[]
                    {
                        ' '
                    });
                    for (int i = 0; i < n; i++)
                    {
                        Convert.ChangeType(array[i], typeof(T));
                    }
                }
                catch
                {
                    return false;
                }
                result = true;
            }
            return result;
        }

        // Token: 0x06000A3E RID: 2622 RVA: 0x000A442C File Offset: 0x000A262C
        private static T[] GetInfoChat<T>(string text, string s, int n)
        {
            T[] array = new T[n];
            string[] array2 = text.Substring(s.Length).Split(new char[]
            {
                ' '
            });
            for (int i = 0; i < n; i++)
            {
                array[i] = (T)((object)Convert.ChangeType(array2[i], typeof(T)));
            }
            return array;
        }

        // Token: 0x04001325 RID: 4901
        private const int ID_ITEM_GEM = 77;

        // Token: 0x04001326 RID: 4902
        private const int ID_ITEM_GEM_LOCK = 861;

        // Token: 0x04001327 RID: 4903
        private const int DEFAULT_HP_BUFF = 20;

        // Token: 0x04001328 RID: 4904
        private const int DEFAULT_MP_BUFF = 20;

        // Token: 0x04001329 RID: 4905
        private static readonly sbyte[] IdSkillsBase = new sbyte[]
        {
            0,
            2,
            17,
            4,
            13
        };

        // Token: 0x0400132A RID: 4906
        public static readonly short[] IdItemBlockBase = new short[]
        {
            225,
            353,
            354,
            355,
            356,
            357,
            358,
            359,
            360,
            362
        };

        // Token: 0x0400132B RID: 4907
        public static bool IsTanSat = false;

        // Token: 0x0400132C RID: 4908
        public static bool IsNeSieuQuai = false;

        // Token: 0x0400132D RID: 4909
        public static bool IsVuotDiaHinh = true;

        // Token: 0x0400132E RID: 4910
        public static List<int> IdMobsTanSat = new List<int>();

        // Token: 0x0400132F RID: 4911
        public static List<int> TypeMobsTanSat = new List<int>();

        // Token: 0x04001330 RID: 4912
        public static List<sbyte> IdSkillsTanSat = new List<sbyte>(TanSatHandler.IdSkillsBase);

        // Token: 0x04001331 RID: 4913
        public static bool IsAutoPickItems = false;

        // Token: 0x04001332 RID: 4914
        public static bool IsItemMe = true;

        // Token: 0x04001333 RID: 4915
        public static bool IsLimitTimesPickItem = true;

        // Token: 0x04001334 RID: 4916
        public static int TimesAutoPickItemMax = 10;

        // Token: 0x04001335 RID: 4917
        public static List<short> IdItemPicks = new List<short>();

        // Token: 0x04001336 RID: 4918
        public static List<short> IdItemBlocks = new List<short>(TanSatHandler.IdItemBlockBase);

        // Token: 0x04001337 RID: 4919
        public static List<sbyte> TypeItemPicks = new List<sbyte>();

        // Token: 0x04001338 RID: 4920
        public static List<sbyte> TypeItemBlock = new List<sbyte>();

        // Token: 0x04001339 RID: 4921
        public static int HpBuff = 0;

        // Token: 0x0400133A RID: 4922
        public static int MpBuff = 0;
    }

