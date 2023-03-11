using MilsatIMS.Enums;

namespace MilsatIMS.Common
{
    public class Utils
    {
        public static string GetUserPicture(string storepath, string fileName)
        {
            var filePath = Path.Combine(storepath, fileName);
            if (!File.Exists(filePath))
            {
                return String.Empty;
            }
            byte[] contents = File.ReadAllBytes(filePath);
            string image = Convert.ToBase64String(contents);
            return image;
        }

        // Function to convert the string to a list of TeamType
        public List<TeamType> GetOtherTeamList(string teamList)
        {
            if (string.IsNullOrEmpty(teamList))
            {
                return new List<TeamType>();
            }

            return teamList.Split(",").Select(x => (TeamType)Enum.Parse(typeof(TeamType), x)).ToList();
        }

        // Function to convert the list of TeamType to a string
        public string SetOtherTeamList(List<TeamType> teamList)
        {
            return string.Join(",", teamList.Select(x => x.ToString()));
        }
    }
}
