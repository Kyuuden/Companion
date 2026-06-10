using System;

namespace FF.Rando.Companion.Extensions;

public static class ByteArrayExtensionHelpers
{
    internal static void MoveBits(byte[] input, uint inOffset, uint inWidth, byte[] output, uint outOffset, uint outWidth)
    {
        if (inOffset % 8 == 0 && outOffset % 8 == 0 && inWidth % 8 == 0 && outWidth % 8 == 0)
        {
            Array.Copy(input, (int)inOffset / 8, output, (int)outOffset / 8, (int)Math.Min(inWidth / 8, outWidth / 8));
            return;
        }

        uint remaining = Math.Min(inWidth, outWidth);

        while (remaining > 0)
        {
            uint inByte = inOffset / 8;
            byte inBit = (byte)(inOffset % 8);
            uint outByte = outOffset / 8;
            byte outBit = (byte)(outOffset % 8);
            uint chunkSize = (uint)Math.Min(remaining, Math.Min(8 - inBit, 8 - outBit));

            output[outByte] &= (byte)~(BitMasks8[chunkSize] << outBit);
            output[outByte] |= (byte)((input[inByte] >> inBit & BitMasks8[chunkSize]) << outBit);

            remaining -= chunkSize;
            outOffset += chunkSize;
            inOffset += chunkSize;
        }
    }

    internal static void MoveData(byte[] input, uint inOffset, uint inWidth, ByteOrder? inOrder, byte[] output, uint outOffset, uint outWidth, ByteOrder? outOrder)
    {
        inOrder ??= ByteOrder.LittleEndian;
        outOrder ??= ByteOrder.LittleEndian;

        if (inOrder.Value == ByteOrder.LittleEndian && outOrder.Value == ByteOrder.LittleEndian)
        {
            ByteArrayExtensionHelpers.MoveBits(input, inOffset, inWidth, output, outOffset, outWidth);
        }
        else if (inOrder.Value == ByteOrder.BigEndian && outOrder.Value == ByteOrder.BigEndian)
        {
            if (outWidth > inWidth)
                MoveBits(input, inOffset, inWidth, output, outOffset + (outWidth - inWidth), outWidth);
            else
                MoveBits(input, inOffset + (inWidth - outWidth), inWidth, output, outOffset, outWidth);
        }
        else if (inOrder.Value == ByteOrder.LittleEndian && outOrder.Value == ByteOrder.BigEndian)
        {
            while (inWidth > 0 && outWidth > 0)
            {
                uint chunkSize = Math.Min(inWidth, 8);

                MoveBits(input, inOffset, chunkSize, output, outOffset + outWidth - chunkSize, chunkSize);

                inWidth -= chunkSize;
                outWidth -= chunkSize;
                inOffset += chunkSize;
            }
        }
        else if (inOrder.Value == ByteOrder.BigEndian && outOrder.Value == ByteOrder.LittleEndian)
        {
            while (inWidth > 0 && outWidth > 0)
            {
                uint chunkSize = Math.Min(inWidth, 8);

                MoveBits(input, inOffset + inWidth - chunkSize, chunkSize, output, outOffset, chunkSize);

                inWidth -= chunkSize;
                outWidth -= chunkSize;
                outOffset += chunkSize;
            }
        }
    }

    private static readonly byte[] BitMasks8 = [0x00, 0x01, 0x03, 0x07, 0x0F, 0x1F, 0x3F, 0x7F, 0xFF];
}