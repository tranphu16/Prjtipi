using System;
using UnityEngine;

    // Token: 0x020000C5 RID: 197
    public static class CharExtensions
    {
        // Token: 0x06000AF3 RID: 2803 RVA: 0x00014097 File Offset: 0x00012297
        public static int getTimeTDHS()
        {
            return 0;
        }

        // Token: 0x06000AF4 RID: 2804 RVA: 0x0009C5FC File Offset: 0x0009A7FC
        public static int getTimeHold(this Char @char)
        {
            return 35;
        }

        // Token: 0x06000AF5 RID: 2805 RVA: 0x0009C600 File Offset: 0x0009A800
        public static int getTimeMonkey(this Char @char)
        {
            int num = 60;
            try
            {
                if (!@char.me)
                {
                    switch (@char.head)
                    {
                        case 192:
                            num = 60;
                            break;
                        case 195:
                            num = 70;
                            break;
                        case 196:
                            num = 80;
                            break;
                        case 197:
                            num = 100;
                            break;
                        case 198:
                            num = 120;
                            break;
                        case 199:
                            num = 90;
                            break;
                        case 200:
                            num = 110;
                            break;
                    }
                }
                else if (Char.myCharz().cgender == 2)
                {
                    num = Char.myCharz().getSkill(Char.myCharz().nClass.skillTemplates[3]).point * 10 + 50;
                }
            }
            catch
            {
                num = 121;
            }
            return num;
        }

        // Token: 0x06000AF6 RID: 2806 RVA: 0x0009C6C4 File Offset: 0x0009A8C4
        public static int getTimeShield(this Char @char)
        {
            int num;
            try
            {
                if (!@char.me)
                {
                    num = 45;
                }
                else
                {
                    num = Char.myCharz().getSkill(Char.myCharz().nClass.skillTemplates[7]).point * 5 + 10;
                }
            }
            catch
            {
                num = 45;
            }
            return num;
        }

        // Token: 0x06000AF7 RID: 2807 RVA: 0x0009C720 File Offset: 0x0009A920
        public static int getTimeMobMe(this Char @char)
        {
            int num = 60;
            try
            {
                if (!@char.me)
                {
                    int templateId = @char.mobMe.templateId;
                    if (templateId <= 25)
                    {
                        if (templateId != 8)
                        {
                            if (templateId != 11)
                            {
                                if (templateId == 25)
                                {
                                    num = 165;
                                }
                            }
                            else
                            {
                                num = 95;
                            }
                        }
                        else
                        {
                            num = 60;
                        }
                    }
                    else if (templateId <= 43)
                    {
                        if (templateId != 32)
                        {
                            if (templateId == 43)
                            {
                                num = 200;
                            }
                        }
                        else
                        {
                            num = 130;
                        }
                    }
                    else if (templateId != 49)
                    {
                        if (templateId == 50)
                        {
                            num = 270;
                        }
                    }
                    else
                    {
                        num = 235;
                    }
                }
                else if (Char.myCharz().cgender == 1)
                {
                    num = (Char.myCharz().getSkill(Char.myCharz().nClass.skillTemplates[4]).point - 1) * 35 + 60;
                }
            }
            catch
            {
                num = 270;
            }
            return num;
        }

        // Token: 0x06000AF8 RID: 2808 RVA: 0x0009C800 File Offset: 0x0009AA00
        public static int getTimeHypnotize(this Char @char)
        {
            return 12;
        }

        // Token: 0x06000AF9 RID: 2809 RVA: 0x0009C804 File Offset: 0x0009AA04
        public static int getTimeStone(this Char @char)
        {
            return 5;
        }

        // Token: 0x06000AFA RID: 2810 RVA: 0x0009C807 File Offset: 0x0009AA07
        public static int getTimeHuytSao(this Char @char)
        {
            return 31;
        }

        // Token: 0x06000AFB RID: 2811 RVA: 0x0009C807 File Offset: 0x0009AA07
        public static int getTimeChocolate(this Char @char)
        {
            return 31;
        }

        // Token: 0x06000AFC RID: 2812 RVA: 0x0009C80C File Offset: 0x0009AA0C
        public static string getNameWithoutClanTag(this Char @char, bool enableRichText = false)
        {
            string text = (@char.cName.Contains("Whis") ? @char.cName : @char.cName.Remove(0, @char.cName.IndexOf(']') + 1).TrimStart(new char[] { ' ', '#', '$' }));
            if (enableRichText)
            {
                if (@char.isPet())
                {
                    text = "<color=cyan>" + text + "</color>";
                }
                else if (@char.isBoss())
                {
                    text = string.Format("<color=red><size={0}>{1}</size></color>", 7 * mGraphics.zoomLevel, text);
                }
                else
                {
                    text = "<color=yellow>" + text + "</color>";
                }
            }
            return text;
        }

        // Token: 0x06000AFD RID: 2813 RVA: 0x0009C8B7 File Offset: 0x0009AAB7
        public static string getClanTag(this Char @char)
        {
            if (!@char.cName.Contains("Whis"))
            {
                return @char.cName.Substring(0, @char.cName.IndexOf(']') + 1) + " ";
            }
            return string.Empty;
        }

        // Token: 0x06000AFE RID: 2814 RVA: 0x0009C8F8 File Offset: 0x0009AAF8
        public static bool isNormalChar(this Char @char, bool isIncludeBoss = false, bool isIncludePet = false)
        {
            bool flag = !string.IsNullOrEmpty(@char.cName) && @char.cName != "Trọng tài";
            if (!string.IsNullOrEmpty(@char.cName))
            {
                if (!isIncludeBoss)
                {
                    flag = flag && !char.IsUpper(@char.getNameWithoutClanTag(false)[0]);
                }
                if (!isIncludePet)
                {
                    flag = flag && !@char.isPet();
                }
            }
            return flag;
        }

        // Token: 0x06000AFF RID: 2815 RVA: 0x0009C966 File Offset: 0x0009AB66
        public static bool isBoss(this Char @char)
        {
            return !@char.isPet() && @char.cName != "Trọng tài" && char.IsUpper(@char.getNameWithoutClanTag(false)[0]);
        }

        // Token: 0x06000B00 RID: 2816 RVA: 0x0009C996 File Offset: 0x0009AB96
        public static bool isPet(this Char @char)
        {
            return @char.isPet || @char.isMiniPet || @char.cName.StartsWith("#") || @char.cName.StartsWith("$");
        }

        // Token: 0x06000B01 RID: 2817 RVA: 0x0009C9CC File Offset: 0x0009ABCC
        public static bool isPk(this Char @char, Char cme)
        {
            return @char.cTypePk == 5 || cme.cTypePk == 5 || (@char.cTypePk == 3 && cme.cTypePk == 3) || (@char.cFlag != 0 && cme.cFlag != 0 && cme.cFlag != @char.cFlag) || @char.cFlag == 8 || cme.cFlag == 8;
        }

        // Token: 0x06000B02 RID: 2818 RVA: 0x0009CA34 File Offset: 0x0009AC34
        public static Char ClosestChar(int maxDistance, bool isNormalCharOnly)
        {
            int num = 9999;
            Char @char = null;
            if (GameScr.vCharInMap.size() <= 0)
            {
                return null;
            }
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char char2 = (Char)GameScr.vCharInMap.elementAt(i);
                if (!isNormalCharOnly || char2.isNormalChar(false, false))
                {
                    int num2 = Res.distance(Char.myCharz().cx, Char.myCharz().cy, char2.cx, char2.cy);
                    if (!char2.me && num2 < num)
                    {
                        num = num2;
                        @char = char2;
                    }
                }
            }
            if (@char != null && Res.distance(Char.myCharz().cx, Char.myCharz().cy, @char.cx, @char.cy) > maxDistance)
            {
                @char = null;
            }
            return @char;
        }

        // Token: 0x06000B03 RID: 2819 RVA: 0x0009CAF4 File Offset: 0x0009ACF4
        public static string getGender(this Char @char, bool enableRichText = false)
        {
            if (enableRichText)
            {
                if (@char.cgender == 0)
                {
                    return "<color=#0080ffff>TĐ</color>";
                }
                if (@char.cgender == 1)
                {
                    return "<color=#00c000ff>NM</color>";
                }
                if (@char.cgender == 2)
                {
                    return "<color=#ffff80ff>XD</color>";
                }
                return "<color=magenta>BĐ</color>";
            }
            else
            {
                if (@char.cgender == 0)
                {
                    return "TĐ";
                }
                if (@char.cgender == 1)
                {
                    return "NM";
                }
                if (@char.cgender == 2)
                {
                    return "XD";
                }
                return "BĐ";
            }
        }

        // Token: 0x06000B04 RID: 2820 RVA: 0x0009CB68 File Offset: 0x0009AD68
        public static Color getFlagColor(this Char @char)
        {
            switch (@char.cFlag)
            {
                case 1:
                    return Color.cyan;
                case 2:
                    return Color.red;
                case 3:
                    return new Color(0.56f, 0.19f, 0.77f);
                case 4:
                    return Color.yellow;
                case 5:
                    return Color.green;
                case 6:
                    return Color.magenta;
                case 7:
                    return new Color(1f, 0.5f, 0f);
                case 8:
                    return new Color(0.18f, 0.18f, 0.18f);
                case 9:
                    return Color.blue;
                case 10:
                    return Color.red;
                case 11:
                    return Color.blue;
                case 12:
                    return Color.white;
                case 13:
                    return Color.black;
                default:
                    return Color.clear;
            }
        }

        // Token: 0x06000B05 RID: 2821 RVA: 0x0009CC3C File Offset: 0x0009AE3C
        public static int getSuicideRange(this Char @char)
        {
            int num = 880;
            if (@char.me && @char.cgender == 2)
            {
                Skill skill = Char.myCharz().getSkill(Char.myCharz().nClass.skillTemplates[4]);
                if (skill == null)
                {
                    return 0;
                }
                num = 340 * (skill.point - 1) / 3 + 200;
            }
            return num;
        }

        // Token: 0x06000B06 RID: 2822 RVA: 0x0009CC9C File Offset: 0x0009AE9C
        public static Char findCharInMap(string name)
        {
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                Char @char = (Char)GameScr.vCharInMap.elementAt(i);
                if (@char.getNameWithoutClanTag(false) == name)
                {
                    return @char;
                }
            }
            return null;
        }
    }

