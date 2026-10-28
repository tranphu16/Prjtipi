using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

// Token: 0x0200000A RID: 10
public class AutoItem : IActionListener, IChatable
{
    // Token: 0x0600002C RID: 44 RVA: 0x0000EF8C File Offset: 0x0000D18C
    public AutoItem()
    {
        this.list_0 = new List<AutoItem.ItemAuto>();
        this.list_1 = new List<string>();
        this.list_2 = new List<string>();
        this.string_0 = new string[]
        {
            "Nhập delay",
            "giây"
        };
        this.string_1 = new string[]
        {
            "Nhập số lượng bán",
            "số lượng"
        };
        this.string_2 = new string[]
        {
            "Nhập số lượng mua",
            "số lượng"
        };
    }

    // Token: 0x0600002D RID: 45 RVA: 0x0000F014 File Offset: 0x0000D214
    public void method_0()
    {
        if (this.list_0.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < this.list_0.Count; i++)
        {
            AutoItem.ItemAuto itemAuto = this.list_0[i];
            if (mSystem.currentTimeMillis() - itemAuto.lastTimeUse > (long)(itemAuto.delay * 1000) && itemAuto != null)
            {
                itemAuto.lastTimeUse = mSystem.currentTimeMillis();
                Service.gI().useItem(0, 1, (sbyte)itemAuto.index, -1);
                return;
            }
        }
    }

