using JetBrains.Annotations;
using System;

public class SanBoss
{
    public string name;
    public string mapName;
    public int mapID;
    public DateTime time;
    public SanBoss(string text)
    {
        text = text.Replace("BOSS ", "").Replace(" vừa xuất hiện tại ", "|").Replace(" appear at ", "|");
        string[] array = text.Split(new char[]
        {
            '|'
        });
        this.name = array[0].Trim();
        this.mapName = array[1].Trim();
        this.mapID = this.GetMapID(this.mapName);
        this.time = DateTime.Now;
    }
    public void paint(mGraphics g, int x, int y, int align)
    {
        TimeSpan timeSpan = DateTime.Now.Subtract(this.time);
        int num = (int)timeSpan.TotalSeconds;
        mFont mFont = mFont.tahoma_7b_yellowL;
        if (TileMap.mapID == this.mapID)
        {
            mFont = mFont.tahoma_7_red;
            for (int i = 0; i < GameScr.vCharInMap.size(); i++)
            {
                if (((global::Char)GameScr.vCharInMap.elementAt(i)).cName.Equals(this.name))
                {
                    mFont = mFont.tahoma_7b_red;
                    break;
                }
            }
        }
        mFont.drawString(g, string.Concat(new string[]
        {
            this.name,
            " - ",
            this.mapName,
            " - ",
            (num < 60) ? (num.ToString() + "s") : (timeSpan.Minutes.ToString() + "ph"),
            " trước"
        }), x, y, align, mFont.tahoma_7_grey);
    }
    public int GetMapID(string mapName)
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
}