using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

namespace RenaissanceRestart
{
    public interface IPerformCore
    {
        /// <summary>
        /// 异步控制
        /// </summary>
        CancellationToken AsyncToken { get; }
    }
    public interface IPerformBase
    {
        /// <summary>
        /// 演出的名称
        /// </summary>
        string PerformName { get; }
    }
    public interface IPerformChara
    {
        InGameDialogueBox DialogueBox { get; }
    }

    public abstract class PerformBase : IPerformBase
    {
        public abstract string PerformName { get; }

        protected Vector2 AnchorPosition;
        protected IPerformCore core;

        private CancellationToken Token;
        private bool isInit = false;
        private bool isDisposeed = false;
        public bool IsPressForceSkip = false;
        public bool IsPressSkip = false;

        protected GameRuleToggle INPERFORM_CANT_SAVE;
        protected GameRuleToggle WaitFadeBlackMask;


        /// <summary>
        /// 执行演出
        /// </summary>
        /// <param name="core"></param>
        /// <param name="anchor"></param>
        /// <returns></returns>
        public async UniTask DoPerform(IPerformCore core, Vector2Int anchor)
        {
            this.InitAndBeginPerform(core, anchor);
            await this.Run();
            this.DisposeAndEndPerform();
        }
        private void InitAndBeginPerform(IPerformCore core, Vector2Int positionAnchor)
        {
            this.Token = core.AsyncToken;
            if (isInit)
                return;
            isInit = true;
            this.AnchorPosition = positionAnchor;
            this.core = core;

            this.INPERFORM_CANT_SAVE = new GameRuleToggle(this, PerformName.ToString() + "_Perform_INPERFORM_CANT_SAVE",
                PerformRuleConst.DisableSaveGame);
            this.INPERFORM_CANT_SAVE.RuleOn();
        }
        private void DisposeAndEndPerform()
        {
            if (isDisposeed)
                return;
            isDisposeed = true;

            this.INPERFORM_CANT_SAVE?.RuleOff();
        }
        protected abstract UniTask Run();






        public async UniTask Skipable(Func<UniTask<bool>> waitfor_update, Action OnSkip, bool onlyForceSkip, bool cannotSkip = false)
        {
            bool isskip = false;
            bool condition = false;
            while (!condition)
            {
                this.Token.ThrowIfCancellationRequested();
                if (!cannotSkip)
                {
                    if (onlyForceSkip)
                        isskip = IsPressForceSkip;
                    else
                        isskip = IsPressSkip || IsPressForceSkip;
                }

                condition = await waitfor_update();
                if (condition == false)
                {
                    if (isskip)
                    {
                        IsPressForceSkip = false;
                        IsPressSkip = false;
                        OnSkip();
                        break;
                    }
                }
                if (condition == true)
                {
                    break;
                }
                await UniTask.Yield(PlayerLoopTiming.Update, this.Token);
            }
        }
        public async UniTask Skipable(Func<bool> waitforcond_update, Action OnSkip, bool onlyForceSkip, bool cannotSkip = false)
        {
            bool isskip = false;
            bool condition = false;
            while (!condition)
            {
                this.Token.ThrowIfCancellationRequested();
                if (!cannotSkip)
                {
                    if (onlyForceSkip)
                        isskip = IsPressForceSkip;
                    else
                        isskip = IsPressSkip || IsPressForceSkip;
                }

                condition = waitforcond_update();
                if (condition == false)
                {
                    if (isskip)
                    {
                        IsPressForceSkip = false;
                        IsPressSkip = false;
                        OnSkip();
                        break;
                    }
                }
                if (condition == true)
                {
                    break;
                }
                await UniTask.Yield(PlayerLoopTiming.Update, this.Token);
            }
        }
        public async UniTask WaitTimes_Skipable(float seconds, bool onlyForceSkip)
        {
            float wait_s = 0;
            await Skipable(() =>
            {
                wait_s += Time.deltaTime;
                return wait_s >= seconds;
            }, () =>
            {
                //Debug.Log($"等待时间跳过 {seconds - wait_s}s");
            }, onlyForceSkip);
        }
        public async UniTask WaitTimes(float times)
        {
            await WaitTimes_Skipable(times, true);
        }
        public async UniTask MustWait(string error_id, Func<bool> cond)
        {
            float seconds = 10f;
            await Skipable(() =>
            {
                seconds -= Time.deltaTime;
                if (seconds <= 0)
                {
                    Debug.LogError($"等待的目标始终无法完成(10s): {error_id}!");
                }
                return cond();
            }, () => { }, true, true);
        }
        public async UniTask Waitable(Func<bool> waitforcond_update/*, Action OnSkip, bool onlyForceSkip, bool cannotSkip = false*/)
        {
            //bool isskip = false;
            bool condition = false;
            while (!condition)
            {
                this.Token.ThrowIfCancellationRequested();
                //if (!cannotSkip)
                //{
                //    if (onlyForceSkip)
                //        isskip = IsPressForceSkip;
                //    else
                //        isskip = IsPressSkip || IsPressForceSkip;
                //}

                condition = waitforcond_update();
                //if (condition == false)
                //{
                //    if (isskip)
                //    {
                //        IsPressForceSkip = false;
                //        IsPressSkip = false;
                //        OnSkip();
                //        break;
                //    }
                //}
                if (condition == true)
                {
                    break;
                }
                await UniTask.Yield(PlayerLoopTiming.Update, this.Token);
            }
        }



