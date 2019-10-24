using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace FancyScrollView.Example02
{
    public class Example02 : MonoBehaviour
    {
        [SerializeField] ScrollView scrollView = default;
        [SerializeField] Text selectedItemInfo = default;
        string[] name = { "butterfly" ,"Don't say lazy" ,"Im sorry" ,"LATATA" ,"LOVE" ,"Mirotic" ,"Oh!" ,"One Night In 北京" ,"PON PON PON" ,"Roly Poly" ,"SORRY SORRY" ,"Trouble Maker" ,"Tunak Tunak Tun" ,
        "YES or YES" ,"三國戀" ,"千年之戀" ,"不得不愛" ,"月牙灣" ,"回レ! 雪月花" ,"我不配" ,"我還年輕 我還年輕" ,"牡丹江" ,"東區東區" ,"直感" ,"星空" ,"夏祭り" ,"恋","恋は渾沌の隷也" ,
        "恋愛サーキュレーション" ,"夠愛" ,"將軍令" ,"華陽炎" ,"極楽浄土" ,"憂愁" ,"憨人" ,"樹枝孤鳥" ,"Burn It Down","Counting Stars","Good Time","I Really Like You","Maps","One More Night",
        "Poker Face","Thunder","What Ive Done","What Makes You Beautiful"
        };
        void Start()
        {
            scrollView.OnSelectionChanged(OnSelectionChanged);

            var items = Enumerable.Range(0, 46)
                .Select(i => new ItemData(name[i], name[i]))
                .ToArray();

            scrollView.UpdateData(items);
            scrollView.SelectCell(0);
        }

        void OnSelectionChanged(int index)
        {
            selectedItemInfo.text = name[index];
        }
    }
}
