using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using DG.Tweening;
using System.Text;
using System.Collections;
using UnityEngine.Events;

public class 一般对话 : MonoBehaviour, I_强制退出
{
	[SerializeField ]
  UnityEvent 对话结束;
	[SerializeField] UnityEvent 对话开始;
	void 更新变量()
	{
		//i += (int)story?.variablesState["MaoHAOGan"]; 
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
 
			if (obj == gameObject)
			{
				Player_.关闭灵魂(); 
				对话开始?.Invoke();
			}
		});

		Event_M.I.Add(Event_M.对话退出, delegate ()
		{
			对话结束?.Invoke();
			清清理关闭();
			说话画布1.gameObject.SetActive(false); 
			Player_.开启灵魂(); 
		});
	}
	public void 强制退出()
	{
		清清理关闭();
		Player_.开启灵魂();
	}
	// Creates a new Story object with the compiled story which we can then play!
	public void 开始播放剧情()
	{
		OnCreateStory?.Invoke(Story);
		Story = new Story(inkJSON文件.text); 
		Event_M.I.Invoke(Event_M.对话触发_OBJ, gameObject);
		更新显示文本();
	}

	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!

	
	string 当前文本;
	void 更新显示文本()
	{
		更新变量();

		等玩家按一下嘛 = false;
		// 删除当前
		说话画布1.enabled = true;
 
		// Read all the content until we can't continue any more
		if (Story.canContinue)
		{
			清清理关闭();
			if (正在逐字打印)
			{
				Text.text = 当前文本;
				正在逐字打印 = false;
				StopCoroutine(这是逐字打印);

			}
            else
			{
				正在逐字打印 = true ;
				string text = Story.Continue(); 
				Tag列表 = Story.currentTags;
				text = text.Trim();
				当前文本 = text;
 
				更新说话的文本(text); 
			}

		}
		else
		{           //后面没有了
			if (正在逐字打印)
			{
				Text.text = 当前文本;
				正在逐字打印 = false;
				StopCoroutine(这是逐字打印);
            }
            else
            {
				//如果打印完成
				Event_M.I.Invoke(Event_M.对话退出);
			} 
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

        if (Input.GetButtonDown(Initialize .Exite))
        {
			强制退出();
		}
 
		if (等玩家按一下嘛 == true)
		{
			if (Input.GetButtonDown(Initialize.Enter))
			{
				更新显示文本();
			}
		}
        if (EventSystem.current!=null)
        {
			当前 = EventSystem.current.currentSelectedGameObject;
		}


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

	void 更新说话的文本(string text)
	{

		byte[] 总字节 = Encoding.Unicode.GetBytes(text);
		float 总时间 = 语速 * 总字节.Length;
		while (总时间 > 2f)
		{
			总时间 *= 0.9f;
		}
		float 单字节间隔 = 总时间 / 总字节.Length;
        这是逐字打印 = StartCoroutine ( 打印机(text,单字节间隔)); 
    }
	

Coroutine 这是逐字打印 { get; set;}
	IEnumerator 打印机(string text, float 单字节间隔)
    {
 
		for (int i = 0; i < text.Length; i++)
		{
		var   SS=	text.Substring(i, 1);
		float 单字间隔=	单字节间隔 *Encoding.Unicode.GetBytes(SS).Length;
			Text.text = text.Substring(0, i+1); 
			yield return new WaitForSeconds(单字间隔);
		}
		正在逐字打印 = false;
	}
[DisplayOnly]
	[SerializeField]
	bool 正在逐字打印;
 
    // Creates a button showing the choice text
    Button 创建选择按钮(string text)
	{
		// Creates the button from a prefab
		Button choice = Instantiate(按钮预制体);
		choice.transform.SetParent(说话画布1.transform, false);

		// Gets the text from the button prefab
		Text choiceText = choice.GetComponentInChildren<Text>();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
		layoutGroup.childForceExpandHeight = false;

		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	void 清清理关闭()
	{ 
        Text.text ="";
 
    }
	public static event Action<Story> OnCreateStory;

	BiologyBase Player_;
	public int i;
	public List<string> Tag列表;
	 

	[SerializeField]
	private TextAsset inkJSON文件 = null;
	private Story story;

	[SerializeField]
	private Canvas 说话画布;
 


	// UI Prefabs
	[SerializeField]
	private Text Txt = null;
	[SerializeField]
	private Button 按钮预制体 = null;

	public Text Text { get => Txt; set => Txt = value; }
	public Story Story { get => story; set => story = value; }
    public Canvas 说话画布1 { get => 说话画布; set => 说话画布 = value; }
}
