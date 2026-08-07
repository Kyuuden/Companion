using System;

namespace FF.Rando.Companion.Games.JetsOfTime;
internal static class Utils
{
	private static void EnsureSpace(ref byte[] data, uint length)
	{
		if (data.Length > length) return;

		var newBuffer = new byte[data.Length * 2];

		data.AsSpan().CopyTo(newBuffer.AsSpan());
		data = newBuffer;
	}

	public static byte[] DecompressData(byte[] source)
	{
		var result = new byte[source.Length];

		//Chrono Trigger Decompression Routine
		//Reverse engineered by Michael Springer (evilpeer@hotmail.com)
		bool bCarryFlag;
		ushort nCompressedSize = (ushort)(source[0] | (source[1] << 8)); ;
		uint nBytePos = 2;
		uint nByteAfter = nBytePos + nCompressedSize;
		byte nBitCtr;
		byte nCurByte;
		byte nMem0D;
		uint nWorkPos = 0;
		bool bSmallerBitWidth = false;

		if ((source[nByteAfter] & 0xC0) != 0)
		{
			bSmallerBitWidth = true;
		}

		//C3/076B:	A908    	LDA #$08
		//C3/076D:	850B    	STA nBitCtr
		nBitCtr = 8;
		while (true)
		{
			//C3/076F:	E409    	CPX nByteAfter
			//C3/0771:	F056    	BEQ $07C9
			if (nBytePos == nByteAfter)
			{
				//C3/07C9:	BD0000  	LDA $0000,X
				nCurByte = source[nBytePos];
				//C3/07CC:	293F    	AND #$3F
				nCurByte &= 0x3F;
				//C3/07CE:	F012    	BEQ $07E2
				if (nCurByte == 0)
				{
					//C3/07E2:	4CA408  	JMP $08A4
					return result.AsSpan()[..(int)nWorkPos].ToArray();
				}
				//C3/07D0:	850B    	STA nBitCtr
				nBitCtr = nCurByte;
				//C3/07D2:	C221    	REP #$21
				//C3/07D4:	BD0100  	LDA $0001,X
				//C3/07D7:	6500    	ADC $00
				//C3/07D9:	8509    	STA nByteAfter
				nByteAfter = (uint)((source[nBytePos + 2] << 8) | source[nBytePos + 1]);
				//C3/07DB:	E8      	INX 
				//C3/07DC:	E8      	INX 
				//C3/07DD:	E8      	INX 
				nBytePos += 3;
				//C3/07DE:	E220    	SEP #$20
				//C3/07E0:	808D    	BRA $076F
			}
			else
			{
				//C3/0773:	BD0000  	LDA $0000,X
				nCurByte = source[nBytePos];
				//C3/0776:	F0AB    	BEQ $0723
				if (nCurByte == 0)
				{
					EnsureSpace(ref result, nWorkPos + 9);
					//C3/0723:	BD0100  	LDA $0001,X
					nCurByte = source[nBytePos + 1];
                    //C3/0726:	8F802100	STA $002180
                    result[nWorkPos++] = nCurByte;
					//C3/072A:	BD0200  	LDA $0002,X
					nCurByte = source[nBytePos + 2];
                    //C3/072D:	8F802100	STA $002180
                    result[nWorkPos++] = nCurByte;
					//C3/0731:	BD0300  	LDA $0003,X
					nCurByte = source[nBytePos + 3];
                    //C3/0734:	8F802100	STA $002180
                    result[nWorkPos++] = nCurByte;
					//C3/0738:	BD0400  	LDA $0004,X
					nCurByte = source[nBytePos + 4];
                    //C3/073B:	8F802100	STA $002180
                    result[nWorkPos++] = nCurByte;
					//C3/073F:	BD0500  	LDA $0005,X
					nCurByte = source[nBytePos + 5];
                    //C3/0742:	8F802100	STA $002180
                    result[nWorkPos++] = nCurByte;
					//C3/0746:	BD0600  	LDA $0006,X
					nCurByte = source[nBytePos + 6];
                    //C3/0749:	8F802100	STA $002180
                    result[nWorkPos++] = nCurByte;
					//C3/074D:	BD0700  	LDA $0007,X
					nCurByte = source[nBytePos + 7];
                    //C3/0750:	8F802100	STA $002180
                    result[nWorkPos++] = nCurByte;
					//C3/0754:	BD0800  	LDA $0008,X
					nCurByte = source[nBytePos + 8];
                    //C3/0757:	8F802100	STA $002180
                    result[nWorkPos++] = nCurByte;
					//C3/075B:	C221    	REP #$21
					//C3/075D:	8A      	TXA 
					//C3/075E:	690900  	ADC #$0009
					//C3/0761:	AA      	TAX 
					nBytePos += 9;
					//C3/0762:	98      	TYA 
					//C3/0763:	690800  	ADC #$0008
					//C3/0766:	A8      	TAY 
					//C3/0767:	E220    	SEP #$20
					//C3/0769:	8004    	BRA $076F
				}
				else
				{
					//C3/0778:	E8      	INX 
					nBytePos++;
					//C3/0779:	4A      	LSR A
					//C3/077A:	850D    	STA $0D
					if ((nCurByte & 0x01) == 1)
					{
						bCarryFlag = true;
					}
					else
					{
						bCarryFlag = false;
					}
					nCurByte >>= 1;
					nMem0D = nCurByte;
					//C3/077C:	B01C    	BCS $079A
					if (bCarryFlag)
					{
						CTCopyBytes(source, ref nBytePos, ref result, ref nWorkPos, bSmallerBitWidth);
					}
					else
					{
						//C3/077E:	BD0000  	LDA $0000,X
						nCurByte = source[nBytePos];
                        //C3/0781:	8F802100	STA $002180
                        EnsureSpace(ref result, nWorkPos + 1);
                        result[nWorkPos++] = nCurByte;
						//C3/0785:	C8      	INY 
						//C3/0786:	E8      	INX 
						nBytePos++;
					}
					while (true)
					{
						//C3/0787:	C60B    	DEC nBitCtr
						nBitCtr--;
						//C3/0789:	F0E0    	BEQ $076B
						if (nBitCtr == 0)
						{
							nBitCtr = 8;
							break;
						}
						else
						{
							//C3/078B:	460D    	LSR $0D
							if ((nMem0D & 0x01) == 1)
							{
								bCarryFlag = true;
							}
							else
							{
								bCarryFlag = false;
							}
							nMem0D >>= 1;
							//C3/078D:	B00B    	BCS $079A
							if (bCarryFlag)
							{
								CTCopyBytes(source, ref nBytePos, ref result, ref nWorkPos, bSmallerBitWidth);
							}
							else
							{
								//C3/078F:	BD0000  	LDA $0000,X
								nCurByte = source[nBytePos];
                                //C3/0792:	8F802100	STA $002180
                                EnsureSpace(ref result, nWorkPos + 9);
                                result[nWorkPos++] = nCurByte;
								//C3/0796:	C8      	INY 
								//C3/0797:	E8      	INX 
								nBytePos++;
								//C3/0798:	80ED    	BRA $0787
							}
						}
					}
				}
			}
		}
	}

