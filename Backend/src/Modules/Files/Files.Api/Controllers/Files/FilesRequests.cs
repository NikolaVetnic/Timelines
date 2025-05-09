using BuildingBlocks.Domain.Enums;
using System.Collections.Generic;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace Files.Api.Controllers.Files;

public record CreateFileAssetRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Size { get; set; }
    public EFileType Type { get; set; }
    public byte[] Content { get; set; }
    public List<string> SharedWith { get; set; }
    public bool IsPublic { get; set; }
    public NodeId NodeId { get; set; }
}