        /// <summary>
        /// 根据演出的锚定坐标,获得相对坐标
        /// </summary>
        /// <param name="offsetx"></param>
        /// <param name="offsety"></param>
        /// <returns></returns>
        public Vector2 GetPosByOffset(float offsetx, float offsety)
        {
            return new Vector2(offsetx + this.AnchorPosition.x, offsety + this.AnchorPosition.y);
        }
        public Vector3 GetPosByOffset(float offsetx, float offsety,float z)
        {
            return new Vector3(offsetx + this.AnchorPosition.x, offsety + this.AnchorPosition.y, z);
        }
        /// <summary>
        /// 打字机式吐字
        /// </summary>
        /// <param name="Chara"></param>
        /// <param name="localizationManager"></param>
        /// <param name="id"></param>
        /// <param name="totalTime"></param>
        /// <param name="waittime"></param>
        /// <param name="textDecorater"></param>
        /// <returns></returns>
        public async UniTask WaitDialogueTextPlay(IPerformChara Chara, ILocalizationManager localizationManager, string id, float totalTime, float waittime, TextDecorater textDecorater = null)
        {
            var dialogueBox = Chara.DialogueBox;
            var Text = dialogueBox.Text;
            var Parent = dialogueBox.Parent;

            Text.maxVisibleCharacters = 0;
            Text.font = localizationManager.GetCurrentFont();
            string text;
            if (textDecorater == null)
                text = localizationManager.GetText(id);
            else
                text = textDecorater.GetFinal(localizationManager.GetText(id));
            Text.text = text;

            var tween = Parent.DOScale(1, 0.3f);
            await Skipable(() =>
            {
                if (tween.IsActive() && tween.IsPlaying())
                    return false;
                return true;
            }, () =>
            {
                tween.Complete();
            }, true);

            var count = text.Length;
            float now = 0;
            int frame = 0;
            int currentletter = 0;
            int currentword = 0;
            if (LocalizationManager.I.GetCurrentLanguage() == "en")
                currentword = 1;
            var str = Text.text;

            //GlobalUIViewModel.I.SkipableTips.Value = true;

            await Skipable(() =>
            {
                if (now < totalTime)
                {
                    now += Time.deltaTime;
                    float percent = Mathf.Clamp01(now / totalTime);
                    var displayCount = Mathf.CeilToInt(count * percent);
                    Text.maxVisibleCharacters = displayCount;
                    frame++;
                    if (displayCount > currentletter)
                    {
                        currentletter = displayCount;
                        if (LocalizationManager.I.GetCurrentLanguage() == "en")
                        {
                            var nowstr = str.Substring(0, displayCount);
                            int wordCount = Regex.Matches(nowstr, @"\b[A-Za-z]+\b").Count;
                            if (displayCount == count)
                                wordCount += 1;
                            if (wordCount > currentword)
                            {
                                currentword = wordCount;
                                //Debug.Log(nowstr);
                                //AudioRuntime.I.PlayEffect("event:/keyboard_2", 0.5f);
                            }
                        }
                        else
                        {
                            var nowstr = str.Substring(0, displayCount);
                            int letter_count = Regex.Matches(nowstr, @"(?=\P{P})(?=\S).").Count;
                            if (letter_count > currentword)
                            {
                                currentword = letter_count;
                                //Debug.Log(nowstr);
                                //AudioRuntime.I.PlayEffect("event:/keyboard_2", 0.5f);
                            }
                        }
                    }
                    return false;
                }
                return true;
            }, () =>
            {
                Text.maxVisibleCharacters = count;
            }, false);


            float wait = 0;
            await Skipable(() =>
            {
                if (wait < waittime)
                {
                    wait += Time.deltaTime;
                    return false;
                }
                return true;
            }, () => { }, false);
            //GlobalUIViewModel.I.SkipableTips.Value = false;

            var tween2 = (Parent.DOScale(0, 0.2f));
            await Skipable(() =>
            {
                if (tween2.IsActive() && tween2.IsPlaying())
                    return false;
                return true;
            }, () =>
            {
                tween2.Complete();
            }, true);
        }
        //public async UniTask MakePerform(Func<UniTask> perform, bool autofocusMainChara)
        //{
        //    await UniTask.WaitUntil(() => performtool.IsInPerformPlay == null, PlayerLoopTiming.Update, this.Token);
        //    performtool.IsInPerformPlay = new PerformPlaySetting() { HideUI = true };

