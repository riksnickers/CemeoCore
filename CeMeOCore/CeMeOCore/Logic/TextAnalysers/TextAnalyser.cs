using CeMeOCore.Logic.TextAnalysers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CeMeOCore.Logic.TextAnalysers
{
    public class TextAnalyser
    {
        /*
         * Sample sentences:
         *      I want to plan a meeting / Plan me a meeting        (a meeting can also be a reservation)
         *      I want to make a meeting / Make me a meeting
         *      I want to organise a meeting / Organise me a meeting
         *      Prepare me a meeting
         * 
         * 
         */

        //An intention is something that someone want to do.
        private static HashSet<string> CommandIntentions = new HashSet<string>() { "i want to", "show me"  };
        private static HashSet<string> CommandPlanAction = new HashSet<string>() { "plan", "make", "organise", "prepare", "arrange" };
        private static HashSet<string> CommandModifyAction = new HashSet<string>() { "modify", "change", "alter", "edit" };
        private static HashSet<string> CommandCancelAction = new HashSet<string>() { "cancel", "stop", "delete", "drop" };
        private static HashSet<string> CommandOnWhat = new HashSet<string>() { "a meeting", "meeting", "reservation", "a reservation", "session", "a session", "gathering", "a gathering", "conference", "a conference" };
        private static HashSet<string> CommandMeetingBeforeDate = new HashSet<string>() { "today", "within this work week", "within seven days", "within 7 days", "within this month", "within thirty days", "within 30 days", "before" };

        private string _sentence;
        
        private AnalyserAction _sentenceAction;
        public AnalyserAction SentenceAction
        {
            get
            {
                return this._sentenceAction;
            }
            private set 
            {
                this._sentenceAction = value;
            }
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        public TextAnalyser(string sentence)
        {
            this._sentence = sentence.ToLower();

            //Get the intention

            //Get the action of the sentence
            SentenceAction = AnalyseSentenceAction();
        }

        public AnalyserAction AnalyseSentenceAction()
        {

            foreach (string action in CommandPlanAction)
            {
                if (this._sentence.Contains(action))
                {
                    return AnalyserAction.Create;
                }
            }

            foreach (string action in CommandModifyAction)
            {
                if (this._sentence.Contains(action))
                {
                    return AnalyserAction.Edit;
                }
            }

            foreach (string action in CommandCancelAction)
            {
                if (this._sentence.Contains(action))
                {
                    return AnalyserAction.Delete;
                }
            }

            return AnalyserAction.Error;
        }
    }
}