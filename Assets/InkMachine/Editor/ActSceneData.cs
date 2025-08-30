using System.Collections.Generic;
namespace InkMachine{
    public class ActSceneData{
        public string Act_Scene; // A5_S2
        public string Act; // Act 5
        public string ActScene; // A5S2

        public ActSceneData(List<string> days){
            Act_Scene = getAct_Scene(days);
            ActScene = Act_Scene.Replace("_", "");
            Act = GetAct(ActScene);
        }

        // Get Act Scene and validity of days in files -> A5_S2
        private static string getAct_Scene(List<string> days){
            if (days.Count == 0)
                return "Error";
            // get the first day
            string firstDay = days[0];
            string actScene = firstDay.Substring(0, 5);
            // check if the day is shared with the rest of the files
            foreach(string day in days){
                if(day.Contains(actScene) == false)
                    return "Error";
            }

            return actScene;
        }

        // Reads A5S2 and Returns Act 5, etc
        private static string GetAct(string actScene){
            return $"Act {actScene.ToCharArray()[1]}";
        }
    }
}