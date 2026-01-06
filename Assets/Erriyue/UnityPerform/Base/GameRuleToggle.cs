using System;
using System.Collections.Generic;

namespace RenaissanceRestart
{


    public class GameRuleToggle
    {
        private GameRulePair[] Rule;
        public string Name { get; }
        public IPerformBase Perform { get; }
        public GameRuleToggle(IPerformBase perform, string name, params GameRulePair[] rule)
        {
            this.Perform = perform;
            this.Name = perform.PerformName.ToString() + "_" + perform.GetHashCode() + ":" + name;
            this.Rule = rule;
        }
        public void RuleOn()
        {
            foreach (var item in Rule)
            {
                item.AddIfNotHasRule(this.Name);
            }
        }
        public void RuleOff()
        {
            foreach (var item in Rule)
            {
                item.RemoveIfHasRule(this.Name);
            }
        }
    }
    public class GameRulePair
    {
        public List<string> RuleNames = new List<string>();
        public Action<bool> Effect;
        public GameRulePair(Action<bool> effect)
        {
            Effect = effect;
        }
        public void Init()
        {
            RuleNames.Clear();
            Effect(false);
        }
        public void AddIfNotHasRule(string rule)
        {
            RuleNames.Add_ifnotExist(rule);
            Effect(RuleNames.Count > 0);
        }
        public void RemoveIfHasRule(string rule)
        {
            RuleNames.Remove_ifExist(rule);
            Effect(RuleNames.Count > 0);
        }
    }
}