        //    var PerformDisableUIRule = new GameRuleToggle(this, "PerfromUIHide",
        //        PerformRuleConst.Hide_EntityResourceUI,
        //        PerformRuleConst.Hide_SolidResourceUI,
        //        PerformRuleConst.Hide_ItemUI,
        //        PerformRuleConst.Hide_InputTipsUI
        //        );
        //    PerformDisableUIRule.RuleOn();

        //    GameCameraFollowMode oldcammode = cameratool.CameraMode;
        //    cameratool.CameraMode = GameCameraFollowMode.Freemode;
        //    InputSimulateType oldmode = InputSimulateType.Raw;
        //    var mainchara = runtime.MainCharacter;
        //    if (mainchara != null)
        //    {
        //        oldmode = mainchara.InputState.Mode;
        //        mainchara.InputState.Mode = InputSimulateType.PerformModeScriptOverdrive;
        //    }
        //    var r = core;
        //    var MakePerform = new GameRuleToggle(this, "MakePerform",
        //        PerformRuleConst.DisableDigActionInput,
        //        PerformRuleConst.CannotBreakAnyBlockButWhiteList,
        //        PerformRuleConst.CantPlaceAnyThing,
        //        PerformRuleConst.CantMoveAndJump,
        //        PerformRuleConst.CantDragAndZoomScreen,
        //        PerformRuleConst.DisableESC);
        //    MakePerform.RuleOn();

        //    GlobalUIViewModel.I.FilmMask.Value = true;
        //    if (autofocusMainChara && core.GetRuntime().MainCharacter != null)
        //        await WaitScreenForcusIn_Skipable(core.GetRuntime().MainCharacter.CoordPos.ToVector());
        //    await perform();
        //    cameratool.TargetSize = CameraConst.NormalSize;
        //    GlobalUIViewModel.I.FilmMask.Value = false;
        //    await WaitTimes_Skipable(0.5f, true);
        //    MakePerform.RuleOff();
        //    if (mainchara != null)
        //    {
        //        mainchara.InputState.Mode = oldmode;
        //    }
        //    cameratool.CameraMode = oldcammode;
        //    performtool.IsInPerformPlay = null;
        //    PerformDisableUIRule.RuleOff();
        //}









