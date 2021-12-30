public interface IMetadataIO {
    void IOWriteMetadata(string meta, object data);
    object IOReadMetadata(string meta);
}