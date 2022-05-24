using System.Text;

namespace DMT.TaskProcessing;

public static class CsvFileWorker
{
    // public byte[] CreateCsv()
    // {
    //     //before your loop
    //     var csv = new StringBuilder();
    //     
    //     //Suggestion made by KyleMit
    //     var newLine = string.Format("{0},{1}", first, second);
    //     csv.AppendLine(newLine);  
    //     
    //     // File.WriteAllText(filePath, csv.ToString());
    //     return Encoding.UTF8.GetBytes(csv.ToString());
    // }

    public static byte[] CreateCsvTest()
    {
        //before your loop
        var csv = new StringBuilder();

        var list = new List<string>();
        
        // need create
        // loop for parameters
        //
        
        list.Add("par1");
        list.Add("par2");
        list.Add("par3");
        csv.AppendLine(string.Join(",", list));

        for (int i = 0; i < 10; i++)
        {
            var list1 = new List<string>();
            list1.Add(i.ToString());
            list1.Add((i+1).ToString());
            list1.Add((i+2).ToString());
            var newLine = string.Join(",", list1);
            csv.AppendLine(newLine);
        }
        
        // File.WriteAllText(filePath, csv.ToString());
        return Encoding.UTF8.GetBytes(csv.ToString());
    }
}