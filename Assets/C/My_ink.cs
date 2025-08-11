using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using DG.Tweening;
using System.Text;

interface I_强制退出
{
	public void 强制退出();
}
// This is a super bare bones example of how to play and display a ink story in Unity.
public class My_ink : MonoBehaviour,I_强制退出
{
	public static event Action<Story> OnCreateStory;

	BiologyBase Player_;
public 	int i;
	public    List<string> Tag列表;


	void asdasd()
    {
		i += (int)story.variablesState["MaoHAOGan"];
 

	}
    private void Start()
	{
		Player_ = Player.I;//更换玩家后要重新引用
		Story = new Story(inkJSON文件.text);
 
  //Story.ObserveVariable("MaoHAOGan", (string varName, object newValue)=>
		//{
		//i=(int) newValue; 
		//}
		// );

		Event_M.I.Add(Event_M.对话触发_OBJ, delegate (GameObject obj)
        {
            if (obj==gameObject )
            {
				Player_.关闭灵魂();
            } });

		Event_M.I.Add(Event_M.对话退出, delegate ( )
		{
	 
				Player_.开启灵魂();
 
		});
	}
	public void 强制退出()
    {
		移除所有子物体();
		Player_.开启灵魂();
	}
	// Creates a new Story object with the compiled story which we can then play!
	public 	void 开始播放剧情()
	{
		Story = new Story(inkJSON文件.text);
		OnCreateStory?.Invoke(Story);
		Event_M.I.Invoke(Event_M .对话触发_OBJ,gameObject );
		更新显示文本();
	}

	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	void 更新显示文本()
	{
		asdasd();
		等玩家按一下嘛  = false;
		// 删除当前
		移除所有子物体();

        // Read all the content until we can't continue any more
        if (Story.canContinue)
        {
            string text = Story.Continue();
			Tag列表 = Story.currentTags ;
			text = text.Trim();

            创造说话文本(text);
        } 
		else
        {
			Event_M.I.Invoke(Event_M.对话退出 );
		}
		if (Story.currentChoices.Count > 0)
		{
			//有选项等按键
			for (int i = 0; i < Story.currentChoices.Count; i++)
			{
				Choice choice = Story.currentChoices[i];
				Button button = 创建选择按钮(choice.text.Trim());
                //if (i == 0)
                //            { 
                //EventSystem.current.SetSelectedGameObject(button.gameObject);
                //}

                // Tell the button what to do when we press it
                button.onClick.AddListener(
					delegate {
						按下了这个按钮(choice);
					});
			}
			
        }
        else
		{

			//没有选项
			//等玩家按钮
			等玩家按一下嘛 = true;
		}
 
	}
	public bool 等玩家按一下嘛;

	[SerializeField]
	bool canContinue;
	public GameObject 当前;
    private void Update()
    {
		canContinue = Story.canContinue;
 
			if (Input.GetKeyDown(UI_input.I.退出))
			{
			强制退出();
			}
 
		if (等玩家按一下嘛 == true )
        {
            if (Input.GetKeyDown(UI_input .I.确认))
            {
				更新显示文本(); 
			}
		}
		当前 = EventSystem.current.currentSelectedGameObject;
 
	}
    // When we click the choice button, tell the story to choose that choice!
    void 按下了这个按钮(Choice choice)
	{
		//往Story里面调用方法    
		Story.ChooseChoiceIndex(choice.index);
		更新显示文本();
	}

	// Creates a textbox showing the the line of text
	public float 语速 = 0.2f;
 
	void 创造说话文本(string text)
	{
		Text storyText = Instantiate(TextPrefab);//从原植体创造出来
												 //storyText.text = text;
		byte[] B = Encoding.Unicode.GetBytes(text);
		float a = 语速 * B.Length;
		while (a > 2f)
		{
			a *= 0.9f;
		}

		storyText.DOText(text, a);
		storyText.transform.SetParent(canvas.transform, false);
	}

	// Creates a button showing the choice text
	Button 创建选择按钮(string text)
	{
		// Creates the button from a prefab
		Button choice = Instantiate(按钮预制体);
		choice.transform.SetParent(canvas.transform, false);

		// Gets the text from the button prefab
		Text choiceText = choice.GetComponentInChildren<Text>();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
		layoutGroup.childForceExpandHeight = false;

		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	void 移除所有子物体()
	{
		int childCount = canvas.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i)
		{
			Destroy(canvas.transform.GetChild(i).gameObject);
		}
	}

	[SerializeField]
	private TextAsset inkJSON文件 = null;
	private Story story;

	[SerializeField]
	private Canvas canvas = null;

	// UI Prefabs
	[SerializeField]
	private Text Txt预制体 = null;
	[SerializeField]
	private Button 按钮预制体 = null;

	public Text TextPrefab { get => Txt预制体; set => Txt预制体 = value; }
	public Story Story { get => story; set => story = value; }
}
