using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Nodes.Phase.ValueObjects;

namespace Nodes.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static IEnumerable<Node> Nodes =>
        new List<Node>
        {
            Node.Create(
                NodeId.Of(Guid.Parse("2df76835-c92b-45d0-9232-61901c4abe97")),
                "Start Court Proceedings",
                "Beginning of the court proceedings in John Doe vs New York City.",
                Phase.Create(
                    PhaseId.Of(Guid.NewGuid()),
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
                DateTime.UtcNow,
                0,
                ["court", "complaint"],
                ["investigation", "interview"]
            ),

            Node.Create(
                NodeId.Of(Guid.Parse("6968d886-9e39-4fc0-9f2c-a5fbc1548970")),
                "Witness Cross-Examination",
                "Cross-examination of the witness is due on 16 January 2025.",
                Phase.Create(
                    PhaseId.Of(Guid.NewGuid()), 
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
                ),
                DateTime.UtcNow,
                1,
                ["court", "hearing"],
                ["witness", "vital"]
            )
        };
}