        #region
        //public bool IsCameraOverGuideStageCoordPos(int offsetx, int offsety)
        //{
        //    var pos = GetCoordPosByOffset(offsetx, offsety);
        //    return cameratool.IsInRealRect(new Vector2(pos.x, pos.y));
        //}
        //public bool IsCharaInCoordAround(CharaEntityData chara, int offsetx, int offsety, float distance)
        //{
        //    var pos = GetCoordPosByOffset(offsetx, offsety);
        //    var dis = runtime.DistanceGameSize(pos, chara.Pos);
        //    //Debug.Log($"Distance {pos.x},{pos.y} : {dis}");
        //    return dis <= distance;
        //}
        //public bool IsCharaInCoordAround(CharaEntityData chara, CharaEntityData targetChara, float distance)
        //{
        //    var dis = runtime.DistanceGameSize(targetChara.Pos, chara.Pos);
        //    //Debug.Log($"Distance {pos.x},{pos.y} : {dis}");
        //    return dis <= distance;
        //}
        //public async UniTask WaitScreenDirectShift_Skipable(Vector2 coordPos, int size = CameraConst.MinSize, float waitSecond = 1)
        //{
        //    GameCameraFollowMode oldcammode = cameratool.CameraMode;
        //    cameratool.CameraMode = GameCameraFollowMode.Freemode;

        //    var WaitScreenForcusIn = new GameRuleToggle(this, "WaitScreenForcusIn",
        //        PerformRuleConst.DisableDigActionInput,
        //        PerformRuleConst.CannotBreakAnyBlockButWhiteList,
        //        PerformRuleConst.CantPlaceAnyThing,
        //        PerformRuleConst.CantMoveAndJump,
        //        PerformRuleConst.CantDragAndZoomScreen,
        //        PerformRuleConst.DisableESC);
        //    WaitScreenForcusIn.RuleOn();

        //    var camera = cameratool;
        //    camera.TargetPosition = coordPos;
        //    camera.TargetSize = size;
        //    camera.SetCameraPosition(coordPos);
        //    camera.SetCameraSize(size);
        //    await WaitTimes_Skipable(waitSecond, true);

        //    WaitScreenForcusIn.RuleOff();
        //    cameratool.CameraMode = oldcammode;
        //}
        //public async UniTask WaitScreenForcusIn_Skipable(Vector2 coordPos, int size = CameraConst.MinSize, float waitSecond = 1)
        //{
        //    var WaitScreenForcusIn = new GameRuleToggle(this, "WaitScreenForcusIn",
        //        PerformRuleConst.DisableDigActionInput,
        //        PerformRuleConst.CannotBreakAnyBlockButWhiteList,
        //        PerformRuleConst.CantPlaceAnyThing,
        //        PerformRuleConst.CantMoveAndJump,
        //        PerformRuleConst.CantDragAndZoomScreen,
        //        PerformRuleConst.DisableESC);
        //    WaitScreenForcusIn.RuleOn();

        //    var camera = cameratool;
        //    camera.SetSlowSpeed();
        //    camera.TargetPosition = coordPos;
        //    camera.TargetSize = size;
        //    await WaitTimes_Skipable(waitSecond, true);
        //    camera.SetNormalSpeed();

        //    WaitScreenForcusIn.RuleOff();
        //}
        //public async UniTask ControlCharacterMoveTo_Skipable(CharaEntityData chara, int xoffset, int yoffset, Vector2Int anchor)
        //{
        //    var target = core.GetRuntime().ToGameSizePos(xoffset + anchor.x, yoffset + anchor.y);
        //    var old = chara.InputState.Mode;
        //    chara.InputState.Mode = InputSimulateType.PerformModeScriptOverdrive;
        //    float testingx = 0.2f;

