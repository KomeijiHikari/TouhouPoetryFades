using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RenaissanceRestart
{
    public class PerformRuleConst
    {
        private static IPerformCore Core;
        public static void Init(IPerformCore r)
        {
            Core = r;
            InitRule();
        }
        private static void InitRule()
        {
            DisableESC.Init();
            DisableMomoyosPickaxeDisplay.Init();
            CannotBreakAnyBlockButWhiteList.Init();
            DisableDigActionInput.Init();
            CantMoveAndJump.Init();
            CantPlaceAnyThing.Init();
            CantDragAndZoomScreen.Init();
            CantExitShop.Init();
            CantOpenShop.Init();
            CantSellInShop.Init();
            CantBuyInShop.Init();
            CantBuyAbilityLevelUp.Init();
            DisableShopItemSelectChange.Init();
            DisableShopItemFilterSelectChange.Init();
            DisablePlaceableItemSelectChange.Init();
            CantGnaw.Init();
            DisableFocusPlayerMode.Init();
            CantOpenProp.Init();
            DisableSaveGame.Init();
            Volume_OldYearsMask.Init();
            DisableBagOpen.Init();

            Hide_EntityResourceUI.Init();
            Hide_InputTipsUI.Init();
            Hide_ItemUI.Init();
            Hide_SolidResourceUI.Init();
        }


        public static GameRulePair Volume_OldYearsMask = new GameRulePair(on =>
        {
            //if (on)
            //    Core.GetLogicBase().GetLogic<VolumeUpdator>().AddInstance(VolumeConst.OldYearsMask);
            //else
            //    Core.GetLogicBase().GetLogic<VolumeUpdator>().RemoveInstance(VolumeConst.OldYearsMask);
        });



        /// <summary>
        /// 关闭白白是的啃食功能
        /// </summary>
        public static GameRulePair CantGnaw = new GameRulePair(on =>
        {
            //Core.GetRuntime().PlayerRuntimeSetting.DisableGnaw = on;
        });
        /// <summary>
        /// 关闭聚焦到玩家的功能
        /// </summary>
        public static GameRulePair DisableFocusPlayerMode = new GameRulePair(on =>
        {
            //Core.GetLogicBase().GetLogic<GameCameraUpdator>().DisableFocusPlayerMode = on;
        });

        /// <summary>
        /// 关闭ESC
        /// </summary>
        public static GameRulePair DisableESC = new GameRulePair(on =>
        {
            //Core.GetLogicBase().GetLogic<PauseListenner>().DisableESC = on;
        });
        /// <summary>
        /// 关闭保存
        /// </summary>
        public static GameRulePair DisableSaveGame = new GameRulePair(on =>
        {
            //GlobalUIViewModel.I.CantSave.Value = on;
        });
        /// <summary>
        /// 关闭白白是的稿子显示
        /// </summary>
        public static GameRulePair DisableMomoyosPickaxeDisplay = new GameRulePair(on =>
        {
            //Core.GetRuntime().PlayerRuntimeSetting.DisablePickaxeDisplayAndDig = on;
        });
        /// <summary>
        /// 不能挖任何东西, 但是除了白名单
        /// </summary>
        public static GameRulePair CannotBreakAnyBlockButWhiteList = new GameRulePair(on =>
        {
            //Core.GetLogicBase().GetLogic<BlockBreakProtectionListenner>().CannotBreakAnyBlockButWhiteList = on;
        });

        /// <summary>
        /// 不能用任何方式挖任何东西, 而且白名单也不能挖掘
        /// 彻底禁用挖掘事件
        /// </summary>
        public static GameRulePair DisableDigActionInput = new GameRulePair(on =>
        {
            //Core.GetRuntime().PlayerRuntimeSetting.DisableDigActionInput = on;
        });
        /// <summary>
        /// 不能移动和跳跃
        /// </summary>
        public static GameRulePair CantMoveAndJump = new GameRulePair(on =>
        {
            //Core.GetRuntime().PlayerRuntimeSetting.DisableMoveAction = on;
            //Core.GetRuntime().PlayerRuntimeSetting.DisableJumpAction = on;
        });
        /// <summary>
        /// 不能放置任何东西
        /// </summary>
        public static GameRulePair CantPlaceAnyThing = new GameRulePair(on =>
        {
            //Core.GetLogicBase().GetLogic<MainCharaPlaceActionListenner>().CannotPlaceAnyBlockButWhiteList = on;
        });
        /// <summary>
        /// 开启放置东西白名单
        /// </summary>
        //public static List<(int x, int y)> PlaceBlockWhiteList => Core.GetLogicBase().GetLogic<MainCharaPlaceActionListenner>().PlaceActionPosWhiteList;
        /// <summary>
        /// 不能拖动和缩放屏幕
        /// </summary>
        public static GameRulePair CantDragAndZoomScreen = new GameRulePair(on =>
        {
            //Core.GetLogicBase().GetLogic<GameCameraUpdator>().DisableDragAction = on;
            //Core.GetLogicBase().GetLogic<GameCameraUpdator>().DisableZoomAction = on;
        });
        /// <summary>
        /// 不能关闭商店
        /// </summary>
        public static GameRulePair CantOpenProp = new GameRulePair(on =>
        {
            //Core.GetLogicBase().GetLogic<PropListenner>().DisablePropOpen = on;
        });
        /// <summary>
        /// 不能关闭商店
        /// </summary>
        public static GameRulePair CantExitShop = new GameRulePair(on =>
        {

        });
        /// <summary>
        /// 不能打开商店
        /// </summary>
        public static GameRulePair CantOpenShop = new GameRulePair(on =>
        {

        });
        /// <summary>
        /// 不能出售商品
        /// </summary>
        public static GameRulePair CantSellInShop = new GameRulePair(on =>
        {

        });
        /// <summary>
        /// 不买商品
        /// </summary>
        public static GameRulePair CantBuyInShop = new GameRulePair(on =>
        {

        });
        /// <summary>
        /// 不能买能力升级
        /// </summary>
        public static GameRulePair CantBuyAbilityLevelUp = new GameRulePair(on =>
        {

        });
        /// <summary>
        /// 不能更换选择商品和背包
        /// </summary>
        public static GameRulePair DisableShopItemSelectChange = new GameRulePair(on =>
        {

        });
        /// <summary>
        /// 不能更换选择商品和背包
        /// </summary>
        public static GameRulePair DisableShopItemFilterSelectChange = new GameRulePair(on =>
        {

        });
        /// <summary>
        /// 不能更换选择物品
        /// </summary>
        public static GameRulePair DisablePlaceableItemSelectChange = new GameRulePair(on =>
        {

        });
        /// <summary>
        /// 不能更换选择物品
        /// </summary>
        public static GameRulePair DisableBagOpen = new GameRulePair(on =>
        {

        });


        public static GameRulePair Hide_EntityResourceUI = new GameRulePair(on =>
        {

        });
        public static GameRulePair Hide_InputTipsUI = new GameRulePair(on =>
        {

        });
        public static GameRulePair Hide_ItemUI = new GameRulePair(on =>
        {

        });
        public static GameRulePair Hide_SolidResourceUI = new GameRulePair(on =>
        {

        });
    }
}
