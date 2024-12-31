namespace Files.Application.Entities.Files.Exceptions;

public class FileAssetNotFoundException(string id) : NotFoundException("FileAsset", id);
