using MODMANHHDC;
using System;

public class InventoryBag : IActionListener, IChatable
{

    private static InventoryBag instance;
    public static InventoryBag gI()
    {
        if (instance == null)
        {
            instance = new InventoryBag();
        }
        return instance;
    }
    public Image bag, bagF;
    public InventoryBag()
    {
        //HairMod.imgGIF = Image.createImage("res/x2/mainImage/logo1");
        imgBgr = Image.createImage("res/rto/img_bgr");
        imgItem = Image.createImage("res/rto/img_bgr_item");
        wallBag = Image.createImage("res/rto/img_wall_bag");
        btn_bag = Image.createImage("res/rto/btn_bag");
        btn_bag_click = Image.createImage("res/rto/btn_bag_click");
        img_border_coin = Image.createImage("res/rto/img_border_coin");
        img_title = Image.createImage("res/rto/img_title");
        img_title_select = Image.createImage("res/rto/img_title_select");
        img_bgr_skill = Image.createImage("res/rto/img_bgr_skill");
        img_tab_info_item = Image.createImage("res/rto/img_tab_info_item");
        btn_close = Image.createImage("res/rto/btn_close");
        btn_close_click = Image.createImage("res/rto/btn_close_click");
        btn_cmd_mini = Image.createImage("res/rto/btn_cmd_mini");
        btn_cmd_mini_click = Image.createImage("res/rto/btn_cmd_mini_click");
        img_bgr_info_player = Image.createImage("res/rto/img_bgr_info_player");
        nutX = Image.createImage("res/rto/btn_close");
        nutX_focus = Image.createImage("res/rto/btn_close_click");
        bag = Image.createImage("res/rto/bag");
        bagF = Image.createImage("res/rto/bagF");
        if (mGraphics.zoomLevel != 2)
        {
            resizeImageBgr(imgBgr);
            resizeImage(imgItem);
            resizeImage(wallBag);
            resizeImage(btn_bag);
            resizeImage(btn_bag_click);
            resizeImage(img_border_coin);
            resizeImage(img_title);
            resizeImage(img_title_select);
            resizeImage(img_bgr_skill);
            resizeImage(img_tab_info_item);
            resizeImage(btn_close);
            resizeImage(btn_close_click);
            resizeImage(btn_cmd_mini);
            resizeImage(btn_cmd_mini_click);
            resizeImage(img_bgr_info_player);
            resizeImage(nutX);
            resizeImage(nutX_focus);
            resizeImage(bag);
            resizeImage(bagF);
            //resizeImage(HairMod.imgGIF);
        }



        scrollPopup = new Scroll();
        scrollInven = new Scroll();
        x = GameCanvas.hw - mGraphics.getImageWidth(imgBgr) / 2;
        y = GameCanvas.h - mGraphics.getImageHeight(imgBgr);
    }
    public void setScroll()
    {
        Item[] items = isInven ? Char.myCharz().arrItemBag : Char.myCharz().arrItemBox;

        numCol = 6;
        numRow = UnityEngine.Mathf.Max(6, UnityEngine.Mathf.CeilToInt(items.Length / 6f));
        int itemSize = mGraphics.getImageWidth(imgItem) + 5;
        int height = mGraphics.getImageHeight(imgBgr) - 40 - mGraphics.getImageHeight(img_border_coin) - mGraphics.getImageHeight(btn_bag);
        scrollInven.setStyle(numRow * numCol, itemSize, x + 20, y + 20 + mGraphics.getImageHeight(btn_bag) + 10, 6 * itemSize, height, true, 6);

    }
    public void resizeImage(Image img)
    {
        int wx1 = img.w / 2;//vi la hinh anh x2 nen chuyen size ve x1 xong tinh len
        int hx1 = img.h / 2;
        int w = wx1 * mGraphics.zoomLevel;
        int h = hx1 * mGraphics.zoomLevel;
        img.texture = myCommand.Resize(img.texture, w, h);
        img.w = img.texture.width;
        img.h = img.texture.height;
        Image.setTextureQuality(img.texture);
    }

    public void resizeImageBgr(Image img)
    {
        int tabH = mGraphics.getImageHeight(img_title_select) + 20;
        int h = GameCanvas.h - tabH;

        int wx1 = img.w / 2;//vi la hinh anh x2 nen chuyen size ve x1 xong tinh len

        int w = wx1 * mGraphics.zoomLevel;

        img.texture = myCommand.Resize(img.texture, w, h * mGraphics.zoomLevel);
        img.w = img.texture.width;
        img.h = img.texture.height;
        Image.setTextureQuality(img.texture);
    }


    private int numCol = 6;

    private int numRow = 6;

    private int x;
    private int y = 50;

    private Cell[] cellsInven;

    private Cell[] cellsBody;

    private Image img_bgr_info_player;

    private Image imgBgr;

    private Image nutX;
    private Image nutX_focus;

    private Image imgItem;

    private Image wallBag;

    private Image btn_bag_click;

    private Image btn_bag;

    private Image img_border_coin;

    private Image img_title;

    private Image img_title_select;

    private Image img_bgr_skill;

    private Image img_tab_info_item;

    private Image btn_close;

    private Image btn_close_click;

    private Image btn_cmd_mini;

    private Image btn_cmd_mini_click;

    private Scroll scrollPopup;

    private Scroll scrollInven;

    private ChatPopup cp;

    private ChatTextField chatTField;

    private short idIcon;

    private Item currItem;

    private bool tabTrangBi = true, tabSkill;

    private int waitToPerform;

    private bool isInven = true;

    private bool isBox;

    private bool isBody = true;

    private bool isInfo;

    private bool m_isClick;

    private string namePop;

    private MyVector menu = new MyVector();

    private bool m_isShow;

    public bool isShow
    {
        get => m_isShow;
        set
        {
            m_isShow = value;

            setScroll();
        }
    }


    /// <summary>
    /// Gọi ở cuối paint GameScr
    /// </summary>
    /// <param name="g"></param>
    public void paint(mGraphics g)
    {
        if (!isShow)
            return;


        //paintTab(g, "Trang Bị", x + 25, y, tabTrangBi);

        //paintTab(g, "Kĩ năng", x + 25 + mGraphics.getImageWidth(img_title), y, tabSkill);

        //vẽ background
        g.drawImage(imgBgr, x, y);
        g.drawImage((mScreen.keyTouch != 103) ? nutX : nutX_focus, GameCanvas.w - x - nutX.getWidth(), y);
        //đặt translate
        g.translate(x + 20, y + 20);



        if (tabTrangBi)
        {
            paintTabInventory(g, "Túi đồ", 0, 0, mGraphics.LEFT, isInven);

            paintTabInventory(g, "Rương đồ", mGraphics.getImageWidth(btn_bag) + 10, 0, mGraphics.LEFT, isBox);

            paintTabInventory(g, "Trang bị", mGraphics.getImageWidth(imgBgr) - mGraphics.getImageWidth(btn_bag) * 2 - 10, 0, mGraphics.RIGHT, isBody);

            paintTabInventory(g, "Thông tin", mGraphics.getImageWidth(imgBgr) - mGraphics.getImageWidth(btn_bag), 0, mGraphics.RIGHT, isInfo);
            paintInventory(g);
            if (isInfo)
                paintInfo(g);

        }
      

        //đặt lại vị trí translate
        g.translate(-x - 20, -y - 20);
        g.reset();
        g.drawImage(img_border_coin, x + 20, y + mGraphics.getImageHeight(imgBgr) - 10 - mGraphics.getImageHeight(img_border_coin));
        g.drawImage(Panel.imgXu, x + 25, y + mGraphics.getImageHeight(imgBgr) - 10 - mGraphics.getImageHeight(img_border_coin) / 2, mGraphics.VCENTER);
        mFont.tahoma_7b_white.drawString(g, NinjaUtil.getMoneys(Char.myCharz().xu), x + 15 + mGraphics.getImageWidth(img_border_coin), y + mGraphics.getImageHeight(imgBgr) - 10 - mGraphics.getImageHeight(img_border_coin) / 2 - mFont.tahoma_7b_white.getHeight() / 2, mFont.RIGHT);


        g.drawImage(img_border_coin, x + mGraphics.getImageWidth(imgBgr) - 20, y + mGraphics.getImageHeight(imgBgr) - 10 - mGraphics.getImageHeight(img_border_coin), mGraphics.RIGHT);
        g.drawImage(Panel.imgLuong, x + mGraphics.getImageWidth(imgBgr) - mGraphics.getImageWidth(img_border_coin) - 5, y + mGraphics.getImageHeight(imgBgr) - 10 - mGraphics.getImageHeight(img_border_coin) / 2, mGraphics.VCENTER | mGraphics.RIGHT);
        mFont.tahoma_7b_white.drawString(g, NinjaUtil.getMoneys(Char.myCharz().luong), x + mGraphics.getImageWidth(imgBgr) - 25, y + mGraphics.getImageHeight(imgBgr) - 10 - mGraphics.getImageHeight(img_border_coin) / 2 - mFont.tahoma_7b_white.getHeight() / 2, mFont.RIGHT);

      
        paintDetail(g);
        g.translate(-g.getTranslateX(), -g.getTranslateY());
        g.reset();
        if (chatTField != null)
            chatTField.paint(g);
    }

