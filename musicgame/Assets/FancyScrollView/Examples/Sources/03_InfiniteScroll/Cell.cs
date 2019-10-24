using UnityEngine;
using UnityEngine.UI;

namespace FancyScrollView.Example03
{
    public class Cell : FancyScrollViewCell<ItemData, Context>
    {
        [SerializeField] Animator animator = default;
        [SerializeField] Text message = default;
        [SerializeField] Text messageLarge = default;
        [SerializeField] Image image = default;
        [SerializeField] Image imageLarge = default;
        [SerializeField] Button button = default;
        [SerializeField] Button button3 = default;
        [SerializeField] Button button6 = default;
        public GameObject page;
        public GameObject page2;
        static class AnimatorHash
        {
            public static readonly int Scroll = Animator.StringToHash("scroll");
        }

        void Start()
        {
            button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
            button3.onClick.AddListener(Active_Text);
            button6.onClick.AddListener(Active_Text2);
        }

        public void Active_Text()
        {
            if (!page.activeInHierarchy)
            { page.SetActive(true); }
            else
            { page.SetActive(false); }
        }

        public void Active_Text2()
        {
            if (!page2.activeInHierarchy)
            { page2.SetActive(true); }
            else
            { page2.SetActive(false); }
        }

        public override void UpdateContent(ItemData itemData)
        {
            message.text = itemData.Message;
            messageLarge.name = itemData.Message; //歌曲名稱
            
          //   Debug.Log(itemData.imageName);
            imageLarge.sprite = Resources.Load<Sprite>(itemData.imageName);
          //  imageLarge.name = itemData.Message;
            var selected = Context.SelectedIndex == Index;
            image.color = selected
                ? new Color32(0, 255, 255, 100)
                : new Color32(255, 255, 255, 77);
            if(selected)
            {
                button3.gameObject.SetActive(true);
                button6.gameObject.SetActive(true);
            }
            else
            {
                button3.gameObject.SetActive(false);
                button6.gameObject.SetActive(false);
            }
        }

        public override void UpdatePosition(float position)
        {
            currentPosition = position;

            if (animator.isActiveAndEnabled)
            {
                animator.Play(AnimatorHash.Scroll, -1, position);
            }

            animator.speed = 0;
        }

        // GameObject が非アクティブになると Animator がリセットされてしまうため
        // 現在位置を保持しておいて OnEnable のタイミングで現在位置を再設定します
        float currentPosition = 0;

        void OnEnable() => UpdatePosition(currentPosition);
    }
}