        //    await Skipable(() =>
        //    {
        //        var my = core.GetRuntime().NearstMappingPos(chara.Pos, target.ToVector());
        //        var target_y = target.y;
        //        var target_x = target.x;
        //        var my_y = my.y;
        //        var my_x = my.x;

        //        if (target_y > my_y + 0.25f && PerformConstTool.TestingEntityForwardIsCollision(core, chara)
        //        && chara.State.IsInGround && chara.State.IsInGroundTimes >= 0.3f)
        //        {
        //            chara.ScriptInput.DoSimulate(e =>
        //            {
        //                e.JumpInput = true;
        //            });
        //        }
        //        if (my_x != target_x && !PerformConstTool.TestingEntityForwardIsCollision_IsBodyCollisioned(core, chara))
        //        {
        //            chara.ScriptInput.DoSimulate(e =>
        //            {
        //                e.HorizontalInput = target_x > my_x ? 1 : -1;
        //            });
        //        }

        //        if (Mathf.Abs(target_x - my_x) <= testingx)
        //            return true;
        //        return false;
        //    }, () =>
        //    {
        //        //直接设定位置
        //        core.GetLogicBase().GetLogic<PhysicsAndMovementUpdator>().SetEntityPosition(chara.IPos, target);
        //    }, true);

        //    chara.State.HorizontalMovementPause();
        //    chara.InputState.Mode = old;
        //}
        //public async UniTask ControlCharacterMoveTo_Skipable(CharaEntityData chara, CharaEntityData another_chara, Vector2Int anchor)
        //{
        //    var target = core.GetRuntime().ToGameSizePos(another_chara.Pos.x, another_chara.Pos.y);
        //    var old = chara.InputState.Mode;
        //    chara.InputState.Mode = InputSimulateType.PerformModeScriptOverdrive;
        //    float testingx = 1f;

        //    await Skipable(() =>
        //    {
        //        var my = core.GetRuntime().NearstMappingPos(chara.Pos, target.ToVector());
        //        var target_y = target.y;
        //        var target_x = target.x;
        //        var my_y = my.y;
        //        var my_x = my.x;

        //        if (target_y > my_y + 0.25f && PerformConstTool.TestingEntityForwardIsCollision(core, chara)
        //        && chara.State.IsInGround && chara.State.IsInGroundTimes >= 0.3f)
        //        {
        //            chara.ScriptInput.DoSimulate(e =>
        //            {
        //                e.JumpInput = true;
        //            });
        //        }
        //        if (my_x != target_x && !PerformConstTool.TestingEntityForwardIsCollision_IsBodyCollisioned(core, chara))
        //        {
        //            chara.ScriptInput.DoSimulate(e =>
        //            {
        //                e.HorizontalInput = target_x > my_x ? 1 : -1;
        //            });
        //        }

        //        if (Mathf.Abs(target_x - my_x) <= testingx)
        //        {
        //            bool isleft = !(target_x - my_x > 0);
        //            logic.GetLogic<EntityInputListenner>().SetEntityDir(chara.Entity_id, isleft);
        //            chara.State.Flip = isleft;
        //            return true;
        //        }
        //        return false;
        //    }, () =>
        //    {
        //        //直接设定位置
        //        core.GetLogicBase().GetLogic<PhysicsAndMovementUpdator>().SetEntityPosition(chara.IPos, target);
        //    }, true);

        //    chara.State.HorizontalMovementPause();
        //    chara.InputState.Mode = old;
        //}
        //public async UniTask WaitFadeBlackMask_Skipable(string title_loc_id, Func<UniTask> wait_whenfade = null, float watitimes = 2)
        //{
        //    var WaitShowMaskTips_KeySkip = new GameRuleToggle(this, "WaitBlackFadeMask_Skipable",
        //        PerformRuleConst.DisableESC);
        //    WaitShowMaskTips_KeySkip.RuleOn();

