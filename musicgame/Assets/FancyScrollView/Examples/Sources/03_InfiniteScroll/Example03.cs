using System.Linq;
using UnityEngine;

namespace FancyScrollView.Example03
{
    public class Example03 : MonoBehaviour
    {
        [SerializeField] ScrollView scrollView = default;
        string[] name = { "Im sorry", "Don_t say lazy","Butter-Fly", "LATATA","LOVE","Mirotic","Oh!","One Night In北京","PON PON PON","Roly Poly", "SORRY, SORRY", "Trouble Maker","Tunak Tunak Tun","YES or YES","三國戀","千年之戀", "恋愛サーキュレーション", "恋は渾沌の隷也", "回レ! 雪月花", "夠愛", "夏祭り"};
        void Start()
        {
            var items = Enumerable.Range(0,20)
                .Select(i => new ItemData(name[i], name[i]))
                .ToArray();

            scrollView.UpdateData(items);
            scrollView.SelectCell(0);
        }
    }
}
