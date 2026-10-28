public class Skill
{
    public const sbyte ATT_STAND = 0;

    public const sbyte ATT_FLY = 1;

    public const sbyte SKILL_AUTO_USE = 0;

    public const sbyte SKILL_CLICK_USE_ATTACK = 1;

    public const sbyte SKILL_CLICK_USE_BUFF = 2;

    public const sbyte SKILL_CLICK_NPC = 3;

    public const sbyte SKILL_CLICK_LIVE = 4;

    public SkillTemplate template;

    public short skillId;

    public int point;

    public long powRequire;

    public int coolDown;

    public long lastTimeUseThisSkill;

    public int dx;

    public int dy;

    public int maxFight;

    public int manaUse;

    public SkillOption[] options;

    public bool paintCanNotUseSkill;

    public short damage;

    public string moreInfo;

    public short price;

    public short curExp;

    public string strCurExp()
    {
        if (curExp / 10 >= 100)
        {
            return "MAX";
        }
        if (curExp % 10 == 0)
        {
            return curExp / 10 + "%";
        }
        int num = curExp % 10;
        return curExp / 10 + "." + num % 10 + "%";
    }

    public string strTimeReplay()
    {
        if (coolDown % 1000 == 0)
        {
            return coolDown / 1000 + string.Empty;
        }
        int num = coolDown % 1000;
        return coolDown / 1000 + "." + ((num % 100 != 0) ? (num / 10) : (num / 100));
    }

    public void paint(int x, int y, mGraphics g)
    {
        SmallImage.drawSmallImage(g, this.template.iconId, x, y, 0, StaticObj.VCENTER_HCENTER);
       
        long num = mSystem.currentTimeMillis() - this.lastTimeUseThisSkill;
        if (num >= (long)this.coolDown)
        {
            this.paintCanNotUseSkill = false;
            return;
        }
        this.paintCanNotUseSkill = true;
        try
        {
            int num2 = (int)(((long)this.coolDown - num) * 84L / (long)this.coolDown);
            if (num2 > 74)
            {
                this.method_2(g, x, y, x + 84 - num2, x + 10, y - 11);
                this.method_3(g, x, y, x + 10, y - 11, y + 10);
                this.method_2(g, x, y, x - 11, x + 10, y + 10);
                this.method_3(g, x, y, x - 11, y - 11, y + 10);
                this.method_2(g, x, y, x - 11, x, y - 11);
                g.setColor(0);
                g.fillRect(x + 84 - num2, y - 11, num2 - 74, 1);
                g.fillRect(x + 10, y - 11, 1, 22);
                g.fillRect(x - 11, y + 10, 21, 1);
                g.fillRect(x - 11, y - 11, 1, 22);
                g.fillRect(x - 11, y - 11, 11, 1);
            }
            else if (num2 > 53)
            {
                this.method_3(g, x, y, x + 10, y + 63 - num2, y + 10);
                this.method_2(g, x, y, x - 11, x + 10, y + 10);
                this.method_3(g, x, y, x - 11, y - 11, y + 10);
                this.method_2(g, x, y, x - 11, x, y - 11);
                g.setColor(0);
                g.fillRect(x + 10, y + 63 - num2, 1, num2 - 53);
                g.fillRect(x - 11, y + 10, 22, 1);
                g.fillRect(x - 11, y - 11, 1, 22);
                g.fillRect(x - 11, y - 11, 11, 1);
            }
            else if (num2 > 32)
            {
                this.method_2(g, x, y, x - 11, x - 11 + num2 - 32, y + 10);
                this.method_3(g, x, y, x - 11, y - 11, y + 10);
                this.method_2(g, x, y, x - 11, x, y - 11);
                g.setColor(0);
                g.fillRect(x - 11, y + 10, num2 - 32, 1);
                g.fillRect(x - 11, y - 11, 1, 22);
                g.fillRect(x - 11, y - 11, 11, 1);
            }
            else if (num2 > 11)
            {
                this.method_3(g, x, y, x - 11, y - 11, y - 11 + num2 - 11);
                this.method_2(g, x, y, x - 11, x, y - 11);
                g.setColor(0);
                g.fillRect(x - 11, y - 11, 1, num2 - 11);
                g.fillRect(x - 11, y - 11, 11, 1);
            }
            else
            {
                this.method_2(g, x, y, x - num2, x, y - 11);
                g.setColor(0);
                g.fillRect(x - num2, y - 11, num2, 1);
            }
            long num3 = (long)this.coolDown - num;
         
           
            if (num3 > 10000L)
            {
                mFont.tahoma_7.drawString(g, NinjaUtil.getMoneys(num3).Split(new char[]
                {
                    '.'
                })[0], x, y - 6, 2);
            }
            else if (num3 > 1000L)
            {
                mFont.tahoma_7.drawString(g, NinjaUtil.getMoneys(num3).Substring(0, 3), x, y - 6, 2);
            }
            else
            {
                mFont.tahoma_7.drawString(g, "0." + num3.ToString().Substring(0, 2), x, y - 6, 2);
            }
        }
        catch
        {
        }
    }

    // Token: 0x06000A31 RID: 2609 RVA: 0x00097924 File Offset: 0x00095B24
    public void method_2(mGraphics g, int x, int y, int x1, int x2, int y1)
    {
        g.setColor(2721889, 0.7f);
        for (int i = x1; i < x2; i++)
        {
            g.drawLine(x, y, i, y1);
        }
    }

    // Token: 0x06000A32 RID: 2610 RVA: 0x0009795C File Offset: 0x00095B5C
    public void method_3(mGraphics g, int x, int y, int x1, int y1, int y2)
    {
        g.setColor(2721889, 0.7f);
        for (int i = y1; i < y2; i++)
        {
            g.drawLine(x, y, x1, i);
        }
    }
}
