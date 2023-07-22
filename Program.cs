using ConsoleApp1;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using JsonSerializer = System.Text.Json.JsonSerializer;



var json = System.IO.File.ReadAllText(@"C:\Users\gr8tk\Desktop\praca\ConsoleApp1\answers.json"); // change your location folder
var JsonResponse = JsonConvert.DeserializeObject<List<Data>>(json);
Dictionary<int, Dictionary<int, Dictionary<int, int>>> score = new Dictionary<int, Dictionary<int, Dictionary<int, int>>>();
int finalscore = 0;
int groupscore = 0;
string currentmonth = "";
foreach (var data in JsonResponse)
{
    int employeeId = data.EmployeeId;
    int groupId = data.GroupID;
    int month = data.AnsweredOn.Month;
    int totalScore = data.answer1 + data.answer2 + data.answer3 + data.answer4 + data.answer5;

    if (!score.ContainsKey(groupId))
    {
        score[groupId] = new Dictionary<int, Dictionary<int, int>>();
    }

    if (!score[groupId].ContainsKey(month))
    {
        score[groupId][month] = new Dictionary<int, int>();
    }

    score[groupId][month][employeeId] = totalScore;
}

// Print the scores for each EmployeeId for all months
foreach (var groupEntry in score)
{
    int groupId = groupEntry.Key;
    var monthScores = groupEntry.Value;

    Console.WriteLine($"GroupId: {groupId}");
    foreach (var monthEntry in monthScores)
    {
        int month = monthEntry.Key;
        var employeeScores = monthEntry.Value;
        if (month == 1)
        {
            currentmonth = "January";
        }
        if (month == 2)
        {
            currentmonth = "February";
        }
        if (month == 3)
        {
            currentmonth = "March";
        }

        Console.WriteLine($"  Month: {currentmonth}");
        foreach (var employeeEntry in employeeScores)
        {
            int employeeId = employeeEntry.Key;
            int totalScore = employeeEntry.Value;
            finalscore = finalscore + totalScore;
            //Console.WriteLine($"    EmployeeId: {employeeId}, Total Score: {totalScore}");
        }

        double scorerange = Math.Round((double)finalscore / employeeScores.Count, 2);
        //string scorerange = (finalscore / (double)employeeScores.Count).ToString("F2");
        //Console.WriteLine($"     Final month score is: {scorerange}");
        if (scorerange <= 10)
        {
            groupscore = 0;
        }
        if (scorerange > 10 && scorerange <= 10.8)
        {
            groupscore = 1;
        }
        if (scorerange > 10.8 && scorerange <= 11.6)
        {
            groupscore = 2;
        }
        if (scorerange > 11.6 && scorerange <= 12.2)
        {
            groupscore = 3;
        }
        if (scorerange > 12.2 && scorerange <= 12.8)
        {
            groupscore = 4;
        }
        if (scorerange > 12.8)
        {
            groupscore = 5;
        }
        Console.WriteLine($"     Number of employees is: {employeeScores.Count}");
        Console.WriteLine($"     Earned month score is: {finalscore}");
        //Console.WriteLine($"     Final month score is: {scorerange}");
        Console.WriteLine($"     Earned group score is: {groupscore}");
        finalscore = 0;
    }
}

