using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;

namespace Nodes.Infrastructure.Data.Extensions.Phases;

internal static class InitialData
{
    public static IEnumerable<Phase> Phases =>
        new List<Phase>
        {
            Phase.Create(
                PhaseId.Of(Guid.Parse("e7153019-b191-49a1-b7c4-b40576064afa")),
                "Investigation Phase",
                "Gathering evidence, interviewing witnesses, and reviewing case files for trial preparation.",
                DateTime.UtcNow.AddDays(-7),
                DateTime.UtcNow.AddDays(14),
                TimeSpan.FromDays(21),
                "InProgress",
                0.4m,
                false,
                PhaseId.Of(Guid.NewGuid()),
                [
                    PhaseId.Of(Guid.NewGuid()),
                    PhaseId.Of(Guid.NewGuid())
                ],
                "Jane Doe",
                ["Lead Investigator", "Forensic Analyst", "Legal Advisor"],
                ["investigation", "evidence", "pre-trial"]
            ),

            Phase.Create(
                PhaseId.Of(Guid.Parse("9dc36dce-566b-4471-8785-29d5679857ef")),
                "Witness Cross-Examination Preparation",
                "Detailed preparation and strategy review for the upcoming cross-examination of the witness. This phase includes reviewing testimony, identifying potential inconsistencies, and refining questioning tactics.", // Description
                new DateTime(2025, 1, 10),
                new DateTime(2025, 1, 16),
                TimeSpan.FromDays(6),
                "Scheduled",
                0.0m,
                false,
                PhaseId.Of(Guid.NewGuid()),
                [
                    PhaseId.Of(Guid.NewGuid()),
                    PhaseId.Of(Guid.NewGuid())
                ],
                "Attorney Smith",
                ["Defense Team", "Investigation Unit", "Legal Advisor"],
                ["witness", "cross-examination", "court", "legal"]
            )
        };
}