	private static void CTCopyBytes(byte[] source, ref uint nBytePos, ref byte[] result, ref uint nWorkPos, bool bSmallerBitWidth)
	{
		byte nBytesCopyNum;
		ushort nBytesCopyOff;

		//C3/079A:	BD0100  	LDA $0001,X
		nBytesCopyNum = source[nBytePos + 1];
		//C3/079D:	4A      	LSR A
		//C3/079E:	4A      	LSR A
		//C3/079F:	4A      	LSR A
		if (bSmallerBitWidth)
		{
			nBytesCopyNum >>= 3;
		}
		else
		{
			nBytesCopyNum >>= 4;
		}
		//C3/07A0:	1A      	INC A
		//C3/07A1:	1A      	INC A
		nBytesCopyNum += 2;
		//C3/07A2:	850F    	STA $0F
		//C3/07A4:	C220    	REP #$20
		//C3/07A6:	BD0000  	LDA $0000,X
		nBytesCopyOff = (ushort)((source[nBytePos + 1] << 8) | source[nBytePos]);
		//C3/07A9:	29FF07  	AND #$07FF
		//C3/07AC:	8515    	STA $15
		if (bSmallerBitWidth)
		{
			nBytesCopyOff &= 0x07FF;
		}
		else
		{
			nBytesCopyOff &= 0x0FFF;
		}
        //C3/07AE:	98      	TYA 
        //C3/07AF:	38      	SEC 
        //C3/07B0:	E515    	SBC $15
        //C3/07B2:	8615    	STX $15
        //C3/07B4:	AA      	TAX 
        //C3/07B5:	A50F    	LDA $0F
        //C3/07B7:	8B      	PHB 
        //C3/07B8:	547E7E  	MVN $7E,$7E
        EnsureSpace(ref result, nWorkPos + nBytesCopyNum + 1);

        for (int i = 0; i < nBytesCopyNum + 1; i++)
		{
            result[nWorkPos + i] = result[nWorkPos - nBytesCopyOff + i];
		}
		nWorkPos += (uint)(nBytesCopyNum + 1);
		//C3/07BB:	AB      	PLB 
		//C3/07BC:	98      	TYA 
		//C3/07BD:	8F812100	STA $002181
		//C3/07C1:	E220    	SEP #$20
		//C3/07C3:	A615    	LDX $15
		//C3/07C5:	E8      	INX 
		//C3/07C6:	E8      	INX 
		nBytePos += 2;
		//C3/07C7:	80BE    	BRA $0787
	}

	public static bool IsFlagSet(this ReadOnlySpan<byte> bytes, int offset, byte mask)
		=> (bytes[offset] & mask) != 0;
}