        //    GlobalUIViewModel.I.FadeText.Value = title_loc_id;
        //    GlobalUIViewModel.I.DoFade.Value = true;
        //    await Skipable(() =>
        //    {
        //        return GlobalUIViewModel.I.IsInFadeing.Value == true;
        //    }, () =>
        //    {

        //    }, true);
        //    if (wait_whenfade != null)
        //    {
        //        await wait_whenfade();
        //    }
        //    else
        //    {
        //        await WaitTimes_Skipable(watitimes, true);
        //    }

        //    GlobalUIViewModel.I.DoFade.Value = false;
        //    GlobalUIViewModel.I.FadeText.Value = null;
        //    await WaitTimes_Skipable(0.7f, true);

        //    WaitShowMaskTips_KeySkip.RuleOff();
        //}
        //public void WaitFadeBlackMask_Begin(string title_loc_id)
        //{
        //    if (WaitFadeBlackMask == null)
        //    {
        //        this.WaitFadeBlackMask = new GameRuleToggle(this, "WaitFadeBlackMask",
        //        PerformRuleConst.DisableESC);
        //    }

        //    WaitFadeBlackMask.RuleOn();
        //    GlobalUIViewModel.I.FadeText.Value = title_loc_id;
        //    GlobalUIViewModel.I.DoFade.Value = true;
        //}
        //public void WaitFadeBlackMask_End()
        //{
        //    GlobalUIViewModel.I.DoFade.Value = false;
        //    GlobalUIViewModel.I.FadeText.Value = null;

        //    WaitFadeBlackMask?.RuleOff();
        //}
        //public void DoMask_Begin(MaskParam param)
        //{
        //    DoMask_Begin(param.Layout, param.id, param.FocusItem);
        //}
        //public void DoMask_Begin(MaskTextLayout layout, string tips, RectTransform highlight = null)
        //{
        //    GlobalUIViewModel.I.DoMask_Seconds.Value = -1;
        //    GlobalUIViewModel.I.DoMask.Value = new MaskParam() { Layout = layout, id = tips, FocusItem = highlight };
        //}
        //public void DoMask_End()
        //{
        //    GlobalUIViewModel.I.DoMask_Seconds.Value = 0;
        //    GlobalUIViewModel.I.DoMask.Value = null;
        //}
        //public async UniTask WaitShowMaskTips_Skipable(MaskTextLayout layout, string tips, float min_seconds = 3f, float maxSeconds = 6f, RectTransform highlight = null)
        //{
        //    var WaitShowMaskTips_KeySkip = new GameRuleToggle(this, "WaitShowMaskTips_KeySkip",
        //        PerformRuleConst.DisableESC);
        //    WaitShowMaskTips_KeySkip.RuleOn();


        //    //在最小时间里,禁用玩家所有控制移动
        //    GlobalUIViewModel.I.DoMask.Value = new MaskParam() { Layout = layout, id = tips, FocusItem = highlight };
        //    float time = min_seconds;
        //    await Skipable(() =>
        //    {
        //        time -= PhysicsConst.DeltaTime;
        //        if (time < 0) time = 0;
        //        GlobalUIViewModel.I.DoMask_Seconds.Value = time;
        //        return time <= 0;
        //    }, () => { }, true);
        //    if (maxSeconds > min_seconds)
        //    {
        //        //提示按任意键跳过
        //        GlobalUIViewModel.I.SkipableTips.Value = true;
        //        await WaitTimes_Skipable(maxSeconds - min_seconds, false);
        //        //结束提示
        //        GlobalUIViewModel.I.SkipableTips.Value = false;
        //    }
        //    GlobalUIViewModel.I.DoMask.Value = null;


