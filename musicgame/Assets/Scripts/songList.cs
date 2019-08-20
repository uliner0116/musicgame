using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class songList
{
    public static int GetSongNumber(string songName)
    {
        int result = 0;
        if (string.Compare(songName, "Im sorry") == 0)
        {
            result = 1;
        }
        else if (string.Compare(songName, "LATATA") == 0)
        {
            result = 2;
        }
        else if (string.Compare(songName, "LOVE") == 0)
        {
            result = 3;
        }
        else if (string.Compare(songName, "Mirotic") == 0)
        {
            result = 4;
        }
        else if (string.Compare(songName, "三國戀") == 0)
        {
            result = 5;
        }
        else if (string.Compare(songName, "SORRY SORRY") == 0)
        {
            result = 6;
        }
        else if (string.Compare(songName, "千年之戀") == 0)
        {
            result = 7;
        }
        else if (string.Compare(songName, "回レ! 雪月花") == 0)
        {
            result = 8;
        }
        else if (string.Compare(songName, "Tunak Tunak Tun") == 0)
        {
            result = 9;
        }
        else if (string.Compare(songName, "恋愛サーキュレーション") == 0)
        {
            result = 10;
        }
        else if (string.Compare(songName, "Oh!") == 0)
        {
            result = 11;
        }
        else if (string.Compare(songName, "Roly Poly") == 0)
        {
            result = 12;
        }
        else if (string.Compare(songName, "One Night In 北京") == 0)
        {
            result = 13;
        }
        else if (string.Compare(songName, "Trouble Maker") == 0)
        {
            result = 14;
        }
        else if (string.Compare(songName, "夏祭り") == 0)
        {
            result = 15;
        }
        else if (string.Compare(songName, "Don't say lazy") == 0)
        {
            result = 16;
        }
        else if (string.Compare(songName, "butterfly") == 0)
        {
            result = 17;
        }
        else if (string.Compare(songName, "YES or YES") == 0)
        {
            result = 18;
        }
        else if (string.Compare(songName, "PON PON PON") == 0)
        {
            result = 19;
        }
        else if (string.Compare(songName, "恋は渾沌の隷也") == 0)
        {
            result = 20;
        }
        else if (string.Compare(songName, "夠愛") == 0)
        {
            result = 21;
        }
        return result;
    }

}

