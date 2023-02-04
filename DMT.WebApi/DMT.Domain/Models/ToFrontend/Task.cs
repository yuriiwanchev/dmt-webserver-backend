namespace DMT.Domain.Models.ToFrontend;

public record Task(ApiTask Api, ParameterToFront[] Parameters);