using System;

// Token: 0x02000010 RID: 16
public class AutoPean : IActionListener, IChatable
{
	// Token: 0x06000070 RID: 112 RVA: 0x00011458 File Offset: 0x0000F658
	public AutoPean()
	{
		this.string_0 = new string[]
		{
			"Nhập %HP Pean",
			"%HP"
		};
		this.string_1 = new string[]
		{
			"Nhập %MP Pean",
			"%MP"
		};
		this.string_2 = new string[]
		{
			"Nhập HP Pean",
			"HP"
		};
		this.string_3 = new string[]
		{
			"Nhập MP Pean",
			"MP"
		};
		this.method_2();
	}

	// Token: 0x06000071 RID: 113 RVA: 0x000114E4 File Offset: 0x0000F6E4
	public void method_0()
	{
		if (GameCanvas.panel.indexMod[7] && global::Char.myCharz().havePet)
		{
			if(GameCanvas.gameTick % 20 == 0)
			{
				Service.gI().petInfo();
				Char c = Char.myCharz();
				Char p = Char.myPetz();
				if(!c.meDead && (p.cHP < (p.cHPFull / 10) || p.cMP < (p.cMPFull / 10) || p.cStamina < 150))
				{
					GameScr.gI().doUseHP();
				}
			}
		}
		if (this.bool_0)
		{
			this.method_4();
		}
		if (GameCanvas.panel.indexMod[10] && GameCanvas.gameTick % 20 == 0)
		{
			this.method_5();
		}
		if (GameCanvas.panel.indexMod[11])
		{
			this.method_6();
		}
		if (!global::Char.myCharz().meDead)
		{
			this.method_7();
		}
	}

	// Token: 0x06000072 RID: 114
	public void onChatFromMe(string text, string to)
	{
		ChatTextField chatTextField = ChatTextField.gI();
		if (chatTextField.tfChat.getText() == null || chatTextField.tfChat.getText().Equals(string.Empty) || text.Equals(string.Empty) || text == null)
		{
            HairMod.ResetTF();
			return;
		}
		if (chatTextField.strChat.Equals(this.string_0[0]))
		{
			try
			{
				int num = int.Parse(chatTextField.tfChat.getText());
				if (num >= 100)
				{
					num = 99;
				}
				if (num <= 0)
				{
					num = 1;
				}
				this.int_0 = num;
				GameScr.info1.addInfo("Auto pean khi HP dưới: " + num.ToString() + "%", 0);
				if (this.bool_3)
				{
					Rms.saveRMSInt("AutoPeanPercentHP", this.int_0);
				}
			}
			catch
			{
				GameScr.info1.addInfo("%HP không hợp lệ, vui lòng nhập lại", 0);
			}
			HairMod.ResetTF();
			return;
		}
		if (chatTextField.strChat.Equals(this.string_1[0]))
		{
			try
			{
				int num2 = int.Parse(chatTextField.tfChat.getText());
				if (num2 >= 100)
				{
					num2 = 99;
				}
				if (num2 <= 0)
				{
					num2 = 1;
				}
				this.int_1 = num2;
				GameScr.info1.addInfo("Auto pean khi MP dưới: " + num2.ToString() + "%", 0);
				if (this.bool_3)
				{
					Rms.saveRMSInt("AutoPeanPercentMP", this.int_1);
				}
			}
			catch
			{
				GameScr.info1.addInfo("%MP không hợp lệ, vui lòng nhập lại", 0);
			}
			HairMod.ResetTF();
			return;
		}
		if (chatTextField.strChat.Equals(this.string_2[0]))
		{
			try
			{
				int num3 = this.int_2 = int.Parse(chatTextField.tfChat.getText());
				if (this.bool_3)
				{
					Rms.saveRMSString("AutoPeanHP", this.int_2.ToString());
				}
				GameScr.info1.addInfo("Auto pean khi HP dưới: " + NinjaUtil.getMoneys((long)num3) + "HP", 0);
			}
			catch
			{
				GameScr.info1.addInfo("HP không hợp lệ, vui lòng nhập lại", 0);
			}
			HairMod.ResetTF();
			return;
		}
		if (!chatTextField.strChat.Equals(this.string_3[0]))
		{
			return;
		}
		try
		{
			int num4 = this.int_3 = int.Parse(chatTextField.tfChat.getText());
			if (this.bool_3)
			{
				Rms.saveRMSString("AutoPeanMP", this.int_3.ToString());
			}
			GameScr.info1.addInfo("Auto pean khi MP dưới: " + NinjaUtil.getMoneys((long)num4) + "MP", 0);
		}
		catch
		{
			GameScr.info1.addInfo("MP không hợp lệ, vui lòng nhập lại", 0);
		}
		HairMod.ResetTF();
	}