    private void paintInfo(mGraphics g)
    {
        int clipX = g.getClipX();
        int clipY = g.getClipY();
        int clipW = g.getClipWidth();
        int clipH = g.getClipHeight();

        int dx = numCol * (mGraphics.getImageWidth(imgItem) + 5) + 10;
        g.translate(dx, mGraphics.getImageHeight(btn_bag));
        g.setClip(0, 0, mGraphics.getImageWidth(img_bgr_info_player), mGraphics.getImageHeight(img_bgr_info_player));
        g.drawImage(img_bgr_info_player, 0, 0);
        SmallImage.drawSmallImage(g, Char.myCharz().avatarz(), 27, 57, 0, 33);

        mFont.tahoma_7b_white.drawString(g, ((GameScr.isNewMember == 1) ? "       " : string.Empty) + Char.myCharz().cName, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
        if (GameScr.isNewMember == 1)
        {
            SmallImage.drawSmallImage(g, 5427, 55, 4, 0, 0);
        }
        if (Char.myCharz().cMaxStamina > 0)
        {
            mFont.tahoma_7_yellow.drawString(g, mResources.vitality, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
            g.drawImage(GameScr.imgMPLost, 95, 19, 0);
            int num = Char.myCharz().cStamina * mGraphics.getImageWidth(GameScr.imgMP) / Char.myCharz().cMaxStamina;
            g.setClip(95, 19, num, 20);
            g.drawImage(GameScr.imgMP, 95, 19, 0);
        }
        g.setClip(0, 0, mGraphics.getImageWidth(img_bgr_info_player), mGraphics.getImageHeight(img_bgr_info_player));
        if (Char.myCharz().cPower > 0)
        {
            mFont.tahoma_7_yellow.drawString(g, Char.myCharz().getStrLevel(), 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
        }
        mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(Char.myCharz().cPower), 60, 38, mFont.LEFT, mFont.tahoma_7_grey);

        g.setColor(0);
        g.fillRect(5, 60, mGraphics.getImageWidth(img_bgr_info_player) - 10, 1);

        mFont.tahoma_7_white.drawString(g, mResources.HP + ": " + Char.myCharz().cHP + " / " + Char.myCharz().cHPFull, 10, 65, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_white.drawString(g, mResources.KI + ": " + Char.myCharz().cMP + " / " + Char.myCharz().cMPFull, 10, 77, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_white.drawString(g, mResources.hit_point + ": " + Char.myCharz().cDamFull, 10, 89, mFont.LEFT, mFont.tahoma_7_grey);
        mFont.tahoma_7_white.drawString(g, mResources.armor + ": " + Char.myCharz().cDefull + ", " + mResources.critical + ": " + Char.myCharz().cCriticalFull + "%", 10, 89 + 12, mFont.LEFT, mFont.tahoma_7_grey);

        g.translate(-dx, -mGraphics.getImageHeight(btn_bag));
        g.setClip(clipX, clipY, clipW, clipH);
    }


    private void paintTab(mGraphics g, string name, int x, int y, bool isSelect)
    {
        if (!isSelect)
        {
            g.drawImage(img_title, x, y - mGraphics.getImageHeight(img_title) + 2);
        }
        else
        {
            g.drawImage(img_title_select, x, y - mGraphics.getImageHeight(img_title_select) + 2);
        }
        var font = isSelect ? mFont.tahoma_7b_dark : mFont.tahoma_7b_blue;
        font.drawString(g, name, x + mGraphics.getImageWidth(img_title) / 2, y - mGraphics.getImageHeight(img_title) / 2 - font.getHeight() / 2, mFont.CENTER);
    }

    private void paintInventory(mGraphics g)
    {
        //vẽ các ô item
        g.translate(0, mGraphics.getImageHeight(btn_bag) + 10);

        int dx = numCol * (mGraphics.getImageWidth(imgItem) + 5) + 10;
        if (!isInfo)
        {
            g.translate(dx, 0);
            //vẽ nhân vật
            g.drawImage(wallBag, numCol * (mGraphics.getImageWidth(imgItem) + 5) / 2, 4 * (mGraphics.getImageHeight(imgItem) + 5) / 2, mGraphics.VCENTER | mGraphics.HCENTER);

            Char.myCharz().paintCharBody(g, numCol * (mGraphics.getImageWidth(imgItem) + 5) / 2, 4 * (mGraphics.getImageHeight(imgItem) + 5) - 20, 1, Char.myCharz().cf, true);

            g.translate(-dx, 0);
        }

        if (cellsInven == null || cellsInven.Length != numRow * numCol)
            cellsInven = new Cell[numCol * numRow];

        if (cellsBody == null || cellsBody.Length != Char.myCharz().arrItemBody.Length)
            cellsBody = new Cell[Char.myCharz().arrItemBody.Length];

        bool isClip = g.isClip;
        int clipX = g.getClipX();
        int clipY = g.getClipY();
        int clipW = g.getClipWidth();
        int clipH = g.getClipHeight();

        int maxRowBody = UnityEngine.Mathf.Max(6, UnityEngine.Mathf.CeilToInt(Char.myCharz().arrItemBody.Length / 6f));

        g.setClip(0, 0, mGraphics.getImageWidth(imgBgr) - 20, scrollInven.height);

        for (int row = 0; row < numRow; row++)
        {
            for (int col = 0; col < numCol; col++)
            {
                g.translate(0, -scrollInven.cmy);
                //vẽ phần hành trang
                int x = col * (mGraphics.getImageWidth(imgItem) + 5);
                int y = row * (mGraphics.getImageHeight(imgItem) + 5);

                //g.drawImage(imgItem, x, y);

                int index = row * numCol + col;

                if (cellsInven[index] == null)
                    cellsInven[index] = new Cell(x, y);

                cellsInven[index].paint(g);

                var arrItem = isInven ? Char.myCharz().arrItemBag : Char.myCharz().arrItemBox;

                if (arrItem != null && index < arrItem.Length && arrItem[index] != null)
                {
                    SmallImage.drawSmallImage(g, arrItem[index].template.iconID, x + mGraphics.getImageWidth(imgItem) / 2, y + mGraphics.getImageHeight(imgItem) / 2, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                    if (arrItem[index].quantity > 1)
                    {
                        mFont.tahoma_7.drawString(g, "x" + NinjaUtil.getMoneys(arrItem[index].quantity).ToString(), x + mGraphics.getImageWidth(imgItem) / 2, y + mGraphics.getImageHeight(imgItem) - mFont.tahoma_7_white.getHeight() + 1, mGraphics.VCENTER | mGraphics.HCENTER);
                    }
                }
                g.translate(0, scrollInven.cmy);

                if (isInfo/* || row > maxRowBody*/)
                    continue;

                //vẽ phần nhân vật
                g.translate(dx, 0);

                if (row < 4 && (col == 0 || col == numCol - 1))
                {
                    g.drawImage(imgItem, x, y);

                    Item item = null;
                    int idex = -1;

                    if (col == 0 && row <= 4)
                    {
                        if (Char.myCharz().arrItemBody != null && row < Char.myCharz().arrItemBody.Length && Char.myCharz().arrItemBody[row] != null)
                        {
                            item = Char.myCharz().arrItemBody[row];
                        }
                        idex = row;
                    }
                    else if (col == numCol - 1 && row <= 4)
                    {
                        if (Char.myCharz().arrItemBody != null && row + 4 < Char.myCharz().arrItemBody.Length && Char.myCharz().arrItemBody[row + 4] != null)
                        {
                            item = Char.myCharz().arrItemBody[row + 4];
                        }
                        idex = row + 4;
                    }

                    if (item != null)
                        SmallImage.drawSmallImage(g, item.template.iconID, x + mGraphics.getImageWidth(imgItem) / 2, y + mGraphics.getImageHeight(imgItem) / 2, 0, mGraphics.VCENTER | mGraphics.HCENTER);

                    if (idex != -1)
                    {
                        if (cellsBody[idex] == null)
                            cellsBody[idex] = new Cell(x, y);
                    }
                }
                else if (row >= 4)
                {
                    g.drawImage(imgItem, x, y);

                    int idex = (row - 4) * numCol + (col + 8);

                    if (Char.myCharz().arrItemBody != null && idex < Char.myCharz().arrItemBody.Length && Char.myCharz().arrItemBody[idex] != null)
                    {
                        SmallImage.drawSmallImage(g, Char.myCharz().arrItemBody[idex].template.iconID, x + mGraphics.getImageWidth(imgItem) / 2, y + mGraphics.getImageHeight(imgItem) / 2, 0, mGraphics.VCENTER | mGraphics.HCENTER);
                    }
                    if (idex < cellsBody.Length && cellsBody[idex] == null)
                        cellsBody[idex] = new Cell(x, y);
                }

                g.translate(-dx, 0);

            }
        }
        if (isClip)
            g.setClip(clipX, clipY, clipW, clipH);
        g.translate(0, -mGraphics.getImageHeight(btn_bag) - 10);
    }

    private void paintSkill(mGraphics g)
    {
        int transX = g.getTranslateX();
        int transY = g.getTranslateY();

        g.drawImage(img_bgr_skill, (mGraphics.getImageWidth(imgBgr) - 40) / 2, 20, mGraphics.HCENTER);
        g.translate((mGraphics.getImageWidth(imgBgr) - 40 - mGraphics.getImageWidth(img_bgr_skill)) / 2, 20);
        g.setClip(2, 2, mGraphics.getImageWidth(img_bgr_skill) - 2, mGraphics.getImageHeight(img_bgr_skill) - 2);

        int padding = (mGraphics.getImageWidth(img_bgr_skill) - 40) - mGraphics.getImageWidth(GameScr.imgSkill) * Char.myCharz().nClass.skillTemplates.Length;

        padding /= (Char.myCharz().nClass.skillTemplates.Length - 1);

        for (int i = 0; i < Char.myCharz().nClass.skillTemplates.Length; i++)
        {
            g.drawImage(GameScr.imgSkill, 20 + i * (mGraphics.getImageWidth(GameScr.imgSkill) + padding), mGraphics.getImageHeight(img_bgr_skill) / 2, mGraphics.VCENTER);
            SmallImage.drawSmallImage(g, Char.myCharz().nClass.skillTemplates[i].iconId, 24 + i * (mGraphics.getImageWidth(GameScr.imgSkill) + padding), mGraphics.getImageHeight(img_bgr_skill) / 2, 0, mGraphics.VCENTER);
        }

        g.setClip(-g.getTranslateX(), -g.getTranslateY(), GameCanvas.w, GameCanvas.h);

        mFont.tahoma_7b_blue.drawString(g, "Điểm tiềm năng: " + NinjaUtil.getMoneys(Char.myCharz().cTiemNang), 0, 10 + mGraphics.getImageHeight(img_bgr_skill), 0);



        g.translate(0, 30 + mGraphics.getImageHeight(img_bgr_skill));

        #region VẼ HP
        g.setColor(15196114);
        g.fillRect(0, 0, Panel.WIDTH_PANEL - 34, 34);

        g.drawImage(GameScr.imgSkill, 4, 4, 0);
        SmallImage.drawSmallImage(g, 567, 8, 8, 0, 0);
        string st = mResources.HP + " " + mResources.root + ": " + NinjaUtil.getMoneys(Char.myCharz().cHPGoc);
        mFont.tahoma_7b_blue.drawString(g, st, mGraphics.getImageWidth(GameScr.imgSkill) + 9, 7, 0);
        mFont.tahoma_7_green.drawString(g, NinjaUtil.getMoneys(Char.myCharz().cHPGoc + 1000) + " " + mResources.potential + ": " + mResources.increase + " " + Char.myCharz().hpFrom1000TiemNang, mGraphics.getImageWidth(GameScr.imgSkill) + 9, 19, 0);
        #endregion

        #region Vẽ MP
        g.translate((mGraphics.getImageWidth(imgBgr) - 40) / 2, 0);
        g.fillRect(0, 0, Panel.WIDTH_PANEL - 34, 34);
        g.drawImage(GameScr.imgSkill, 4, 4, 0);
        SmallImage.drawSmallImage(g, 569, 8, 8, 0, 0);
        st = mResources.KI + " " + mResources.root + ": " + NinjaUtil.getMoneys(Char.myCharz().cMPGoc);
        mFont.tahoma_7b_blue.drawString(g, st, mGraphics.getImageWidth(GameScr.imgSkill) + 9, 7, 0);
        mFont.tahoma_7_green.drawString(g, NinjaUtil.getMoneys(Char.myCharz().cMPGoc + 1000) + " " + mResources.potential + ": " + mResources.increase + " " + Char.myCharz().mpFrom1000TiemNang, mGraphics.getImageWidth(GameScr.imgSkill) + 9, 19, 0);
        #endregion

        #region VẼ DAME
        g.translate(-(mGraphics.getImageWidth(imgBgr) - 40) / 2, 36);
        g.fillRect(0, 0, Panel.WIDTH_PANEL - 34, 34);
        g.drawImage(GameScr.imgSkill, 4, 4, 0);
        SmallImage.drawSmallImage(g, 568, 8, 8, 0, 0);
        st = mResources.hit_point + " " + mResources.root + ": " + NinjaUtil.getMoneys(Char.myCharz().cDamGoc);
        mFont.tahoma_7b_blue.drawString(g, st, mGraphics.getImageWidth(GameScr.imgSkill) + 9, 7, 0);
        mFont.tahoma_7_green.drawString(g, NinjaUtil.getMoneys(Char.myCharz().cDamGoc * 100) + " " + mResources.potential + ": " + mResources.increase + " " + Char.myCharz().damFrom1000TiemNang, mGraphics.getImageWidth(GameScr.imgSkill) + 9, 19, 0);
        #endregion

        #region VẼ GIÁP
        g.translate((mGraphics.getImageWidth(imgBgr) - 40) / 2, 0);

        g.fillRect(0, 0, Panel.WIDTH_PANEL - 34, 34);
        g.drawImage(GameScr.imgSkill, 4, 4, 0);
        SmallImage.drawSmallImage(g, 721, 8, 8, 0, 0);
        st = mResources.armor + " " + mResources.root + ": " + NinjaUtil.getMoneys(Char.myCharz().cDefGoc);
        mFont.tahoma_7b_blue.drawString(g, st, mGraphics.getImageWidth(GameScr.imgSkill) + 9, 7, 0);
        mFont.tahoma_7_green.drawString(g, NinjaUtil.getMoneys(500000 + Char.myCharz().cDefGoc * 100000) + " " + mResources.potential + ": " + mResources.increase + " " + Char.myCharz().defFrom1000TiemNang, mGraphics.getImageWidth(GameScr.imgSkill) + 9, 19, 0);
        #endregion

        #region VẼ CHÍ MẠNG
        g.translate(-(mGraphics.getImageWidth(imgBgr) - 40) / 2, 36);
        g.drawImage(GameScr.imgSkill, 4, 4, 0);
        g.fillRect(0, 0, Panel.WIDTH_PANEL - 34, 34);
        SmallImage.drawSmallImage(g, 719, 8, 8, 0, 0);
        string st5 = mResources.critical + " " + mResources.root + ": " + Char.myCharz().cCriticalGoc + "%";
        long num9 = 50000000L;
        int num10 = Char.myCharz().cCriticalGoc;
        if (num10 > Panel.t_tiemnang.Length - 1)
        {
            num10 = Panel.t_tiemnang.Length - 1;
        }
        num9 = Panel.t_tiemnang[num10];
        mFont.tahoma_7b_blue.drawString(g, st5, mGraphics.getImageWidth(GameScr.imgSkill) + 9, 7, 0);
        mFont.tahoma_7_green.drawString(g, Res.formatNumber2(num9) + " " + mResources.potential + ": " + mResources.increase + " " + Char.myCharz().criticalFrom1000Tiemnang, mGraphics.getImageWidth(GameScr.imgSkill) + 9, 19, 0);
        #endregion

        #region VẼ NỘI TẠI
        g.translate((mGraphics.getImageWidth(imgBgr) - 40) / 2, 0);
        g.fillRect(0, 0, Panel.WIDTH_PANEL - 34, 34);
        g.drawImage(GameScr.imgSkill, 4, 4, 0);
        if (Panel.specialInfo != null)
        {
            SmallImage.drawSmallImage(g, Panel.spearcialImage, 8, 8, 0, 0);
            string[] array = mFont.tahoma_7.splitFontArray(Panel.specialInfo, 120);
            for (int j = 0; j < array.Length; j++)
            {
                mFont.tahoma_7_green2.drawString(g, array[j], mGraphics.getImageWidth(GameScr.imgSkill) + 9, 8 + j * 12, 0);
            }
        }
        else
        {
            mFont.tahoma_7_green2.drawString(g, string.Empty, mGraphics.getImageWidth(GameScr.imgSkill) + 9, 13, 0);
        }
        #endregion

        g.translate(-g.getTranslateX(), -g.getTranslateY());
        g.translate(transX, transY);

    }

    private void doFireSkill()
    {
        if (!tabSkill)
            return;
        int transX = x + 20 + (mGraphics.getImageWidth(imgBgr) - 40 - mGraphics.getImageWidth(img_bgr_skill)) / 2;
        int transY = y + 70 + mGraphics.getImageHeight(img_bgr_skill);
        long cTiemNang = Char.myCharz().cTiemNang;
        long cHPGoc = Char.myCharz().cHPGoc;
        long cMPGoc = Char.myCharz().cMPGoc;
        long cDamGoc = Char.myCharz().cDamGoc;
        int cDefGoc = Char.myCharz().cDefGoc;
        int cCriticalGoc = Char.myCharz().cCriticalGoc;
        int num = 0;
        int num2 = 1000;

        int transX2 = x + 20 + (mGraphics.getImageWidth(imgBgr) - 40 - mGraphics.getImageWidth(img_bgr_skill)) / 2;
        int transY2 = y + 40;
        if (GameCanvas.isPointerHoldIn(transX2, transY2, mGraphics.getImageWidth(img_bgr_skill) - 2, mGraphics.getImageHeight(img_bgr_skill) - 2))
        {
            if (!GameCanvas.isPointerClick || !GameCanvas.isPointerJustRelease)
                return;

            int padding = (mGraphics.getImageWidth(img_bgr_skill) - 40) - mGraphics.getImageWidth(GameScr.imgSkill) * Char.myCharz().nClass.skillTemplates.Length;

            padding /= (Char.myCharz().nClass.skillTemplates.Length - 1);
            for (int i = 0; i < Char.myCharz().nClass.skillTemplates.Length; i++)
            {
                if (GameCanvas.isPointerHoldIn(transX2 + 20 + i * (mGraphics.getImageWidth(GameScr.imgSkill) + padding),
                    transY2 + (mGraphics.getImageHeight(img_bgr_skill) - mGraphics.getImageHeight(GameScr.imgSkill)) / 2,
                    mGraphics.getImageWidth(GameScr.imgSkill),
                    mGraphics.getImageHeight(GameScr.imgSkill)))
                {
                    SkillTemplate skillTemplate = Char.myCharz().nClass.skillTemplates[i];
                    Skill skill = Char.myCharz().getSkill(skillTemplate);
                    Skill skill2 = null;
                    menu.removeAllElements();

                    if (skill != null)
                    {
                        if (skill.point != skillTemplate.maxPoint)
                        {
                            skill2 = skillTemplate.skills[skill.point];
                            menu.addElement(new Command("Cộng", GameCanvas.panel, 9002, skill2));
                        }
                        menu.addElement(new Command("Gán", GameCanvas.panel, 9003, skill.template));
                    }
                    else
                    {
                        skill2 = skillTemplate.skills[0];
                        menu.addElement(new Command(mResources.learn, GameCanvas.panel, 9004, skill2));
                    }
                    UnityEngine.Debug.Log("Skill " + skillTemplate.name);
                    addSkillDetail(Char.myCharz().nClass.skillTemplates[i], skill, skill2);
                    break;
                }
            }
            GameCanvas.clearAllPointerEvent();
            return;
        }

        menu.removeAllElements();
        if (GameCanvas.isPointerHoldIn(transX, transY, Panel.WIDTH_PANEL - 34, 34))
        {
            GameCanvas.clearAllPointerEvent();
            addSkillDetail2(0);
            GameCanvas.panel.selected = 0;
            waitToPerform = 2;
        }
        else if (GameCanvas.isPointerHoldIn(transX + (mGraphics.getImageWidth(imgBgr) - 40) / 2, transY, Panel.WIDTH_PANEL - 34, 34))
        {
            GameCanvas.clearAllPointerEvent();
            addSkillDetail2(1);
            GameCanvas.panel.selected = 1;
            waitToPerform = 2;
        }
        else if (GameCanvas.isPointerHoldIn(transX, transY + 36, Panel.WIDTH_PANEL - 34, 34))
        {
            GameCanvas.clearAllPointerEvent();
            addSkillDetail2(2);
            GameCanvas.panel.selected = 2;
            waitToPerform = 2;
        }
        else if (GameCanvas.isPointerHoldIn(transX + (mGraphics.getImageWidth(imgBgr) - 40) / 2, transY + 36, Panel.WIDTH_PANEL - 34, 34))
        {
            GameCanvas.clearAllPointerEvent();
            addSkillDetail2(3);
            GameCanvas.panel.selected = 3;
            waitToPerform = 2;
        }
        else if (GameCanvas.isPointerHoldIn(transX, transY + 36 * 2, Panel.WIDTH_PANEL - 34, 34))
        {
            GameCanvas.clearAllPointerEvent();
            addSkillDetail2(4);
            GameCanvas.panel.selected = 4;
            waitToPerform = 2;
        }
        else if (GameCanvas.isPointerHoldIn(transX + (mGraphics.getImageWidth(imgBgr) - 40) / 2, transY + 36 * 2, Panel.WIDTH_PANEL - 34, 34))//nội tại
        {
            GameCanvas.clearAllPointerEvent();
            Service.gI().speacialSkill(0);
            waitToPerform = 2;
        }
        menu.addElement(new Command("Cộng", GameCanvas.panel, 9000, 0));
        menu.addElement(new Command("Cộng 200", GameCanvas.panel, 9006, 0));
        menu.addElement(new Command("Cộng 2000", GameCanvas.panel, 9007, 0));
    }

    public void chatTFUpdateKey()
    {
        if (chatTField != null && chatTField.isShow)
        {
            if (chatTField.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(chatTField.left)) && chatTField.left != null)
            {
                chatTField.left.performAction();
            }
            if (chatTField.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(chatTField.right)) && chatTField.right != null)
            {
                chatTField.right.performAction();
            }
            if (chatTField.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(chatTField.center)) && chatTField.center != null)
            {
                chatTField.center.performAction();
            }
            if (chatTField.isShow && GameCanvas.keyAsciiPress != 0)
            {
                chatTField.keyPressed(GameCanvas.keyAsciiPress);
                GameCanvas.keyAsciiPress = 0;
            }
            GameCanvas.clearKeyHold();
            GameCanvas.clearKeyPressed();
        }
    }

    /// <summary>
    /// Bỏ vào UpdateKey của GameScr
    /// đoạn
    /// if (GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu)
    ///    {
    ///        updateKeyTouchControl();
    ///        MainMod.gI().updateKey();
    ///        NewInventory.gI().updateKey();
    ///    }
    /// </summary>
    public void updateKey()
    {
        if (!isShow || GameCanvas.currentScreen != GameScr.gI() || GameCanvas.menu.showMenu
            || (GameCanvas.currentDialog != null) || ChatPopup.currChatPopup != null
            || (chatTField != null && chatTField.isShow))
            return;
        if (waitToPerform > 0)
        {
            waitToPerform--;
            return;
        }
        if (chatTField != null)
        {
            chatTFUpdateKey();
            chatTField.update();
        }
        if (GameCanvas.isPointerHoldIn(GameCanvas.w - x - nutX.getWidth(), y, nutX.getWidth(), nutX.getHeight()))
        {
            mScreen.keyTouch = 103;
            if (GameCanvas.isPointerJustRelease)
            {
                isShow = false;
            }
            GameCanvas.clearAllPointerEvent();
        }
        if (cp != null)
        {
            scrollPopup.updatecm();

            if (scrollPopup.cmyLim > 0)
            {
                ScrollResult result = scrollPopup.updateKey();
                if (result.isDowning)
                    GameCanvas.isPointerMove = false;
                if (scrollPopup.cmy < 0 && result.isDowning == false)
                {

                    scrollPopup.cmy -= scrollPopup.cmy / 2;
                }
                else if (scrollPopup.cmy > scrollPopup.cmyLim && result.isDowning == false)
                {
                    scrollPopup.cmy -= (scrollPopup.cmy - scrollPopup.cmyLim + 6) / 2;
                }
            }


            if (GameCanvas.isPointerHoldIn(GameCanvas.hw + (mGraphics.getImageWidth(img_tab_info_item) / 2 - mGraphics.getImageWidth(btn_close) / 2),
                GameCanvas.hh - (mGraphics.getImageHeight(img_tab_info_item) + mGraphics.getImageHeight(btn_close)) / 2,
                mGraphics.getImageWidth(btn_close),
                mGraphics.getImageHeight(btn_close)))
            {
                GameCanvas.panel.cp = cp = null;
                GameCanvas.clearAllPointerEvent();
            }
            else
            {
                if (!GameCanvas.isPointerClick || !GameCanvas.isPointerJustRelease)
                    return;

                for (int i = 0; i < menu.size(); i++)
                {
                    Command cmd = (Command)menu.elementAt(i);
                    int x = mGraphics.getImageWidth(img_tab_info_item) - (mGraphics.getImageWidth(btn_cmd_mini) + 5) * i - 5 - mGraphics.getImageWidth(btn_cmd_mini);
                    x += GameCanvas.hw - mGraphics.getImageWidth(img_tab_info_item) / 2;
                    int y = mGraphics.getImageHeight(img_tab_info_item) - mGraphics.getImageHeight(btn_cmd_mini) - 5;
                    y += GameCanvas.hh - mGraphics.getImageHeight(img_tab_info_item) / 2;
                    if (GameCanvas.isPointerHoldIn(x, y, mGraphics.getImageWidth(btn_cmd_mini), mGraphics.getImageHeight(btn_cmd_mini)))
                    {
                        cmd.performAction();
                        GameCanvas.panel.cp = cp = null;
                        GameCanvas.clearAllPointerEvent();
                        break;
                    }

                }
            }
            return;
        }


        //doFireSkill();


        if (tabTrangBi && !GameCanvas.isPointerClick)
        {
            if (scrollInven.cmyLim > 0)
            {
                ScrollResult result = scrollInven.updateKey();

                if (scrollInven.cmy < 0 && result.isDowning == false)
                {

                    scrollInven.cmy -= scrollInven.cmy / 2;
                }
                else if (scrollInven.cmy > scrollInven.cmyLim && result.isDowning == false)
                {
                    scrollInven.cmy -= (scrollInven.cmy - scrollInven.cmyLim + 6) / 2;
                }
                if (result.isDowning)
                    GameCanvas.isPointerMove = false;
            }

        }


        int transX = x;
        int transY = y;

        if (GameCanvas.isPointerHoldIn(transX, transY, mGraphics.getImageWidth(imgBgr), mGraphics.getImageWidth(imgBgr)))
        {

            updateClickInside();
        }
        else if (GameCanvas.isPointerHoldIn(x + 25, y - mGraphics.getImageHeight(img_title) + 2, mGraphics.getImageWidth(img_title), mGraphics.getImageHeight(img_title))
            && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
        {
            tabTrangBi = true;
            //tabSkill = false;
            GameCanvas.clearAllPointerEvent();
        }
        //else if (GameCanvas.isPointerHoldIn(x + 25 + mGraphics.getImageWidth(img_title), y - mGraphics.getImageHeight(img_title) + 2, mGraphics.getImageWidth(img_title), mGraphics.getImageHeight(img_title))
        //     && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
        //{
        //    tabTrangBi = false;
        //    //tabSkill = true;
        //    scrollPopup.setStyle(Char.myCharz().nClass.skillTemplates.Length, mGraphics.getImageWidth(GameScr.imgSkill), x + 20 + (mGraphics.getImageWidth(imgBgr) - 40 - mGraphics.getImageWidth(img_bgr_skill)) / 2, y + 40,
        //    mGraphics.getImageWidth(img_bgr_skill) - 2, mGraphics.getImageHeight(img_bgr_skill) - 2, false, 1);
        //    GameCanvas.clearAllPointerEvent();
        //}


        GameCanvas.clearKeyHold();
        for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
        {
            GameCanvas.keyPressed[i] = false;
        }
        GameCanvas.isPointerJustRelease = false;
    }

    public void addItemDetail(Item item)
    {
        try
        {
            GameCanvas.panel.cp = cp = new ChatPopup();
            string empty = string.Empty;
            string text = string.Empty;
            if (item.template.gender != Char.myCharz().cgender)
            {
                if (item.template.gender == 0)
                {
                    text = text + "\n|7|1|" + mResources.from_earth;
                }
                else if (item.template.gender == 1)
                {
                    text = text + "\n|7|1|" + mResources.from_namec;
                }
                else if (item.template.gender == 2)
                {
                    text = text + "\n|7|1|" + mResources.from_sayda;
                }
            }
            string text2 = string.Empty;
            if (item.itemOption != null)
            {
                for (int i = 0; i < item.itemOption.Length; i++)
                {
                    if (item.itemOption[i].optionTemplate.id == 72)
                    {
                        text2 = " [+" + item.itemOption[i].param + "]";
                    }
                }
            }
            bool flag = false;
            if (item.itemOption != null)
            {
                for (int j = 0; j < item.itemOption.Length; j++)
                {
                    if (item.itemOption[j].optionTemplate.id == 41)
                    {
                        flag = true;
                        if (item.itemOption[j].param == 1)
                        {
                            text = text + "|1|1|" + item.template.name + text2;
                        }
                        if (item.itemOption[j].param == 2)
                        {
                            text = text + "|2|1|" + item.template.name + text2;
                        }
                        if (item.itemOption[j].param == 3)
                        {
                            text = text + "|8|1|" + item.template.name + text2;
                        }
                        if (item.itemOption[j].param == 4)
                        {
                            text = text + "|7|1|" + item.template.name + text2;
                        }
                    }
                }
            }
            if (!flag)
            {
                text = text + "|1|1|" + item.template.name + text2;
            }
            if (item.itemOption != null)
            {
                for (int k = 0; k < item.itemOption.Length; k++)
                {
                    if (item.itemOption[k].optionTemplate.name.StartsWith("$") ? true : false)
                    {
                        empty = item.itemOption[k].getOptiongColor();
                        if (item.itemOption[k].param == 1)
                        {
                            text = text + "\n|1|1|" + empty;
                        }
                        if (item.itemOption[k].param == 0)
                        {
                            text = text + "\n|0|1|" + empty;
                        }
                        continue;
                    }
                    empty = item.itemOption[k].getOptionString();
                    if (!empty.Equals(string.Empty) && item.itemOption[k].optionTemplate.id != 72)
                    {
                        if (item.itemOption[k].optionTemplate.id == 102)
                        {
                            cp.starSlot = (sbyte)item.itemOption[k].param;
                            Res.outz("STAR SLOT= " + cp.starSlot);
                        }
                        else if (item.itemOption[k].optionTemplate.id == 107)
                        {
                            cp.maxStarSlot = (sbyte)item.itemOption[k].param;
                            Res.outz("STAR SLOT= " + cp.maxStarSlot);
                        }
                        else
                        {
                            text = text + "\n|1|1|" + empty;
                        }
                    }
                }
            }
            if (currItem.template.strRequire > 1)
            {
                string text3 = mResources.pow_request + ": " + currItem.template.strRequire;
                if (currItem.template.strRequire > Char.myCharz().cPower)
                {
                    text = text + "\n|3|1|" + text3;
                    string text4 = text;
                    text = text4 + "\n|3|1|" + mResources.your_pow + ": " + Char.myCharz().cPower;
                }
                else
                {
                    text = text + "\n|6|1|" + text3;
                }
            }
            else
            {
                text += "\n|6|1|";
            }
            currItem.compare = GameCanvas.panel.getCompare(currItem);
            text += "\n--";
            text = text + "\n|2|" + item.template.description;
            if (!item.reason.Equals(string.Empty))
            {
                if (!item.template.description.Equals(string.Empty))
                {
                    text += "\n--";
                }
                text = text + "\n|2|" + item.reason;
            }
            if (cp.maxStarSlot > 0)
            {
                text += "\n\n";
            }
            popUpDetailInit(cp, text);
            idIcon = item.template.iconID;
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogException(ex);
            Res.outz("ex " + ex.StackTrace);
        }
    }

    private void addSkillDetail2(int type)
    {
        string empty = string.Empty;
        long num = 0;
        if (type == 0)
        {
            num = Char.myCharz().cHPGoc + 1000;
            empty = "|1|HP\n";
        }
        if (type == 1)
        {
            num = Char.myCharz().cMPGoc + 1000;
            empty = "|1|MP\n";
        }
        if (type == 2)
        {
            num = Char.myCharz().cDamGoc * Char.myCharz().expForOneAdd;
            empty = "|1|Sức Đánh\n";
        }
        if (type == 3)
        {
            num = 500000 + Char.myCharz().cDefGoc * 100000;
            empty = "|1|Giáp\n";
        }
        if (type == 4)
        {
            empty = "|1|Chí mạng\n";
        }
        string text = empty;
        empty = text + "|5|" + mResources.USE + " " + num + " " + mResources.potential;
        if (type == 0)
        {
            empty = empty + "\n|5|" + mResources.to_gain_20hp;
        }
        if (type == 1)
        {
            empty = empty + "\n|5|" + mResources.to_gain_20mp;
        }
        if (type == 2)
        {
            empty = empty + "\n|5|" + mResources.to_gain_1pow;
        }
        if (type == 3)
        {
            empty = empty + "\n|5|" + mResources.to_gain_1pow;
        }
        currItem = null;
        idIcon = -1;
        GameCanvas.panel.cp = (cp = new ChatPopup());
        popUpDetailInit(cp, empty);
    }

    public void addSkillDetail(SkillTemplate tp, Skill skill, Skill nextSkill)
    {
        string text = "|0|" + tp.name;
        for (int i = 0; i < tp.description.Length; i++)
        {
            text = text + "\n|4|" + tp.description[i];
        }
        text += "\n--";
        if (skill != null)
        {
            string text2 = text;
            text = text2 + "\n|2|" + mResources.cap_do + ": " + skill.point;
            text = text + "\n|5|" + NinjaUtil.replace(tp.damInfo, "#", skill.damage + string.Empty);
            text2 = text;
            text = text2 + "\n|5|" + mResources.KI_consume + skill.manaUse + ((tp.manaUseType != 1) ? string.Empty : "%");
            text2 = text;
            text = text2 + "\n|5|" + mResources.cooldown + ": " + skill.strTimeReplay() + "s";
            text += "\n--";
            if (skill.point == tp.maxPoint)
            {
                text = text + "\n|0|" + mResources.max_level_reach;
            }
            else
            {
                if (!skill.template.isSkillSpec())
                {
                    text2 = text;
                    text = text2 + "\n|1|" + mResources.next_level_require + Res.formatNumber(nextSkill.powRequire) + " " + mResources.potential;
                }
                text = text + "\n|4|" + NinjaUtil.replace(tp.damInfo, "#", nextSkill.damage + string.Empty);
            }
        }
        else
        {
            text = text + "\n|2|" + mResources.not_learn;
            string text2 = text;
            text = text2 + "\n|1|" + mResources.learn_require + Res.formatNumber(nextSkill.powRequire) + " " + mResources.potential;
            text = text + "\n|4|" + NinjaUtil.replace(tp.damInfo, "#", nextSkill.damage + string.Empty);
            text2 = text;
            text = text2 + "\n|4|" + mResources.KI_consume + nextSkill.manaUse + ((tp.manaUseType != 1) ? string.Empty : "%");
            text2 = text;
            text = text2 + "\n|4|" + mResources.cooldown + ": " + nextSkill.strTimeReplay() + "s";
        }
        currItem = null;
        GameCanvas.panel.cp = (cp = new ChatPopup());
        popUpDetailInit(cp, text);
        idIcon = 0;
    }

    public void paintDetail(mGraphics g)
    {
        try
        {
            if (cp == null || cp.says == null)
            {
                return;
            }
            g.drawImage(img_tab_info_item, GameCanvas.hw, GameCanvas.hh, mGraphics.VCENTER | mGraphics.HCENTER);
            g.drawImage(btn_close, GameCanvas.hw + (mGraphics.getImageWidth(img_tab_info_item) - mGraphics.getImageWidth(btn_close)) / 2, GameCanvas.hh - (mGraphics.getImageHeight(img_tab_info_item) + mGraphics.getImageHeight(btn_close)) / 2);
            g.translate(GameCanvas.hw - mGraphics.getImageWidth(img_tab_info_item) / 2, GameCanvas.hh - mGraphics.getImageHeight(img_tab_info_item) / 2);

            if (idIcon > 0)
            {
                SmallImage.drawSmallImage(g, idIcon, 10, 12, 0, 0);
            }

            g.setClip(0, 0, mGraphics.getImageWidth(img_tab_info_item), mGraphics.getImageHeight(img_tab_info_item));
            g.translate(0, -scrollPopup.cmy);
            int num6 = -1;
            int dy = 0;
            for (int i = 0; i < cp.says.Length; i++)
            {
                if (cp.says[i].StartsWith("--"))
                {
                    g.setColor(0);
                    g.fillRect(10, dy + cp.sayRun + i * 12 + 8, mGraphics.getImageWidth(img_tab_info_item) - 20, 1);
                    continue;
                }

                mFont mFont2 = mFont.tahoma_7;
                int num7 = 0;
                string st = cp.says[i];
                int num8 = 0;
                if (cp.says[i].StartsWith("|"))
                {
                    string[] array = Res.split(cp.says[i], "|", 0);
                    if (array.Length == 3)
                    {
                        st = array[2];
                    }
                    if (array.Length == 4)
                    {
                        st = array[3];
                        num7 = int.Parse(array[2]);
                    }
                    num8 = int.Parse(array[1]);
                    num6 = num8;
                }
                else
                {
                    num8 = num6;
                }
                switch (num8)
                {
                    case -1:
                        mFont2 = mFont.tahoma_7;
                        break;
                    case 0:
                        mFont2 = mFont.tahoma_7b_dark;
                        break;
                    case 1:
                        mFont2 = mFont.tahoma_7b_green;
                        break;
                    case 2:
                        mFont2 = mFont.tahoma_7b_blue;
                        break;
                    case 3:
                        mFont2 = mFont.tahoma_7_red;
                        break;
                    case 4:
                        mFont2 = mFont.tahoma_7_green;
                        break;
                    case 5:
                        mFont2 = mFont.tahoma_7_blue;
                        break;
                    case 7:
                        mFont2 = mFont.tahoma_7b_red;
                        break;
                    case 8:
                        mFont2 = mFont.tahoma_7b_yellow;
                        break;
                }
                int x = 10;
                if (num7 == 1)
                    x = cp.sayWidth - 5;

                mFont2.drawString(g, st, x, dy + cp.sayRun + i * 12 + 12 - cp.strY, num7);
            }
            g.translate(0, scrollPopup.cmy);
            g.setClip(-g.getTranslateX(), -g.getTranslateY(), GameCanvas.w, GameCanvas.h);


            for (int num9 = 0; num9 < cp.maxStarSlot; num9++)
            {
                g.drawImage(Panel.imgMaxStar, 10 + num9 * 20 + mGraphics.getImageWidth(Panel.imgMaxStar), mGraphics.getImageHeight(img_tab_info_item) - mGraphics.getImageHeight(Panel.imgMaxStar) - 5, 0);
            }
            if (cp.starSlot > 0)
            {
                for (int num10 = 0; num10 < cp.starSlot; num10++)
                {
                    g.drawImage(Panel.imgStar, 10 + num10 * 20 + mGraphics.getImageWidth(Panel.imgStar), mGraphics.getImageHeight(img_tab_info_item) - mGraphics.getImageHeight(Panel.imgStar) - 5, 0);
                }
            }

            paintCmdPopup(g);

            g.translate(-(GameCanvas.hw - mGraphics.getImageWidth(img_tab_info_item) / 2), -(GameCanvas.hh - mGraphics.getImageHeight(img_tab_info_item) / 2));
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogException(e);

        }
    }

    private void paintCmdPopup(mGraphics g)
    {
        for (int i = 0; i < menu.size(); i++)
        {
            Command cmd = (Command)menu.elementAt(i);
            int x = mGraphics.getImageWidth(img_tab_info_item) - (mGraphics.getImageWidth(btn_cmd_mini) + 5) * i - 5;
            int y = mGraphics.getImageHeight(img_tab_info_item) - mGraphics.getImageHeight(btn_cmd_mini) - 5;
            g.drawImage(btn_cmd_mini, x, y, mGraphics.RIGHT);
            mFont.tahoma_7b_white.drawString(g, cmd.caption, x - mGraphics.getImageWidth(btn_cmd_mini) / 2, y + (mGraphics.getImageHeight(btn_cmd_mini) - mFont.tahoma_7b_white.getHeight()) / 2, mFont.CENTER);
        }
    }

    public void popUpDetailInit(ChatPopup cp, string chat)
    {
        cp.isClip = false;
        cp.sayWidth = mGraphics.getImageWidth(img_tab_info_item) - 10;
        cp.cx = 100;
        cp.says = mFont.tahoma_7_red.splitFontArray(chat, cp.sayWidth - 10);
        cp.delay = 10000000;
        cp.c = null;
        cp.sayRun = 7;
        cp.ch = 15 - cp.sayRun + cp.says.Length * 12 + 10;
        if (cp.ch > GameCanvas.h - 80)
        {
            cp.ch = GameCanvas.h - 80;
            cp.lim = cp.says.Length * 12 - cp.ch + 17;
            if (cp.lim < 0)
            {
                cp.lim = 0;
            }
            ChatPopup.cmyText = 0;
            cp.isClip = true;
        }
        cp.cy = GameCanvas.menu.menuY - cp.ch;
        while (cp.cy < 10)
        {
            cp.cy++;
            GameCanvas.menu.menuY++;
        }
        cp.mH = 0;
        cp.strY = 10;
        scrollPopup.setStyle(cp.says.Length, mFont.tahoma_7b_dark.getHeight(), GameCanvas.hw - mGraphics.getImageWidth(img_tab_info_item) / 2, GameCanvas.hh - mGraphics.getImageHeight(img_tab_info_item) / 2,
            mGraphics.getImageWidth(img_tab_info_item), mGraphics.getImageHeight(img_tab_info_item) - 20, true, 1);
    }

    private void updateClickInside()
    {
        if (!GameCanvas.isPointerClick || !GameCanvas.isPointerJustRelease)
            return;

        m_isClick = false;
        int transX = x + 20;
        int transY = y + 20 + mGraphics.getImageHeight(btn_bag) + 10;

        int cellW = mGraphics.getImageWidth(imgItem);
        int cellH = mGraphics.getImageHeight(imgItem);

        int dx = numCol * (mGraphics.getImageWidth(imgItem) + 5) + 10;//tọa độ phần body
        if (tabTrangBi)
        {
            //click vào ô thông tin
            if (GameCanvas.isPointerHoldIn(transX + mGraphics.getImageWidth(imgBgr) - mGraphics.getImageWidth(btn_bag) * 2, y + 20, mGraphics.getImageWidth(btn_bag), mGraphics.getImageHeight(btn_bag)))
            {
                isInfo = true;
                isBody = false;
                if (GameCanvas.isPointerDown)
                {
                    m_isClick = true;
                }
                GameCanvas.clearAllPointerEvent();
            }//click vào ô trang bị
            else if (GameCanvas.isPointerHoldIn(transX + mGraphics.getImageWidth(imgBgr) - mGraphics.getImageWidth(btn_bag) * 3 - 10, y + 20, mGraphics.getImageWidth(btn_bag), mGraphics.getImageHeight(btn_bag)))
            {
                isInfo = false;
                isBody = true;
                if (GameCanvas.isPointerDown)
                {
                    m_isClick = true;
                }
                GameCanvas.clearAllPointerEvent();


            }//click vào ô rương đồ
            else if (GameCanvas.isPointerHoldIn(transX + mGraphics.getImageWidth(btn_bag) + 10, transY - mGraphics.getImageHeight(btn_bag) - 10, mGraphics.getImageWidth(btn_bag), mGraphics.getImageHeight(btn_bag)))
            {
                isInven = false;
                isBox = true;
                if (GameCanvas.isPointerDown)
                {
                    m_isClick = true;
                }
                setScroll();
                GameCanvas.clearAllPointerEvent();
                UnityEngine.Debug.Log("isInven");

            }//click vào ô hành trang
            else if (GameCanvas.isPointerHoldIn(transX, transY - mGraphics.getImageHeight(btn_bag) - 10, mGraphics.getImageWidth(btn_bag), mGraphics.getImageHeight(btn_bag)))
            {
                isInven = true;
                isBox = false;
                if (GameCanvas.isPointerDown)
                {
                    m_isClick = true;
                }
                setScroll();
                GameCanvas.clearAllPointerEvent();
                UnityEngine.Debug.Log("isInven");
            }
            else if (GameCanvas.isPointerHoldIn(transX, transY, numCol * (cellW + 5), numRow * (cellH + 5)))//xử lý click vào phần inventory
            {
                menu.removeAllElements();
                for (int i = 0; i < cellsInven.Length; i++)
                {
                    if (cellsInven[i].isPointerHoldIn(transX, transY - scrollInven.cmy))
                    {
                        UnityEngine.Debug.Log("click to cell " + i);

                        var arrItem = isInven ? Char.myCharz().arrItemBag : Char.myCharz().arrItemBox;
                        if (i < arrItem.Length && arrItem[i] != null)
                        {
                            currItem = arrItem[i];

                            if (isInven)
                            {
                                GameCanvas.panel.selected = i;
                                if (currItem.isTypeBody())
                                {
                                    menu.addElement(new Command(mResources.USE, this, 2000, currItem));
                                    if (Char.myCharz().havePet)
                                    {
                                        menu.addElement(new Command("Đệ tử", this, 2005, currItem));
                                    }
                                }
                                else
                                {
                                    menu.addElement(new Command(mResources.USE, this, 2001, currItem));
                                }
                                menu.addElement(new Command("Cất", this, 1001, currItem));
                            }
                            else
                            {
                                GameCanvas.panel.selected = i;
                                if (currItem.isTypeBody())
                                {
                                    menu.addElement(new Command(mResources.GETOUT, this, 1000, currItem));
                                }
                                else
                                {
                                    menu.addElement(new Command(mResources.GETOUT, this, 1000, currItem));
                                }
                            }

                            Char.myCharz().setPartTemp(currItem.headTemp, currItem.bodyTemp, currItem.legTemp, currItem.bagTemp);
                            addItemDetail(arrItem[i]);
                        }
                        break;
                    }
                }

                GameCanvas.clearAllPointerEvent();
            }
            else if (GameCanvas.isPointerHoldIn(transX + dx, transY, numCol * (cellW + 5), numRow * (cellH + 5)) && isBody)//xử lý khi click vào phần body
            {
                menu.removeAllElements();
                for (int i = 0; i < cellsBody.Length; i++)
                {
                    if (cellsBody[i].isPointerHoldIn(transX + dx, transY))
                    {
                        UnityEngine.Debug.Log("click to cell body" + i);
                        var arrItem = Char.myCharz().arrItemBody;
                        if (i < arrItem.Length && arrItem[i] != null)
                        {
                            currItem = arrItem[i];
                            GameCanvas.panel.selected = i;
                            if (isInven)
                            {
                                menu.addElement(new Command(mResources.GETOUT, this, 2002, currItem));
                            }
                            else
                            {
                                menu.addElement(new Command("Cất", this, 1002, currItem));
                            }
                            addItemDetail(arrItem[i]);
                        }
                        break;
                    }
                }
                GameCanvas.clearAllPointerEvent();
            }
        }
    }

    private void paintTabInventory(mGraphics g, string name, int x, int y, int anchor = 4, bool isSelect = false)
    {
        if (isSelect)
        {
            g.drawImage(btn_bag_click, x, y, anchor);
        }
        else
        {
            g.drawImage(btn_bag, x, y, anchor);
        }

        if ((anchor & mGraphics.RIGHT) == mGraphics.RIGHT)
            x -= mGraphics.getImageWidth(btn_bag);

        mFont.tahoma_7b_white.drawString(g, name, x + mGraphics.getImageWidth(btn_bag) / 2, y + (mGraphics.getImageHeight(btn_bag) - mFont.tahoma_7b_white.getHeight()) / 2, mFont.CENTER);

    }

    public void perform(int idAction, object p)
    {
        if (idAction == 2000)
        {
            Service.gI().getItem(Panel.BAG_BODY, (sbyte)GameCanvas.panel.selected);
        }
        if (idAction == 2005)
        {
            sbyte id3 = (sbyte)GameCanvas.panel.selected;
            Service.gI().getItem(Panel.BAG_PET, id3);
        }
        if (idAction == 2001)
        {
            Res.outz("use item");
            Item item9 = (Item)p;
            sbyte b = (sbyte)GameCanvas.panel.selected;
            Service.gI().useItem(0, 1, b, -1);
            if (item9.template.id == 193 || item9.template.id == 194)
            {
                GameCanvas.panel.hide();
            }
        }
        if (idAction == 1000)
        {
            Service.gI().getItem(Panel.BOX_BAG, (sbyte)GameCanvas.panel.selected);
        }
        if (idAction == 1002)
        {
            Service.gI().getItem(Panel.BODY_BOX, (sbyte)GameCanvas.panel.selected);
        }
        if (idAction == 2002)
        {
            Service.gI().getItem(Panel.BODY_BAG, (sbyte)GameCanvas.panel.selected);
        }
        if (idAction == 1001)
        {
            sbyte id = (sbyte)GameCanvas.panel.selected;
            Service.gI().getItem(Panel.BAG_BOX, id);
        }
        if (idAction == 1)
        {
            if (chatTField == null)
            {
                chatTField = new ChatTextField();
                chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
                chatTField.initChatTextField();
                chatTField.parentScreen = this;
            }
            chatTField.strChat = "Cộng nhiều điểm";
            chatTField.tfChat.name = "Cộng nhiều điểm";
            chatTField.to = string.Empty;
            chatTField.isShow = true;
            chatTField.tfChat.isFocus = true;
            chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
            if (Main.isWindowsPhone)
            {
                chatTField.tfChat.strInfo = chatTField.strChat;
            }
            chatTField.startChat2(this, string.Empty);
        }
    }

    public void onChatFromMe(string text, string to)
    {
        if (chatTField.tfChat.getText() == null || chatTField.tfChat.getText().Equals(string.Empty) || text.Equals(string.Empty) || text == null)
        {
            chatTField.isShow = false;
            return;
        }
        if (chatTField.strChat.Equals("Cộng nhiều điểm"))
        {
            UnityEngine.Debug.LogError("up up up ");
            InfoDlg.showWait();
            chatTField.isShow = false;
            int num = 0;
            try
            {
                num = int.Parse(chatTField.tfChat.getText());
            }
            catch (Exception)
            {
                GameCanvas.startOKDlg(mResources.input_quantity_wrong);
                chatTField.isShow = false;
                chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
                return;
            }
            if (num != 1 && num != 10 && num != 100)
            {
                GameCanvas.startOKDlg(mResources.input_quantity_wrong);
                chatTField.isShow = false;
                chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
                return;
            }
            UnityEngine.Debug.LogError("up up up " + GameCanvas.panel.selected + " " + num);
            Service.gI().upPotential(GameCanvas.panel.selected, num);
            GameCanvas.endDlg();
            return;
        }
    }

    public void onCancelChat()
    {

    }

    private class Cell
    {
        private int x;
        private int y;

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void setPos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void paint(mGraphics g)
        {
            g.drawImage(InventoryBag.gI().imgItem, x, y);
        }

        public bool isPointerHoldIn(int transX, int transY)
        {
            return GameCanvas.isPointerHoldIn(x + transX, y + transY, mGraphics.getImageWidth(gI().imgItem), mGraphics.getImageHeight(gI().imgItem));
        }
    }


}