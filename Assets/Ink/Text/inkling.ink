->Scene1
VAR MaoHAOGan = 0


=== Scene1 ===
勇者死了嘛  #这是魔王将领说的#威严满满
BOSS再问你话呢 # 这是魔王杂兵说的#小杂碎的表情
* 没死

放P
回答错误
~MaoHAOGan--
->No


*死了

是的捏
你是对的捏
~MaoHAOGan++
->Yes


= Yes
你是一个诚实的士兵， 但是奖励你一个暴栗
-> Comvine
= No
勇者怎么可能没死捏，但是奖励你一块菠萝披萨饼
-> Comvine
 
= Comvine
{Yes:这个栗子是糖炒栗子}
（魔王军对你的好感为：{MaoHAOGan}）

{MaoHAOGan >0:->END1|->END2}
= END1
This is the content of the stitch that should be embedded within a knot.
-> END

= END2
This is the content of the stitch that should be embedded within a knot.
-> END
合体！！！
-> END

-> END
