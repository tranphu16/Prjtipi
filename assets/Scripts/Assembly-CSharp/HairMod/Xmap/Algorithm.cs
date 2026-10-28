
public class Algorithm
{
    public static void GotoNpc(int npcID)
    {
        for (int i = 0; i < GameScr.vNpc.size(); i++)
        {
            Npc npc = (Npc)GameScr.vNpc.elementAt(i);
            if (npc.template.npcTemplateId == npcID && global::Math.abs(npc.cx - global::Char.myCharz().cx) >= 50)
            {
                GotoXY(npc.cx, npc.cy - 1);
                global::Char.myCharz().focusManualTo(npc);
                return;
            }

        }

    }
    public static void FocusTo(int charId)
    {
        for (int i = 0; i < GameScr.vCharInMap.size(); i++)
        {
            global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
            bool flag = !@char.isMiniPet && !@char.isPet && @char.charID == charId;
            if (flag)
            {
                global::Char.myCharz().mobFocus = null;
                global::Char.myCharz().npcFocus = null;
                global::Char.myCharz().itemFocus = null;
                global::Char.myCharz().charFocus = @char;
                break;
            }
        }
    }
    public static string getFlag(int flag)
    {
        if (flag == -1 || flag == 0)
        {
            return "";
        }
        string text = "";
        switch (flag)
        {
            case 1:
                text = "Cờ xanh";
                break;
            case 2:
                text = "Cờ đỏ";
                break;
            case 3:
                text = "Cờ tím";
                break;
            case 4:
                text = "Cờ vàng";
                break;
            case 5:
                text = "Cờ lục";
                break;
            case 6:
                text = "Cờ hồng";
                break;
            case 7:
                text = "Cờ cam";
                break;
            case 8:
                text = "Cờ đen";
                break;
            case 9:
                text = "Cờ Kaio";
                break;
            case 10:
                text = "Cờ Mabu";
                break;
            case 11:
                text = "Cờ xanh dương";
                break;
        }
        if (!text.Equals(""))
        {
            return "(" + text + ") ";
        }
        return text;
    }
    public static void MoveMyChar(int x, int y)
    {
        if (GameScr.canAutoPlay)
        {
            global::Char.myCharz().cx = x;
            global::Char.myCharz().cy = y;
            Service.gI().charMove();
            return;
        }
        global::Char.myCharz().cx = x;
        global::Char.myCharz().cy = y;
        Service.gI().charMove();
        global::Char.myCharz().cx = x;
        global::Char.myCharz().cy = y + 1;
        Service.gI().charMove();
        global::Char.myCharz().cx = x;
        global::Char.myCharz().cy = y;
        Service.gI().charMove();
    }
    public static int MapToX(int xSd)
    {
        int num = 50;
        int i = 0;
        while (i < 30)
        {
            i++;
            num += 24;
            if (TileMap.tileTypeAt(xSd, num, 2))
            {
                if (num % 24 != 0)
                {
                    num -= num % 24;
                }
                return num;
            }
        }
        return num;
    }
    public static void VarInfo(string text, bool var)
    {
        GameScr.info1.addInfo(string.Concat(new object[] {
            "|5|",
            text,
            " ",
            (var) ? "[Bật]" : "[Tắt]"
        }), 0);
    }
    public static Item FindNameItemBagUI(string name)
    {
        for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
        {
            Item item = global::Char.myCharz().arrItemBag[i];
            if (item != null && item.template.name.Contains(name))
            {
                return item;
            }
        }
        return null;
    }
    public static void UseItemBagUI(int idItem)
    {
        for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
        {
            Item item = Char.myCharz().arrItemBag[i];
            if (item != null && item.template.id == idItem)
            {
                Service.gI().useItem(0, 1, (sbyte)item.indexUI, -1);
                break;
            }
        }
    }
    public static bool FindItemBagUI(int idItem)
    {
        for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
        {
            Item item = global::Char.myCharz().arrItemBag[i];
            if (item != null && item.template.id == idItem)
            {
                return true;
            }
        }
        return false;
    }
    public static int FindIndexItem(int id)
    {
        for(int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
        {
            Item item = global::Char.myCharz().arrItemBag[i];
            if (item != null && item.template.id == id)
            {
                return item.indexUI;
            }
        }
        return -1;
    }
    public static void GotoXY(int x, int y)
    {
        global::Char.myCharz().cx = x;
        global::Char.myCharz().cy = y;
        Service.gI().charMove();
    }

}
