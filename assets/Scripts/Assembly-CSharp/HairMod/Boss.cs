using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

    // Token: 0x020000C4 RID: 196
    public class Boss
    {
        // Token: 0x06000ADD RID: 2781 RVA: 0x0009AE63 File Offset: 0x00099063
        private Boss(string name, string map)
        {
            this.name = name;
            this.map = map;
            this.GetMapId(name, map);
            this.AppearTime = DateTime.Now;
        }

        // Token: 0x06000ADE RID: 2782 RVA: 0x0009AE94 File Offset: 0x00099094
        private void GetMapId(string name, string map)
        {
            if (map == "Vách núi Aru")
            {
                this.mapId = 42;
                return;
            }
            if (map == "Vách núi Moori")
            {
                this.mapId = 43;
                return;
            }
            if (map == "Trạm tàu vũ trụ")
            {
                if (name.StartsWith("Số ") || name.StartsWith("Tiểu đội"))
                {
                    this.mapId = 25;
                    return;
                }
                if (name.Contains("Bojack") || name.StartsWith("Bujin") || name.StartsWith("Bido") || name.StartsWith("Zangya") || name.StartsWith("Bido"))
                {
                    this.mapId = 24;
                    return;
                }
            }
            else
            {
                this.mapId = Boss.GetMapID(map);
            }
        }

        // Token: 0x06000ADF RID: 2783 RVA: 0x0009AF54 File Offset: 0x00099154
        public static void AddBoss(string chatVip)
        {
            if (Boss.strBossHasBeenKilled.Any(new Func<string, bool>(chatVip.Contains)))
            {
                Boss.strBossHasBeenKilled.ForEach(delegate (string s)
                {
                    chatVip = chatVip.Replace(s, "|");
                });
                string[] array2 = chatVip.Split('|', StringSplitOptions.None);
                Boss boss = null;
                try
                {
                    boss = Boss.listBosses.Last((Boss b) => (!new int[] { 79, 82, 83 }.Contains(b.mapId) || !Regex.IsMatch(b.name, "(Tiểu đội trưởng|(Captain|Kapten) Ginyu|Số [1-4]|Jeice|Burter|Recoome|Guldo)")) && b.name == array2[1] && string.IsNullOrEmpty(b.killer));
                }
                catch (InvalidOperationException)
                {
                }
                if (boss == null)
                {
                    boss = new Boss(array2[1], "");
                    Boss.listBosses.Add(boss);
                }
                boss.isDied = true;
                boss.killer = array2[0];
                return;
            }
            if (chatVip.StartsWith(Boss.strBossAppeared[0]))
            {
                Boss.strBossAppeared.ForEach(delegate (string s)
                {
                    chatVip = chatVip.Replace(s, "|");
                });
                string[] array = chatVip.Split('|', StringSplitOptions.None);
                Boss boss2 = null;
                try
                {
                    int mapId = Boss.GetMapID(array[2]);
                    boss2 = Boss.listBosses.Last((Boss b) => (!new int[] { 79, 82, 83 }.Contains(mapId) || !Regex.IsMatch(array[1], "(Tiểu đội trưởng|(Captain|Kapten) Ginyu|Số [1-4]|Jeice|Burter|Recoome|Guldo)")) && string.IsNullOrEmpty(b.map) && b.name == array[1]);
                }
                catch (InvalidOperationException)
                {
                }
                if (boss2 == null)
                {
                    boss2 = new Boss(array[1], array[2]);
                    Boss.listBosses.Add(boss2);
                }
                else
                {
                    boss2.map = array[2];
                    boss2.GetMapId(boss2.name, boss2.map);
                }
                if (array.Length == 4)
                {
                    boss2.zoneId = int.Parse(array[3]);
                }
                if (Boss.listBosses.Count > Boss.MAX_BOSS)
                {
                    Boss.listBosses.RemoveAt(0);
                }
                int num;
                int num2;
                int num3;
                Boss.getScrollBar(out num, out num2, out num3);
                if (Boss.listBosses.Count > Boss.MAX_BOSS_DISPLAY && Boss.offsetX == 0)
                {
                    Boss.offsetX = num;
                }
            }
        }

        // Token: 0x06000AE0 RID: 2784 RVA: 0x0009B160 File Offset: 0x00099360
        public override string ToString()
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(this.AppearTime);
            string text = this.name + " - ";
            if (string.IsNullOrEmpty(this.map))
            {
                text += "chưa biết";
            }
            else
            {
                text += string.Format("{0} [{1}]", this.map, this.mapId);
            }
            text += " - ";
            if (!this.isDied)
            {
                if (this.zoneId > -1)
                {
                    text += string.Format("khu {0} - ", this.zoneId);
                }
                int num = (int)System.Math.Floor((decimal)timeSpan.TotalHours);
                if (num > 0)
                {
                    text += string.Format("{0}h", num);
                }
                if (timeSpan.Minutes > 0)
                {
                    text += string.Format("{0}m", timeSpan.Minutes);
                }
                text += string.Format("{0}s", timeSpan.Seconds);
            }
            else if (!string.IsNullOrEmpty(this.killer))
            {
                text = text + "Bị " + this.killer + " tiêu diệt";
            }
            else
            {
                text += "Đã chết";
            }
            return text;
        }

        // Token: 0x06000AE1 RID: 2785 RVA: 0x0009B2B8 File Offset: 0x000994B8
        public string ToString(bool enableRichText)
        {
            if (!enableRichText)
            {
                return this.ToString();
            }
            TimeSpan timeSpan = DateTime.Now.Subtract(this.AppearTime);
            string text = "yellow";
            string text2 = "yellow";
            if (TileMap.mapID == this.mapId)
            {
                text = "orange";
                text2 = "red";
                if (CharExtensions.findCharInMap(this.name) != null)
                {
                    text = "red";
                }
            }
            string text3 = string.Concat(new string[] { "<color=", text, ">", this.name, "</color> - " });
            if (string.IsNullOrEmpty(this.map))
            {
                text3 += "chưa biết";
            }
            else
            {
                text3 += string.Format("<color={0}>{1}</color> [<color={2}>{3}</color>]", new object[] { text2, this.map, text2, this.mapId });
            }
            text3 += " - ";
            if (!this.isDied)
            {
                if (this.zoneId > -1)
                {
                    if (TileMap.mapID == this.mapId)
                    {
                        if (TileMap.zoneID == this.zoneId)
                        {
                            text3 += string.Format("<color=yellow>khu</color> <color=red>{0}</color> - ", this.zoneId);
                        }
                        else
                        {
                            text3 += string.Format("<color=yellow>khu {0}</color> - ", this.zoneId);
                        }
                    }
                    else
                    {
                        text3 += string.Format("khu <color=yellow>{0}</color> - ", this.zoneId);
                    }
                }
                int num = (int)System.Math.Floor((decimal)timeSpan.TotalHours);
                if (num > 0)
                {
                    text3 += string.Format("<color=orange>{0}</color>h", num);
                }
                if (timeSpan.Minutes > 0)
                {
                    text3 += string.Format("<color=orange>{0}</color>m", timeSpan.Minutes);
                }
                text3 += string.Format("<color=orange>{0}</color>s", timeSpan.Seconds);
            }
            else if (!string.IsNullOrEmpty(this.killer))
            {
                text3 = text3 + "Bị <color=orange>" + this.killer + "</color> tiêu diệt";
            }
            else
            {
                text3 += "Đã chết";
            }
            return text3;
        }

        // Token: 0x06000AE2 RID: 2786 RVA: 0x0009B4DE File Offset: 0x000996DE
        public static void Paint(mGraphics g)
        {
            if (Boss.listBosses.Count <= 0)
            {
                return;
            }
            Boss.maxLength = 0;
            if (!Boss.isCollapsed)
            {
                Boss.PaintListBosses(g);
                Boss.PaintScroll(g);
            }
            Boss.PaintRect(g);
        }

        // Token: 0x06000AE3 RID: 2787 RVA: 0x0009B518 File Offset: 0x00099718
        private static void PaintListBosses(mGraphics g)
        {
            int num = 0;
            if (Boss.listBosses.Count > Boss.MAX_BOSS_DISPLAY)
            {
                num = Boss.listBosses.Count - Boss.MAX_BOSS_DISPLAY;
            }
            GUIStyle[] array = new GUIStyle[Boss.MAX_BOSS_DISPLAY];
            for (int i = num - Boss.offset; i < Boss.listBosses.Count - Boss.offset; i++)
            {
                array[i - num + Boss.offset] = new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.UpperRight,
                    fontSize = 6 * mGraphics.zoomLevel,
                    fontStyle = FontStyle.Bold,
                    richText = true
                };
                Boss boss = Boss.listBosses[i];
                Boss.maxLength = Math.max(Boss.getWidth(array[i - num + Boss.offset], string.Format("{0}. {1}", i + 1, boss)), Boss.maxLength);
            }
            Boss.FillBackground(g);
            int num2 = GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength;
            for (int j = num - Boss.offset; j < Boss.listBosses.Count - Boss.offset; j++)
            {
                int num3 = Boss.y + Boss.distanceBetweenLines * (j - num + Boss.offset);
                Boss boss2 = Boss.listBosses[j];
                g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.4f));
                if (boss2.isDied)
                {
                    g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.2f));
                }
                if (GameCanvas.isMouseFocus(num2, num3, Boss.maxLength, 7))
                {
                    g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.7f));
                }
                g.fillRect(num2, num3 + 1, Boss.maxLength, 7);
                if (GameCanvas.isMouseFocus(num2, num3, Boss.maxLength, 7))
                {
                    Boss.fillRect(num2 + 1, num3 + 7, (Boss.maxLength - 2) * mGraphics.zoomLevel + 2, 1, Color.white, true);
                }
                g.drawString(string.Format("{0}. {1}", j + 1, boss2.ToString(true)), -(Boss.x + Boss.offsetX), mGraphics.zoomLevel - 3 + num3, array[j - num + Boss.offset]);
            }
        }

        // Token: 0x06000AE4 RID: 2788 RVA: 0x0009B764 File Offset: 0x00099964
        private static void PaintScroll(mGraphics g)
        {
            if (Boss.listBosses.Count > Boss.MAX_BOSS_DISPLAY)
            {
                int num;
                int num2;
                Boss.getButtonUp(out num, out num2);
                int num3;
                int num4;
                Boss.getButtonDown(out num3, out num4);
                int num5;
                int num6;
                int num7;
                Boss.getScrollBar(out num5, out num6, out num7);
                g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.4f));
                g.fillRect(num, num2, 9, num6 + 12);
                g.drawRegion(Mob.imgHP, 0, (Boss.offset < Boss.listBosses.Count - Boss.MAX_BOSS_DISPLAY) ? 24 : 54, 9, 6, 1, num, num2, 0);
                g.drawRegion(Mob.imgHP, 0, (Boss.offset > 0) ? 24 : 54, 9, 6, 0, num3, num4, 0);
                g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.7f));
                g.fillRect(num, num2 + 6 + Mathf.CeilToInt((float)num6 / (float)Boss.listBosses.Count * (float)(Boss.listBosses.Count - Boss.offset - Boss.MAX_BOSS_DISPLAY)), num5, num7);
                g.setColor(new Color(0.7f, 0.7f, 0f, 1f));
                g.drawRect(num, num2 + 6 + Mathf.CeilToInt((float)num6 / (float)Boss.listBosses.Count * (float)(Boss.listBosses.Count - Boss.offset - Boss.MAX_BOSS_DISPLAY)), num5 - 1, num7 - 1);
            }
        }

        // Token: 0x06000AE5 RID: 2789 RVA: 0x0009B8DC File Offset: 0x00099ADC
        private static void PaintRect(mGraphics g)
        {
            int num;
            int num2;
            int num3;
            Boss.getScrollBar(out num, out num2, out num3);
            if (Boss.listBosses.Count <= Boss.MAX_BOSS_DISPLAY)
            {
                num = 0;
            }
            int num4 = Boss.maxLength + 5 + ((num > 0) ? (num + 2) : 0);
            int num5 = Boss.distanceBetweenLines * Math.min(Boss.MAX_BOSS_DISPLAY, Boss.listBosses.Count) + 7;
            if (!Boss.isCollapsed && Boss.listBosses.Count > 0)
            {
                g.setColor(Color.yellow);
                g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength - 3, Boss.y - 5, num4 - Boss.titleWidth - 9 - ((num > 0) ? 2 : 0), 1);
                g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) + num, Boss.y - 5, 3 + ((num > 0) ? 1 : 0), 1);
                g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength - 3, Boss.y - 5, 1, num5);
                g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength - 3 + num4, Boss.y - 5, 1, num5 + 1);
                g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength - 3, Boss.y - 5 + num5, num4 + 1, 1);
            }
            GUIStyle guistyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 7 * mGraphics.zoomLevel,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.UpperRight,
                richText = true
            };
            guistyle.normal.textColor = Color.white;
            Boss.titleWidth = Boss.getWidth(guistyle, Boss.LIST_BOSS);
            g.setColor(new Color(0.2f, 0.2f, 0.2f, 1f));
            g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.titleWidth + num, Boss.y - Boss.distanceBetweenLines, Boss.titleWidth, 8);
            if (GameCanvas.isMouseFocus(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.titleWidth + num, Boss.y - Boss.distanceBetweenLines, Boss.titleWidth, 8))
            {
                g.setColor(guistyle.normal.textColor);
                g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.titleWidth + num, Boss.y - 1, Boss.titleWidth - 1, 1);
            }
            g.drawString(Boss.LIST_BOSS, -(Boss.x + Boss.offsetX) + num, Boss.y - Boss.distanceBetweenLines - 2, guistyle);
            int num6;
            int num7;
            Boss.getCollapseButton(out num6, out num7);
            g.drawRegion(Mob.imgHP, 0, 18, 9, 6, Boss.isCollapsed ? 5 : 4, num6, num7, 0);
        }

        // Token: 0x06000AE6 RID: 2790 RVA: 0x0009BBA4 File Offset: 0x00099DA4
        private static void FillBackground(mGraphics g)
        {
            if (!Boss.isCollapsed && Boss.listBosses.Count > 0)
            {
            g.setColor(0, 0.1f);
                int num;
                int num2;
                int num3;
                Boss.getScrollBar(out num, out num2, out num3);
                if (Boss.listBosses.Count <= Boss.MAX_BOSS_DISPLAY)
                {
                    num = 0;
                }
                int num4 = Boss.maxLength + 5 + ((num > 0) ? (num + 2) : 0);
                int num5 = Boss.distanceBetweenLines * Math.min(Boss.MAX_BOSS_DISPLAY, Boss.listBosses.Count) + 7;
                g.fillRect(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength - 3, Boss.y - 5, num4, num5);
            }
        }

        // Token: 0x06000AE7 RID: 2791 RVA: 0x0009BC60 File Offset: 0x00099E60
        private static int GetMapID(string mapName)
        {
            for (int i = 0; i < TileMap.mapNames.Length; i++)
            {
                if (TileMap.mapNames[i].Equals(mapName))
                {
                    return i;
                }
            }
            return -1;
        }

        // Token: 0x06000AE8 RID: 2792 RVA: 0x0009BC94 File Offset: 0x00099E94
        public static void UpdateTouch()
        {
            if (Boss.lastBoss != -1 && GameCanvas.isHoldPress())
            {
                Boss.lastBoss = -1;
            }
            if (!GameCanvas.isTouch || ChatTextField.gI().isShow || GameCanvas.menu.showMenu)
            {
                return;
            }
            int num;
            int num2;
            int num3;
            Boss.getScrollBar(out num, out num2, out num3);
            int num4;
            int num5;
            Boss.getCollapseButton(out num4, out num5);
            if (GameCanvas.isPointerHoldIn(num4, num5, 9, 6) || GameCanvas.isPointerHoldIn(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.titleWidth + num, Boss.y - Boss.distanceBetweenLines, Boss.titleWidth, 8))
            {
                GameCanvas.isPointerJustDown = false;
                GameScr.gI().isPointerDowning = false;
                if (GameCanvas.isPointerClick)
                {
                    Boss.isCollapsed = !Boss.isCollapsed;
                }
                GameCanvas.clearAllPointerEvent();
                return;
            }
            if (Boss.isCollapsed)
            {
                return;
            }
            int num6 = 0;
            if (Boss.listBosses.Count > Boss.MAX_BOSS_DISPLAY)
            {
                num6 = Boss.listBosses.Count - Boss.MAX_BOSS_DISPLAY;
            }
            for (int i = num6 - Boss.offset; i < Boss.listBosses.Count - Boss.offset; i++)
            {
                if (GameCanvas.isPointerHoldIn(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength, Boss.y + 1 + Boss.distanceBetweenLines * (i - num6 + Boss.offset), Boss.maxLength, 7))
                {
                    GameCanvas.isPointerJustDown = false;
                    GameScr.gI().isPointerDowning = false;
                    if (GameCanvas.isPointerClick)
                    {
                        if (Boss.listBosses[i].isDied)
                        {
                            GameScr.info1.addInfo("Boss đã " + (string.IsNullOrEmpty(Boss.listBosses[i].killer) ? "chết" : ("bị " + Boss.listBosses[i].killer + " tiêu diệt")) + "!", 0);
                        }
                        else
                        {
                            if (Boss.lastBoss == i)
                            {
                                if (TileMap.mapID != Boss.listBosses[i].mapId)
                                {
                                if (Pk9rXmap.IsXmapRunning)
                                {
                                    XmapController.FinishXmap();
                                }
                                XmapController.StartRunToMapId(listBosses[i].mapId);
                                Boss.lastBoss = -1;
                                    return;
                                }
                            }
                            else
                            {
                                Boss.lastBoss = i;
                            }
                            if (TileMap.mapID == Boss.listBosses[i].mapId)
                            {
                                int j = 0;
                                while (j < GameScr.vCharInMap.size())
                                {
                                    Char @char = GameScr.vCharInMap.elementAt(j) as Char;
                                    if (@char.cName == Boss.listBosses[i].name)
                                    {
                                        Char.myCharz().deFocusNPC();
                                        Char.myCharz().itemFocus = null;
                                        Char.myCharz().mobFocus = null;
                                        if (Char.myCharz().charFocus != @char)
                                        {
                                            Char.myCharz().charFocus = @char;
                                            break;
                                        }
                                        Boss.teleportMyChar(@char);
                                        break;
                                    }
                                    else
                                    {
                                        j++;
                                    }
                                }
                                if (j == GameScr.vCharInMap.size())
                                {
                                    if (Boss.listBosses[i].zoneId != -1 && TileMap.zoneID != Boss.listBosses[i].zoneId)
                                    {
                                        GameScr.info1.addInfo(string.Format("Vào khu {0}!", Boss.listBosses[i].zoneId), 0);
                                        Service.gI().requestChangeZone(Boss.listBosses[i].zoneId, 0);
                                        return;
                                    }
                                    GameScr.info1.addInfo("Boss không có trong khu!", 0);
                                }
                            }
                        }
                    }
                    GameCanvas.clearAllPointerEvent();
                    return;
                }
            }
            if (Boss.listBosses.Count > Boss.MAX_BOSS_DISPLAY)
            {
                int num7;
                int num8;
                Boss.getButtonUp(out num7, out num8);
                if (GameCanvas.isPointerMove && GameCanvas.isPointerDown && GameCanvas.isPointerHoldIn(num7, num8, num, num2))
                {
                    float num9 = (float)num2 / (float)Boss.listBosses.Count;
                    float num10 = (float)(GameCanvas.pyMouse - num8) / num9;
                    if (float.IsNaN(num10))
                    {
                        return;
                    }
                    Boss.offset = Mathf.Clamp(Boss.listBosses.Count - Mathf.RoundToInt(num10), 0, Boss.listBosses.Count - Boss.MAX_BOSS_DISPLAY);
                    return;
                }
                else
                {
                    if (GameCanvas.isPointerHoldIn(num7, num8, 9, 6))
                    {
                        GameCanvas.isPointerJustDown = false;
                        GameScr.gI().isPointerDowning = false;
                        if (GameCanvas.isPointerClick)
                        {
                            if (Boss.offset + Boss.MAX_BOSS_DISPLAY <= Boss.listBosses.Count - Boss.MAX_BOSS_DISPLAY)
                            {
                                Boss.offset += Boss.MAX_BOSS_DISPLAY;
                            }
                            else if (Boss.offset < Boss.listBosses.Count - Boss.MAX_BOSS_DISPLAY)
                            {
                                Boss.offset++;
                            }
                        }
                        GameCanvas.clearAllPointerEvent();
                        return;
                    }
                    int num11;
                    int num12;
                    Boss.getButtonDown(out num11, out num12);
                    if (GameCanvas.isPointerHoldIn(num11, num12, 9, 6))
                    {
                        GameCanvas.isPointerJustDown = false;
                        GameScr.gI().isPointerDowning = false;
                        if (GameCanvas.isPointerClick)
                        {
                            if (Boss.offset - Boss.MAX_BOSS_DISPLAY >= 0)
                            {
                                Boss.offset -= Boss.MAX_BOSS_DISPLAY;
                            }
                            else if (Boss.offset > 0)
                            {
                                Boss.offset--;
                            }
                        }
                        GameCanvas.clearAllPointerEvent();
                        return;
                    }
                }
            }
        }

        // Token: 0x06000AE9 RID: 2793 RVA: 0x0009C190 File Offset: 0x0009A390
        public static void Update()
        {
            for (int i = Boss.listBosses.Count - 1; i >= 0; i--)
            {
                Boss boss = Boss.listBosses[i];
                if (boss.mapId == TileMap.mapID && !Char.isLoadingMap)
                {
                    int j = 0;
                    while (j < GameScr.vCharInMap.size())
                    {
                        Char @char = GameScr.vCharInMap.elementAt(j) as Char;
                        if (@char.cName == boss.name)
                        {
                            if (boss.zoneId == -1)
                            {
                                boss.zoneId = TileMap.zoneID;
                            }
                            if (@char.isDie || @char.cHP == 0L)
                            {
                                boss.isDied = true;
                                break;
                            }
                            break;
                        }
                        else
                        {
                            j++;
                        }
                    }
                    if (boss.zoneId == TileMap.zoneID && j == GameScr.vCharInMap.size())
                    {
                        boss.isDied = true;
                    }
                }
            }
            if (GameCanvas.panel.indexMod[0] && !Boss.isCollapsed && GameCanvas.isMouseFocus(GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.maxLength, Boss.y + 1, Boss.maxLength, 8 * Boss.MAX_BOSS_DISPLAY))
            {
                if (GameCanvas.pXYScrollMouse > 0 && Boss.offset < Boss.listBosses.Count - Boss.MAX_BOSS_DISPLAY)
                {
                    Boss.offset++;
                }
                if (GameCanvas.pXYScrollMouse < 0 && Boss.offset > 0)
                {
                    Boss.offset--;
                }
            }
        }

        // Token: 0x06000AEA RID: 2794 RVA: 0x0009C2ED File Offset: 0x0009A4ED
        private static void getButtonUp(out int buttonUpX, out int buttonUpY)
        {
            buttonUpX = GameCanvas.w - (Boss.x + Boss.offsetX) + 2;
            buttonUpY = Boss.y + 1;
        }

        // Token: 0x06000AEB RID: 2795 RVA: 0x0009C30D File Offset: 0x0009A50D
        private static void getButtonDown(out int buttonDownX, out int buttonDownY)
        {
            buttonDownX = GameCanvas.w - (Boss.x + Boss.offsetX) + 2;
            buttonDownY = Boss.y + 2 + Boss.distanceBetweenLines * (Boss.MAX_BOSS_DISPLAY - 1);
        }

        // Token: 0x06000AEC RID: 2796 RVA: 0x0009C33B File Offset: 0x0009A53B
        private static void getScrollBar(out int scrollBarWidth, out int scrollBarHeight, out int scrollBarThumbHeight)
        {
            scrollBarWidth = 9;
            scrollBarHeight = Boss.MAX_BOSS_DISPLAY * Boss.distanceBetweenLines - 1 - 12;
            scrollBarThumbHeight = Mathf.CeilToInt((float)Boss.MAX_BOSS_DISPLAY / (float)Boss.listBosses.Count * (float)scrollBarHeight);
        }

        // Token: 0x06000AED RID: 2797 RVA: 0x0009C370 File Offset: 0x0009A570
        private static void getCollapseButton(out int collapseButtonX, out int collapseButtonY)
        {
            int num;
            int num2;
            int num3;
            Boss.getScrollBar(out num, out num2, out num3);
            if (Boss.listBosses.Count <= Boss.MAX_BOSS_DISPLAY)
            {
                num = 0;
            }
            collapseButtonX = GameCanvas.w - (Boss.x + Boss.offsetX) - Boss.titleWidth + num - 8;
            collapseButtonY = Boss.y - Boss.distanceBetweenLines + 1;
        }

        // Token: 0x06000AEE RID: 2798 RVA: 0x0009C3C7 File Offset: 0x0009A5C7
        internal static int getWidth(GUIStyle gUIStyle, string s)
        {
            return (int)(gUIStyle.CalcSize(new GUIContent(s)).x * 1.05f / (float)mGraphics.zoomLevel);
        }

        // Token: 0x06000AEF RID: 2799 RVA: 0x0009C3E8 File Offset: 0x0009A5E8
        public static void fillRect(int x, int y, int w, int h, Color color, bool scale = true)
        {
            if (scale)
            {
                x *= mGraphics.zoomLevel;
                y *= mGraphics.zoomLevel;
            }
            if (w < 0 || h < 0)
            {
                return;
            }
            Texture2D texture2D = new Texture2D(1, 1);
            texture2D.SetPixel(0, 0, color);
            texture2D.Apply();
            GUI.DrawTexture(new Rect((float)x, (float)y, (float)w, (float)h), texture2D);
        }

        // Token: 0x06000AF0 RID: 2800 RVA: 0x0009C43F File Offset: 0x0009A63F
        public static void teleportMyChar(IMapObject obj)
        {
            Boss.teleportMyChar(obj.getX(), obj.getY());
        }

        // Token: 0x06000AF1 RID: 2801 RVA: 0x0009C454 File Offset: 0x0009A654
        public static void teleportMyChar(int x, int y)
        {
            Char.myCharz().currentMovePoint = new MovePoint(x, y);
            Char.myCharz().cx = x;
            Char.myCharz().cy = y;
            Service.gI().charMove();
            Char.myCharz().cx = x;
            Char.myCharz().cy = y + 1;
            Service.gI().charMove();
            Char.myCharz().cx = x;
            Char.myCharz().cy = y;
            Service.gI().charMove();
        }

        // Token: 0x04001428 RID: 5160
        public string name;

        // Token: 0x04001429 RID: 5161
        public string map;

        // Token: 0x0400142A RID: 5162
        public int mapId;

        // Token: 0x0400142B RID: 5163
        public int zoneId = -1;

        // Token: 0x0400142C RID: 5164
        public DateTime AppearTime;

        // Token: 0x0400142D RID: 5165
        public bool isDied;

        // Token: 0x0400142E RID: 5166
        public string killer;

        // Token: 0x0400142F RID: 5167
        public static List<Boss> listBosses = new List<Boss>();

        // Token: 0x04001430 RID: 5168
        public static int distanceBetweenLines = 8;

        // Token: 0x04001431 RID: 5169
        private static int offset = 0;

        // Token: 0x04001432 RID: 5170
        public static int x = 6;

        // Token: 0x04001433 RID: 5171
        public static int y = 50;

        // Token: 0x04001434 RID: 5172
        private static int maxLength = 0;

        // Token: 0x04001435 RID: 5173
        private static int lastBoss = -1;

        // Token: 0x04001436 RID: 5174
        public static bool isCollapsed;

        // Token: 0x04001437 RID: 5175
        public static readonly int MAX_BOSS_DISPLAY = 5;

        // Token: 0x04001438 RID: 5176
        private static readonly int MAX_BOSS = 100;

        // Token: 0x04001439 RID: 5177
        private static readonly string LIST_BOSS = "Danh sách Boss";

        // Token: 0x0400143A RID: 5178
        private static GUIStyle collapsedStyle;

        // Token: 0x0400143B RID: 5179
        private static int listBossWidth = 0;

        // Token: 0x0400143C RID: 5180
        private static int titleWidth;

        // Token: 0x0400143D RID: 5181
        private static int offsetX;

        // Token: 0x0400143E RID: 5182
        private static readonly List<string> strBossHasBeenKilled = new List<string> { " mọi người đều ngưỡng mộ", " vừa tiêu diệt được ", " everyone admired.", " semua orang mengagumi.", " đã đánh bại và nhận được cải trang thành ", " killed and receive disguise of ", " membunuh Dan menerima disguise ", ": Đã tiêu diệt được ", ": defeated ", ": mengalahkan " };

        // Token: 0x0400143F RID: 5183
        private static readonly List<string> strBossAppeared = new List<string> { "Boss ", " vừa xuất hiện tại ", " appear at ", " muncul di ", " khu vực ", " zone ", " zona " };
    }

