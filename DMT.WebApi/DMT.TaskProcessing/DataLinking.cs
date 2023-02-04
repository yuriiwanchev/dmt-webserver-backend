using System.Globalization;
using System.Text;
using CsvHelper;
using DMT.Domain.Models.ToFrontend;
using DMT.Domain.Models.ToTaskProcessing;
using DMT.TaskProcessing.Models;

namespace DMT.TaskProcessing;

public static class DataLinking
{
    public static byte[] GetLinkingData(UserTask<TaskData> data)
    {
        var apisWithParametersWithValues = data.Insides;
        
        // var typesToConnect = new string[]{"time"};
        var typeToConnect = "date";
        var apiConnectParameter = new Dictionary<string, string>();
        var parameterPair = new Dictionary<string, Pair[]>();
        int numberOfColumns = 0;

        var baseParameter = new ParameterValue[]{};

        for (int i = 0; i < apisWithParametersWithValues.Length; i++)
        {
            foreach (var parameter in apisWithParametersWithValues[i].Data)
            {
                // if(typesToConnect.Any(element => element == parameterWithValues.Type))
                if (parameter.Parameter_name == typeToConnect && !apiConnectParameter.ContainsKey(apisWithParametersWithValues[i].Api))
                {
                    apiConnectParameter[apisWithParametersWithValues[i].Api] = parameter.Parameter_name;
                    if (apiConnectParameter.Count != 1)
                    {
                        var otherParameter = parameter.Values;
                        
                        var valuesPair = from values1 in baseParameter
                            join values2 in otherParameter
                                on values1.Value equals values2.Value
                            select new
                            {
                                Value = values1.Value,
                                BaseId = values1.Id,
                                OtherId = values2.Id
                            };

                        var listPairs = new List<Pair>(); 

                        foreach (var el in valuesPair)
                        {
                            listPairs.Add(new Pair(el.Value, el.BaseId, el.OtherId));
                        }

                        parameterPair[apisWithParametersWithValues[i].Api] = listPairs.ToArray();
                    }
                    else
                    {
                        baseParameter = parameter.Values;
                        numberOfColumns = parameter.Values.Length;
                    }
                }
                    
                // if (numberOfColumns == 0) 
                //     numberOfColumns = parameter.Values.Length;
            }
        }
        // Теперь есть словарь с ключом ввиде апи и значением ввиде параметра с датой (по которому объеденять)
        
        // Теперь надо создать словарь со связями между массивом зночений первого апи и каждого другого
        /*
         *  "api": {
         *      value: "data",
         *      baseId: у одного и того же parameter
         *      otherId: разное
         * },
         * 
         * 
         */
        
        
        
        // Далее надо проходясь по значениям заходит в нужное api
        // заходить значения и по дате смотреть какое значение по id ставить
        
        // 
        
        var csv = new StringBuilder();
        string connectionValue = null;

        for (int i = 0; i < numberOfColumns; i++)
        {
            var lineValues = new List<string>();
            var isFirstApi = true;
            foreach (var api in apisWithParametersWithValues)
            {
                var f = parameterPair[api.Api];
                int id; 
                if (isFirstApi)
                {
                    connectionValue = f[i].Value;
                    id = f[i].BaseId;
                    lineValues.Add(connectionValue);
                    
                    isFirstApi = false;
                }
                else
                    id = f[i].OtherId;

                
                foreach (var parameterData in api.Data)
                {
                    if (parameterData.Parameter_name != apiConnectParameter[api.Api])
                    {
                        foreach (var value in parameterData.Values)
                        {
                            if (value.Id == id)
                            {
                                lineValues.Add(value.Value);
                                break;
                            }
                        }
                    }
                }
                
                
                // var list1 = new List<string>();
                // list1.Add(i.ToString());
                // list1.Add((i+1).ToString());
                // list1.Add((i+2).ToString());
                // var newLine = string.Join(",", list1);
                // csv.AppendLine(newLine);
                //
                // // File.WriteAllText(filePath, csv.ToString());
                // return Encoding.UTF8.GetBytes(csv.ToString());
                //
                //  csv.AppendLine(newLine);  
                //  
                //  // File.WriteAllText(filePath, csv.ToString());
                //  return Encoding.UTF8.GetBytes(csv.ToString());
            }
            var newLine = string.Join(",", lineValues);
            csv.AppendLine(newLine);
        }
        
        return Encoding.UTF8.GetBytes(csv.ToString());
    }
    
    
}