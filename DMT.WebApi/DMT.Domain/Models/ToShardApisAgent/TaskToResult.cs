namespace DMT.Domain.Models.ToShardApisAgent;

public record TaskToResult(string Api, ParameterToResult[] Parameters);