	// Token: 0x06000073 RID: 115
	public void onCancelChat()
	{
	}

	// Token: 0x06000074 RID: 116
	public void perform(int idAction, object p)
	{
		switch (idAction)
		{
			case 1:
				GameCanvas.panel.indexMod[9] = !GameCanvas.panel.indexMod[9];
				GameScr.info1.addInfo("Xin Đậu\n" + (GameCanvas.panel.indexMod[9] ? "[ON]" : "[OFF]"), 0);
				if (this.bool_3)
				{
					Rms.saveRMSInt("AutoPeanIsAutoRequestPean", GameCanvas.panel.indexMod[9] ? 1 : 0);
					return;
				}
				break;
			case 2:
                GameCanvas.panel.indexMod[10] = !GameCanvas.panel.indexMod[10];
				GameScr.info1.addInfo("Cho Đậu\n" + (GameCanvas.panel.indexMod[10] ? "[ON]" : "[OFF]"), 0);
				if (this.bool_3)
				{
					Rms.saveRMSInt("AutoPeanIsAutoSendPean", GameCanvas.panel.indexMod[10] ? 1 : 0);
					return;
				}
				break;
			case 3:
                GameCanvas.panel.indexMod[11] = !GameCanvas.panel.indexMod[11];
				GameScr.info1.addInfo("Thu Đậu\n" + (GameCanvas.panel.indexMod[11] ? "[ON]" : "[OFF]"), 0);
				if (this.bool_3)
				{
					Rms.saveRMSInt("AutoPeanIsAutoHarvestPean", GameCanvas.panel.indexMod[11] ? 1 : 0);
					return;
				}
				break;
			case 4:
				if (this.int_2 != 0)
				{
					this.int_2 = 0;
					GameScr.info1.addInfo("Auto đậu: 0HP", 0);
					if (this.bool_3)
					{
						Rms.saveRMSString("AutoPeanHP", this.int_2.ToString());
						return;
					}
				}
				else if (this.int_2 == 0)
				{
					HairMod.InputTF(this, this.string_2[0], this.string_2[1], TField.INPUT_TYPE_NUMERIC);
					return;
				}
				break;
			case 5:
				if (this.int_0 != 0)
				{
					this.int_0 = 0;
					GameScr.info1.addInfo("Auto đậu: 0% HP", 0);
					if (this.bool_3)
					{
						Rms.saveRMSInt("AutoPeanPercentHP", this.int_0);
						return;
					}
				}
				else if (this.int_0 == 0)
				{
                    HairMod.InputTF(this, this.string_0[0], this.string_0[1], TField.INPUT_TYPE_NUMERIC);
					return;
				}
				break;
			case 6:
				if (this.int_3 != 0)
				{
					this.int_3 = 0;
					GameScr.info1.addInfo("Auto đậu: 0MP", 0);
					if (this.bool_3)
					{
						Rms.saveRMSString("AutoPeanMP", this.int_3.ToString());
						return;
					}
				}
				else if (this.int_3 == 0)
				{
					HairMod.InputTF(this, this.string_3[0], this.string_3[1], TField.INPUT_TYPE_NUMERIC);
					return;
				}
				break;
			case 7:
				if (this.int_1 != 0)
				{
					this.int_1 = 0;
					GameScr.info1.addInfo("Auto đậu: 0% MP", 0);
					if (this.bool_3)
					{
						Rms.saveRMSInt("AutoPeanPercentMP", this.int_1);
						return;
					}
				}
				else if (this.int_1 == 0)
				{
					HairMod.InputTF(this, this.string_1[0], this.string_1[1], TField.INPUT_TYPE_NUMERIC);
					return;
				}
				break;
			case 8:
				this.bool_3 = !this.bool_3;
				GameScr.info1.addInfo("Lưu Cài Đặt\n" + (this.bool_3 ? "[ON]" : "[OFF]"), 0);
				Rms.saveRMSInt("AutoPeanIsSaveRms", this.bool_3 ? 1 : 0);
				if (this.bool_3)
				{
					this.method_3();
					return;
				}
				break;
			case 9:
                GameCanvas.panel.indexMod[7] = !GameCanvas.panel.indexMod[7];
				GameScr.info1.addInfo("Auto Bơm Đậu Cho Đệ\n" + (GameCanvas.panel.indexMod[7] ? "[ON]" : "[OFF]"), 0);
				break;
			default:
				return;
		}
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00011CC8 File Offset: 0x0000FEC8
	public void method_1()
	{
		this.method_2();
		MyVector myVector = new MyVector();
		myVector.addElement(new Command("Auto Đậu\nCho Đệ\n" + (GameCanvas.panel.indexMod[7] ? "[ON]" : "[OFF]"), this, 9, null));
		myVector.addElement(new Command("Xin Đậu\n" + (GameCanvas.panel.indexMod[9] ? "[ON]" : "[OFF]"), this, 1, null));
		myVector.addElement(new Command("Cho Đậu\n" + (GameCanvas.panel.indexMod[10] ? "[ON]" : "[OFF]"), this, 2, null));
		myVector.addElement(new Command("Thu Đậu\n" + (GameCanvas.panel.indexMod[11] ? "[ON]" : "[OFF]"), this, 3, null));
		myVector.addElement(new Command("Ăn Đậu Khi HP Dưới: " + NinjaUtil.getMoneys((long)this.int_2) + "HP", this, 4, null));
		myVector.addElement(new Command("Ăn Đậu Khi HP Dưới: " + this.int_0.ToString() + "%", this, 5, null));
		myVector.addElement(new Command("Ăn Đậu Khi MP Dưới: " + NinjaUtil.getMoneys((long)this.int_3) + "MP", this, 6, null));
		myVector.addElement(new Command("Ăn Đậu Khi MP Dưới: " + this.int_1.ToString() + "%", this, 7, null));
		myVector.addElement(new Command("Lưu Cài Đặt\n" + (this.bool_3 ? "[ON]" : "[OFF]"), this, 8, null));
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00011E8C File Offset: 0x0001008C
	private void method_2()
	{
		this.bool_3 = (Rms.loadRMSInt("AutoPeanIsSaveRms") == 1);
		if (this.bool_3)
		{
            GameCanvas.panel.indexMod[9] = (Rms.loadRMSInt("AutoPeanIsAutoRequestPean") == 1);
            GameCanvas.panel.indexMod[10] = (Rms.loadRMSInt("AutoPeanIsAutoSendPean") == 1);
            GameCanvas.panel.indexMod[11] = (Rms.loadRMSInt("AutoPeanIsAutoHarvestPean") == 1);
			if (Rms.loadRMSInt("AutoPeanPercentHP") == -1)
			{
				this.int_0 = 0;
			}
			else
			{
				this.int_0 = Rms.loadRMSInt("AutoPeanPercentHP");
			}
			if (Rms.loadRMSInt("AutoPeanPercentMP") == -1)
			{
				this.int_1 = 0;
			}
			else
			{
				this.int_1 = Rms.loadRMSInt("AutoPeanPercentMP");
			}
			if (Rms.loadRMSString("AutoPeanHP") == null)
			{
				this.int_2 = 0;
			}
			else
			{
				this.int_2 = int.Parse(Rms.loadRMSString("AutoPeanHP"));
			}
			if (Rms.loadRMSString("AutoPeanMP") == null)
			{
				this.int_3 = 0;
				return;
			}
			this.int_3 = int.Parse(Rms.loadRMSString("AutoPeanMP"));
		}
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00011FA4 File Offset: 0x000101A4
	private void method_3()
	{
		Rms.saveRMSInt("AutoPeanIsAutoRequestPean", GameCanvas.panel.indexMod[9] ? 1 : 0);
		Rms.saveRMSInt("AutoPeanIsAutoSendPean", GameCanvas.panel.indexMod[10] ? 1 : 0);
		Rms.saveRMSInt("AutoPeanIsAutoHarvestPean", GameCanvas.panel.indexMod[11] ? 1 : 0);
		Rms.saveRMSString("AutoPeanHP", this.int_2.ToString());
		Rms.saveRMSInt("AutoPeanPercentHP", this.int_0);
		Rms.saveRMSString("AutoPeanMP", this.int_3.ToString());
		Rms.saveRMSInt("AutoPeanPercentMP", this.int_1);
	}

	// Token: 0x06000078 RID: 120 RVA: 0x0000819E File Offset: 0x0000639E
	private void method_4()
	{
		if (mSystem.currentTimeMillis() - this.long_0 >= 301000L)
		{
			this.long_0 = mSystem.currentTimeMillis();
			Service.gI().clanMessage(1, "", -1);
		}
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00012050 File Offset: 0x00010250
	private void method_5()
	{
		for (int i = 0; i < ClanMessage.vMessage.size(); i++)
		{
			ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(i);
			if (clanMessage.maxCap != 0 && clanMessage.playerName != global::Char.myCharz().cName && clanMessage.recieve != clanMessage.maxCap)
			{
				Service.gI().clanDonate(clanMessage.id);
				return;
			}
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x000120D0 File Offset: 0x000102D0
	private void method_6()
	{
		if (TileMap.mapID != 21 && TileMap.mapID != 22 && TileMap.mapID != 23)
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < global::Char.myCharz().arrItemBox.Length; i++)
		{
			if (global::Char.myCharz().arrItemBox[i] != null && global::Char.myCharz().arrItemBox[i].template.type == 6)
			{
				num += global::Char.myCharz().arrItemBox[i].quantity;
			}
		}
		if (num < 20 && GameCanvas.gameTick % 100 == 0)
		{
			for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
			{
				if (global::Char.myCharz().arrItemBag[j] != null && global::Char.myCharz().arrItemBag[j].template.type == 6)
				{
					Service.gI().getItem(1, (sbyte)j);
					break;
				}
			}
		}
		if (GameScr.gI().magicTree.currPeas > 0 && (GameScr.hpPotion < 10 || num < 20) && GameCanvas.gameTick % 200 == 0)
		{
			Service.gI().openMenu(4);
			Service.gI().menu(4, 0, 0);
		}
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00012208 File Offset: 0x00010408
	private void method_7()
	{
		if (GameScr.hpPotion > 0)
		{
			if (this.int_0 != 0 && this.method_10(this.int_2, this.int_0) && GameCanvas.gameTick % 20 == 0)
			{
				GameScr.gI().doUseHP();
				return;
			}
			if (this.int_1 != 0 && this.method_11(this.int_3, this.int_1) && GameCanvas.gameTick % 20 == 0)
			{
				GameScr.gI().doUseHP();
			}
		}
	}

	// Token: 0x0600007C RID: 124 RVA: 0x000081D3 File Offset: 0x000063D3
	private int method_8()
	{
		return (int)((long)global::Char.myCharz().cHP * 100L / (long)global::Char.myCharz().cHPFull);
	}

	// Token: 0x0600007D RID: 125 RVA: 0x000081F7 File Offset: 0x000063F7
	private int method_9()
	{
		return (int)((long)global::Char.myCharz().cMP * 100L / (long)global::Char.myCharz().cMPFull);
	}

	// Token: 0x0600007E RID: 126 RVA: 0x0000821B File Offset: 0x0000641B
	private bool method_10(int int_4, int int_5)
	{
		return global::Char.myCharz().cHP > 0 && (this.method_8() <= int_5 || global::Char.myCharz().cHP < int_4);
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00008244 File Offset: 0x00006444
	private bool method_11(int int_4, int int_5)
	{
		return global::Char.myCharz().cHP > 0 && (this.method_9() <= int_5 || global::Char.myCharz().cMP < int_4);
	}

	// Token: 0x04000087 RID: 135
	public static AutoPean gclass10_0 = new AutoPean();

	// Token: 0x04000088 RID: 136
	private bool bool_0;

	// Token: 0x04000089 RID: 137
	private long long_0;

	// Token: 0x0400008A RID: 138
	private bool bool_1;

	// Token: 0x0400008B RID: 139
	private bool bool_2;

	// Token: 0x0400008C RID: 140
	private int int_0;

	// Token: 0x0400008D RID: 141
	private int int_1;

	// Token: 0x0400008E RID: 142
	private int int_2;

	// Token: 0x0400008F RID: 143
	private int int_3;

	// Token: 0x04000090 RID: 144
	private bool bool_3;

	// Token: 0x04000091 RID: 145
	private string[] string_0;

	// Token: 0x04000092 RID: 146
	private string[] string_1;

	// Token: 0x04000093 RID: 147
	private string[] string_2;

	// Token: 0x04000094 RID: 148
	private string[] string_3;

	// Token: 0x04000095 RID: 149
	private bool bool_4;
}
