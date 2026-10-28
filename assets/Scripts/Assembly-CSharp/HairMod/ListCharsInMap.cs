using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    // Token: 0x020000C6 RID: 198
    public class ListCharsInMap
    {
        // Token: 0x06000B07 RID: 2823 RVA: 0x0009CCE4 File Offset: 0x0009AEE4
        public static void update()
        {
            if (GameCanvas.panel.indexMod[0] && Boss.listBosses.Count > 0)
            {
                if (Boss.isCollapsed)
                {
                    if (ListCharsInMap.y != Boss.y + Boss.distanceBetweenLines + 3)
                    {
                        ListCharsInMap.y = Boss.y + Boss.distanceBetweenLines + 3;
                    }
                }
                else if (ListCharsInMap.y != Boss.y + 5 + Boss.distanceBetweenLines * Mathf.Clamp(Boss.listBosses.Count, 0, Boss.MAX_BOSS_DISPLAY) + 10)
                {
                    ListCharsInMap.y = Boss.y + 5 + Boss.distanceBetweenLines * Mathf.Clamp(Boss.listBosses.Count, 0, Boss.MAX_BOSS_DISPLAY) + 10;
                }
            }
            else if (ListCharsInMap.y != 60)
            {
                ListCharsInMap.y = 60;
            }
            if (!HairMod.showchars)
            {
                return;
            }
            ListCharsInMap.listChars.Clear();
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char @char = (Char)GameScr.vCharInMap.elementAt(i);
                if (@char.isNormalChar(true, false))
                {
                    ListCharsInMap.listChars.Add(@char);
                    if (ListCharsInMap.isShowPet && @char.charID > 0)
                    {
                        Char char2 = GameScr.findCharInMap(-@char.charID);
                        if (char2 != null)
                        {
                            ListCharsInMap.listChars.Add(char2);
                        }
                    }
                }
            }
            if (ListCharsInMap.isShowPet)
            {
                for (int j = 0; j < GameScr.vCharInMap.size(); j++)
                {
                    Char char3 = (Char)GameScr.vCharInMap.elementAt(j);
                    if (char3.isNormalChar(false, true) && !ListCharsInMap.listChars.Contains(char3))
                    {
                        ListCharsInMap.listChars.Add(char3);
                    }
                }
            }
            if (ListCharsInMap.offset >= ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR)
            {
                if (ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR > 0)
                {
                    ListCharsInMap.offset = ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR;
                }
                else
                {
                    ListCharsInMap.offset = 0;
                }
            }
            if (GameCanvas.isMouseFocus(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength, ListCharsInMap.y + 1, ListCharsInMap.maxLength, 8 * ListCharsInMap.MAX_CHAR))
            {
                if (GameCanvas.pXYScrollMouse > 0 && ListCharsInMap.offset < ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR)
                {
                    ListCharsInMap.offset++;
                }
                if (GameCanvas.pXYScrollMouse < 0 && ListCharsInMap.offset > 0)
                {
                    ListCharsInMap.offset--;
                }
            }
            int num;
            int num2;
            int num3;
            ListCharsInMap.getScrollBar(out num, out num2, out num3);
            if (ListCharsInMap.listChars.Count > ListCharsInMap.MAX_CHAR)
            {
                ListCharsInMap.offsetX = num;
                return;
            }
            ListCharsInMap.offsetX = 0;
        }

        // Token: 0x06000B08 RID: 2824 RVA: 0x0009CF5C File Offset: 0x0009B15C
        public static void paint(mGraphics g)
        {
            if (!HairMod.showchars)
            {
                return;
            }
            if (ListCharsInMap.offset >= ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR)
            {
                if (ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR > 0)
                {
                    ListCharsInMap.offset = ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR;
                }
                else
                {
                    ListCharsInMap.offset = 0;
                }
            }
            ListCharsInMap.maxLength = 0;
            if (!ListCharsInMap.isCollapsed)
            {
                ListCharsInMap.PaintListChars(g);
                ListCharsInMap.PaintScroll(g);
            }
            ListCharsInMap.PaintRect(g);
        }

        // Token: 0x06000B09 RID: 2825 RVA: 0x0009CFD8 File Offset: 0x0009B1D8
        private static string formatHP(Char ch)
        {
            long cHP = ch.cHP;
            long cHPFull = ch.cHPFull;
            float num = (float)cHP / (float)cHPFull;
            Color color = new Color(Mathf.Clamp(2f - num * 2f, 0f, 1f), Mathf.Clamp(num * 2f, 0f, 1f), 0f);
            string text = string.Format("#{0:x2}{1:x2}{2:x2}{3:x2}", new object[]
            {
                (int)(color.r * 255f),
                (int)(color.g * 255f),
                (int)(color.b * 255f),
                (int)(color.a * 255f)
            });
            if (cHP == 0L)
            {
                text = "black";
            }
            return string.Concat(new string[]
            {
                "<color=white>[<color=",
                text,
                ">",
                NinjaUtil.getMoneys(ch.cHP),
                "</color>/<color=lime>",
                NinjaUtil.getMoneys(ch.cHPFull),
                "</color>]</color>"
            });
        }

        // Token: 0x06000B0A RID: 2826 RVA: 0x0009D0F4 File Offset: 0x0009B2F4
        private static void PaintListChars(mGraphics g)
        {
            int num = 0;
            List<KeyValuePair<string, GUIStyle>> list = new List<KeyValuePair<string, GUIStyle>>();
            int num2 = 0;
            if (ListCharsInMap.listChars.Count > ListCharsInMap.MAX_CHAR)
            {
                num2 = ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR;
            }
            for (int i = num2 - ListCharsInMap.offset; i < ListCharsInMap.listChars.Count - ListCharsInMap.offset; i++)
            {
                GUIStyle guistyle = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 6 * mGraphics.zoomLevel,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.UpperRight,
                    richText = true
                };
                Char @char = ListCharsInMap.listChars[i];
                string text = string.Concat(new string[]
                {
                    "<color=orange>",
                    @char.getClanTag(),
                    "</color>",
                    @char.getNameWithoutClanTag(true),
                    " ",
                    ListCharsInMap.formatHP(@char)
                });
                text = text + " - " + @char.getGender(true);
                if (@char.isPet())
                {
                    text = text + " - " + @char.getGender(true);
                    Char char2 = GameScr.findCharInMap(-@char.charID);
                    if (char2 != null)
                    {
                        text = text + " [<color=cyan>Đệ tử</color> của " + char2.getNameWithoutClanTag(true) + "]";
                    }
                    else
                    {
                        text += " [<color=cyan>Đệ tử</color> bị lạc sư phụ]";
                    }
                    num++;
                }
                else
                {
                    text = (i + 1 - num).ToString() + ". " + text;
                }
                if ((Char.myCharz().isStandAndCharge || (!Char.myCharz().isDie && Char.myCharz().cgender == 2 && Char.myCharz().myskill == Char.myCharz().getSkill(Char.myCharz().nClass.skillTemplates[4]))) && SuicideRange.mapObjsInMyRange.Contains(@char))
                {
                    if (GameCanvas.gameTick % 40 > 20)
                    {
                        text += " - <color=red>Trong tầm</color>";
                    }
                    else
                    {
                        text += " - <color=yellow>Trong tầm</color>";
                    }
                }
                if (@char.bag == 31 || @char.isBoss())
                {
                    text = "<color=red>" + text + "</color>";
                }
                else if (@char.isPet())
                {
                    text = "<color=cyan>" + text + "</color>";
                }
                if (@char.cHP <= 0L)
                {
                    text = "<color=black>" + text + "</color>";
                }
                list.Add(new KeyValuePair<string, GUIStyle>(text, guistyle));
                ListCharsInMap.maxLength = Math.max(ListCharsInMap.maxLength, Boss.getWidth(list[i - num2 + ListCharsInMap.offset].Value, text) + ((@char.cFlag != 0) ? (ListCharsInMap.distanceBetweenLines + 1) : 0));
            }
            ListCharsInMap.FillBackground(g);
            for (int j = num2 - ListCharsInMap.offset; j < ListCharsInMap.listChars.Count - ListCharsInMap.offset; j++)
            {
                int num3 = 0;
                Char char3 = ListCharsInMap.listChars[j];
                if (char3.cFlag != 0)
                {
                    num3 = ListCharsInMap.distanceBetweenLines + 1;
                    if (char3.cFlag == 9 || char3.cFlag == 10)
                    {
                        GUIStyle guistyle2 = new GUIStyle(GUI.skin.label)
                        {
                            alignment = TextAnchor.UpperCenter,
                            fontSize = 6 * mGraphics.zoomLevel
                        };
                        guistyle2.normal.textColor = Color.white;
                        if (char3.cFlag == 9)
                        {
                            g.drawString("K", -(ListCharsInMap.x + ListCharsInMap.offsetX), mGraphics.zoomLevel - 3 + ListCharsInMap.y + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset), guistyle2);
                        }
                        if (char3.cFlag == 10)
                        {
                            g.drawString("M", -(ListCharsInMap.x + ListCharsInMap.offsetX), mGraphics.zoomLevel - 3 + ListCharsInMap.y + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset), guistyle2);
                        }
                    }
                    g.setColor(char3.getFlagColor());
                    g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.distanceBetweenLines + 1, ListCharsInMap.y + 1 + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset), ListCharsInMap.distanceBetweenLines - 1, ListCharsInMap.distanceBetweenLines - 1);
                }
                g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.4f));
                if (GameCanvas.isMouseFocus(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - num3, ListCharsInMap.y + 1 + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset), ListCharsInMap.maxLength, ListCharsInMap.distanceBetweenLines - 1))
                {
                    g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.7f));
                }
                if (Char.myCharz().charFocus == ListCharsInMap.listChars[j])
                {
                    g.setColor(new Color(1f, 0.5f, 0f, 0.5f));
                }
                if (SuicideRange.mapObjsInMyRange.Contains(ListCharsInMap.listChars[j]))
                {
                    g.setColor(new Color(0.5f, 0.5f, 0f, 1f));
                    if (Char.myCharz().isStandAndCharge && GameCanvas.gameTick % 10 >= 5)
                    {
                        g.setColor(new Color(1f, 0f, 0f, 1f));
                    }
                }
                g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength, ListCharsInMap.y + 1 + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset), ListCharsInMap.maxLength - num3, ListCharsInMap.distanceBetweenLines - 1);
                if (GameCanvas.isMouseFocus(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - num3, ListCharsInMap.y + 1 + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset), ListCharsInMap.maxLength, ListCharsInMap.distanceBetweenLines - 1))
                {
                    int width = Boss.getWidth(list[j - num2 + ListCharsInMap.offset].Value, list[j - num2 + ListCharsInMap.offset].Key);
                    g.setColor(Color.white);
                    g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - width - num3 + 1, ListCharsInMap.y + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset) + 7, width - 2, 1);
                    float cHP = (float)char3.cHP;
                    long cHPFull = char3.cHPFull;
                    float num4 = cHP / (float)cHPFull;
                    Color color = new Color(Mathf.Clamp(2f - num4 * 2f, 0f, 1f), Mathf.Clamp(num4 * 2f, 0f, 1f), 0f);
                    g.setColor(color);
                    g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - width - num3 + 1, ListCharsInMap.y + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset) + 7, (int)(num4 * (float)(width - 2)), 1);
                }
                int num5 = 0;
                g.drawString(list[j - num2 + ListCharsInMap.offset].Key, -(ListCharsInMap.x + ListCharsInMap.offsetX) - num3, mGraphics.zoomLevel - 3 + ListCharsInMap.y + ListCharsInMap.distanceBetweenLines * (j - num2 + ListCharsInMap.offset) + num5, list[j - num2 + ListCharsInMap.offset].Value);
            }
        }

        // Token: 0x06000B0B RID: 2827 RVA: 0x0009D860 File Offset: 0x0009BA60
        private static void FillBackground(mGraphics g)
        {
            if (!ListCharsInMap.isCollapsed && ListCharsInMap.listChars.Count > 0)
            {
                g.setColor(0, 0.1f);
                int num;
                int num2;
                int num3;
                ListCharsInMap.getScrollBar(out num, out num2, out num3);
                if (ListCharsInMap.listChars.Count <= ListCharsInMap.MAX_CHAR)
                {
                    num = 0;
                }
                int num4 = ListCharsInMap.maxLength + 5 + ((num > 0) ? (num + 2) : 0);
                int num5 = ListCharsInMap.distanceBetweenLines * Math.min(ListCharsInMap.MAX_CHAR, ListCharsInMap.listChars.Count) + 7;
                g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - 3, ListCharsInMap.y - 5, num4, num5);
            }
        }

        // Token: 0x06000B0C RID: 2828 RVA: 0x0009D91C File Offset: 0x0009BB1C
        private static void PaintScroll(mGraphics g)
        {
            if (ListCharsInMap.listChars.Count > ListCharsInMap.MAX_CHAR)
            {
                int num;
                int num2;
                ListCharsInMap.getButtonUp(out num, out num2);
                int num3;
                int num4;
                ListCharsInMap.getButtonDown(out num3, out num4);
                int num5;
                int num6;
                int num7;
                ListCharsInMap.getScrollBar(out num5, out num6, out num7);
                g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.4f));
                g.fillRect(num, num2, 9, num6 + 12);
                g.drawRegion(Mob.imgHP, 0, (ListCharsInMap.offset < ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR) ? 18 : 54, 9, 6, 1, num, num2, 0);
                g.drawRegion(Mob.imgHP, 0, (ListCharsInMap.offset > 0) ? 18 : 54, 9, 6, 0, num3, num4, 0);
                g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.7f));
                g.fillRect(num, num2 + 6 + Mathf.CeilToInt((float)num6 / (float)ListCharsInMap.listChars.Count * (float)(ListCharsInMap.listChars.Count - ListCharsInMap.offset - ListCharsInMap.MAX_CHAR)), num5, num7);
                g.setColor(new Color(0.7f, 0.7f, 0f, 1f));
                g.drawRect(num, num2 + 6 + Mathf.CeilToInt((float)num6 / (float)ListCharsInMap.listChars.Count * (float)(ListCharsInMap.listChars.Count - ListCharsInMap.offset - ListCharsInMap.MAX_CHAR)), num5 - 1, num7 - 1);
            }
        }

        // Token: 0x06000B0D RID: 2829 RVA: 0x0009DA94 File Offset: 0x0009BC94
        private static void PaintRect(mGraphics g)
        {
            int num;
            int num2;
            int num3;
            ListCharsInMap.getScrollBar(out num, out num2, out num3);
            if (ListCharsInMap.listChars.Count <= ListCharsInMap.MAX_CHAR)
            {
                num = 0;
            }
            int num4 = ListCharsInMap.maxLength + 5 + ((num > 0) ? (num + 2) : 0);
            int num5 = ListCharsInMap.distanceBetweenLines * Math.min(ListCharsInMap.MAX_CHAR, ListCharsInMap.listChars.Count) + 7;
            if (!ListCharsInMap.isCollapsed && ListCharsInMap.listChars.Count > 0)
            {
                g.setColor(Color.yellow);
                g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - 3, ListCharsInMap.y - 5, num4 - ListCharsInMap.titleWidth - 9 - ((num > 0) ? 2 : 0), 1);
                g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) + num, ListCharsInMap.y - 5, 3 + ((num > 0) ? 1 : 0), 1);
                g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - 3, ListCharsInMap.y - 5, 1, num5);
                g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - 3 + num4, ListCharsInMap.y - 5, 1, num5 + 1);
                g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - 3, ListCharsInMap.y - 5 + num5, num4 + 1, 1);
            }
            float num6 = (float)(ListCharsInMap.listChars.Where((Char c) => c.isNormalChar(false, false)).Count<Char>() + 1) / (float)ListCharsInMap.playerCount();
            Color color = new Color(Mathf.Clamp(num6 * 2f, 0f, 1f), Mathf.Clamp(2f - num6 * 2f, 0f, 1f), 0f);
            string text = string.Format("#{0:x2}{1:x2}{2:x2}{3:x2}", new object[]
            {
                (int)(color.r * 255f),
                (int)(color.g * 255f),
                (int)(color.b * 255f),
                (int)(color.a * 255f)
            });
            string text2 = "<color=yellow>{0}</color> khu <color=yellow>{1}</color> [<color={2}>{3}</color>/<color=red>{4}</color>]";
            object[] array = new object[5];
            array[0] = TileMap.mapName;
            array[1] = TileMap.zoneID;
            array[2] = text;
            array[3] = ListCharsInMap.listChars.Where((Char c) => c.isNormalChar(false, false)).Count<Char>() + 1;
            array[4] = ListCharsInMap.playerCount();
            string text3 = string.Format(text2, array);
            GUIStyle guistyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 7 * mGraphics.zoomLevel,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.UpperRight,
                richText = true
            };
            guistyle.normal.textColor = Color.white;
            ListCharsInMap.titleWidth = Boss.getWidth(guistyle, text3);
            g.setColor(new Color(0.2f, 0.2f, 0.2f, 1f));
            g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.titleWidth + num, ListCharsInMap.y - ListCharsInMap.distanceBetweenLines, ListCharsInMap.titleWidth, 8);
            if (GameCanvas.isMouseFocus(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.titleWidth + num, ListCharsInMap.y - ListCharsInMap.distanceBetweenLines, ListCharsInMap.titleWidth, 8))
            {
                g.setColor(guistyle.normal.textColor);
                g.fillRect(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.titleWidth + num, ListCharsInMap.y - 1, ListCharsInMap.titleWidth - 1, 1);
            }
            g.drawString(text3, -(ListCharsInMap.x + ListCharsInMap.offsetX) + num, ListCharsInMap.y - ListCharsInMap.distanceBetweenLines - 2, guistyle);
            int num7;
            int num8;
            ListCharsInMap.getCollapseButton(out num7, out num8);
            g.drawRegion(Mob.imgHP, 0, 18, 9, 6, ListCharsInMap.isCollapsed ? 5 : 4, num7, num8, 0);
        }

        // Token: 0x06000B0E RID: 2830 RVA: 0x0009DEAC File Offset: 0x0009C0AC
        public static void updateTouch()
        {
            if (!HairMod.showchars)
            {
                return;
            }
            try
            {
                if (GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu)
                {
                    int num;
                    int num2;
                    int num3;
                    ListCharsInMap.getScrollBar(out num, out num2, out num3);
                    int num4;
                    int num5;
                    ListCharsInMap.getCollapseButton(out num4, out num5);
                    if (GameCanvas.isPointerHoldIn(num4, num5, 9, 6) || GameCanvas.isPointerHoldIn(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.titleWidth + num, ListCharsInMap.y - ListCharsInMap.distanceBetweenLines, ListCharsInMap.titleWidth, 8))
                    {
                        GameCanvas.isPointerJustDown = false;
                        GameScr.gI().isPointerDowning = false;
                        if (GameCanvas.isPointerClick)
                        {
                            ListCharsInMap.isCollapsed = !ListCharsInMap.isCollapsed;
                        }
                        GameCanvas.clearAllPointerEvent();
                    }
                    else
                    {
                        int num6 = 0;
                        if (ListCharsInMap.listChars.Count > ListCharsInMap.MAX_CHAR)
                        {
                            num6 = ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR;
                        }
                        for (int i = num6 - ListCharsInMap.offset; i < ListCharsInMap.listChars.Count - ListCharsInMap.offset; i++)
                        {
                            if (GameCanvas.isPointerHoldIn(GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.maxLength - ((ListCharsInMap.listChars[i].cFlag != 0) ? (ListCharsInMap.distanceBetweenLines + 1) : 0), ListCharsInMap.y + 1 + ListCharsInMap.distanceBetweenLines * (i - num6 + ListCharsInMap.offset), ListCharsInMap.maxLength, ListCharsInMap.distanceBetweenLines - 1))
                            {
                                GameCanvas.isPointerJustDown = false;
                                GameScr.gI().isPointerDowning = false;
                                if (GameCanvas.isPointerClick)
                                {
                                    if (Char.myCharz().charFocus != ListCharsInMap.listChars[i])
                                    {
                                        Char.myCharz().clearFocus(2);
                                        Char.myCharz().charFocus = ListCharsInMap.listChars[i];
                                    }
                                    else
                                    {
                                        Boss.teleportMyChar(ListCharsInMap.listChars[i]);
                                    }
                                }
                                GameCanvas.clearAllPointerEvent();
                                return;
                            }
                        }
                        if (ListCharsInMap.listChars.Count > ListCharsInMap.MAX_CHAR)
                        {
                            int num7;
                            int num8;
                            ListCharsInMap.getButtonUp(out num7, out num8);
                            if (GameCanvas.isPointerMove && GameCanvas.isPointerDown && GameCanvas.isPointerHoldIn(num7, num8, num, num2))
                            {
                                float num9 = (float)num2 / (float)ListCharsInMap.listChars.Count;
                                float num10 = (float)(GameCanvas.pyMouse - num8) / num9;
                                if (!float.IsNaN(num10))
                                {
                                    ListCharsInMap.offset = Mathf.Clamp(ListCharsInMap.listChars.Count - Mathf.RoundToInt(num10), 0, ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR);
                                }
                            }
                            else if (GameCanvas.isPointerHoldIn(num7, num8, 9, 6))
                            {
                                GameCanvas.isPointerJustDown = false;
                                GameScr.gI().isPointerDowning = false;
                                if (GameCanvas.isPointerClick && ListCharsInMap.offset < ListCharsInMap.listChars.Count - ListCharsInMap.MAX_CHAR)
                                {
                                    ListCharsInMap.offset++;
                                }
                                GameCanvas.clearAllPointerEvent();
                            }
                            else
                            {
                                int num11;
                                int num12;
                                ListCharsInMap.getButtonDown(out num11, out num12);
                                if (GameCanvas.isPointerHoldIn(num11, num12, 9, 6))
                                {
                                    GameCanvas.isPointerJustDown = false;
                                    GameScr.gI().isPointerDowning = false;
                                    if (GameCanvas.isPointerClick && ListCharsInMap.offset > 0)
                                    {
                                        ListCharsInMap.offset--;
                                    }
                                    GameCanvas.clearAllPointerEvent();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        // Token: 0x06000B0F RID: 2831 RVA: 0x0009E1D8 File Offset: 0x0009C3D8
        private static int playerCount()
        {
        try
        {
            if (TileMap.zoneID >= GameScr.gI().maxPlayer.Length)
            {
                return 0;
            }
            return GameScr.gI().maxPlayer[TileMap.zoneID];
        }
        catch
        {
            return 0;
        }
        }

        // Token: 0x06000B10 RID: 2832 RVA: 0x0009E1FF File Offset: 0x0009C3FF
        private static void getButtonUp(out int buttonUpX, out int buttonUpY)
        {
            buttonUpX = GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) + 2;
            buttonUpY = ListCharsInMap.y + 1;
        }

        // Token: 0x06000B11 RID: 2833 RVA: 0x0009E21F File Offset: 0x0009C41F
        private static void getButtonDown(out int buttonDownX, out int buttonDownY)
        {
            buttonDownX = GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) + 2;
            buttonDownY = ListCharsInMap.y + 2 + ListCharsInMap.distanceBetweenLines * (ListCharsInMap.MAX_CHAR - 1);
        }

        // Token: 0x06000B12 RID: 2834 RVA: 0x0009E24D File Offset: 0x0009C44D
        private static void getScrollBar(out int scrollBarWidth, out int scrollBarHeight, out int scrollBarThumbHeight)
        {
            scrollBarWidth = 9;
            scrollBarHeight = ListCharsInMap.MAX_CHAR * ListCharsInMap.distanceBetweenLines - 1 - 12;
            scrollBarThumbHeight = Mathf.CeilToInt((float)ListCharsInMap.MAX_CHAR / (float)ListCharsInMap.listChars.Count * (float)scrollBarHeight);
        }

        // Token: 0x06000B13 RID: 2835 RVA: 0x0009E284 File Offset: 0x0009C484
        private static void getCollapseButton(out int collapseButtonX, out int collapseButtonY)
        {
            int num;
            int num2;
            int num3;
            ListCharsInMap.getScrollBar(out num, out num2, out num3);
            if (ListCharsInMap.listChars.Count <= ListCharsInMap.MAX_CHAR)
            {
                num = 0;
            }
            collapseButtonX = GameCanvas.w - (ListCharsInMap.x + ListCharsInMap.offsetX) - ListCharsInMap.titleWidth + num - 8;
            collapseButtonY = ListCharsInMap.y - ListCharsInMap.distanceBetweenLines + 1;
        }

        // Token: 0x06000B14 RID: 2836 RVA: 0x0009E2DB File Offset: 0x0009C4DB
        public static void setStatePet(bool value)
        {
            ListCharsInMap.isShowPet = value;
        }

        // Token: 0x04001440 RID: 5184
        public static List<Char> listChars = new List<Char>();

        // Token: 0x04001441 RID: 5185
        public static bool isShowPet;

        // Token: 0x04001442 RID: 5186
        private static int maxLength = 0;

        // Token: 0x04001443 RID: 5187
        private static int x = 6;

        // Token: 0x04001444 RID: 5188
        private static int y = 60;

        // Token: 0x04001445 RID: 5189
        private static readonly int MAX_CHAR = 15;

        // Token: 0x04001446 RID: 5190
        private static int distanceBetweenLines = 8;

        // Token: 0x04001447 RID: 5191
        private static int offset = 0;

        // Token: 0x04001448 RID: 5192
        private static bool isCollapsed;

        // Token: 0x04001449 RID: 5193
        private static int titleWidth;

        // Token: 0x0400144A RID: 5194
        private static int offsetX;
    }

