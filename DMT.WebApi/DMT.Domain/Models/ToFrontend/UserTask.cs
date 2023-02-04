namespace DMT.Domain.Models.ToFrontend;

public record UserTask<TInside>(string Task_id, string User_id, TInside[] Insides);