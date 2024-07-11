﻿namespace IWantApp.Extensions;

public static class ProblemDetailsExtension
{
    public static Dictionary<string, string[]> ConvertToProblemsDetail(this IReadOnlyCollection<Notification> notifications)
    {
        return notifications
             .GroupBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
    }

    public static Dictionary<string, string[]> ConvertToProblemsDetail(this IEnumerable<IdentityError> error)
    {
        return error
             .GroupBy(g => g.Code)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Description).ToArray());
    }
}