    // Token: 0x0600002E RID: 46 RVA: 0x0000F090 File Offset: 0x0000D290
    public void onChatFromMe(string text, string to)
    {
        ChatTextField chatTextField = ChatTextField.gI();
        if (chatTextField.tfChat.getText() == null || chatTextField.tfChat.getText().Equals(string.Empty) || text.Equals(string.Empty) || text == null)
        {
            chatTextField.isShow = false;
            return;
        }
        if (chatTextField.strChat.Equals(this.string_0[0]))
        {
            try
            {
                int delay = int.Parse(chatTextField.tfChat.getText());
                this.gclass6_0.delay = delay;
                GameScr.info1.addInfo(string.Concat(new string[]
                {
                    "Auto ",
                    this.gclass6_0.name,
                    ": ",
                    delay.ToString(),
                    " giây"
                }), 0);
                this.list_0.Add(this.gclass6_0);
            }
            catch
            {
                GameScr.info1.addInfo("Delay Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
            }
            HairMod.ResetTF();
            return;
        }
        if (chatTextField.strChat.Equals(this.string_2[0]))
        {
            try
            {
                int quantity = int.Parse(chatTextField.tfChat.getText());
                this.gclass6_0.quantity = quantity;
                new Thread(new ThreadStart(this.method_11)).Start();
            }
            catch
            {
                GameScr.info1.addInfo("Số Lượng Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
            }
            HairMod.ResetTF();
            return;
        }
        if (!chatTextField.strChat.Equals(this.string_1[0]))
        {
            return;
        }
        try
        {
            int quantity2 = int.Parse(chatTextField.tfChat.getText());
            this.gclass6_0.quantity = quantity2;
            new Thread(new ThreadStart(this.method_12)).Start();
        }
        catch
        {
            GameScr.info1.addInfo("Số Lượng Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
        }
        HairMod.ResetTF();
    }

    // Token: 0x0600002F RID: 47
    public void onCancelChat()
    {
    }

    // Token: 0x06000030 RID: 48
    public void perform(int idAction, object p)
    {
        switch (idAction)
        {
            case 1:
                GameCanvas.panel.hide();
                this.method_3((AutoItem.ItemAuto)p);
                return;
            case 2:
                this.method_2((int)p);
                return;
            case 3:
                this.method_4((AutoItem.ItemAuto)p);
                return;
            case 4:
                this.list_1.Add(((Item)p).getFullName());
                return;
            case 5:
                this.list_2.Add(((Item)p).getFullName());
                return;
            case 6:
                this.list_1.Remove(((Item)p).getFullName());
                return;
            case 7:
                this.list_2.Remove(((Item)p).getFullName());
                return;
            default:
                return;
        }
    }

    // Token: 0x06000031 RID: 49 RVA: 0x0000F378 File Offset: 0x0000D578
    public bool method_1(int itemID)
    {
        for (int i = 0; i < this.list_0.Count; i++)
        {
            if (this.list_0[i].id == itemID)
            {
                return true;
            }
        }
        return false;
    }

    // Token: 0x06000032 RID: 50 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
    private void method_2(int int_0)
    {
        for (int i = 0; i < this.list_0.Count; i++)
        {
            if (this.list_0[i].id == int_0)
            {
                this.list_0.RemoveAt(i);
                return;
            }
        }
    }

    // Token: 0x06000033 RID: 51 RVA: 0x00008008 File Offset: 0x00006208
    private void method_3(AutoItem.ItemAuto gclass6_1)
    {
        this.gclass6_0 = gclass6_1;
        HairMod.InputTF(this, this.string_0[0], this.string_0[1], TField.INPUT_TYPE_NUMERIC);
    }

    // Token: 0x06000034 RID: 52 RVA: 0x0000F3FC File Offset: 0x0000D5FC
    private void method_4(AutoItem.ItemAuto gclass6_1)
    {
        this.gclass6_0 = gclass6_1;
        GameCanvas.panel.isShow = false;
        if (gclass6_1.isSell)
        {
            HairMod.InputTF(this, this.string_1[0], this.string_1[1], TField.INPUT_TYPE_NUMERIC);
            return;
        }
        HairMod.InputTF(this, this.string_2[0], this.string_2[1], TField.INPUT_TYPE_NUMERIC);
    }

    // Token: 0x06000035 RID: 53 RVA: 0x0000F46C File Offset: 0x0000D66C
    private void method_5(AutoItem.ItemAuto gclass6_1)
    {
        Thread.Sleep(100);
        short index = gclass6_1.index;
        while (gclass6_1.quantity > 0)
        {
            if (global::Char.myCharz().arrItemBag[(int)index] != null)
            {
                if (global::Char.myCharz().arrItemBag[(int)index] == null || global::Char.myCharz().arrItemBag[(int)index].template.id == (short)gclass6_1.id)
                {
                    Service.gI().saleItem(0, 1, (short)global::Char.myCharz().arrItemBag[(int)index].indexUI);
                    Thread.Sleep(100);
                    Service.gI().saleItem(1, 1, (short)global::Char.myCharz().arrItemBag[(int)index].indexUI);
                    Thread.Sleep(1000);
                    gclass6_1.quantity--;
                    if (global::Char.myCharz().xu <= 1963100000L)
                    {
                        continue;
                    }
                    GameScr.info1.addInfo("Xong!", 0);
                    return;
                }
            }
            GameScr.info1.addInfo("Không Tìm Thấy Item!", 0);
            return;
        }
        GameScr.info1.addInfo("Xong!", 0);
    }

    // Token: 0x06000036 RID: 54 RVA: 0x0000F58C File Offset: 0x0000D78C
    private void method_6(AutoItem.ItemAuto gclass6_1)
    {
        while (gclass6_1.quantity > 0 && !GameScr.gI().isBagFull())
        {
            Service.gI().buyItem((sbyte)((!gclass6_1.isGold) ? 1 : 0), gclass6_1.id, 0);
            gclass6_1.quantity--;
            Thread.Sleep(1000);
        }
        GameScr.info1.addInfo("Xong!", 0);
    }


    // Token: 0x06000039 RID: 57 RVA: 0x00008031 File Offset: 0x00006231
    public List<string> method_9()
    {
        return this.list_1;
    }

    // Token: 0x0600003A RID: 58 RVA: 0x00008039 File Offset: 0x00006239
    public List<string> method_10()
    {
        return this.list_2;
    }

    // Token: 0x0600003C RID: 60 RVA: 0x0000804D File Offset: 0x0000624D
    [CompilerGenerated]
    private void method_11()
    {
        this.method_6(this.gclass6_0);
    }

    // Token: 0x0600003D RID: 61 RVA: 0x0000805B File Offset: 0x0000625B
    [CompilerGenerated]
    private void method_12()
    {
        this.method_5(this.gclass6_0);
    }

    // Token: 0x0400003D RID: 61
    public static AutoItem gclass5_0 = new AutoItem();

    // Token: 0x0400003E RID: 62
    private List<AutoItem.ItemAuto> list_0;

    // Token: 0x0400003F RID: 63
    private AutoItem.ItemAuto gclass6_0;

    // Token: 0x04000040 RID: 64
    private List<string> list_1;

    // Token: 0x04000041 RID: 65
    private List<string> list_2;

    // Token: 0x04000042 RID: 66
    private bool bool_0;

    // Token: 0x04000043 RID: 67
    private string[] string_0;

    // Token: 0x04000044 RID: 68
    private string[] string_1;

    // Token: 0x04000045 RID: 69
    private string[] string_2;

    // Token: 0x0200000B RID: 11
    public class ItemAuto
    {
        // Token: 0x0600003E RID: 62 RVA: 0x00007FC8 File Offset: 0x000061C8
        public ItemAuto()
        {
        }

        // Token: 0x0600003F RID: 63 RVA: 0x00008069 File Offset: 0x00006269
        public ItemAuto(int id, short index, string name)
        {
            this.id = id;
            this.index = index;
            this.name = name;
        }

        // Token: 0x06000040 RID: 64 RVA: 0x0000807F File Offset: 0x0000627F
        public ItemAuto(int id, short index, bool isGold, bool isSell)
        {
            this.id = id;
            this.isGold = isGold;
            this.index = index;
            this.isSell = isSell;
        }

        // Token: 0x04000046 RID: 70
        public int id;

        // Token: 0x04000047 RID: 71
        public string name;

        // Token: 0x04000048 RID: 72
        public int quantity;

        // Token: 0x04000049 RID: 73
        public short index;

        // Token: 0x0400004A RID: 74
        public bool isGold;

        // Token: 0x0400004B RID: 75
        public bool isSell;

        // Token: 0x0400004C RID: 76
        public int delay;

        // Token: 0x0400004D RID: 77
        public long lastTimeUse;
    }

}
