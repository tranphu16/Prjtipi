using System;

// Token: 0x0200009A RID: 154
public class ServerScr : mScreen, IActionListener
{
    // Token: 0x060007FF RID: 2047 RVA: 0x0008AB04 File Offset: 0x00088D04
    public ServerScr()
    {
        TileMap.bgID = (int)((byte)(mSystem.currentTimeMillis() % 9L));
        bool flag = TileMap.bgID == 5 || TileMap.bgID == 6;
        if (flag)
        {
            TileMap.bgID = 4;
        }
        GameScr.loadCamera(true, -1, -1);
        GameScr.cmx = 100;
        GameScr.cmy = 200;
    }

    // Token: 0x06000800 RID: 2048 RVA: 0x0008AB64 File Offset: 0x00088D64
    public override void switchToMe()
    {
        SoundMn.gI().stopAll();
        base.switchToMe();
        this.vecServer = new Command[ServerListScreen.nameServer.Length];
        for (int i = 0; i < ServerListScreen.nameServer.Length; i++)
        {
            this.vecServer[i] = new Command(ServerListScreen.nameServer[i], this, 100 + i, null);
        }
        this.mainSelect = ServerListScreen.ipSelect;
        this.w2c = 5;
        this.wc = 76;
        this.hc = mScreen.cmdH;
        this.numw = 2;
        bool flag = GameCanvas.w > 3 * (this.wc + this.w2c);
        if (flag)
        {
            this.numw = 3;
        }
        this.numh = this.vecServer.Length / this.numw + ((this.vecServer.Length % this.numw != 0) ? 1 : 0);
        for (int j = 0; j < this.vecServer.Length; j++)
        {
            bool flag2 = this.vecServer[j] != null;
            if (flag2)
            {
                int num = GameCanvas.hw - this.numw * (this.wc + this.w2c) / 2 + j % this.numw * (this.wc + this.w2c);
                int num2 = GameCanvas.hh - this.numh * (this.hc + this.w2c) / 2 + j / this.numw * (this.hc + this.w2c);
                this.vecServer[j].x = num;
                this.vecServer[j].y = num2;
            }
        }
        bool flag3 = !GameCanvas.isTouch;
        if (flag3)
        {
            this.cmdCheck = new Command(mResources.SELECT, this, 99, null);
            this.center = this.cmdCheck;
        }
    }

    // Token: 0x06000801 RID: 2049 RVA: 0x0008AD34 File Offset: 0x00088F34
    public override void update()
    {
        GameScr.cmx++;
        bool flag = GameScr.cmx > GameCanvas.w * 3 + 100;
        if (flag)
        {
            GameScr.cmx = 100;
        }
        for (int i = 0; i < this.vecServer.Length; i++)
        {
            bool flag2 = !GameCanvas.isTouch;
            if (flag2)
            {
                bool flag3 = i == this.mainSelect;
                if (flag3)
                {
                    bool flag4 = GameCanvas.gameTick % 10 < 4;
                    if (flag4)
                    {
                        this.vecServer[i].isFocus = true;
                    }
                    else
                    {
                        this.vecServer[i].isFocus = false;
                    }
                }
                else
                {
                    this.vecServer[i].isFocus = false;
                }
            }
            else
            {
                bool flag5 = this.vecServer[i] != null && this.vecServer[i].isPointerPressInside();
                if (flag5)
                {
                    this.vecServer[i].performAction();
                }
            }
        }
    }

    // Token: 0x06000802 RID: 2050 RVA: 0x0008AE24 File Offset: 0x00089024
    public override void paint(mGraphics g)
    {
        GameCanvas.paintBGGameScr(g);
        for (int i = 0; i < this.vecServer.Length; i++)
        {
            bool flag = this.vecServer[i] != null;
            if (flag)
            {
                this.vecServer[i].paint(g);
            }
        }
        base.paint(g);
    }

    // Token: 0x06000803 RID: 2051 RVA: 0x0008AE7C File Offset: 0x0008907C
    public override void updateKey()
    {
        base.updateKey();
        int num = this.mainSelect % this.numw;
        int num2 = this.mainSelect / this.numw;
        bool flag = GameCanvas.keyPressed[4];
        if (flag)
        {
            bool flag2 = num > 0;
            if (flag2)
            {
                this.mainSelect--;
            }
            GameCanvas.keyPressed[4] = false;
        }
        else
        {
            bool flag3 = GameCanvas.keyPressed[6];
            if (flag3)
            {
                bool flag4 = num < this.numw - 1;
                if (flag4)
                {
                    this.mainSelect++;
                }
                GameCanvas.keyPressed[6] = false;
            }
            else
            {
                bool flag5 = GameCanvas.keyPressed[2];
                if (flag5)
                {
                    bool flag6 = num2 > 0;
                    if (flag6)
                    {
                        this.mainSelect -= this.numw;
                    }
                    GameCanvas.keyPressed[2] = false;
                }
                else
                {
                    bool flag7 = GameCanvas.keyPressed[8];
                    if (flag7)
                    {
                        bool flag8 = num2 < this.numh - 1;
                        if (flag8)
                        {
                            this.mainSelect += this.numw;
                        }
                        GameCanvas.keyPressed[8] = false;
                    }
                }
            }
        }
        bool flag9 = this.mainSelect < 0;
        if (flag9)
        {
            this.mainSelect = 0;
        }
        bool flag10 = this.mainSelect >= this.vecServer.Length;
        if (flag10)
        {
            this.mainSelect = this.vecServer.Length - 1;
        }
        bool flag11 = GameCanvas.keyPressed[5];
        if (flag11)
        {
            this.vecServer[num].performAction();
            GameCanvas.keyPressed[5] = false;
        }
        GameCanvas.clearKeyPressed();
    }

    // Token: 0x06000804 RID: 2052 RVA: 0x0008AFFC File Offset: 0x000891FC
    public void perform(int idAction, object p)
    {
        bool flag = idAction == 99;
        if (flag)
        {
            ServerListScreen.ipSelect = this.mainSelect;
            GameCanvas.serverScreen.selectServer();
            GameCanvas.serverScreen.switchToMe();
            //GameCanvas.serverScreen.perform(4, null);
        }
        else
        {
            ServerListScreen.ipSelect = idAction - 100;
            GameCanvas.serverScreen.selectServer();
            GameCanvas.serverScreen.switchToMe();
            //GameCanvas.serverScreen.perform(4, null);
        }
    }

    // Token: 0x04000FBB RID: 4027
    private int mainSelect;

    // Token: 0x04000FBC RID: 4028
    private Command[] vecServer;

    // Token: 0x04000FBD RID: 4029
    private Command cmdCheck;

    // Token: 0x04000FBE RID: 4030
    public const int icmd = 100;

    // Token: 0x04000FBF RID: 4031
    private int wc;

    // Token: 0x04000FC0 RID: 4032
    private int hc;

    // Token: 0x04000FC1 RID: 4033
    private int w2c;

    // Token: 0x04000FC2 RID: 4034
    private int numw;

    // Token: 0x04000FC3 RID: 4035
    private int numh;
}