        //    WaitShowMaskTips_KeySkip.RuleOff();
        //}
        //public async UniTask AwaitCharaInCoordAround(CharaEntityData chara, int offsetx, int offsety, float distance)
        //{
        //    await Skipable(() =>
        //    {
        //        return IsCharaInCoordAround(chara, offsetx, offsety, distance);
        //    }, () =>
        //    {
        //        var charapos = GetCoordPosByOffset(offsetx, offsety);
        //        logic.GetLogic<PhysicsAndMovementUpdator>().SetEntityPosition(chara.IPos, charapos);
        //        chara.State.HorizontalMovementPause();
        //    }, true);
        //}
        //public async UniTask Dialogue(CharaEntityData Chara, string id, float totalTime, float waittime, TextDecorater textDecorater = null)
        //{
        //    await WaitDialogueTextPlay(Chara, id, totalTime, waittime, textDecorater);
        //}
        //public async UniTask ControlCharacterMoveTo_Skipable(CharaEntityData chara, int xoffset, int yoffset)
        //{
        //    await ControlCharacterMoveTo_Skipable(chara, xoffset, yoffset, this.AnchorPosition);
        //}
        //public async UniTask ControlCharacterMoveTo_Skipable(CharaEntityData chara, CharaEntityData another_chara)
        //{
        //    await ControlCharacterMoveTo_Skipable(chara, another_chara, this.AnchorPosition);
        //}
        //public async UniTask WaitDoAttackAnim(CharaEntityData chara)
        //{
        //    var old = chara.InputState.Mode;
        //    chara.InputState.Mode = InputSimulateType.PerformModeScriptOverdrive;
        //    chara.ScriptInput.DoSimulate(e =>
        //    {
        //        e.IsInputWorldPosition = true;
        //        e.WorldPosition = chara.Pos.ToVector() + new Vector2(chara.State.Flip ? -1 : 1, 0);
        //        e.DigOnce = true;

        //    });
        //    await UniTask.WaitUntil(() => chara.State.IsDig, PlayerLoopTiming.Update, this.Token);
        //    chara.InputState.Mode = old;
        //}
        //public async UniTask WaitDoGnawAnim(CharaEntityData chara, float times_seconds)
        //{
        //    var old = chara.InputState.Mode;
        //    chara.InputState.Mode = InputSimulateType.PerformModeScriptOverdrive;

        //    float times = 0;
        //    await Skipable(() =>
        //    {
        //        chara.ScriptInput.DoSimulate(e =>
        //        {
        //            e.GnawInput = true;
        //        });

        //        times += PhysicsConst.DeltaTime;
        //        return times > times_seconds;
        //    }, () => { }, true);

        //    //await UniTask.WaitUntil(() => chara.State.IsDig, PlayerLoopTiming.Update, this.Token);
        //    chara.InputState.Mode = old;
        //}
        //public async UniTask WaitCameraOverGuideStageCoordPos(int offsetx, int offsety)
        //{
        //    await Skipable(() =>
        //    {
        //        return IsCameraOverGuideStageCoordPos(offsetx, offsety);
        //    }, () =>
        //    {
        //        var pos = GetCoordPosByOffset(offsetx, offsety);
        //        cameratool.TargetPosition = pos.ToVector();
        //    }, true);
        //}
        //public async UniTask WaitScreenForcusIn_Skipable((int x, int y) coordPos, int size = CameraConst.MinSize, float waitSecond = 1)
        //{
        //    await WaitScreenForcusIn_Skipable(coordPos.ToVector(), size, waitSecond);
        //}

        //public enum TextBoxOption
        //{
        //    Add,
        //    Remove
        //}
        //public void RemoveGuideTextBox(string param)
        //{
        //    logic.GetLogic<GuideFloatTextBoxUpdator>().RemoveGuideText(param);
        //}
        //public void GuideTextBox(GuideTextParam param, TextBoxOption op)
        //{
        //    param.anchor = this.AnchorPosition;
        //    if (op == TextBoxOption.Add)
        //    {
        //        logic.GetLogic<GuideFloatTextBoxUpdator>().AddGuideText(param);
        //    }
        //    else
        //    {
        //        logic.GetLogic<GuideFloatTextBoxUpdator>().RemoveGuideText(param);
        //    }
        //}
        #endregion
    }
}
